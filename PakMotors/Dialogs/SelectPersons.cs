using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors.Dialogs
{
    public partial class SelectPersons : Form
    {
        public static int id;

        public SelectPersons()
        {
            InitializeComponent();
        }

        private void PersonsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.personsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);

        }

        private void SelectPersons_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.Persons' table. You can move, or remove it, as needed.
            this.personsTableAdapter.Fill(this.pakMotorsDataSet.Persons);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (personsDataGridView.SelectedRows.Count > 0)
            {
                id = (int) personsDataGridView.SelectedRows[0].Cells[0].Value;
                this.Close();
            }
            else
            {
                MessageBox.Show("No Rows were selected");
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                var dataTable = new DataTable();
                Utils.DBManager.QueryAdapter("SELECT * FROM Persons WHERE NOT(Name Like '') OR NOT(CNIC Like '     -       -')").Fill(dataTable);

                this.personsDataGridView.DataSource = dataTable;
            }
            else
            {
                var dataTable = new DataTable();
                Utils.DBManager.QueryAdapter($"SELECT * FROM Persons WHERE Name Like '{textBox1.Text}%'").Fill(dataTable);

                this.personsDataGridView.DataSource = dataTable;
            }
        }
    }
}
