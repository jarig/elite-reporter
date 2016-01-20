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

namespace EliteReporter.Forms
{
    public partial class SettingsForm : Form
    {
        private EDAPI edapi;
        public SettingsForm(EDAPI edapi)
        {
            InitializeComponent();
            this.edapi = edapi;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = ((Language.LanguageType)languageComboBox.SelectedItem).Code;
            Properties.Settings.Default.ExportCommandExec = exportExecPathTextBox.Text;
            Properties.Settings.Default.ExportCommandArgs = exportExecArgsTextBox.Text;
            Properties.Settings.Default.MissionCoolDown = int.Parse(missionCoolDownTextBox.Text);
            Properties.Settings.Default.ExportFilePath = exportFileTextBox.Text;
            Properties.Settings.Default.PicturesFolder = picturePathTextBox.Text;
            Properties.Settings.Default.Save();
            Dispose();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            languageComboBox.Items.AddRange(Language.getAllLanguages().ToArray());
            languageComboBox.SelectedItem = Language.codeToLanguage(Properties.Settings.Default.Language);
            exportExecPathTextBox.Text = Properties.Settings.Default.ExportCommandExec;
            exportExecArgsTextBox.Text = Properties.Settings.Default.ExportCommandArgs;
            missionCoolDownTextBox.Text = Properties.Settings.Default.MissionCoolDown.ToString();
            picturePathTextBox.Text = Properties.Settings.Default.PicturesFolder;
            exportFileTextBox.Text = Properties.Settings.Default.ExportFilePath;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
