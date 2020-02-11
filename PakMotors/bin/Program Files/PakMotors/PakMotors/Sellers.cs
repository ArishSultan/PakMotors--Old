using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors
{
    public partial class Sellers : Form
    {
        public Sellers()
        {
            InitializeComponent();

            comboBox1.Items.Add("CNIC");
            comboBox1.Items.Add("Name");
            comboBox1.Items.Add("Cast");

            comboBox1.SelectedIndex = 0;
        }

        private void sellersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.sellersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);
        }

        private void Sellers_Load(object sender, EventArgs e)
        {
            this.sellersTableAdapter.Fill(this.pakMotorsDataSet.Sellers);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new dialogs.CreateSeller().ShowDialog();
            this.sellersTableAdapter.Fill(this.pakMotorsDataSet.Sellers);
        }

        private void sellersDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new dialogs.CreateSeller((int)this.sellersDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();

            this.sellersTableAdapter.Fill(this.pakMotorsDataSet.Sellers);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                this.sellersTableAdapter.Fill(this.pakMotorsDataSet.Sellers);

            var item = comboBox1.SelectedItem.ToString();

            string query = "";

            if (item == "CNIC")
                query = "SELECT * FROM Sellers WHERE seller_cnic Like '" + textBox1.Text + "%'";
            else if (item == "Name")
                query = "SELECT * FROM Sellers WHERE seller_name Like '" + textBox1.Text + "%'";
            else if (item == "Cast")
                query = "SELECT * FROM Sellers WHERE seller_cast Like '" + textBox1.Text + "%'";


            var dataAdapter = utils.DBManager.QueryAdapter(query);
            var table = new DataTable();
            dataAdapter.Fill(table);
            this.sellersDataGridView.DataSource = table;
        }

        private void Sellers_SizeChanged(object sender, EventArgs e)
        {
            this.sellersDataGridView.Width = this.Width;
            this.sellersDataGridView.Height = this.Height - this.sellersDataGridView.Location.Y;
        }
    }
}
