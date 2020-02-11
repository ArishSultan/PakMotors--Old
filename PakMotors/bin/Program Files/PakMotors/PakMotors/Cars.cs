using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PakMotors
{
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();

            comboBox2.Items.Add("Name");
            comboBox2.Items.Add("Model");
            comboBox2.Items.Add("Engine #");
            comboBox2.Items.Add("Chassis #");
            comboBox2.Items.Add("Registration #");
            comboBox2.Items.Add("PBO");

            comboBox2.SelectedIndex = 0;
        }

        private void carsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.carsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);
        }

        private void Cars_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
        }

        private void Cars_SizeChanged(object sender, EventArgs e)
        {
            this.carsDataGridView.Width = this.Width;
            this.carsDataGridView.Height = this.Height - this.carsDataGridView.Location.Y;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);

            var item = comboBox2.SelectedItem.ToString();

            string query = "";

            if (item == "Engine #")
            {
                query = "SELECT * FROM Cars WHERE car_engine_no Like '" + textBox2.Text + "%'";
            }
            else if (item == "Name")
            {
                query = "SELECT * FROM Cars WHERE car_name Like '" + textBox2.Text + "%'";
            }
            else if (item == "Model")
            {
                query = "SELECT * FROM Cars WHERE car_model Like '" + textBox2.Text + "%'";
            }
            else if (item == "Chassis #")
            {
                query = "SELECT * FROM Cars WHERE car_chasis_no Like '" + textBox2.Text + "%'";
            }
            else if (item == "Registration #")
            {
                query = "SELECT * FROM Cars WHERE car_registration_no Like '" + textBox2.Text + "%'";
            }
            else if (item == "PBO")
            {
                query = "SELECT * FROM Cars WHERE car_pbo Like '" + textBox2.Text + "%'";
            }


            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-QMSSOCP\SQLEXPRESS;Initial Catalog=PakMotors;Integrated Security=True");

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();

            dataAdapter.Fill(table);

            this.carsDataGridView.DataSource = table;

            connection.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new dialogs.CreateCar().ShowDialog();

            this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
        }

        private void carsDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                new dialogs.CreateCar((int)this.carsDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();

                this.carsTableAdapter.Fill(this.pakMotorsDataSet.Cars);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
    }
}
