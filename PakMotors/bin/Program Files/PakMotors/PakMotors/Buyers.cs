using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors
{
    public partial class Buyers : Form
    {
        public Buyers()
        {
            InitializeComponent();

            comboBox1.Items.Add("CNIC");
            comboBox1.Items.Add("Name");
            comboBox1.Items.Add("Cast");

            comboBox1.SelectedIndex = 0;
        }

        private void buyersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.buyersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);
        }

        private void Buyers_Load(object sender, EventArgs e)
        {
            this.buyersTableAdapter.Fill(this.pakMotorsDataSet.Buyers);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new dialogs.CreateBuyers().ShowDialog();
            this.buyersTableAdapter.Fill(this.pakMotorsDataSet.Buyers);
        }

        private void buyersDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new dialogs.CreateBuyers((int)this.buyersDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();

            this.buyersTableAdapter.Fill(this.pakMotorsDataSet.Buyers);
        }

        private void Buyers_SizeChanged(object sender, EventArgs e)
        {
            this.buyersDataGridView.Width = this.Width;
            this.buyersDataGridView.Height = this.Height - this.buyersDataGridView.Location.Y;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                this.buyersTableAdapter.Fill(this.pakMotorsDataSet.Buyers);

            var item = comboBox1.SelectedItem.ToString();

            string query = "";

            if (item == "CNIC")
                query = "SELECT * FROM Buyers WHERE buyer_cnic Like '" + textBox1.Text + "%'";
            else if (item == "Name")
                query = "SELECT * FROM Buyers WHERE buyer_name Like '" + textBox1.Text + "%'";
            else if (item == "Cast")
                query = "SELECT * FROM Buyers WHERE buyer_cast Like '" + textBox1.Text + "%'";


            var dataAdapter = utils.DBManager.QueryAdapter(query);
            var table = new DataTable();
            dataAdapter.Fill(table);
            this.buyersDataGridView.DataSource = table;
        }
    }
}
