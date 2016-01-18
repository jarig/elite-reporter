﻿using System;
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
    public partial class ConfirmationCodeForm : Form
    {
        public string ConfirmationCode {
            get
            {
                return codeTextBox.Text;
            }
        }
        public ConfirmationCodeForm()
        {
            InitializeComponent();
        }

        private void ConfirmationCodeForm_Load(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ConfirmationCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
