using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMotors.Utils
{
    public partial class CashSales : Form
    {
        public CashSales()
        {
            InitializeComponent();
            radioButton2.Select();


            comboBox2.SelectedIndex = 0;

            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += (sender, e) => { Utils.DigitalPersonaUtil._.StopAllActivities(); };
        }

        private void CashSales_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDatabaseDataSet.CashSales' table. You can move, or remove it, as needed.
            fill();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (cashSalesDataGridView.SelectedRows.Count > 0) new ReportView((int)cashSalesDataGridView.SelectedRows[0].Cells[0].Value, false).ShowDialog();
        }

        private void Identify_Click(object sender, EventArgs e)
        {
            if (Identify.Text == "Identify")
            {
                Utils.DigitalPersonaUtil._.StopAllActivities();

                if (Utils.DigitalPersonaUtil._.IsAvailable())
                {
                    Identify.Text = "Cancel";
                    Utils.DigitalPersonaUtil._.StartIdentificationAndFill1(IdentifyLabel, this, cashSalesDataGridView, label4);
                }
            }
            else
            {
                IdentifyLabel.Text = "";
                Identify.Text = "Identify";

                Utils.DigitalPersonaUtil._.StopAllActivities();
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
                    query = $"SELECT * FROM CashSales WHERE [Sr #] Like '%{searchBox.Text}%'";
                else if (comboBox2.SelectedIndex == 0)
                    query = $"SELECT * FROM CashSales WHERE {item} Like '%{searchBox.Text}%'";
                else
                    query = $"SELECT * FROM CashSales WHERE \"{comboBox2.SelectedItem.ToString()}'s {item}\" Like '%" + searchBox.Text + "%'";

                var dataAdapter = Utils.DBManager.QueryAdapter(query);
                var table = new DataTable();

                dataAdapter.Fill(table);

                this.cashSalesDataGridView.DataSource = table;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new Dialogs.NewCashSaleForm().ShowDialog();
            fill();
        }

        private void CashSalesDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new Dialogs.NewCashSaleForm((int)cashSalesDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
            fill();
        }

        private void IdentifyLabel_TextChanged(object sender, EventArgs e)
        {
            if (IdentifyLabel.Text == "Person is Found")
            {
                var dataTable = new DataTable();

                int id = int.Parse(label4.Text);
                Utils.DBManager.QueryAdapter($"SELECT * FROM CashSales WHERE i1 = {id} OR i2 = {id} OR i3 = {id} OR i4 = {id}").Fill(dataTable);
                cashSalesDataGridView.DataSource = dataTable;
            }
        }

        private void fill()
        {
            var data = new DataTable();

            Utils.DBManager.QueryAdapter("SELECT * FROM CashSales WHERE flg " + (radioButton2.Checked ? ">" : "=") + " 0 ORDER BY SaleDate").Fill(data);


            cashSalesDataGridView.DataSource = data;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new Dialogs.NewCashSaleForm().ShowDialog();
            fill();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (this.cashSalesDataGridView.SelectedRows.Count > 0)
            {
                var res = MessageBox.Show("Do You Want to cancel this Deal? This action is not reversable.", "Warning", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    Utils.DBManager.Update(new SqlCommand("UPDATE Cars SET CashSaleFlag = 0, TotalAmount = 0, CreditSaleFlag = 0, BuyerId = NULL, SellerId = NULL, Witness1Id = NULL, Witness2Id = NULL WHERE Id = " + (int)cashSalesDataGridView.SelectedRows[0].Cells[0].Value));
                    Utils.DBManager.Update(new SqlCommand("DELETE FROM CarTransactions WHERE CarId = " + (int)cashSalesDataGridView.SelectedRows[0].Cells[0].Value));
                    fill();
                }
            }
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

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var date = dateTimePicker1.Value;

            string query = $"SELECT * FROM CashSales WHERE SaleDate = '{date.Year}/{date.Month}/{date.Day}'";

            var dataAdapter = Utils.DBManager.QueryAdapter(query);
            var table = new DataTable();

            dataAdapter.Fill(table);

            this.cashSalesDataGridView.DataSource = table;
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
            }
            return false;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
