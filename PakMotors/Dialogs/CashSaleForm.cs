using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PakMotors.Dialogs
{
    public partial class CashSaleForm : Form
    {
        private Dialogs.CameraDialog camera = new Dialogs.CameraDialog();


        private bool isCarUpdate      = false;
        private bool isBuyerUpdate    = false;
        private bool isSellerUpdate   = false;
        private bool isWitness1Update = false;
        private bool isWitness2Update = false;


        private object[] CarFiller;

        private object[] BuyerFiller;
        private object[] SellerFiller;
        private object[] Witness1Filler;
        private object[] Witness2Filler;

        private Models.Car    Car       = new Models.Car();
        private Models.Person Buyer     = new Models.Person();
        private Models.Person Seller    = new Models.Person();
        private Models.Person Witness1  = new Models.Person();
        private Models.Person Witness2  = new Models.Person();

        private List<int> toBeDeleted = new List<int>();

        public CashSaleForm()
        {

            InitializeComponent();

            panel18.Location = new Point(ClientSize.Width / 2 - panel18.Size.Width / 2, panel18.Location.Y);

            CarFiller = new object[] { serial, purchaseAmount, PBO, model, color, engine, variant, chassis, invoiceRecieved, invoiceDatePicker, recievedDatePicker, horsePower, registration, flowLayoutPanel1, invoiceDelivered, InvoiceFlag, RecievedFlag, buyerName, buyerCNIC, carTotalAmmount, carSaleDate, carNote };

            BuyerFiller     = new object[] { buyerName,    buyerCast,    buyerCNIC,    buyerPhone1,    buyerPhone2,    buyerAddress,    buyerFatherName,    buyerPictureBox,    buyerSignatureBox,    buyerThumbBox };
            SellerFiller    = new object[] { sellerName,   sellerCast,   sellerCNIC,   sellerPhone1,   sellerPhone2,   sellerAddress,   sellerFatherName,   sellerPictureBox,   sellerSignatureBox,   sellerThumbBox };
            Witness1Filler  = new object[] { witness1Name, witness1Cast, witness1CNIC, witness1Phone1, witness1Phone2, witness1Address, witness1FatherName, witness1PictureBox, witness1SignatureBox, witness1ThumbBox };
            Witness2Filler  = new object[] { witness2Name, witness2Cast, witness2CNIC, witness2Phone1, witness2Phone2, witness2Address, witness2FatherName, witness2PictureBox, witness2SignatureBox, witness2ThumbBox };

            buyerThumbBox.Image    = buyerPictureBox.Image    = buyerSignatureBox.Image    = null;
            sellerThumbBox.Image   = sellerPictureBox.Image   = sellerSignatureBox.Image   = null;
            witness1ThumbBox.Image = witness1PictureBox.Image = witness1SignatureBox.Image = null;
            witness2ThumbBox.Image = witness2PictureBox.Image = witness2SignatureBox.Image = null;

            carTransactionsBindingNavigatorSaveItem.Enabled = false;

            this.MinimumSize = new Size(1335, 1000);

            this.FormClosed += (sender, e) =>
            {
                Utils.DBManager.Delete("DELETE FROM CarTransactions WHERE CarId is NULL");
            };
        }

        public CashSaleForm(int id): this()
        {
            isCarUpdate = true;
            isBuyerUpdate = true;
            isSellerUpdate = true;
            isWitness1Update = true;
            isWitness2Update = true;

            Car.Fetch(id);

            Buyer.Fetch(Car.BuyerId);
            Seller.Fetch(Car.SellerId);
            Witness1.Fetch(Car.Witness1Id);
            Witness2.Fetch(Car.Witness2Id);

            Models.Car.Fill(CarFiller, Car);
            Models.Person.Fill(BuyerFiller, Buyer);
            Models.Person.Fill(SellerFiller, Seller);
            Models.Person.Fill(Witness1Filler, Witness1);
            Models.Person.Fill(Witness2Filler, Witness2);

            var dataTable = new DataTable();
            Utils.DBManager.QueryAdapter("CarTransactions", "CarId = " + Car.Id).Fill(dataTable);

            carTransactionsDataGridView.DataSource = dataTable;
            carTransactionsBindingNavigatorSaveItem.Enabled = true;

        }

        #region autoComplete

        private void BuyerCNIC_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) AutoCompleteCNIC(buyerCNIC.Text, ref Buyer, BuyerFiller, ref isBuyerUpdate); Buyer.IsBuyer = true; }
        private void SellerCNIC_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter && sellerCNIC.Text != "     -       -") AutoCompleteCNIC(sellerCNIC.Text, ref Seller, SellerFiller, ref isSellerUpdate); Seller.IsSeller = true; }
        private void Witness1CNIC_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) AutoCompleteCNIC(witness1CNIC.Text, ref Witness1, Witness1Filler, ref isWitness1Update); Witness1.IsWitness = true; }
        private void Witness2CNIC_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) AutoCompleteCNIC(witness2CNIC.Text, ref Witness2, Witness2Filler, ref isWitness2Update); Witness2.IsWitness = true; }

        private void PBO_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) AutoCompleteCar("PBO", PBO.Text, ref Car, CarFiller, ref isCarUpdate); }
        private void Engine_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) AutoCompleteCar("EngineNo", engine.Text, ref Car, CarFiller, ref isCarUpdate); }
        private void Chassis_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) AutoCompleteCar("ChasisNo", chassis.Text, ref Car, CarFiller, ref isCarUpdate); }
        private void Registration_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) AutoCompleteCar("RegistrationNo", registration.Text, ref Car, CarFiller, ref isCarUpdate); }

        private void AutoCompleteCNIC(string cnic, ref Models.Person person, object[] filler, ref bool flag)
        { 
            flag = true;
            if (person.Fetch(cnic))
                Models.Person.Fill(filler, person);
        }

        private void AutoCompleteName(ref Models.Person person, object[] filler, ref bool flag)
        {
            flag = true;

            new Dialogs.SelectPersons().ShowDialog();

            if (Dialogs.SelectPersons.id != 0)
            {
                if (person.Fetch(Dialogs.SelectPersons.id))
                    Models.Person.Fill(filler, person);
            }            
        }

        private void AutoCompleteCar(string column, string value, ref Models.Car car, object[] filler, ref bool flag)
        {
            flag = true;
            if (car.FetchStock(column, value))
                Models.Car.Fill(filler, car);
        }

        #endregion

        private void Submit_Click(object sender, System.EventArgs e)
        {
            try
            {
                Car.Images.Clear();
                Models.Car.Fill(Car, CarFiller);

                Models.Person.Fill(Buyer, BuyerFiller);
                Console.WriteLine(Buyer);

                Car.CashSaleFlag = true;
                Car.CreditSaleFlag = false;

                if (isBuyerUpdate) Models.Person.Update(Buyer); else Models.Person.Insert(Buyer, true, false, false);


                if (isSellerUpdate)
                {
                    //Seller.Fetch(Seller.Id);
                    Models.Person.Fill(Seller, SellerFiller);
                    //Seller.IsSeller = true;
                    Models.Person.Update(Seller);
                }
                else
                {
                    Models.Person.Fill(Seller, SellerFiller);
                    Models.Person.Insert(Seller, false, true, false);
                }

                if (isWitness1Update)
                {
                    //Witness1.Fetch(Witness1.Id);
                    Models.Person.Fill(Witness1, Witness1Filler);
                    //Witness1.IsWitness = true;

                    Models.Person.Update(Witness1);
                }
                else
                {
                    Models.Person.Fill(Witness1, Witness1Filler);
                    Models.Person.Insert(Witness1, false, false, true);
                }

                if (isWitness2Update)
                {
                    //Witness2.Fetch(Witness2.Id);
                    Models.Person.Fill(Witness2, Witness2Filler);
                    //Witness2.IsWitness = true;

                    Models.Person.Update(Witness2);
                }
                else
                {
                    Models.Person.Fill(Witness2, Witness2Filler);
                    Models.Person.Insert(Witness2, false, false, true);
                }

                Car.BuyerId = Buyer.Id;
                Car.SellerId = Seller.Id;

                Car.Witness1Id = Witness1.Id;
                Car.Witness2Id = Witness2.Id;

                isSellerUpdate = isBuyerUpdate = isWitness1Update = isWitness2Update = true;

                if (isCarUpdate)
                {
                    Utils.DBManager.DeleteAllTransactions(Car.Id);

                    Car.CarTransactions.Clear();

                    foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                    {
                        Car.CarTransactions.Add(
                            new Models.Car.Transaction
                            {
                                Amount = row.Cells[1].Value,
                                Date = row.Cells[2].Value.ToString(),
                                IsRecieved = row.Cells[3].Value,
                                Note = row.Cells[4].Value.ToString(),
                            }
                        );
                    }
                    Models.Car.Update(Car);
                }

                else
                {
                    Car.CarTransactions.Clear();

                    foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                    {
                        Car.CarTransactions.Add(
                            new Models.Car.Transaction
                            {
                                Amount = row.Cells[1].Value,
                                Date = row.Cells[2].Value.ToString(),
                                IsRecieved = row.Cells[3].Value,
                                Note = row.Cells[4].Value.ToString(),
                            }
                        );
                    }
                    Models.Car.Insert(Car);
                }

                MessageBox.Show("Successfully Added Data");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Utils.DBManager.CloseConnection();
            }
        }


        #region imageControl
        private void BuyerPictureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(buyerPictureBox); }
        private void BuyerPictureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(buyerPictureBox); }
        private void BuyerSignatureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(buyerSignatureBox); }
        private void BuyerSignatureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(buyerSignatureBox); }
        private void SellerPictureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(sellerPictureBox); }
        private void SellerPictureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(sellerPictureBox); }
        private void SellerSignatureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(sellerSignatureBox); }
        private void SellerSignatureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(sellerSignatureBox); }
        private void Witness1PictureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(witness1PictureBox); }
        private void Witness1PictureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(witness1PictureBox); }
        private void Witness1SignatureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(witness1SignatureBox); }
        private void Witness1SignatureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(witness1SignatureBox); }
        private void Witness2PictureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(witness2PictureBox); }
        private void Witness2PictureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(witness2PictureBox); }
        private void Witness2SignatureImage_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithFilePicker(witness2SignatureBox); }
        private void Witness2SignatureCamera_Click(object sender, System.EventArgs e) { Utils.ImageHandling.AddImageWithCamera(witness2SignatureBox); }

        private void CarImage_Click(object sender, System.EventArgs e)
        {
            var fileDialog = Utils.ImageHandling.getImageFilePicker(true);

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                Utils.ImageHandling.InsertInList(fileDialog.FileNames, flowLayoutPanel1);
        }

        private void CarCamera_Click(object sender, System.EventArgs e)
        {
            camera.ShowDialog();
            Utils.ImageHandling.InsertInList(camera.images, flowLayoutPanel1);
            camera.images.Clear();
        }
        #endregion

        private void BuyerCaptureButton_Click(object sender, System.EventArgs e) { ThumbCapture(buyerCaptureButton, buyerThumbBox); }
        private void BuyerEnrolmentButton_Click(object sender, System.EventArgs e) { Utils.DigitalPersonaUtil._.StartEnrolement(Buyer); }
        private void BuyerIdentifyButton_Click(object sender, System.EventArgs e) { Identify(buyerIdentifyButton, buyerIdentifyLabel, Buyer); }
        private void BuyerIdentifyLabel_TextChanged(object sender, System.EventArgs e) { if (buyerIdentifyLabel.Text == "Person is Found") Models.Person.Fill(BuyerFiller, Buyer); isBuyerUpdate = true; Buyer.IsBuyer = true; }

        private void SellerCaptureButton_Click(object sender, System.EventArgs e) { ThumbCapture(sellerCaptureButton, sellerThumbBox); }
        private void SellerEnrolmentButton_Click(object sender, System.EventArgs e) { Utils.DigitalPersonaUtil._.StartEnrolement(Seller); }
        private void SellerIdentifyButton_Click(object sender, System.EventArgs e) { Identify(sellerIdentifyButton, sellerIdentifyLabel, Seller); }
        private void SellerIdentifyLabel_TextChanged(object sender, System.EventArgs e) { if (sellerIdentifyLabel.Text == "Person is Found") Models.Person.Fill(SellerFiller, Seller); isSellerUpdate = true; Seller.IsSeller = true; }

        private void Witness1CaptureButton_Click(object sender, System.EventArgs e) { ThumbCapture(witness1CaptureButton, witness1ThumbBox); }
        private void Witness1EnrolmentButton_Click(object sender, System.EventArgs e) { Utils.DigitalPersonaUtil._.StartEnrolement(Witness1); }
        private void Witness1IdentifyButton_Click(object sender, System.EventArgs e) { Identify(witness1IdentifyButton, witness1IdentifyLabel, Witness1); }
        private void Witness1IdentifyLabel_TextChanged(object sender, System.EventArgs e) { if (witness1IdentifyLabel.Text == "Person is Found") Models.Person.Fill(Witness1Filler, Witness1); isWitness1Update = true; Witness1.IsWitness = true; }


        private void Witness2CaptureButton_Click(object sender, System.EventArgs e) { ThumbCapture(witness2CaptureButton, witness2ThumbBox); }
        private void Witness2EnrolmentButton_Click(object sender, System.EventArgs e) { Utils.DigitalPersonaUtil._.StartEnrolement(Witness2); }
        private void Witness2IdentifyButton_Click(object sender, System.EventArgs e) { Identify(witness2IdentifyButton, witness2IdentifyLabel, Witness2); }
        private void Witness2IdentifyLabel_TextChanged(object sender, System.EventArgs e) { if (witness2IdentifyLabel.Text == "Person is Found") Models.Person.Fill(Witness2Filler, Witness2); isWitness2Update = true; Witness2.IsWitness = true; }

        private void ThumbCapture(Button button, PictureBox box)
        {
            if (button.Text == "Start")
            {
                button.Text = "Capture";
                Utils.DigitalPersonaUtil._.StartCapturing(box, this);
            }
            else
            {
                button.Text = "Start";
                Utils.DigitalPersonaUtil._.StopAllActivities();
            }
        }

        private void Identify(Button button, Label label, Models.Person person)
        {
            if (button.Text == "Identify")
            {
                if (Utils.DigitalPersonaUtil._.IsAvailable())
                {
                    button.Text = "Cancel";
                    Utils.DigitalPersonaUtil._.StartIdentificationAndFill(label, this, person, BuyerFiller);
                }
            }
            else
            {
                label.Text = "";
                button.Text = "Identify";

                Utils.DigitalPersonaUtil._.StopAllActivities();
            }
        }

        private void CarTotalAmmount_TextChanged(object sender, EventArgs e)
        {
            long num = 0;

            if (carTotalAmmount.Text != "")
            {
                try
                {
                    num = long.Parse(carTotalAmmount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Write Numbers Only");
                }
            }

            var res = NumberToWords(num);
            if (res != "Limit Increased") res += " { Half is " + (long)(num / 2) + "} ";

            label64.Text = res;
        }


        private static string NumberToWords(long number)
        {
            if (number > 999999999) return "Limit Increased";

            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        private void BuyerPictureBox_Click(object sender, EventArgs e) { ImageConfirmation(buyerPictureBox); }
        private void BuyerSignatureBox_Click(object sender, EventArgs e) { ImageConfirmation(buyerSignatureBox); }
        private void BuyerThumbBox_Click(object sender, EventArgs e) { ImageConfirmation(buyerThumbBox); }
        private void SellerPictureBox_Click(object sender, EventArgs e) { ImageConfirmation(sellerPictureBox); }
        private void SellerSignatureBox_Click(object sender, EventArgs e) { ImageConfirmation(sellerSignatureBox); }
        private void SellerThumbBox_Click(object sender, EventArgs e) { ImageConfirmation(sellerThumbBox); }
        private void Witness1PictureBox_Click(object sender, EventArgs e) { ImageConfirmation(witness1PictureBox); }
        private void Witness1SignatureBox_Click(object sender, EventArgs e) { ImageConfirmation(witness1SignatureBox); }
        private void Witness1ThumbBox_Click(object sender, EventArgs e) { ImageConfirmation(witness1ThumbBox); }
        private void Witness2PictureBox_Click(object sender, EventArgs e) { ImageConfirmation(witness2PictureBox); }
        private void Witness2SignatureBox_Click(object sender, EventArgs e) { ImageConfirmation(witness2SignatureBox); }
        private void Witness2ThumbBox_Click(object sender, EventArgs e) { ImageConfirmation(witness2ThumbBox); }
        private void ImageConfirmation(PictureBox box)
        {
            if (box.Image == null) return;

            var b = new Dialogs.ImageConfirmationDialog(box.Image);

            b.ShowDialog();

            if (!b.status) box.Image = null;
        }

        private void BuyerPhone1_TextChanged(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.S:
                    {
                        Submit.PerformClick();
                        return true;
                    }
                case Keys.Control | Keys.P:
                    {
                        if (carTransactionsDataGridView.Rows.Count < 2)
                        {
                            MessageBox.Show("Can Not Sale a Car without a credit plan");
                            return true;
                        }

                        Car.Images.Clear();
                        Models.Car.Fill(Car, CarFiller);

                        Models.Person.Fill(Buyer, BuyerFiller);
                        Console.WriteLine(Buyer);

                        Car.CashSaleFlag = true;
                        Car.CreditSaleFlag = false;

                        if (isBuyerUpdate) Models.Person.Update(Buyer); else Models.Person.Insert(Buyer, true, false, false);


                        if (isSellerUpdate)
                        {
                            //Seller.Fetch(Seller.Id);
                            Models.Person.Fill(Seller, SellerFiller);
                            //Seller.IsSeller = true;
                            Models.Person.Update(Seller);
                        }
                        else
                        {
                            Models.Person.Fill(Seller, SellerFiller);
                            Models.Person.Insert(Seller, false, true, false);
                        }

                        if (isWitness1Update)
                        {
                            //Witness1.Fetch(Witness1.Id);
                            Models.Person.Fill(Witness1, Witness1Filler);
                            //Witness1.IsWitness = true;

                            Models.Person.Update(Witness1);
                        }
                        else
                        {
                            Models.Person.Fill(Witness1, Witness1Filler);
                            Models.Person.Insert(Witness1, false, false, true);
                        }

                        if (isWitness2Update)
                        {
                            //Witness2.Fetch(Witness2.Id);
                            Models.Person.Fill(Witness2, Witness2Filler);
                            //Witness2.IsWitness = true;

                            Models.Person.Update(Witness2);
                        }
                        else
                        {
                            Models.Person.Fill(Witness2, Witness2Filler);
                            Models.Person.Insert(Witness2, false, false, true);
                        }

                        Car.BuyerId = Buyer.Id;
                        Car.SellerId = Seller.Id;

                        Car.Witness1Id = Witness1.Id;
                        Car.Witness2Id = Witness2.Id;

                        if (isCarUpdate)
                        {
                            Utils.DBManager.DeleteAllTransactions(Car.Id);

                            Car.CarTransactions.Clear();

                            foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                            {
                                Car.CarTransactions.Add(
                                    new Models.Car.Transaction
                                    {
                                        Amount = row.Cells[1].Value,
                                        Date = row.Cells[2].Value.ToString(),
                                        IsRecieved = row.Cells[3].Value,
                                        Note = row.Cells[4].Value.ToString(),
                                    }
                                );
                            }
                            Models.Car.Update(Car);
                        }

                        else
                        {
                            Car.CarTransactions.Clear();

                            foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                            {
                                Car.CarTransactions.Add(
                                    new Models.Car.Transaction
                                    {
                                        Amount = row.Cells[1].Value,
                                        Date = row.Cells[2].Value.ToString(),
                                        IsRecieved = row.Cells[3].Value,
                                        Note = row.Cells[4].Value.ToString(),
                                    }
                                );
                            }
                            Models.Car.Insert(Car);
                        }

                        var rep = new ReportView(Car.Id, false);

                        rep.FormClosed += (sender, e) =>
                        {
                            this.Close();
                        };

                        rep.ShowDialog();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
            {
                Utils.DBManager.Update("CarTransactions", (int)row.Cells[0].Value,
                    new string[] { "Amount", "Date", "IsRecieved", "Note" },
                    new SqlDbType[] { SqlDbType.Money, SqlDbType.DateTime, SqlDbType.Bit, SqlDbType.Text },
                    new object[] { row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value }
                );
            }

            if (isCarUpdate)
            {
                var command = new SqlCommand($"INSERT INTO CarTransactions(Amount, CarId) VALUES(0, {Car.Id})");
                Utils.DBManager.Insert(command);

                var dataTable = new DataTable();
                Utils.DBManager.QueryAdapter("CarTransactions", "CarId = " + Car.Id).Fill(dataTable);

                carTransactionsDataGridView.DataSource = dataTable;
            }
            else
            {
                Utils.DBManager.Insert(new SqlCommand($"INSERT INTO CarTransactions(Amount) VALUES(0)"));

                var dataTable = new DataTable();

                Utils.DBManager.QueryAdapter("CarTransactions", "CarId IS NULL").Fill(dataTable);
                carTransactionsDataGridView.DataSource = dataTable;
            }
        }

        private void CarTransactionsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (isCarUpdate)
            {
                this.Validate();

                for (int i = 0; i < toBeDeleted.Count; i++)
                {
                    Utils.DBManager.Delete("DELETE FROM CarTransactions WHERE id = " + toBeDeleted[i]);
                }

                foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                {
                    Utils.DBManager.Update("CarTransactions", (int)row.Cells[0].Value,
                        new string[] { "Amount", "Date", "IsRecieved", "Note", "CarId" },
                        new SqlDbType[] { SqlDbType.Money, SqlDbType.DateTime, SqlDbType.Bit, SqlDbType.Text, SqlDbType.Int },
                        new object[] { row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value, row.Cells[5].Value }
                    );
                }

                var dataTable = new DataTable();
                Utils.DBManager.QueryAdapter("CarTransactions", "CarId = " + Car.Id).Fill(dataTable);

                carTransactionsDataGridView.DataSource = dataTable;
            }
        }

        private void CashSaleForm_Load(object sender, EventArgs e)
        {
        }

        private void BindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (carTransactionsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a row to delete");
            } 
            else
            {
                foreach(DataGridViewRow row in carTransactionsDataGridView.SelectedRows)
                {
                    toBeDeleted.Add((int) row.Cells[0].Value);

                    carTransactionsDataGridView.Rows.Remove(row);
                }
            }
        }

        private void Serial_TextChanged(object sender, EventArgs e)
        {

        }

        private void Serial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) AutoCompleteCar("Sr", serial.Text, ref Car, CarFiller, ref isCarUpdate);
        }

        private void CashSaleForm_SizeChanged(object sender, EventArgs e)
        {
            panel18.Location = new Point(ClientSize.Width / 2 - panel18.Size.Width / 2, panel18.Location.Y);
        }

        private void BuyerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoCompleteName(ref Buyer, BuyerFiller, ref isBuyerUpdate); Buyer.IsBuyer = true;
            }
        }

        private void SellerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoCompleteName(ref Seller, SellerFiller, ref isSellerUpdate); Seller.IsSeller = true;
            }
        }

        private void Witness2Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoCompleteName(ref Witness1, Witness1Filler, ref isWitness1Update); Witness1.IsWitness = true;
            }
        }

        private void Witness1Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoCompleteName(ref Witness2, Witness2Filler, ref isWitness2Update); Witness2.IsWitness = true;
            }
        }
    }
}
