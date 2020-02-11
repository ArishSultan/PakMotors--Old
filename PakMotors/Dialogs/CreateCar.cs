using System;
using System.Drawing;
using System.Windows.Forms;

namespace PakMotors.dialogs
{
    public partial class CreateCar : Form
    {
        Models.Person _Seller = null;
        Models.Person _SellerWitness = null;

        public static Models.Car RecentCar = null;

        private bool isUpdate = false;

        private Models.Car Car = new Models.Car();
        private Dialogs.CameraDialog camera = new Dialogs.CameraDialog();

        private object[] CarFiller;

        public CreateCar()
        {
            InitializeComponent();

            panel1.Location = new Point(ClientSize.Width / 2 - panel1.Size.Width / 2, panel1.Location.Y);

            CarFiller = new object[] { serial, purchaseAmount, PBO, model, color, engine, variant, chassis, invoiceRecieved, invoiceDatePicker, recievedDatePicker, horsePower, registration, flowLayoutPanel1, invoiceDelivered, InvoiceFlag, RecievedFlag, invoiceName, noteSecondary, checkBox2, warrantyRecieved };
        }

        public CreateCar(int id): this()
        {
            isUpdate = true;

            Car.Fetch(id);
            Models.Car.FillStock(CarFiller, Car);
            materialRaisedButton2.Text = "Update";

            if (Car.PurchasedFrom != 0)
            {
                this._Seller = new Models.Person();
                _Seller.Fetch(Car.PurchasedFrom);
                Seller.Text = _Seller.Name;
            }

            if (Car.PurchasedFromWitness != 0)
            {
                this._SellerWitness = new Models.Person();
                _SellerWitness.Fetch(Car.PurchasedFromWitness);
                SellerWitness.Text = _SellerWitness.Name;
            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            var fileDialog = Utils.ImageHandling.getImageFilePicker(true);

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                Utils.ImageHandling.InsertInList(fileDialog.FileNames, flowLayoutPanel1);
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            camera.ShowDialog();
            Utils.ImageHandling.InsertInList(camera.images, flowLayoutPanel1);
            camera.images.Clear();
        }

        private void PictureEventHandler(object sender, EventArgs e)
        {
            var dialog = new ShowImage((sender as PictureBox).Image);

            dialog.ShowDialog();

            if (!dialog.status)
                this.flowLayoutPanel1.Controls.Remove(sender as Control);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Models.Car.FillStock(Car, CarFiller);

                if (_Seller != null)
                {
                    Car.PurchasedFrom = _Seller.Id;
                    Car.Buyer = _Seller.Name;
                }
                if (_SellerWitness != null)
                {
                    Car.PurchasedFromWitness = _SellerWitness.Id;
                }

                if (isUpdate)
                    Models.Car.Update(Car);
                else
                    Models.Car.Insert(Car);

                RecentCar = Car;

                var res = MessageBox.Show("Successfully Added Data");

                if (res == DialogResult.OK)
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Utils.DBManager.CloseConnection();
            }
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.S:
                    {
                        materialRaisedButton2.PerformClick();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void CreateCar_SizeChanged(object sender, EventArgs e)
        {
            panel1.Location = new Point(ClientSize.Width / 2 - panel1.Size.Width / 2, panel1.Location.Y);
        }

        #region Seller
        private void SellerAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();

            if (Dialogs.CreatePerson.RecentPerson != null) _Seller = Dialogs.CreatePerson.RecentPerson;

            if (_Seller != null)
            {
                Seller.Text = _Seller.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        private void SellerSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();

            _Seller = Views.Persons.RecentPerson;
            if (_Seller != null)
            {
                Seller.Text = _Seller.Name;
                Views.Persons.RecentPerson = null;
            }
        }


        private void SellerClear_Click(object sender, EventArgs e)
        {
            Car.Buyer = "";
            _Seller = null;
            Seller.Text = "";
        }
        private void SellerView_Click(object sender, EventArgs e)
        {
            if(_Seller == null)
            {
                MessageBox.Show("No person selected");
                return;
            } 

            new Dialogs.CreatePerson(_Seller.Id).ShowDialog();
            if (Dialogs.CreatePerson.RecentPerson != null) _SellerWitness = Dialogs.CreatePerson.GetRecentPerson();

            if (_Seller != null)
            {
                Seller.Text = _Seller.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        #endregion

        #region SellerWitness
        private void SellerWitnessAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();
            _SellerWitness = Dialogs.CreatePerson.GetRecentPerson();

            if (_Seller != null)
            {
                SellerWitness.Text = _SellerWitness.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        private void SellerWitnessSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();

            _SellerWitness = Views.Persons.RecentPerson;
            if (_SellerWitness != null)
            {
                SellerWitness.Text = _SellerWitness.Name;
                Views.Persons.RecentPerson = null;
            }
        }

        private void SellerWitnessClear_Click(object sender, EventArgs e)
        {
            SellerWitness.Text = "";
        }

        private void SellerWitnessView_Click(object sender, EventArgs e)
        {
            if (_Seller == null || _Seller.Id == 0)
            {
                MessageBox.Show("You have selected no Seller");
                return;
            }

            new Dialogs.CreatePerson(_SellerWitness.Id).ShowDialog();

            if (Dialogs.CreatePerson.RecentPerson != null) _SellerWitness = Dialogs.CreatePerson.RecentPerson;

            if (_SellerWitness != null)
            {
                SellerWitness.Text = _SellerWitness.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        #endregion
    }
}
