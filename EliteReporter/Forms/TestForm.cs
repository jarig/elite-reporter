using EliteReporter.Models;
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
    public partial class TestForm : Form
    {
        public TestForm(MissionInfo mInfo)
        {
            InitializeComponent();
            pictureBox1.Image = mInfo.Images.First();
        }
    }
}
