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

namespace PakMotors.Views
{
    public partial class RecyclePersons : Form
    {
        public RecyclePersons()
        {
            InitializeComponent();
        }

        private void RecyclePersons_Load(object sender, EventArgs e)
        {
            var dataTable = new DataTable();

            Utils.DBManager.QueryAdapter("SELECT * FROM Persons WHERE IsDeleted = 1;").Fill(dataTable);

            personsDataGridView.DataSource = dataTable;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in personsDataGridView.SelectedRows)
            {
                Utils.DBManager.Update(new SqlCommand($"Update Persons Set IsDeleted = NULL WHERE Id = {row.Cells[0].Value}"));
            }

            var dataTable = new DataTable();

            Utils.DBManager.QueryAdapter("SELECT * FROM Persons WHERE IsDeleted = 1;").Fill(dataTable);

            personsDataGridView.DataSource = dataTable;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in personsDataGridView.SelectedRows)
            {
                Utils.DBManager.Update(new SqlCommand($"Delete FROM Persons WHERE Id = {row.Cells[0].Value}"));
            }

            var dataTable = new DataTable();

            Utils.DBManager.QueryAdapter("SELECT * FROM Persons WHERE IsDeleted = 1;").Fill(dataTable);

            personsDataGridView.DataSource = dataTable;
        }
    }
}
