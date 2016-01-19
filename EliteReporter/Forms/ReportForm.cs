﻿using EliteReporter.Forms;
using EliteReporter.Models;
using EliteReporter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DuoVia.FuzzyStrings;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace EliteReporter
{
    public partial class ReportForm : Form
    {
        private ScreenAnalyzer analyzer;
        private EDAPI edapi;
        private TimeSpan takenMisisonCoolDown = new TimeSpan(0, 0, 40);
        private string commanderName;
        private FileSystemWatcher watcher = null;
        delegate void AnalyzeScreenShotCallback(string text);

        public ReportForm()
        {
            InitializeComponent();
            edapi = new EDAPI();

            if(string.IsNullOrEmpty(Properties.Settings.Default.PicturesFolder) || !Directory.Exists(Properties.Settings.Default.PicturesFolder))
            {
                Properties.Settings.Default.PicturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\Frontier Developments\Elite Dangerous";
                Properties.Settings.Default.Save();
            }
        }

        private void RegisterMission(object source, FileSystemEventArgs e)
        {
            Trace.TraceInformation("File: " + e.FullPath + " " + e.ChangeType);
            analyzeScreenShot(e.FullPath);
        }

        private void analyzeScreenShot(string pathToBmp)
        {
            if (analyzer == null)
                return;

            if (missionListView.InvokeRequired)
            {
                AnalyzeScreenShotCallback d = new AnalyzeScreenShotCallback(analyzeScreenShot);
                Invoke(d, new object[] { pathToBmp });
            }
            else
            {

                var result = analyzer.findAndAnalyzeMissionSummaryPage(pathToBmp);
                if (result == null)
                    return;

                var existingLvItem = missionListView.Items.Cast<ListViewItem>().
                    Where(i => ((MissionInfo)i.Tag).MissionName.FuzzyEquals(result.MissionName)).FirstOrDefault();
                if (existingLvItem != null)
                {
                    //finshing mission
                    if (((MissionInfo)existingLvItem.Tag).MissionTakenDateTime.Add(takenMisisonCoolDown) < DateTime.UtcNow)
                    {
                        var edProfile = edapi.getProfile();
                        var missionInfo = (MissionInfo)existingLvItem.Tag;
                        if (missionInfo.MissionFinishedEDProfile == null)
                        {
                            missionInfo.MissionFinishedDateTime = DateTime.UtcNow;
                            missionInfo.MissionFinishedEDProfile = edProfile;
                            existingLvItem.SubItems.Add(missionInfo.MissionFinishedDateTime.ToString("dd/MM/yyyy HH:mm"));
                            existingLvItem.SubItems.Add(missionInfo.MissionFinishedEDProfile.ToString());
                        }
                    }
                    else
                    {
                        Trace.TraceInformation("Mission is cooling down still");
                    }
                }
                else
                {
                    //new mission
                    var edProfile = edapi.getProfile();
                    if (string.IsNullOrEmpty(commanderName))
                        commanderName = edProfile.CommanderName;
                    result.MissionTakenDateTime = DateTime.UtcNow;
                    result.MissionTakenEDProfile = edProfile;
                    ListViewItem lvItem = new ListViewItem(result.MissionTakenDateTime.ToString("dd/MM/yyyy HH:mm"));
                    lvItem.SubItems.Add(result.MissionTakenEDProfile.ToString());
                    lvItem.SubItems.Add(result.MissionName);
                    lvItem.Tag = result;
                    missionListView.Items.Add(lvItem);
                }
            }
        }

        private void activateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(commanderName))
            {
                initialize();
                return;
            }
            // Begin watching.
            if (watcher != null && watcher.EnableRaisingEvents)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                activateButton.Text = "Activate";
            }
            else
            {
                watcher = new FileSystemWatcher();
                watcher.Path = Properties.Settings.Default.PicturesFolder;
                /* Watch for changes in LastAccess and LastWrite times, and
                   the renaming of files or directories. */
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                // Only watch text files.
                watcher.Filter = "*.bmp";

                // Add event handlers.
                watcher.Created += new FileSystemEventHandler(RegisterMission);
                watcher.EnableRaisingEvents = true;
                analyzer = new ScreenAnalyzer(Properties.Settings.Default.Language);
                activateButton.Text = "Deactivate";
                toolStripStatusLabel1.Text = "Activated, Wathing for screenshots under: " + Properties.Settings.Default.PicturesFolder;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            var missions = new List<MissionInfo>();
            foreach(ListViewItem row in missionListView.Items)
            {
                missions.Add((MissionInfo)row.Tag);
            }
            var jsonData = JsonConvert.SerializeObject(new { Missions = missions, CommanderName = commanderName }, Formatting.Indented);
            //save it
            System.IO.File.WriteAllText(Properties.Settings.Default.ExportFilePath, jsonData);
            toolStripStatusLabel1.Text = "Data saved to: " + Properties.Settings.Default.ExportFilePath +". ";
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ExportCommandExec))
            {
                var args = Properties.Settings.Default.ExportCommandArgs;
                args = args.Replace("$exportedFile", "\""+ Properties.Settings.Default.ExportFilePath) + "\"";
                Process.Start(new ProcessStartInfo(Properties.Settings.Default.ExportCommandExec) {
                    Arguments = args,
                    LoadUserProfile = true
                });
                toolStripStatusLabel1.Text += "Export script started with arguments: " + args;
            } else
            {
                toolStripStatusLabel1.Text += "Export script not executed as not specified or doesn't exist.";
            }
        }

        private void missionListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {

        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            
        }

        private void ReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsFrom = new SettingsForm(edapi);
            settingsFrom.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void initialize()
        {
            if (edapi.isLoginRequired())
            {
                //show login form
                var loginForm = new LoginForm(edapi);
                if (loginForm.ShowDialog(this) != DialogResult.OK)
                {
                    toolStripStatusLabel1.Text = "Login was cancelled.";
                    activateButton.Text = "Login";
                    return;
                }
                else
                    loginForm.Dispose();
            }
            commanderName = edapi.getProfile().CommanderName;
            if (!string.IsNullOrEmpty(commanderName))
                toolStripStatusLabel1.Text = "Welcome CMDR " + commanderName + ". ";
            if (Directory.Exists(Properties.Settings.Default.PicturesFolder))
                activateButton_Click(null, null);
            else
            {
                toolStripStatusLabel1.Text = "Configure pictures folder in settings and press Activate button to start watching for missions!";
            }
        }

        private void ReportForm_Shown(object sender, EventArgs e)
        {
            initialize();
        }
    }
}
