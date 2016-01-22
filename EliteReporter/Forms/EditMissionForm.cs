using EliteReporter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteReporter.Forms
{
    public partial class EditMissionForm : Form
    {
        MissionInfo mInfo;
        public EditMissionForm(MissionInfo mInfo)
        {
            InitializeComponent();
            this.mInfo = mInfo;
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            mInfo.MissionName = missionNameTextBox.Text;
            try
            {
                mInfo.MissionTakenDateTime = DateTime.ParseExact(dateTakenTextBox.Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            catch (FormatException ex) { }
            mInfo.MissionTakenEDProfile.PortName = stationTakenTextBox.Text;
            mInfo.MissionTakenEDProfile.SystemName = systemTakenTextBox.Text;
            if (mInfo.MissionFinishedEDProfile == null)
            {
                mInfo.MissionFinishedEDProfile = new EDProfile();
            }
            if(!string.IsNullOrEmpty(dateFinishTextBox.Text))
            {
                try {
                    mInfo.MissionFinishedDateTime = DateTime.ParseExact(dateFinishTextBox.Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                }catch(FormatException ex){}
            }
            
            mInfo.MissionFinishedEDProfile.PortName = stationFinishTextBox.Text;
            mInfo.MissionFinishedEDProfile.SystemName = systemFinishTextBox.Text;
            mInfo.Reward = int.Parse(rewardTextBox.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void EditMissionForm_Load(object sender, EventArgs e)
        {
            missionNameTextBox.Text = mInfo.MissionName;
            dateTakenTextBox.Text = mInfo.MissionTakenDateTime.Value.ToString("dd.MM.yyyy HH:mm");
            stationTakenTextBox.Text = mInfo.MissionTakenEDProfile.PortName;
            systemTakenTextBox.Text = mInfo.MissionTakenEDProfile.SystemName;
            if (mInfo.MissionFinishedEDProfile != null)
            {
                stationFinishTextBox.Text = mInfo.MissionFinishedEDProfile.PortName;
                systemFinishTextBox.Text = mInfo.MissionFinishedEDProfile.SystemName;
                if (mInfo.MissionFinishedDateTime != null)
                    dateFinishTextBox.Text = mInfo.MissionFinishedDateTime.Value.ToString("dd.MM.yyyy HH:mm");
            }
            rewardTextBox.Text = mInfo.Reward.ToString();
        }
    }
}
