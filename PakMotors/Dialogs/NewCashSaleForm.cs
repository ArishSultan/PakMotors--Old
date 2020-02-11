using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PakMotors.Dialogs
{
    public partial class NewCashSaleForm : Form
    {
        long sum = 0;
        Models.Car _Car = null;
        Models.Person _Buyer = null;
        Models.Person _Seller = null;
        Models.Person _BuyerWitness = null;
        Models.Person _SellerWitness = null;
        Models.Person _Witness = null;

        Models.Person _PurchasedFrom = null;

        public NewCashSaleForm()
        {
            InitializeComponent();
            this.carTransactionsDataGridView.DataSource = new DataTable();
        }

        public NewCashSaleForm(int id) : this()
        {
            this._Car = new Models.Car();
            this._Car.CashSaleFlag = true;
            this._Car.CreditSaleFlag = false;

            _Car.Fetch(id);

            amount.Text = _Car.TotalAmount.ToString();
            textBox3.Text = _Car.Note;
            Car.Text = _Car.Name;
            try
            {
                if (_Car.CarTransactions.Count > 0) carSaleDate.Value = (DateTime)_Car.CarTransactions[0].Date;
            }
            catch(Exception) { }

            if (_Car.SellerId != 0)
            {
                this._Seller = new Models.Person();
                _Seller.Fetch(_Car.SellerId);
                Seller.Text = _Seller.Name;
            }
            else _Car.SellerId = 0;

            if (_Car.BuyerId != 0)
            {
                this._Buyer = new Models.Person();
                _Buyer.Fetch(_Car.BuyerId);
                Buyer.Text = _Buyer.Name;
            }
            else _Car.BuyerId = 0;

            if (_Car.Witness1Id != 0)
            {
                this._BuyerWitness = new Models.Person();
                _BuyerWitness.Fetch(_Car.Witness1Id);
                BuyerWitness.Text = _BuyerWitness.Name;
            }
            else _Car.Witness1Id = 0;

            if (_Car.Witness2Id != 0)
            {
                this._SellerWitness = new Models.Person();
                _SellerWitness.Fetch(_Car.Witness2Id);
                SellerWitness.Text = _SellerWitness.Name;
            }
            else _Car.Witness2Id = 0;

            if (_Car.PurchasedFrom != 0)
            {
                this._PurchasedFrom = new Models.Person();
                _PurchasedFrom.Fetch(_Car.PurchasedFrom);
                PurchasedFrom.Text = _PurchasedFrom.Name;
            }
            else _Car.PurchasedFrom = 0;

            if (_Car.PurchasedFromWitness != 0)
            {
                this._Witness = new Models.Person();
                _Witness.Fetch(_Car.PurchasedFromWitness);
                Witness.Text = _Witness.Name;
            }
            else _Car.PurchasedFromWitness = 0;

            var dataTable = new DataTable();
            Utils.DBManager.QueryAdapter("CarTransactions", "CarId = " + _Car.Id).Fill(dataTable);

            carTransactionsDataGridView.DataSource = dataTable;

            sum = 0;

            foreach (DataGridViewRow row in this.carTransactionsDataGridView.Rows)
            {
                sum += (long)row.Cells[1].Value;
            }

            this.label11.Text = "Sum: " + sum;
        }

        #region Buyer
        private void BuyerAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();
            if (Dialogs.CreatePerson.GetRecentPerson() != null)
            {
                _Buyer = Dialogs.CreatePerson.GetRecentPerson();

                Buyer.Text = _Buyer.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        private void BuyerSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();
            Utils.DigitalPersonaUtil._.StopAllActivities();

            if (Views.Persons.RecentPerson != null)
            {
                _Buyer = Views.Persons.RecentPerson;


                Buyer.Text = _Buyer.Name;
                Views.Persons.RecentPerson = null;
            }
        }
        private void BuyerClear_Click(object sender, EventArgs e)
        {
            _Buyer = null;
            Buyer.Text = "";
        }
        private void BuyerView_Click(object sender, EventArgs e)
        {
            if (_Buyer == null || _Buyer.Id == 0)
            {
                MessageBox.Show("You have selected no Buyer");
                return;
            }

            new Dialogs.CreatePerson(_Buyer.Id).ShowDialog();

            if (Dialogs.CreatePerson.RecentPerson != null) _Buyer = Dialogs.CreatePerson.RecentPerson;

            if (_Buyer != null) {
                Buyer.Text = _Buyer.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        #endregion

        #region Seller
        private void SellerAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();

            if (Dialogs.CreatePerson.GetRecentPerson() != null)
            {
                _Seller = Dialogs.CreatePerson.GetRecentPerson();

                Seller.Text = _Seller.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        private void SellerSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();
            Utils.DigitalPersonaUtil._.StopAllActivities();

            if (Views.Persons.RecentPerson != null)
            {
                _Seller = Views.Persons.RecentPerson;


                Seller.Text = _Seller.Name;
                Views.Persons.RecentPerson = null;
            }
        }
        private void SellerClear_Click(object sender, EventArgs e)
        {
            _Seller = null;
            Seller.Text = "";
        }
        private void SellerView_Click(object sender, EventArgs e)
        {
            if (_Seller == null || _Seller.Id == 0)
            {
                MessageBox.Show("You have selected no Seller");
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

        #region BuyerWitness
        private void BuyerWitnessAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();
            if (Dialogs.CreatePerson.GetRecentPerson() != null)
            {
                _BuyerWitness = Dialogs.CreatePerson.GetRecentPerson();

                BuyerWitness.Text = _BuyerWitness.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        private void BuyerWitnessSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();
            Utils.DigitalPersonaUtil._.StopAllActivities();

            if (Views.Persons.RecentPerson != null)
            {
                _BuyerWitness = Views.Persons.RecentPerson;

                BuyerWitness.Text = _BuyerWitness.Name;
                Views.Persons.RecentPerson = null;
            }
        }
        private void BuyerWitnessClear_Click(object sender, EventArgs e)
        {
            _BuyerWitness = null;
            BuyerWitness.Text = "";
        }
        private void BuyerWitnessView_Click(object sender, EventArgs e)
        {
            if (_BuyerWitness == null || _BuyerWitness.Id == 0)
            {
                MessageBox.Show("You have selected no Witness");
                return;
            }

            if (_BuyerWitness != null)
            {
                new Dialogs.CreatePerson(_BuyerWitness.Id).ShowDialog();

                if (Dialogs.CreatePerson.RecentPerson != null) _BuyerWitness = Dialogs.CreatePerson.RecentPerson;
                if (_BuyerWitness != null)
                {
                    BuyerWitness.Text = _BuyerWitness.Name;
                    Dialogs.CreatePerson.RecentPerson = null;
                }
            }
        }
        #endregion

        #region SellerWitness
        private void SellerWitnessAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();
            if (Dialogs.CreatePerson.GetRecentPerson() != null)
            {
                _SellerWitness = Views.Persons.RecentPerson;

                SellerWitness.Text = _SellerWitness.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
        private void SellerWitnessSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();
            Utils.DigitalPersonaUtil._.StopAllActivities();

            if (Views.Persons.RecentPerson != null)
            {
                _SellerWitness = Views.Persons.RecentPerson;

                SellerWitness.Text = _SellerWitness.Name;
                Views.Persons.RecentPerson = null;
            }
        }
        private void SellerWitnessClear_Click(object sender, EventArgs e)
        {
            _SellerWitness = null;
            SellerWitness.Text = "";
        }
        private void SellerWitnessView_Click(object sender, EventArgs e)
        {
            if (_SellerWitness == null || _SellerWitness.Id == 0)
            {
                MessageBox.Show("You have selected no Witness");
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

        private void CarTransactionsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.carTransactionsBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void CarAdd_Click(object sender, EventArgs e)
        {
            new dialogs.CreateCar().ShowDialog();
            _Car = dialogs.CreateCar.RecentCar;

            if (_Car != null)
            {
                Car.Text = _Car.Name;

                if (_Car.PurchasedFrom != 0)
                {
                    this._PurchasedFrom = new Models.Person();
                    _PurchasedFrom.Fetch(_Car.PurchasedFrom);
                    PurchasedFrom.Text = _PurchasedFrom.Name;
                }

                if (_Car.PurchasedFromWitness != 0)
                {
                    this._Witness = new Models.Person();
                    _Witness.Fetch(_Car.PurchasedFromWitness);
                    Witness.Text = _Witness.Name;
                }

                dialogs.CreateCar.RecentCar = null;
            }
        }

        private void CarSearch_Click(object sender, EventArgs e)
        {
            new Views.Cars(true).ShowDialog();

            if (Views.Cars.RecentCar != null)
            {
                _Car = Views.Cars.RecentCar;

                Car.Text = _Car.Name;

                if (_Car.PurchasedFrom != 0)
                {
                    this._PurchasedFrom = new Models.Person();
                    _PurchasedFrom.Fetch(_Car.PurchasedFrom);
                    PurchasedFrom.Text = _PurchasedFrom.Name;
                }

                if (_Car.PurchasedFromWitness != 0)
                {
                    this._Witness = new Models.Person();
                    _Witness.Fetch(_Car.PurchasedFromWitness);
                    Witness.Text = _Witness.Name;
                }

                Views.Cars.RecentCar = null;
            }
        }

        private void CarClear_Click(object sender, EventArgs e)
        {
            Car.Text = "";
            _Car = null;
        }

        private void CarView_Click(object sender, EventArgs e)
        {
            if (_Car != null)
            {
                new dialogs.CreateCar(_Car.Id).ShowDialog();
                _Car.Fetch(_Car.Id);

                if (_Car != null)
                {
                    Car.Text = _Car.Name;
                    dialogs.CreateCar.RecentCar = null;
                }
            }
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e) { HandleSubmission(); }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (_Car != null)
            {
                sum = 0;
                Utils.DBManager.DeleteAllTransactions(_Car.Id);
                _Car.CarTransactions.Clear();

                foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                {
                    sum += (long)row.Cells[1].Value;
                    _Car.CarTransactions.Add(
                        new Models.Car.Transaction
                        {
                            Date = row.Cells[0].Value.ToString(),
                            Amount = row.Cells[1].Value,
                            IsRecieved = row.Cells[2].Value,
                            Note = row.Cells[3].Value.ToString(),
                        }
                    );
                }

                this.label11.Text = "Sum: " + sum;
                Models.Car.Update(_Car);

                var command = new SqlCommand($"INSERT INTO CarTransactions(Amount, Date, CarId) VALUES(0, '{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}', {_Car.Id})");
                Utils.DBManager.Insert(command);

                var dataTable = new DataTable();
                Utils.DBManager.QueryAdapter("CarTransactions", "CarId = " + _Car.Id).Fill(dataTable);

                carTransactionsDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("You haven't selected a Car.");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (carTransactionsDataGridView.SelectedRows.Count > 0)
            {
                Utils.DBManager.Delete($"DELETE FROM CarTransactions WHERE Id = {carTransactionsDataGridView.SelectedRows[0].Cells[5].Value}");

                var dataTable = new DataTable();
                Utils.DBManager.QueryAdapter("CarTransactions", "CarId = " + _Car.Id).Fill(dataTable);

                carTransactionsDataGridView.DataSource = dataTable;

                sum = 0;
                foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                {
                    sum += (long)row.Cells[1].Value;
                }

                this.label11.Text = "Sum: " + sum;
            }
            else
            {
                MessageBox.Show("No Row was Selected");
            }
        }

        private void Amount_TextChanged(object sender, EventArgs e)
        {
            long num = 0;

            if (amount.Text != "")
            {
                try
                {
                    num = long.Parse(amount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Write Numbers Only");
                }
            }

            var res = NumberToWords(num);
            if (res != "Limit Increased") res += " { Half is " + (long)(num / 2) + "} ";

            amountWords.Text = res;
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

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.S:
                    {
                        HandleSubmission();
                        return true;
                    }
            }
            return false;
        }

        private void HandleSubmission()
        {
            if (_Car == null && _Buyer == null)
            {
                MessageBox.Show("Please Provide Car and Buyer");
                return;
            }
            try
            {
                _Car.CashSaleFlag = true;
                _Car.CreditSaleFlag = false;

                _Car.BuyerId = _Buyer.Id;
                _Car.SellerId = _Seller != null? _Seller.Id: 0;
                _Car.Witness1Id = _BuyerWitness != null? _BuyerWitness.Id: 0;
                _Car.Witness2Id = _SellerWitness != null? _SellerWitness.Id: 0;
                _Car.PurchasedFrom = _PurchasedFrom != null? _PurchasedFrom.Id: 0;

                if (_Seller != null)
                {
                    _Car.Buyer = _Seller.Name;
                }

                try
                {
                    _Car.TotalAmount = long.Parse(amount.Text);
                    _Car.Note = textBox3.Text;
                }
                catch (Exception)
                {
                    _Car.TotalAmount = 0;
                }

                Utils.DBManager.DeleteAllTransactions(_Car.Id);
                _Car.CarTransactions.Clear();

                foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
                {
                    _Car.CarTransactions.Add(
                        new Models.Car.Transaction
                        {
                            Date        = row.Cells[0].Value.ToString(),
                            Amount      = row.Cells[1].Value,
                            IsRecieved  = row.Cells[2].Value,
                            Note        = row.Cells[3].Value.ToString(),
                        }
                    );
                }
                Models.Car.Update(_Car);

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

        private void Button3_Click(object sender, EventArgs e)
        {
            if (_Car == null)
            {
                MessageBox.Show("Select a Car First");
                return;
            }

            sum = 0;
            Utils.DBManager.DeleteAllTransactions(_Car.Id);
            _Car.CarTransactions.Clear();

            foreach (DataGridViewRow row in carTransactionsDataGridView.Rows)
            {
                sum += (long) row.Cells[1].Value;

                _Car.CarTransactions.Add(
                    new Models.Car.Transaction
                    {
                        Date = row.Cells[0].Value.ToString(),
                        Amount = row.Cells[1].Value,
                        IsRecieved = row.Cells[2].Value,
                        Note = row.Cells[3].Value.ToString(),
                    }
                );
            }

            this.label11.Text = "Sum: " + sum;
            Models.Car.Update(_Car);
        }

        private void PurchasedFromAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();

            if (Dialogs.CreatePerson.GetRecentPerson() != null)
            {
                _PurchasedFrom = Dialogs.CreatePerson.RecentPerson;

                PurchasedFrom.Text = _PurchasedFrom.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }

        private void PurchasedFromSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();
            Utils.DigitalPersonaUtil._.StopAllActivities();

            if (Views.Persons.RecentPerson != null)
            {
                _PurchasedFrom = Views.Persons.RecentPerson;

                PurchasedFrom.Text = _PurchasedFrom.Name;
                Views.Persons.RecentPerson = null;
            }
        }

        private void PurchasedFromCancel_Click(object sender, EventArgs e)
        {
            _PurchasedFrom = null;
            PurchasedFrom.Text = "";
        }

        private void PurchasedFromView_Click(object sender, EventArgs e)
        {
            if (_PurchasedFrom == null || _PurchasedFrom.Id == 0)
            {
                MessageBox.Show("You have selected no Seller");
                return;
            }

            new Dialogs.CreatePerson(_PurchasedFrom.Id).ShowDialog();
            if (Dialogs.CreatePerson.RecentPerson != null) _PurchasedFrom = Dialogs.CreatePerson.GetRecentPerson();

            if (_Seller != null)
            {
                PurchasedFrom.Text = _PurchasedFrom.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }

        private void WitnessAdd_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();

            if (Dialogs.CreatePerson.GetRecentPerson() != null)
            {
                _Witness = Dialogs.CreatePerson.RecentPerson;

                Witness.Text = _Witness.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }

        private void WitnessSearch_Click(object sender, EventArgs e)
        {
            new Views.Persons(true).ShowDialog();
            Utils.DigitalPersonaUtil._.StopAllActivities();

            if (Views.Persons.RecentPerson != null)
            {
                _Witness = Views.Persons.RecentPerson;

                Witness.Text = _Witness.Name;
                Views.Persons.RecentPerson = null;
            }
        }

        private void WitnessClear_Click(object sender, EventArgs e)
        {
            _Witness = null;
            Witness.Text = "";
        }

        private void WitnessView_Click(object sender, EventArgs e)
        {
            if (_Witness == null || _Witness.Id == 0)
            {
                MessageBox.Show("You have selected no Seller");
                return;
            }

            new Dialogs.CreatePerson(_Witness.Id).ShowDialog();
            if (Dialogs.CreatePerson.RecentPerson != null) _Witness = Dialogs.CreatePerson.GetRecentPerson();

            if (_Witness != null)
            {
                Witness.Text = _Witness.Name;
                Dialogs.CreatePerson.RecentPerson = null;
            }
        }
    }
}
