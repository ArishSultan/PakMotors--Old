using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class CashSales : Form
    {
        private object Temp;

        public CashSales()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            cashSalesDataGridView.Columns[9].Visible = false;
            cashSalesDataGridView.Columns[10].Visible = false;

            cashSalesDataGridView.Columns[16].Visible = false;
            cashSalesDataGridView.Columns[17].Visible = false;

            cashSalesDataGridView.Columns[23].Visible = false;
            cashSalesDataGridView.Columns[24].Visible = false;

            cashSalesDataGridView.Columns[30].Visible = false;
            cashSalesDataGridView.Columns[31].Visible = false;

            Temp = cashSalesDataGridView.DataSource;

            comboBox2.SelectedIndex = 0;

            this.FormClosed += (sender, e) => { Utils.DigitalPersonaUtil._.StopAllActivities(); };
        }

        private void CashSales_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet1.CashSales' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'pakMotorsDataSet.CashSales' table. You can move, or remove it, as needed.
            ;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new Dialogs.CashSaleForm().ShowDialog();
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
                if (comboBox2.SelectedIndex == 0)
                    query = $"SELECT * FROM CashSales WHERE {item} Like '{searchBox.Text}%'";
                else
                    query = $"SELECT * FROM CashSales WHERE \"{comboBox2.SelectedItem.ToString()}'s {item}\" Like '" + searchBox.Text + "%'";

                var dataAdapter = Utils.DBManager.QueryAdapter(query);
                var table = new DataTable();

                dataAdapter.Fill(table);

                this.cashSalesDataGridView.DataSource = table;
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            if (comboBox2.SelectedIndex == 0)
            {
                comboBox1.Items.Add("EngineNo");
                comboBox1.Items.Add("ChasisNo");
                comboBox1.Items.Add("RegistrationNo");
            }

            else
            {
                comboBox1.Items.Add("Name");
                comboBox1.Items.Add("CNIC");
            }
            comboBox1.SelectedIndex = 0;
        }

        private void CashSalesDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new Dialogs.NewCashSaleForm((int)cashSalesDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();

            fill();
        }

        private void fill()
        {
            var data = new DataTable();

            Utils.DBManager.QueryAdapter("SELECT * FROM CashSales").Fill(data);

            cashSalesDataGridView.DataSource = data;
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.R:
                    {
                        searchBox.Text = "";

                        fill();
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
    }
}
