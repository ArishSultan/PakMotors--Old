using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors
{
    public partial class CarSales : Form
    {
        public CarSales()
        {
            InitializeComponent();

            comboBox2.Items.Add("Car");
            comboBox2.Items.Add("Buyer");
            comboBox2.Items.Add("Seller");

            comboBox2.SelectedIndex = 0;
        }

        private void CarSales_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.SaleCar_View' table. You can move, or remove it, as needed.
            this.saleCar_ViewTableAdapter.Fill(this.pakMotorsDataSet.SaleCar_View);

            this.saleCar_ViewDataGridView.Width = this.Width;
            this.saleCar_ViewDataGridView.Height = this.Height - this.saleCar_ViewDataGridView.Location.Y;
        }

        private void CarSales_SizeChanged(object sender, EventArgs e)
        {
            this.saleCar_ViewDataGridView.Width = this.Width;
            this.saleCar_ViewDataGridView.Height = this.Height - this.saleCar_ViewDataGridView.Location.Y;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            if (comboBox2.Items[comboBox2.SelectedIndex].ToString() == "Car")
            {
                comboBox1.Items.Add("Model");
                comboBox1.Items.Add("Engine #");
                comboBox1.Items.Add("Chasis #");
            }
            else
            {
                comboBox1.Items.Add("CNIC");
                comboBox1.Items.Add("Name");
            }

            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                this.saleCar_ViewTableAdapter.Fill(this.pakMotorsDataSet.SaleCar_View);

            var item = comboBox1.SelectedItem.ToString();

            string query = "";

            if (comboBox2.Text == "Car")
            {
                if (item == "Model")
                    query = "SELECT * FROM SaleCar_View WHERE car_model Like '" + textBox1.Text + "%'";
                else if (item == "Engine #")
                    query = "SELECT * FROM SaleCar_View WHERE car_engine_no Like '" + textBox1.Text + "%'";
                else if (item == "Chassis #")
                    query = "SELECT * FROM SaleCar_View WHERE car_chassis_no Like '" + textBox1.Text + "%'";
            }
            else
            {
                string ads = comboBox2.Text.ToLower();
                if (item == "CNIC")
                    query = "SELECT * FROM SaleCar_View WHERE " + ads + "_cnic Like '" + textBox1.Text + "%'";
                else if (item == "Name")
                    query = "SELECT * FROM SaleCar_View WHERE " + ads + "_name Like '" + textBox1.Text + "%'";
                else if (item == "Cast")
                    query = "SELECT * FROM SaleCar_View WHERE " + ads + "_cast Like '" + textBox1.Text + "%'";
            }

            var dataAdapter = utils.DBManager.QueryAdapter(query);
            var table = new DataTable();
            dataAdapter.Fill(table);
            this.saleCar_ViewDataGridView.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new SellCarFrom().ShowDialog();
        }

        private void saleCar_ViewDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                new SellCarFrom((int)this.saleCar_ViewDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();

                this.saleCar_ViewTableAdapter.Fill(this.pakMotorsDataSet.SaleCar_View);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
    }
}
