using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class Cars : Form
    {
        public static Models.Car RecentCar;

        private object temp;

        private bool mode;

        public Cars()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;

            searchOptions.SelectedIndex = 0;
            temp = carsDataGridView.DataSource;
        }

        public Cars(bool mode): this()
        {
            this.mode = mode;
            addButton.Visible = mode;
        }

        private void CarsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.carsBindingSource.EndEdit();

            try
            {
                this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cars_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new dialogs.CreateCar().ShowDialog();
            this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                this.carsDataGridView.DataSource = temp;
                this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
            }
            else
            {
                var item = searchOptions.SelectedItem.ToString();

                var query = $"SELECT * FROM Cars WHERE {item} Like '" + searchBox.Text + "%' AND CashSaleFlag = 0 AND CreditSaleFlag = 0";

                var dataAdapter = Utils.DBManager.QueryAdapter(query);
                var table = new DataTable();

                dataAdapter.Fill(table);

                this.carsDataGridView.DataSource = table;
            }
        }

        private void CarsDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.mode)
            {
                Models.Car Car = new Models.Car();
                Car.Fetch((int)carsDataGridView.SelectedRows[0].Cells[0].Value);
                RecentCar = Car;

                this.Close();
            }
            else
            {
                new dialogs.CreateCar((int)carsDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
                this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
            }
        }

        // Shortcuts
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.S:
                {
                    carsBindingNavigatorSaveItem.PerformClick();
                    return true;
                }
                case Keys.Control | Keys.R:
                {
                    searchBox.Text = "";

                    this.carsDataGridView.DataSource = temp;
                    this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
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
            foreach (DataGridViewRow row in carsDataGridView.SelectedRows)
            {
                Utils.DBManager.Update(new SqlCommand($"Update Cars Set IsDeleted = 1 WHERE id = {row.Cells[0].Value}"));
            }

            this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (carsDataGridView.SelectedRows.Count < 1) MessageBox.Show("Select a Row first");

            else
            {
                new ReportView((int)carsDataGridView.SelectedRows[0].Cells[0].Value, false, true).ShowDialog();
            }
        }
    }
}
