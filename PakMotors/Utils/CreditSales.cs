using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PakMotors.Utils
{
    public partial class CreditSales : Form
    {
        private object Temp;

        public CreditSales()
        {
            InitializeComponent();
            comboBox2.SelectedIndex = 0;
            radioButton2.Select();

            Temp = creditSalesDataGridView.DataSource;
            this.WindowState = FormWindowState.Maximized;

            this.FormClosed += (sender, e) => Utils.DigitalPersonaUtil._.StopAllActivities();
        }

        private void CreditSales_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.CreditSales1' table. You can move, or remove it, as needed.
            fill();
        }

        private void fill()
        {
            var data = new DataTable();

            Utils.DBManager.QueryAdapter("SELECT * FROM CreditSales WHERE flg " + (radioButton2.Checked? ">": "=") + " 0 ORDER BY SaleDate").Fill(data);

            creditSalesDataGridView.DataSource = data;
        }

        private void CreditSales1DataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new Dialogs.NewCreditSaleForm((int)creditSalesDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
            fill();
        }

        private void Identify_Click(object sender, EventArgs e)
        {
            Utils.DigitalPersonaUtil._.StopAllActivities();

            if (Identify.Text == "Identify")
            {
                if (Utils.DigitalPersonaUtil._.IsAvailable())
                {
                    Identify.Text = "Cancel";
                    Utils.DigitalPersonaUtil._.StartIdentificationAndFill1(IdentifyLabel, this, creditSalesDataGridView, label4);
                }
            }
            else
            {
                IdentifyLabel.Text = "";
                Identify.Text = "Identify";

                Utils.DigitalPersonaUtil._.StopAllActivities();
            }
        }

        private void IdentifyLabel_TextChanged(object sender, EventArgs e)
        {
            if (IdentifyLabel.Text == "Person is Found")
            {
                var dataTable = new DataTable();

                int id = int.Parse(label4.Text);
                Utils.DBManager.QueryAdapter($"SELECT * FROM CreditSales WHERE i1 = {id} OR i2 = {id} OR i3 = {id} OR i4 = {id}").Fill(dataTable);
                creditSalesDataGridView.DataSource = dataTable;
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            if (comboBox2.SelectedIndex == 0)
            {
                comboBox1.Items.Add("Sr #");
                comboBox1.Items.Add("Variant");
                comboBox1.Items.Add("Model");
                comboBox1.Items.Add("SaleDate");
                comboBox1.Items.Add("EngineNo");
                comboBox1.Items.Add("ChassisNo");
                comboBox1.Items.Add("RegistrationNo");
            }

            else
            {
                comboBox1.Items.Add("Name");
                comboBox1.Items.Add("CNIC");
            }
            comboBox1.SelectedIndex = 0;
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                fill();
            }
            else
            {
                var item = comboBox1.SelectedItem.ToString();

                string query;
                if (comboBox2.SelectedIndex == 0 && comboBox1.SelectedIndex == 0)
                    query = $"SELECT * FROM CreditSales WHERE [Sr #] Like '%{searchBox.Text}%'";
                else if (comboBox2.SelectedIndex == 0)
                    query = $"SELECT * FROM CreditSales WHERE {item} Like '%" + searchBox.Text + "%'";
                else
                    query = $"SELECT * FROM CreditSales WHERE \"{comboBox2.SelectedItem.ToString()}'s {item}\" Like '%" + searchBox.Text + "%'";

                var dataAdapter = Utils.DBManager.QueryAdapter(query);
                var table = new DataTable();

                dataAdapter.Fill(table);

                this.creditSalesDataGridView.DataSource = table;
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            if (creditSalesDataGridView.SelectedRows.Count > 0) new ReportView((int)creditSalesDataGridView.SelectedRows[0].Cells[0].Value, true).ShowDialog();
        }


        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.R:
                    {
                        comboBox2.SelectedIndex = 0;
                        comboBox1.SelectedIndex = 0;
                        searchBox.Text = "";
                        searchBox.Visible = true;
                        dateTimePicker1.Visible = false;

                        fill();
                        return true;
                    }

                case Keys.Control | Keys.P:
                    {
                        button2.PerformClick();
                        return true;
                    }
                case Keys.Control | Keys.F:
                    {
                        searchBox.Focus();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new Dialogs.NewCreditSaleForm().ShowDialog();
            fill();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (creditSalesDataGridView.SelectedRows.Count > 0)
            {
                var res = MessageBox.Show("Do You Want to cancel this Deal? This action is not reversable.", "Warning", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    Utils.DBManager.Update(new SqlCommand("UPDATE Cars SET CashSaleFlag = 0, TotalAmount = 0, CreditSaleFlag = 0, BuyerId = NULL, SellerId = NULL, Witness1Id = NULL, Witness2Id = NULL WHERE Id = " + (int)creditSalesDataGridView.SelectedRows[0].Cells[0].Value));
                    Utils.DBManager.Update(new SqlCommand("DELETE FROM CarTransactions WHERE CarId = " + (int)creditSalesDataGridView.SelectedRows[0].Cells[0].Value));
                    fill();
                }
            }
        }


        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var date = dateTimePicker1.Value;

            string query = $"SELECT * FROM CreditSales WHERE SaleDate = '{date.Year}/{date.Month}/{date.Day}'";

            var dataAdapter = Utils.DBManager.QueryAdapter(query);
            var table = new DataTable();

            dataAdapter.Fill(table);

            this.creditSalesDataGridView.DataSource = table;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((string)comboBox1.SelectedItem) == "SaleDate")
            {
                searchBox.Visible = false;
                dateTimePicker1.Visible = true;
            }
            else
            {
                searchBox.Text = "";
                searchBox.Visible = true;
                dateTimePicker1.Visible = false;

                fill();
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
