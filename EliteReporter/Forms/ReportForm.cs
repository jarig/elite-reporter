using EliteReporter.Forms;
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

namespace EliteReporter
{
    public partial class ReportForm : Form
    {
        private ScreenAnalyzer analyzer;
        private EDAPI edapi;
        private GlobalHotkey ghk;
        private TimeSpan takenMisisonCoolDown = new TimeSpan(0, 0, 40);
        private string commanderName;

        public ReportForm()
        {
            InitializeComponent();
            edapi = new EDAPI();
            ghk = new GlobalHotkey(GlobalHotkey.CTRL + GlobalHotkey.ALT, Keys.M, this);
        }

        private void RegisterMission()
        {
            var result = analyzer.findAndAnalyzeMissionSummaryPage();
            if (result == null)
                return;
            
            var existingLvItem = missionListView.Items.Cast<ListViewItem>().
                Where(i => ((MissionInfo)i.Tag).MissionName.FuzzyEquals(result.MissionName)).FirstOrDefault();
            if (existingLvItem != null)
            { 
                //finshing mission
                if(((MissionInfo)existingLvItem.Tag).MissionTakenDateTime.Add(takenMisisonCoolDown) < DateTime.UtcNow)
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
                } else
                {
                    Trace.TraceInformation("Mission is cooling down still");
                }
            } else
            {
                //new mission
                var edProfile = edapi.getProfile();
                result.MissionTakenDateTime = DateTime.UtcNow;
                result.MissionTakenEDProfile = edProfile;
                ListViewItem lvItem = new ListViewItem(result.MissionTakenDateTime.ToString("dd/MM/yyyy HH:mm"));
                lvItem.SubItems.Add(result.MissionTakenEDProfile.ToString());
                lvItem.SubItems.Add(result.MissionName);
                lvItem.Tag = result;
                missionListView.Items.Add(lvItem);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == GlobalHotkey.WM_HOTKEY_MSG_ID)
                RegisterMission();
            base.WndProc(ref m);
        }

        private void activateButton_Click(object sender, EventArgs e)
        {
            if (ghk.Registered)
            {
                ghk.Unregiser();
                activateButton.Text = "Activate";
                toolStripStatusLabel1.Text = "Hotkey unregistered.";
            }
            else
            {
                activateButton.Text = "Deactivate";
                toolStripStatusLabel1.Text = "Watching for missions!";
                ghk.Register();
                analyzer = new ScreenAnalyzer(Properties.Settings.Default.Language);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            /*var result = analyzer.findAndAnalyzeMissionSummaryPage(true);
            if (result != null)
            {
                var form = new TestForm(result);
                form.Show();
            }*/

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
            activateButton_Click(null, null);
        }

        private void showLoginForm()
        {
            var loginForm = new LoginForm(edapi);
            loginForm.Show();
        }

        private void ReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ghk.Unregiser();
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

        private void ReportForm_Shown(object sender, EventArgs e)
        {
            if (edapi.isLoginRequired())
            {
                //show login form
                showLoginForm();
            }
            commanderName = edapi.getProfile().CommanderName;
            toolStripStatusLabel1.Text = "Welcome CMDR " + commanderName + ". ";
            toolStripStatusLabel1.Text += "Press Activate button to register global hotkey for mission registration (CTRL + ALT + M)";
        }
    }
}
