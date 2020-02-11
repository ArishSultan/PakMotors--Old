namespace PakMotors
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.loginPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.emailPanel = new System.Windows.Forms.Panel();
            this.emailError = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.emailSubmitButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.emailInputField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordError = new System.Windows.Forms.Label();
            this.fingerprintError = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.passwordLabel = new MaterialSkin.Controls.MaterialLabel();
            this.passwordSumbitButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.passwordInputField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label6 = new System.Windows.Forms.Label();
            this.loginPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.emailPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.White;
            this.loginPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loginPanel.Controls.Add(this.label4);
            this.loginPanel.Controls.Add(this.panel2);
            this.loginPanel.Location = new System.Drawing.Point(196, 129);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(448, 500);
            this.loginPanel.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 469);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            this.label4.TextChanged += new System.EventHandler(this.Label4_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.emailPanel);
            this.panel2.Controls.Add(this.passwordError);
            this.panel2.Controls.Add(this.fingerprintError);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.passwordLabel);
            this.panel2.Controls.Add(this.passwordSumbitButton);
            this.panel2.Controls.Add(this.passwordInputField);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(40, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(368, 416);
            this.panel2.TabIndex = 7;
            // 
            // emailPanel
            // 
            this.emailPanel.Controls.Add(this.emailError);
            this.emailPanel.Controls.Add(this.emailLabel);
            this.emailPanel.Controls.Add(this.label3);
            this.emailPanel.Controls.Add(this.label2);
            this.emailPanel.Controls.Add(this.emailSubmitButton);
            this.emailPanel.Controls.Add(this.emailInputField);
            this.emailPanel.Controls.Add(this.label1);
            this.emailPanel.Location = new System.Drawing.Point(0, 0);
            this.emailPanel.Name = "emailPanel";
            this.emailPanel.Size = new System.Drawing.Size(368, 416);
            this.emailPanel.TabIndex = 5;
            // 
            // emailError
            // 
            this.emailError.AutoSize = true;
            this.emailError.Font = new System.Drawing.Font("Roboto Condensed", 8.25F);
            this.emailError.ForeColor = System.Drawing.Color.Red;
            this.emailError.Location = new System.Drawing.Point(-2, 178);
            this.emailError.Name = "emailError";
            this.emailError.Size = new System.Drawing.Size(165, 17);
            this.emailError.TabIndex = 9;
            this.emailError.Text = "Unknown Error was Occurred";
            // 
            // emailLabel
            // 
            this.emailLabel.BackColor = System.Drawing.Color.Transparent;
            this.emailLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.emailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.emailLabel.Location = new System.Drawing.Point(-4, 155);
            this.emailLabel.Margin = new System.Windows.Forms.Padding(0);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(83, 20);
            this.emailLabel.TabIndex = 8;
            this.emailLabel.Text = "Email or ID";
            this.emailLabel.Click += new System.EventHandler(this.emailLabel_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Roboto", 9F);
            this.label3.Location = new System.Drawing.Point(-2, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(378, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "In case you have forgotten password or you need any other services than you can C" +
    "ontact Developers";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto", 15.75F);
            this.label2.Location = new System.Drawing.Point(0, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(368, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "with your Account";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // emailSubmitButton
            // 
            this.emailSubmitButton.AutoSize = true;
            this.emailSubmitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.emailSubmitButton.BackColor = System.Drawing.Color.Yellow;
            this.emailSubmitButton.Depth = 0;
            this.emailSubmitButton.Icon = null;
            this.emailSubmitButton.Location = new System.Drawing.Point(276, 336);
            this.emailSubmitButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.emailSubmitButton.Name = "emailSubmitButton";
            this.emailSubmitButton.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.emailSubmitButton.Primary = true;
            this.emailSubmitButton.Size = new System.Drawing.Size(65, 36);
            this.emailSubmitButton.TabIndex = 2;
            this.emailSubmitButton.Text = "Next";
            this.emailSubmitButton.UseVisualStyleBackColor = false;
            this.emailSubmitButton.Click += new System.EventHandler(this.emailSubmitButton_Click);
            // 
            // emailInputField
            // 
            this.emailInputField.Depth = 0;
            this.emailInputField.Font = new System.Drawing.Font("Roboto Condensed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailInputField.Hint = "";
            this.emailInputField.Location = new System.Drawing.Point(0, 155);
            this.emailInputField.MaxLength = 32767;
            this.emailInputField.MouseState = MaterialSkin.MouseState.HOVER;
            this.emailInputField.Name = "emailInputField";
            this.emailInputField.PasswordChar = '\0';
            this.emailInputField.SelectedText = "";
            this.emailInputField.SelectionLength = 0;
            this.emailInputField.SelectionStart = 0;
            this.emailInputField.Size = new System.Drawing.Size(368, 28);
            this.emailInputField.TabIndex = 4;
            this.emailInputField.TabStop = false;
            this.emailInputField.Tag = "";
            this.emailInputField.UseSystemPasswordChar = false;
            this.emailInputField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.emailInputField_KeyPress);
            this.emailInputField.TextChanged += new System.EventHandler(this.emailInputField_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Google Sans", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sign in";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // passwordError
            // 
            this.passwordError.AutoSize = true;
            this.passwordError.Font = new System.Drawing.Font("Roboto Condensed", 8.25F);
            this.passwordError.ForeColor = System.Drawing.Color.Red;
            this.passwordError.Location = new System.Drawing.Point(-2, 178);
            this.passwordError.Name = "passwordError";
            this.passwordError.Size = new System.Drawing.Size(207, 17);
            this.passwordError.TabIndex = 9;
            this.passwordError.Text = "The password you entered is wrong.";
            // 
            // fingerprintError
            // 
            this.fingerprintError.ForeColor = System.Drawing.Color.Red;
            this.fingerprintError.Location = new System.Drawing.Point(127, 321);
            this.fingerprintError.Name = "fingerprintError";
            this.fingerprintError.Size = new System.Drawing.Size(112, 19);
            this.fingerprintError.TabIndex = 8;
            this.fingerprintError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fingerprintError.TextChanged += new System.EventHandler(this.FingerprintError_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(127, 218);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(112, 100);
            this.panel3.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordLabel.Depth = 0;
            this.passwordLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.passwordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.passwordLabel.Location = new System.Drawing.Point(-4, 155);
            this.passwordLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(265, 24);
            this.passwordLabel.TabIndex = 6;
            this.passwordLabel.Text = "Enter Password or Use Thumb";
            this.passwordLabel.Click += new System.EventHandler(this.passwordLabel_Click);
            // 
            // passwordSumbitButton
            // 
            this.passwordSumbitButton.AutoSize = true;
            this.passwordSumbitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.passwordSumbitButton.BackColor = System.Drawing.Color.Yellow;
            this.passwordSumbitButton.Depth = 0;
            this.passwordSumbitButton.Icon = null;
            this.passwordSumbitButton.Location = new System.Drawing.Point(280, 360);
            this.passwordSumbitButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.passwordSumbitButton.Name = "passwordSumbitButton";
            this.passwordSumbitButton.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.passwordSumbitButton.Primary = true;
            this.passwordSumbitButton.Size = new System.Drawing.Size(67, 36);
            this.passwordSumbitButton.TabIndex = 2;
            this.passwordSumbitButton.Text = "Done";
            this.passwordSumbitButton.UseVisualStyleBackColor = false;
            this.passwordSumbitButton.Click += new System.EventHandler(this.passwordSumbitButton_Click);
            // 
            // passwordInputField
            // 
            this.passwordInputField.Depth = 0;
            this.passwordInputField.Font = new System.Drawing.Font("Roboto Condensed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordInputField.Hint = "";
            this.passwordInputField.Location = new System.Drawing.Point(0, 155);
            this.passwordInputField.MaxLength = 32767;
            this.passwordInputField.MouseState = MaterialSkin.MouseState.HOVER;
            this.passwordInputField.Name = "passwordInputField";
            this.passwordInputField.PasswordChar = '\0';
            this.passwordInputField.SelectedText = "";
            this.passwordInputField.SelectionLength = 0;
            this.passwordInputField.SelectionStart = 0;
            this.passwordInputField.Size = new System.Drawing.Size(368, 28);
            this.passwordInputField.TabIndex = 4;
            this.passwordInputField.TabStop = false;
            this.passwordInputField.Tag = "";
            this.passwordInputField.UseSystemPasswordChar = false;
            this.passwordInputField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordInputField_KeyPress);
            this.passwordInputField.TextChanged += new System.EventHandler(this.passwordInputField_TextChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Google Sans", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(368, 39);
            this.label6.TabIndex = 0;
            this.label6.Text = "Welcome";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(863, 711);
            this.Controls.Add(this.loginPanel);
            this.Name = "LoginForm";
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.emailPanel.ResumeLayout(false);
            this.emailPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel emailPanel;
        private MaterialSkin.Controls.MaterialRaisedButton emailSubmitButton;
        private MaterialSkin.Controls.MaterialSingleLineTextField emailInputField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private MaterialSkin.Controls.MaterialLabel passwordLabel;
        private MaterialSkin.Controls.MaterialRaisedButton passwordSumbitButton;
        private MaterialSkin.Controls.MaterialSingleLineTextField passwordInputField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label emailError;
        private System.Windows.Forms.Label passwordError;
        private System.Windows.Forms.Label fingerprintError;
        private System.Windows.Forms.Label label4;
    }
}