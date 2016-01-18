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
    public partial class LoginForm : Form
    {
        private EDAPI edapi;
        public LoginForm(EDAPI edapi)
        {
            InitializeComponent();
            this.edapi = edapi;
            toolStripStatusLabel1.Text = "Enter your Frontier credentials and press Login";
            passwordTextBox.UseSystemPasswordChar = true;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusLabel1.Text = "Logging in...";
                loginButton.Enabled = false;
                edapi.doLogin(emailTextBox.Text, passwordTextBox.Text);
            } catch (EDAPI.AuthenticationException ex)
            {
                toolStripStatusLabel1.Text = "Login failed, invalid password?";
            } catch (EDAPI.ConfirmationRequired ex)
            {
                var confirmationCodeDialog = new ConfirmationCodeForm();

                toolStripStatusLabel1.Text = "Waiting for confirmation code to be entered...";
                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                if (confirmationCodeDialog.ShowDialog(this) == DialogResult.OK)
                {
                    // Read the contents of testDialog's TextBox.
                    var confirmCode = confirmationCodeDialog.ConfirmationCode;
                    confirmationCodeDialog.Dispose();
                    toolStripStatusLabel1.Text = "Sending confirmation code to Frontier....";
                    edapi.confirm(confirmCode);
                    var profile = edapi.getProfile();
                    if (profile != null)
                    {
                        MessageBox.Show("Greetings commander " + profile.CommanderName, "Welcome!");
                        Dispose();
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Login failed";
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "Confirmation code not retrieved.";
                }
            }
            loginButton.Enabled = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
