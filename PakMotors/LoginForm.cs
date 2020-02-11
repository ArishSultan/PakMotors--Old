using System;
using System.Drawing;
using PakMotors.Utils;
using System.Windows.Forms;
using DPUruNet;
using System.Collections.Generic;

namespace PakMotors
{
    public partial class LoginForm : Form
    {
        private int I;
        private string[] files;
        private List<string> users;
        private List<string> passwords;

        public LoginForm()
        {
            users = new List<string>();
            passwords = new List<string>();


            InitializeComponent();

            try
            {
                files = System.IO.Directory.GetFiles("UserManagement/Users");
            }
            catch (Exception)
            {
                MessageBox.Show("No User Exists Please Add a User using the PakMotorsUserCreater.exe");
                this.Close();
            }

            for (int i = 0; i < files.Length; i ++)
            {
                if (files[i].Contains("_fmd")) continue;
                var split = System.IO.File.ReadAllText(files[i]).Split(',');
                users.Add(split[0]);
                passwords.Add(split[1]);
            }

            emailError.Visible = false;
            passwordError.Visible = false;

            emailSubmitButton.Width = 89;
            emailSubmitButton.AutoSize = false;

            passwordSumbitButton.Width = 89;
            passwordSumbitButton.AutoSize = false;

            loginPanel.Anchor = AnchorStyles.None;
            loginPanel.Location = new Point(ClientSize.Width / 2 - loginPanel.Size.Width / 2, ClientSize.Height / 2 - loginPanel.Size.Height / 2);
        }

        // Text Changed Events
        private void emailInputField_TextChanged(object sender, EventArgs e)
        {
            if (emailInputField.Text != "")
                emailLabel.Visible = false;
            else emailLabel.Visible = true;
        }
        private void passwordInputField_TextChanged(object sender, EventArgs e)
        {
            if (passwordInputField.Text != "")
                passwordLabel.Visible = false;
            else passwordLabel.Visible = true;
        }


        // Label Click Events
        private void emailLabel_Click(object sender, EventArgs e) { emailInputField.Focus(); }
        private void passwordLabel_Click(object sender, EventArgs e) { passwordInputField.Focus(); }

        // Key Press Events
        private void emailInputField_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) emailSubmitButton.PerformClick(); }
        private void passwordInputField_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char) Keys.Enter) passwordSumbitButton.PerformClick(); }

        // Button Click Events
        private void emailSubmitButton_Click(object sender, EventArgs e)
        {
            var text = emailInputField.Text;
            // Check if the email is correct

            for (int i = 0; i < users.Count; i++)
            {
                if (text == users[i])
                {
                    I = i;
                    emailPanel.Dispose();

                    try
                    {
                        Utils.DigitalPersonaUtil._.IdentifyPerson(fingerprintError, label4, this, Fmd.DeserializeXml(System.IO.File.ReadAllText($"UserManagement/Users/{text}_fmd")));
                    }
                    catch(Exception)
                    {

                    }

                    return; 
                }
            }
            emailError.Visible = true;
        }
        private void passwordSumbitButton_Click(object sender, EventArgs e)
        {
            // Check if the password is correct
            if (passwordInputField.Text == passwords[I])
            {
                this.Hide();

                var d = new Dashboard();
                Utils.DigitalPersonaUtil._.StopAllActivities();

                d.FormClosed += (_sender, _e) =>
                {
                    this.Close();
                };

                d.Show();

            }
            else passwordError.Visible = true;
        }


        // OnExitHandler
        public void OnExit()
        {

        }

        private void FingerprintError_TextChanged(object sender, EventArgs e)
        {
        }

        private void Label4_TextChanged(object sender, EventArgs e)
        {
            if (label4.Text == "Found")
            {
                this.Hide();

                var d = new Dashboard();

                d.FormClosed += (_sender, _e) =>
                {
                    this.Close();
                };

                d.Show();
            }
            else label4.Text = "Try Your Password";
        }
    }
}
