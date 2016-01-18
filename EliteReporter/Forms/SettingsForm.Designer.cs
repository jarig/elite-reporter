namespace EliteReporter.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.missionCoolDownTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.exportExecPathTextBox = new System.Windows.Forms.TextBox();
            this.exportExecArgsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.exportFileTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.picturePathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Elite Dangerous Game Language";
            // 
            // languageComboBox
            // 
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Location = new System.Drawing.Point(16, 29);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(339, 21);
            this.languageComboBox.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(280, 294);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 45);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(12, 294);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 45);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // missionCoolDownTextBox
            // 
            this.missionCoolDownTextBox.Location = new System.Drawing.Point(16, 73);
            this.missionCoolDownTextBox.Name = "missionCoolDownTextBox";
            this.missionCoolDownTextBox.Size = new System.Drawing.Size(339, 20);
            this.missionCoolDownTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mission finish cooldown(seconds)";
            // 
            // exportExecPathTextBox
            // 
            this.exportExecPathTextBox.Location = new System.Drawing.Point(16, 112);
            this.exportExecPathTextBox.Name = "exportExecPathTextBox";
            this.exportExecPathTextBox.Size = new System.Drawing.Size(339, 20);
            this.exportExecPathTextBox.TabIndex = 6;
            // 
            // exportExecArgsTextBox
            // 
            this.exportExecArgsTextBox.Location = new System.Drawing.Point(16, 155);
            this.exportExecArgsTextBox.Name = "exportExecArgsTextBox";
            this.exportExecArgsTextBox.Size = new System.Drawing.Size(339, 20);
            this.exportExecArgsTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Executable to run on Export";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Arguments for export executable";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label5.Location = new System.Drawing.Point(13, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(293, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Use $exportedFile placeholder as reference to a file exported.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Exported file destination";
            // 
            // exportFileTextBox
            // 
            this.exportFileTextBox.Location = new System.Drawing.Point(16, 217);
            this.exportFileTextBox.Name = "exportFileTextBox";
            this.exportFileTextBox.Size = new System.Drawing.Size(339, 20);
            this.exportFileTextBox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(225, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Elite Dangerous Path to Pictures(Screenshots)";
            // 
            // picturePathTextBox
            // 
            this.picturePathTextBox.Location = new System.Drawing.Point(15, 256);
            this.picturePathTextBox.Name = "picturePathTextBox";
            this.picturePathTextBox.Size = new System.Drawing.Size(339, 20);
            this.picturePathTextBox.TabIndex = 13;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 351);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.picturePathTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.exportFileTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.exportExecArgsTextBox);
            this.Controls.Add(this.exportExecPathTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.missionCoolDownTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.languageComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox missionCoolDownTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox exportExecPathTextBox;
        private System.Windows.Forms.TextBox exportExecArgsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox exportFileTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox picturePathTextBox;
    }
}