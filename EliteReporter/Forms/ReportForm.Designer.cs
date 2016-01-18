﻿namespace EliteReporter
{
    partial class ReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.activateButton = new System.Windows.Forms.Button();
            this.missionListView = new System.Windows.Forms.ListView();
            this.startDateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startSystemHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.missionNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.endDateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.endSystemHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.missionDetailTextBox = new System.Windows.Forms.RichTextBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameResolutionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // activateButton
            // 
            this.activateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.activateButton.Location = new System.Drawing.Point(737, 390);
            this.activateButton.Margin = new System.Windows.Forms.Padding(2);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(86, 40);
            this.activateButton.TabIndex = 0;
            this.activateButton.Text = "Activate";
            this.activateButton.UseVisualStyleBackColor = true;
            this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // missionListView
            // 
            this.missionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.missionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.startDateHeader,
            this.startSystemHeader,
            this.missionNameHeader,
            this.endDateHeader,
            this.endSystemHeader});
            this.missionListView.FullRowSelect = true;
            this.missionListView.LabelEdit = true;
            this.missionListView.Location = new System.Drawing.Point(9, 26);
            this.missionListView.Margin = new System.Windows.Forms.Padding(2);
            this.missionListView.Name = "missionListView";
            this.missionListView.Size = new System.Drawing.Size(619, 360);
            this.missionListView.TabIndex = 1;
            this.missionListView.UseCompatibleStateImageBehavior = false;
            this.missionListView.View = System.Windows.Forms.View.Details;
            this.missionListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.missionListView_AfterLabelEdit);
            // 
            // startDateHeader
            // 
            this.startDateHeader.Text = "Date taken";
            this.startDateHeader.Width = 95;
            // 
            // startSystemHeader
            // 
            this.startSystemHeader.Text = "System/Station";
            this.startSystemHeader.Width = 120;
            // 
            // missionNameHeader
            // 
            this.missionNameHeader.Text = "Mission name";
            this.missionNameHeader.Width = 150;
            // 
            // endDateHeader
            // 
            this.endDateHeader.Text = "Date finished";
            this.endDateHeader.Width = 95;
            // 
            // endSystemHeader
            // 
            this.endSystemHeader.Text = "System/Station";
            this.endSystemHeader.Width = 120;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(11, 390);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // missionDetailTextBox
            // 
            this.missionDetailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.missionDetailTextBox.Location = new System.Drawing.Point(632, 102);
            this.missionDetailTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.missionDetailTextBox.Name = "missionDetailTextBox";
            this.missionDetailTextBox.ReadOnly = true;
            this.missionDetailTextBox.Size = new System.Drawing.Size(191, 284);
            this.missionDetailTextBox.TabIndex = 3;
            this.missionDetailTextBox.Text = "";
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(647, 390);
            this.exportButton.Margin = new System.Windows.Forms.Padding(2);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(86, 40);
            this.exportButton.TabIndex = 4;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(831, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(831, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // gameResolutionTextBox
            // 
            this.gameResolutionTextBox.Location = new System.Drawing.Point(633, 51);
            this.gameResolutionTextBox.Name = "gameResolutionTextBox";
            this.gameResolutionTextBox.Size = new System.Drawing.Size(190, 20);
            this.gameResolutionTextBox.TabIndex = 7;
            this.gameResolutionTextBox.TextChanged += new System.EventHandler(this.gameResolutionTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(634, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Resolution in game";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 454);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameResolutionTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.missionDetailTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.missionListView);
            this.Controls.Add(this.activateButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elite Mission Reporter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportForm_FormClosing);
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.Shown += new System.EventHandler(this.ReportForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button activateButton;
        private System.Windows.Forms.ListView missionListView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox missionDetailTextBox;
        private System.Windows.Forms.ColumnHeader startDateHeader;
        private System.Windows.Forms.ColumnHeader missionNameHeader;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ColumnHeader startSystemHeader;
        private System.Windows.Forms.ColumnHeader endDateHeader;
        private System.Windows.Forms.ColumnHeader endSystemHeader;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.TextBox gameResolutionTextBox;
        private System.Windows.Forms.Label label1;
    }
}
