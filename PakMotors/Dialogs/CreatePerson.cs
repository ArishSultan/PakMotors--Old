using System;
using System.Drawing;
using PakMotors.Models;
using System.Windows.Forms;

namespace PakMotors.Dialogs
{
    public partial class CreatePerson : Form
    {
        public static Models.Person RecentPerson = null;

        private bool isUpdate = false;

        private object[] BuyerControls;
        private Person Buyer = new Person();

        private Dialogs.CameraDialog camera = new Dialogs.CameraDialog();


        public CreatePerson()
        {
            InitializeComponent();

            label12.Visible = false;
            thumbBox.Image = pictureBox.Image = signatureBox.Image = null;
            panel1.Location = new Point(ClientSize.Width / 2 - panel1.Size.Width / 2, panel1.Location.Y);
            BuyerControls = new object[] { name, cast, cnic, phone1, phone2, address, fatherName, pictureBox, signatureBox, thumbBox, flowLayoutPanel1 };
        }

        public CreatePerson(int id): this()
        {
            isUpdate = true;
            create_update.Text = "Update";

            try
            {
                Buyer.Fetch(id);
                Models.Person.Fill(BuyerControls, Buyer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }

            var query = Utils.DBManager.Query("SELECT * FROM View_2 WHERE Id = " + id);

            if (query.Count > 0)
            {
                label12.Visible = true;
                label12.Text = "Pending Amount: " + query[0]["Expr1"];
            }
        }

        private void create_update_Click(object sender, EventArgs e)
        {
            try
            {
                Person.Fill(Buyer, BuyerControls);

                if (isUpdate)
                {
                    Person.Update(Buyer);
                }
                else
                    Person.Insert(Buyer, false, false, false);

                RecentPerson = Buyer;

                var res = MessageBox.Show("Successfully Added Data");


                if (res == DialogResult.OK)
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Here");
                Utils.DBManager.CloseConnection();
            }
        }

        private void CreateBuyers_SizeChanged(object sender, EventArgs e)
        {
            panel1.Location = new Point(ClientSize.Width / 2 - panel1.Size.Width / 2, panel1.Location.Y);
        }

        #region buyerPicture
        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            var dialog = new dialogs.ShowImage((sender as PictureBox).Image);

            dialog.ShowDialog();

            if (!dialog.status)
            {
                pictureBox.Image = null;
            }
        }
        private void PictureCamera_Click(object sender, EventArgs e) { Utils.ImageHandling.AddImageWithCamera(pictureBox); }
        private void PictureImage_Click(object sender, EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(pictureBox); }
        #endregion

        #region buyerSignature
        private void SignatureBox_Click(object sender, EventArgs e)
        {
            if (signatureBox.Image == null) return;

            var dialog = new dialogs.ShowImage((sender as PictureBox).Image);

            dialog.ShowDialog();

            if (!dialog.status)
            {
                signatureBox.Image = null;
            }
        }
        private void SignatureCamera_Click(object sender, EventArgs e) { Utils.ImageHandling.AddImageWithCamera(signatureBox); }
        private void SignatureImage_Click(object sender, EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(signatureBox); }
        #endregion

        #region buyerFingerPrint
        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if (captureButton.Text == "Start")
            {
                captureButton.Text = "Capture";
                Utils.DigitalPersonaUtil._.StartCapturing(thumbBox, this);
            } 
            else
            {
                captureButton.Text = "Start";
                Utils.DigitalPersonaUtil._.StopAllActivities();
            }
        }
        private void EnrolmentButton_Click(object sender, EventArgs e)
        {
            Utils.DigitalPersonaUtil._.StartEnrolement(Buyer);
        }
        #endregion

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.S:
                    {
                        create_update.PerformClick();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }
        private void AdditionalImageAdd_Click(object sender, EventArgs e)
        {
            var fileDialog = Utils.ImageHandling.getImageFilePicker(true);

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                Utils.ImageHandling.InsertInList(fileDialog.FileNames, flowLayoutPanel1);
        }
        private void AdditionalImageCapture_Click(object sender, EventArgs e)
        {
            camera.ShowDialog();
            Utils.ImageHandling.InsertInList(camera.images, flowLayoutPanel1);
            camera.images.Clear();
        }

        public static Models.Person GetRecentPerson() { return RecentPerson; }

        private void ThumbBox_Click(object sender, EventArgs e)
        {
            if (thumbBox.Image == null) return;

            var dialog = new dialogs.ShowImage((sender as PictureBox).Image);

            if (!dialog.status)
            {
                thumbBox.Image = null;
            }
        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }
    }
}
