using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMotors.Dialogs
{
    public partial class GeneralSearch : Form
    {
        public GeneralSearch()
        {
            InitializeComponent();
        }

        private void GeneralSearch_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            this.WindowState = FormWindowState.Maximized;

            DataTable table = new DataTable();

            Utils.DBManager.QueryAdapter("SELECT * FROM Sales").Fill(table);

            this.salesDataGridView.DataSource = table;
        }

        private void Identify_Click(object sender, EventArgs e)
        {
            if (Identify.Text == "Identify")
            {
                Utils.DigitalPersonaUtil._.StopAllActivities();

                if (Utils.DigitalPersonaUtil._.IsAvailable())
                {
                    Identify.Text = "Cancel";
                    Utils.DigitalPersonaUtil._.StartIdentificationAndFill1(IdentifyLabel, this, salesDataGridView, label4);
                }
            }
            else
            {
                IdentifyLabel.Text = "";
                Identify.Text = "Identify";

                Utils.DigitalPersonaUtil._.StopAllActivities();
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var date = dateTimePicker1.Value;

            string query = $"SELECT * FROM Sales WHERE SaleDate = '{date.Year}/{date.Month}/{date.Day}'";

            var dataAdapter = Utils.DBManager.QueryAdapter(query);
            var table = new DataTable();

            dataAdapter.Fill(table);

            this.salesDataGridView.DataSource = table;
        }

        private void IdentifyLabel_TextChanged(object sender, EventArgs e)
        {
            if (IdentifyLabel.Text == "Person is Found")
            {
                var dataTable = new DataTable();

                int id = int.Parse(label4.Text);
                Utils.DBManager.QueryAdapter($"SELECT * FROM sales WHERE i1 = {id} OR i2 = {id} OR i3 = {id} OR i4 = {id}").Fill(dataTable);
                salesDataGridView.DataSource = dataTable;
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
                comboBox1.Items.Add("InvoiceName");
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
                var dataTable = new DataTable();

                Utils.DBManager.QueryAdapter("SELECT * FROM Sales").Fill(dataTable);
                salesDataGridView.DataSource = dataTable;
            }
            else
            {
                try
                {
                    var item = comboBox1.SelectedItem.ToString();

                    string query;
                    if (comboBox2.SelectedIndex == 0 && comboBox1.SelectedIndex == 0)
                        query = $"SELECT * FROM Sales WHERE sr Like '%{searchBox.Text}%'";
                    else if (comboBox2.SelectedIndex == 0)
                        query = $"SELECT * FROM Sales WHERE {item} Like '%{searchBox.Text}%'";
                    else
                        query = $"SELECT * FROM Sales WHERE \"{comboBox2.SelectedItem.ToString()}'s {item}\" Like '%" + searchBox.Text + "%'";

                    var dataAdapter = Utils.DBManager.QueryAdapter(query);
                    var table = new DataTable();

                    dataAdapter.Fill(table);

                    this.salesDataGridView.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                    this.salesTableAdapter.Fill(this.pakMotorsDataSet.Sales);
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

                var dataTable = new DataTable();

                Utils.DBManager.QueryAdapter("SELECT * FROM Sales").Fill(dataTable);
                salesDataGridView.DataSource = dataTable;
            }
        }

        private void SalesDataGridView_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!(Boolean)(salesDataGridView.SelectedRows[0].Cells[43].Value) && !(Boolean)(salesDataGridView.SelectedRows[0].Cells[44].Value))
            {
                new dialogs.CreateCar((int)salesDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
            }
            else if ((Boolean)(salesDataGridView.SelectedRows[0].Cells[44].Value))
            {
                new Dialogs.NewCreditSaleForm((int)salesDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
                this.salesTableAdapter.Fill(this.pakMotorsDataSet.Sales);
            }
            else
            {
                new Dialogs.NewCashSaleForm((int)salesDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
                this.salesTableAdapter.Fill(this.pakMotorsDataSet.Sales);
            }
        }
    }
}
