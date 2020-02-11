using System;
using PakMotors.utils;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PakMotors
{
    public partial class SellCarFrom : Form
    {
        private model.Car     Car       = new model.Car();
        private model.Buyer   Buyer     = new model.Buyer();
        private model.Seller  Seller    = new model.Seller();
        private model.Witness Witness1  = new model.Witness();
        private model.Witness Witness2  = new model.Witness();

        private object[] CarFiller;
        private object[] BuyerFiller;
        private object[] SellerFiller;
        private object[] Witness1Filler;
        private object[] Witness2Filler;

        private bool isUpdate = false;
        private bool isCarAutoFilled = false;
        private bool isBuyerAutoFilled = false;
        private bool isSellerAutoFilled = false;

        private dialogs.CameraDialog camera = new dialogs.CameraDialog();
        private DigitalPersonaUtil fingerPrint = new DigitalPersonaUtil();

        public SellCarFrom()
        {
            InitializeComponent();

            CarFiller = new object[]
            {
                PBOTextField,
                modelTextField,
                colorTextField,
                engineTextField,
                variantTextField,
                chassisTextField,
                checkBox1,
                invoiceDatePicker,
                recievedDatePicker,
                horsePowerTextField,
                registrationTextField,
                carImageList,

                carTotalAmmount,
                carNote,
                carNextPayableDate,
                carNextPayableAmmount,
            };
            BuyerFiller = new object[] { buyerName, buyerCast, buyerCNIC, buyerPhone1, buyerPhone2, buyerAddress, buyerFatherName, BuyerThumbBox, buyerImageList };
            SellerFiller = new object[] { sellerName, sellerCast, sellerCNIC, sellerPhone1, sellerPhone2, sellerAddress, sellerFatherName, SellerThumbBox, sellerImageList };
            Witness1Filler = new object[] { witness1Name, witness1Cast, witness1CNIC, witness1Phone1, witness1Phone2, witness1Address, witness1FatherName, Witness1ThumbBox, witness1ImageList };
            Witness2Filler = new object[] { witness2Name, witness2Cast, witness2CNIC, witness2Phone1, witness2Phone2, witness2Address, witness2FatherName, Witness2ThumbBox, witness2ImageList };
        }

        public SellCarFrom(int sr)
        {
            isUpdate = true;

            InitializeComponent();
            carAdvance.Visible = false;
            Car.Full_FillWith(sr);

            Buyer.FillWith(Car.BuyerID);
            Seller.FillWith(Car.SellerID);
            Witness1.FillWith(Car.Witness1ID);
            Witness2.FillWith(Car.Witness2ID);

            CarFiller = new object[]
            {
                PBOTextField,
                modelTextField,
                colorTextField,
                engineTextField,
                variantTextField,
                chassisTextField,
                checkBox1,
                invoiceDatePicker,
                recievedDatePicker,
                horsePowerTextField,
                registrationTextField,
                carImageList,

                carTotalAmmount,
                carNote,
                carNextPayableDate,
                carNextPayableAmmount,
            };
            BuyerFiller = new object[] { buyerName, buyerCast, buyerCNIC, buyerPhone1, buyerPhone2, buyerAddress, buyerFatherName, BuyerThumbBox, buyerImageList };
            SellerFiller = new object[] { sellerName, sellerCast, sellerCNIC, sellerPhone1, sellerPhone2, sellerAddress, sellerFatherName, SellerThumbBox, sellerImageList };
            Witness1Filler = new object[] { witness1Name, witness1Cast, witness1CNIC, witness1Phone1, witness1Phone2, witness1Address, witness1FatherName, Witness1ThumbBox, witness1ImageList };
            Witness2Filler = new object[] { witness2Name, witness2Cast, witness2CNIC, witness2Phone1, witness2Phone2, witness2Address, witness2FatherName, Witness2ThumbBox, witness2ImageList };

            Car.Full_Fill(CarFiller);
            Buyer.Fill(BuyerFiller);
            Seller.Fill(SellerFiller);
            Witness1.Fill(Witness1Filler);
            Witness2.Fill(Witness2Filler);
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
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            var res = NumberToWords(long.Parse(carAdvance.Text));

            if (res != "Limit Increased") res += " { Half is "+ long.Parse(carAdvance.Text) +"} ";

            label15.Text = res;
        }

        #region car
        private void carAddImage_Click(object sender, EventArgs e)
        {
            var fileDialog = utils.ImageHandling.getImageFilePicker();

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                utils.ImageHandling.InsertInList(fileDialog.FileNames, carImageList);
        }
        private void carOpenCamera_Click(object sender, EventArgs e)
        {
            camera.ShowDialog();
            utils.ImageHandling.InsertInList(camera.images, carImageList);
            camera.images.Clear();
        }
        #endregion

        #region buyer
        private void buyerAddImage_Click(object sender, EventArgs e)
        {
            var fileDialog = utils.ImageHandling.getImageFilePicker();

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                utils.ImageHandling.InsertInList(fileDialog.FileNames, buyerImageList);
        }
        private void buyerOpenCamera_Click(object sender, EventArgs e)
        {
            camera.ShowDialog();
            utils.ImageHandling.InsertInList(camera.images, buyerImageList);
            camera.images.Clear();
        }
        private void BuyerIdentifyButton_Click(object sender, EventArgs e)
        {
            fingerPrint.IdentifyPerson(BuyerIdentificationLabel, this, "Buyer");
        }
        private void BuyerFingerPrintButton_Click(object sender, EventArgs e)
        {
            if (BuyerFingerPrintButton.Text == "Start")
            {
                try
                {
                    fingerPrint.StartCapturingMode(BuyerThumbBox, this);
                    BuyerFingerPrintButton.Text = "Capture";
                }
                catch (Exception)
                {
                    MessageBox.Show("You haven't finished capturing the previous fingerprint", "Device is Busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                fingerPrint.StopAllActivities();
                BuyerFingerPrintButton.Text = "Start";
            }
        }
        #endregion

        #region seller
        private void sellerAddImage_Click(object sender, EventArgs e)
        {
            var fileDialog = utils.ImageHandling.getImageFilePicker();

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                utils.ImageHandling.InsertInList(fileDialog.FileNames, sellerImageList);
        }
        private void sellerOpenCamera_Click(object sender, EventArgs e)
        {
            camera.ShowDialog();
            utils.ImageHandling.InsertInList(camera.images, sellerImageList);
            camera.images.Clear();
        }
        private void SellerIdentifyImage_Click(object sender, EventArgs e)
        {
            fingerPrint.IdentifyPerson(SellerIdentificationLabel, this, "Seller");
        }
        private void SellerFingerPrintButton_Click(object sender, EventArgs e)
        {
            if (SellerFingerPrintButton.Text == "Start")
            {
                try
                {
                    fingerPrint.StartCapturingMode(SellerThumbBox, this);
                    SellerFingerPrintButton.Text = "Capture";
                }
                catch (Exception)
                {
                    MessageBox.Show("You haven't finished capturing the previous fingerprint", "Device is Busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                fingerPrint.StopAllActivities();
                SellerFingerPrintButton.Text = "Start";
            }
        }
        #endregion

        #region witness1
        private void witness1AddImage_Click(object sender, EventArgs e)
        {
            var fileDialog = utils.ImageHandling.getImageFilePicker();

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                utils.ImageHandling.InsertInList(fileDialog.FileNames, witness1ImageList);
        }
        private void witness1OpenCamera_Click(object sender, EventArgs e)
        {
            camera.ShowDialog();
            utils.ImageHandling.InsertInList(camera.images, witness1ImageList);
            camera.images.Clear();
        }
        private void Witness1FingerPrintButton_Click(object sender, EventArgs e)
        {
            if (Witness1FingerPrintButton.Text == "Start")
            {
                try
                {
                    fingerPrint.StartCapturingMode(Witness1ThumbBox, this);
                    Witness1FingerPrintButton.Text = "Capture";
                }
                catch (Exception)
                {
                    MessageBox.Show("You haven't finished capturing the previous fingerprint", "Device is Busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                fingerPrint.StopAllActivities();
                Witness1FingerPrintButton.Text = "Start";
            }
        }
        #endregion
        
        #region witness2
        private void witness2AddImage_Click(object sender, EventArgs e)
        {
            var fileDialog = utils.ImageHandling.getImageFilePicker();

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                utils.ImageHandling.InsertInList(fileDialog.FileNames, witness2ImageList);
        }
        private void witness2OpenCamera_Click(object sender, EventArgs e)
        {
            camera.ShowDialog();
            utils.ImageHandling.InsertInList(camera.images, witness2ImageList);
            camera.images.Clear();
        }
        private void Witness2FingerPrintButton_Click(object sender, EventArgs e)
        {
            if (Witness2FingerPrintButton.Text == "Start")
            {
                try
                {
                    fingerPrint.StartCapturingMode(Witness2ThumbBox, this);
                    Witness2FingerPrintButton.Text = "Capture";
                }
                catch (Exception)
                {
                    MessageBox.Show("You haven't finished capturing the previous fingerprint", "Device is Busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                fingerPrint.StopAllActivities();
                Witness2FingerPrintButton.Text = "Start";
            }
        }
        #endregion

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            // Extract IDs
            var carId       = Car.Sr;
            var buyerId     = Buyer.ID;
            var sellerId    = Seller.ID;
            var witness1Id  = Witness1.ID;
            var witness2Id  = Witness2.ID;

            // Assign empty fields
            Buyer       = new model.Buyer();
            Seller      = new model.Seller();
            Witness1    = new model.Witness();
            Witness2    = new model.Witness();

            // Fill Each object with new values
            Car.Full_FillWith(CarFiller);
            Buyer.FillWith(BuyerFiller);
            Seller.FillWith(SellerFiller);
            Witness1.FillWith(Witness1Filler);
            Witness2.FillWith(Witness2Filler);

            Car.Sr = carId;
            Buyer.ID = Car.BuyerID;
            Seller.ID = Car.SellerID;
            Witness1.ID = Car.Witness1ID;
            Witness2.ID = Car.Witness2ID;

            if (isUpdate)
            {
                Car.Update();
                Buyer.Update();
                Seller.Update();
                Witness1.Update();
                Witness2.Update();
            }
            else
            {
                if (isBuyerAutoFilled)
                {
                    Buyer.ID = buyerId;
                    Buyer.Update();
                }
                else Buyer.CreateNew();

                if (isSellerAutoFilled)
                {
                    Seller.ID = sellerId;
                    Seller.Update();
                }
                else Seller.CreateNew();

                Witness1.CreateNew();
                Witness2.CreateNew();

                Car.BuyerID = Buyer.ID;
                Car.SellerID = Seller.ID;
                Car.Witness1ID = Witness1.ID;
                Car.Witness2ID = Witness2.ID;

                if (isCarAutoFilled)
                {
                    Car.Sr = carId;
                    Car.Update();
                }
                else Car.CreateNew();

                var command = new SqlCommand("INSERT INTO Car_Transactions(car_id, transaction_date, transaction_amount) VALUES(" + Car.Sr + ", @a, " + carAdvance.Text + ")");
                command.Parameters.AddWithValue("@a", carSaleDate.Value);
            }
        }

        private void carEngine_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (carEngine.Text != "")
            //    {
            //        connection.Open();

            //        var dataAdapter = new SqlDataAdapter("SELECT * FROM Cars WHERE car_engine_no = '" + carEngine.Text + "'", connection);
            //        var data = new DataSet();

            //        dataAdapter.Fill(data);

            //        if (data.Tables[0].Rows.Count > 1)
            //        {
            //            this.carID = (int)data.Tables[0].Rows[0]["car_sr"];
            //            carName.Text = data.Tables[0].Rows[0]["car_name"].ToString();
            //            carModel.Text = data.Tables[0].Rows[0]["car_model"].ToString();
            //            carEngine.Text = data.Tables[0].Rows[0]["car_engine_no"].ToString();
            //            carChasis.Text = data.Tables[0].Rows[0]["car_chasis_no"].ToString();
            //            carRegistration.Text = data.Tables[0].Rows[0]["car_registration_no"].ToString();
            //            carHorsePower.Text = data.Tables[0].Rows[0]["car_horse_power"].ToString();
            //            carColor.Text = data.Tables[0].Rows[0]["car_color"].ToString();
            //        }

            //        connection.Close();
            //    }
            //}
        }

        private void carChasis_KeyDown(object sender, KeyEventArgs e)
        {
            //if (carChasis.Text != "")
            //{
            //    connection.Open();

            //    var dataAdapter = new SqlDataAdapter("SELECT * FROM Cars WHERE car_chasis_no = '" + carChasis.Text + "'", connection);
            //    var data = new DataSet();

            //    dataAdapter.Fill(data);

            //    if (data.Tables[0].Rows.Count > 1)
            //    {
            //        this.carID = (int)data.Tables[0].Rows[0]["car_sr"];
            //        carName.Text = data.Tables[0].Rows[0]["car_name"].ToString();
            //        carModel.Text = data.Tables[0].Rows[0]["car_model"].ToString();
            //        carEngine.Text = data.Tables[0].Rows[0]["car_engine_no"].ToString();
            //        carChasis.Text = data.Tables[0].Rows[0]["car_chasis_no"].ToString();
            //        carRegistration.Text = data.Tables[0].Rows[0]["car_registration_no"].ToString();
            //        carHorsePower.Text = data.Tables[0].Rows[0]["car_horse_power"].ToString();
            //        carColor.Text = data.Tables[0].Rows[0]["car_color"].ToString();
            //    }

            //    connection.Close();
            //}
        }

        private void carRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            //if (carRegistration.Text != "")
            //{
            //    connection.Open();

            //    var dataAdapter = new SqlDataAdapter("SELECT * FROM Cars WHERE car_registration_no = '" + carRegistration.Text + "'", connection);
            //    var data = new DataSet();

            //    dataAdapter.Fill(data);

            //    if (data.Tables[0].Rows.Count > 1)
            //    {
            //        this.carID = (int)data.Tables[0].Rows[0]["car_sr"];
            //        carName.Text = data.Tables[0].Rows[0]["car_name"].ToString();
            //        carModel.Text = data.Tables[0].Rows[0]["car_model"].ToString();
            //        carEngine.Text = data.Tables[0].Rows[0]["car_engine_no"].ToString();
            //        carChasis.Text = data.Tables[0].Rows[0]["car_chasis_no"].ToString();
            //        carRegistration.Text = data.Tables[0].Rows[0]["car_registration_no"].ToString();
            //        carHorsePower.Text = data.Tables[0].Rows[0]["car_horse_power"].ToString();
            //        carColor.Text = data.Tables[0].Rows[0]["car_color"].ToString();
            //    }

            //    connection.Close();
            //}
        }


        #region autoFilling
        private void BuyerIdentificationLabel_TextChanged(object sender, EventArgs e)
        {
            if (BuyerIdentificationLabel.Text.Length == 15 || BuyerIdentificationLabel.Text.Length == 13)
            {
                var temp = model.Buyer.FindByCNIC(BuyerIdentificationLabel.Text);

                if (temp.Validate())
                {
                    temp.Fill(BuyerFiller);
                    Buyer.ID = temp.ID;
                    isBuyerAutoFilled = true;
                }

                BuyerIdentificationLabel.Text = "";
            }
        }

        private void buyerCNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var format = model.Person.FormatCNIC(buyerCNIC.Text);
                var temp = model.Buyer.FindByCNIC(format);

                if (temp.Validate())
                {
                    temp.Fill(BuyerFiller);
                    Buyer.ID = temp.ID;
                    isBuyerAutoFilled = true;
                }
            }
        }

        private void sellerCNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var format = model.Person.FormatCNIC(sellerCNIC.Text);
                var temp = model.Seller.FindByCNIC(format);

                if (temp.Validate())
                {
                    temp.Fill(SellerFiller);
                    Seller.ID = temp.ID;
                    isSellerAutoFilled = true;
                }
            }
        }

        private void SellerIdentificationLabel_TextChanged(object sender, EventArgs e)
        {
            if (SellerIdentificationLabel.Text.Length == 15 || SellerIdentificationLabel.Text.Length == 13)
            {
                var temp = model.Buyer.FindByCNIC(SellerIdentificationLabel.Text);

                if (temp.Validate())
                {
                    temp.Fill(BuyerFiller);
                    Seller.ID = temp.ID;
                    isSellerAutoFilled = true;
                }

                SellerIdentificationLabel.Text = "";
            }
        }
        #endregion

        private void car_TransactionsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.car_TransactionsBindingSource.EndEdit();


            foreach (DataGridViewRow row in this.car_TransactionsDataGridView.Rows)
            {
                Console.WriteLine(row.Cells[0].Value + ", " + row.Cells[1].Value + ", " + row.Cells[2].Value);

                var command = new SqlCommand("UPDATE Car_Transactions SET transaction_date = @a, transaction_amount = " + row.Cells[2].Value.ToString() + " WHERE id = " + row.Cells[3].Value.ToString());
                command.Parameters.AddWithValue("@a", DateTime.Parse(row.Cells[1].Value.ToString()));

                utils.DBManager.Update(command);
            }
            this.car_TransactionsTableAdapter.Fill(this.pakMotorsDataSet.Car_Transactions);
        }

        private void SellCarFrom_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.Car_Transactions' table. You can move, or remove it, as needed.
            this.car_TransactionsTableAdapter.Fill(this.pakMotorsDataSet.Car_Transactions);
        }

        private void create_new_transaction_Click(object sender, EventArgs e)
        {
            // Insert New
            if (isUpdate)
            {
                var command = new SqlCommand("INSERT INTO Car_Transactions(car_id, transaction_date, transaction_amount) VALUES(" + Car.Sr + ", @a, 0)");
                command.Parameters.AddWithValue("@a", carNextPayableDate.Value);

                carNextPayableDate.Value = carNextPayableDate.Value.AddDays(30);

                utils.DBManager.Insert(command);

                this.car_TransactionsTableAdapter.Fill(this.pakMotorsDataSet.Car_Transactions);
            }
            else
            {
                MessageBox.Show("This cannot be filled when you are creating new car");
            }
        }

        private void carTotalAmmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void witness1CNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var format = model.Person.FormatCNIC(witness1CNIC.Text);
                var temp = model.Seller.FindByCNIC(format);

                if (temp.Validate())
                {
                    temp.Fill(Witness1Filler);
                    Witness1.ID = temp.ID;
                    isSellerAutoFilled = true;
                }
            }
        }

        private void witness2CNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var format = model.Person.FormatCNIC(witness2CNIC.Text);
                var temp = model.Seller.FindByCNIC(format);

                if (temp.Validate())
                {
                    temp.Fill(Witness2Filler);
                    Witness2.ID = temp.ID;
                    isSellerAutoFilled = true;
                }
            }
        }
    }
}
