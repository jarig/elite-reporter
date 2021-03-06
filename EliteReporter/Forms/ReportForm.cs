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
using System.Threading;

namespace EliteReporter
{
    public partial class ReportForm : Form
    {
        private ScreenAnalyzer analyzer;
        private EDAPI edapi;
        private TimeSpan takenMisisonCoolDown;
        private string commanderName;
        private FileSystemWatcher watcher = null;
        delegate void AnalyzeScreenShotCallback(string text);

        private AnalyzeScreenShotCallback callback;
        public ReportForm()
        {
            InitializeComponent();
            edapi = new EDAPI();
            takenMisisonCoolDown = new TimeSpan(0, 0, Properties.Settings.Default.MissionCoolDown);
            if (string.IsNullOrEmpty(Properties.Settings.Default.PicturesFolder) || !Directory.Exists(Properties.Settings.Default.PicturesFolder))
            {
                Properties.Settings.Default.PicturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\Frontier Developments\Elite Dangerous";
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.SettingsSaving += onSettingsChanged;
            callback = new AnalyzeScreenShotCallback(analyzeScreenShot);
        }

        private void RegisterMission(object source, FileSystemEventArgs e)
        {
            Trace.TraceInformation("File: " + e.FullPath + " " + e.ChangeType);
            Thread.Sleep(500); // wait until file is ready
            try {
                analyzeScreenShot(e.FullPath);
            }catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error during screen analysis: " + ex.Message;
            }
        }

        private void analyzeScreenShot(string pathToBmp)
        {
            if (analyzer == null)
                return;

            if (missionListView.InvokeRequired)
            {
                missionListView.Invoke(callback, new object []{ pathToBmp });
            }   
            else
            {
                var result = analyzer.findAndAnalyzeMissionSummaryPage(pathToBmp);
                if (result == null || result.MissionName == null)
                {
                    Trace.TraceInformation("Can't recognize anything from pic: " + pathToBmp);
                    return;
                }

                var existingLvItem = missionListView.Items.Cast<ListViewItem>().
                    Where(i => ((MissionInfo)i.Tag).MissionName.LevenshteinDistance(result.MissionName) <= 3).FirstOrDefault();
                if (existingLvItem != null)
                {
                    //finshing mission
                    if (((MissionInfo)existingLvItem.Tag).MissionTakenDateTime.Value.Add(takenMisisonCoolDown) < DateTime.Now)
                    {
                        var edProfile = edapi.getProfile();
                        var missionInfo = (MissionInfo)existingLvItem.Tag;
                        if (missionInfo.MissionFinishedEDProfile == null)
                        {
                            missionInfo.MissionFinishedDateTime = DateTime.Now;
                            missionInfo.MissionFinishedEDProfile = edProfile;
                            fillMissionListViewItem(existingLvItem, missionInfo);
                        }
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Mission \""+ ((MissionInfo)existingLvItem.Tag).MissionName +"\" is cooling down still.";
                        Trace.TraceInformation("Mission is cooling down still");
                    }
                }
                else
                {
                    //new mission
                    var edProfile = edapi.getProfile();
                    if (string.IsNullOrEmpty(commanderName))
                        commanderName = edProfile.CommanderName;
                    result.MissionTakenDateTime = DateTime.Now;
                    result.MissionTakenEDProfile = edProfile;
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Tag = result;
                    string[] str = new string[missionListView.Columns.Count];
                    for (int i = 0; i < str.Length; i++) { str[i] = ""; }
                    lvItem.SubItems.AddRange(str);
                    fillMissionListViewItem(lvItem, result);
                    missionListView.Items.Add(lvItem);
                }
            }
        }


        private void onSettingsChanged(object sender, CancelEventArgs e)
        {
            if (analyzer != null && analyzer.getLanguage().Code != Properties.Settings.Default.Language)
            {
                analyzer.Dispose();
                analyzer = new ScreenAnalyzer(Properties.Settings.Default.Language);
            }
            if (watcher != null && watcher.Path != Properties.Settings.Default.PicturesFolder)
            {
                watcher.Path = Properties.Settings.Default.PicturesFolder;
            }
            if(takenMisisonCoolDown.TotalSeconds != Properties.Settings.Default.MissionCoolDown)
                takenMisisonCoolDown = new TimeSpan(0, 0, Properties.Settings.Default.MissionCoolDown);
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
                watcher = null;
                activateButton.Text = "Activate";
                toolStripStatusLabel1.Text = "Press Activate to start watching for missions";
            }
            else
            {   
                watcher = new FileSystemWatcher();
                watcher.Path = Properties.Settings.Default.PicturesFolder;
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                // Only watch text files.
                watcher.Filter = "*.bmp";

                // Add event handlers.
                watcher.Created += new FileSystemEventHandler(RegisterMission);
                watcher.EnableRaisingEvents = true;
                if (analyzer != null)
                    analyzer.Dispose();
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Delete selected missions?", "Confirm removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = missionListView.SelectedItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem itm = missionListView.SelectedItems[i];
                    missionListView.Items[itm.Index].Remove();
                }
            }
        }

        private void missionListView_DoubleClick(object sender, EventArgs e)
        {
            var selItems = missionListView.SelectedItems;
            if (selItems.Count > 0)
            {
                var mInfo = (MissionInfo)selItems[0].Tag;
                var form = new EditMissionForm(mInfo);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    fillMissionListViewItem(selItems[0], mInfo);
                }
                form.Dispose();
            }
        }

        private void fillMissionListViewItem(ListViewItem vItem, MissionInfo mInfo)
        {
            vItem.Text = mInfo.MissionTakenDateTime.Value.ToString("dd.MM.yyyy HH:mm");
            vItem.SubItems[missionNameHeader.Index] = new ListViewItem.ListViewSubItem(vItem, mInfo.MissionName);
            if (mInfo.MissionType != null)
                vItem.SubItems[missionTypeHeader.Index] = new ListViewItem.ListViewSubItem(vItem, mInfo.MissionType);
            vItem.SubItems[startSystemHeader.Index] = new ListViewItem.ListViewSubItem(vItem, mInfo.MissionTakenEDProfile.ToString());
            vItem.SubItems[rewardHeader.Index] = new ListViewItem.ListViewSubItem(vItem, mInfo.Reward.ToString());
            if (mInfo.MissionFinishedDateTime != null)
                vItem.SubItems[endDateHeader.Index] = new ListViewItem.ListViewSubItem(vItem, mInfo.MissionFinishedDateTime.Value.ToString("dd.MM.yyyy HH:mm"));
            if (mInfo.MissionFinishedEDProfile != null)
            {    
                vItem.SubItems[endSystemHeader.Index] = new ListViewItem.ListViewSubItem(vItem, mInfo.MissionFinishedEDProfile.ToString());
            }
        }
    }
}
