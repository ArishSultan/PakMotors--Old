using System;
using DPUruNet;
using System.Drawing;
using PakMotors.utils;
using System.Windows.Forms;

namespace PakMotors
{
    public partial class LoginForm : Form
    {
        DigitalPersonaUtil fingerPrint;

        public LoginForm()
        {
            InitializeComponent();

            fingerPrint = new DigitalPersonaUtil();

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
            if (text == "Arish" /*Will be changed with email*/)
            {
                emailPanel.Dispose(); // Destroy the Email Form.

                // Initialize the Fingerprint Identification System.
                //fingerPrint.IdentifyPerson(fingerprintError, 
                    //Fmd.DeserializeXml(System.IO.File.ReadAllText(@"C:\\temp\\thumb-prints\\" + text + "\\fmd.xml")), this);
            }
            else emailError.Visible = true;
        }
        private void passwordSumbitButton_Click(object sender, EventArgs e)
        {
            // Check if the password is correct
            if (passwordInputField.Text == "ASDASDASD")
            {
                Console.WriteLine("Login Success");
                this.Close();
            }
            else passwordError.Visible = true;
        }


        // OnExitHandler
        public void OnExit()
        {

        }
    }
}
