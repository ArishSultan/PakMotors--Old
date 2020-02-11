using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class OldPattyCashBooks : Form
    {
        int id;

        public OldPattyCashBooks(int id)
        {
            InitializeComponent();

            this.id = id;
        }

        private void OldPattyCashBooks_Load(object sender, EventArgs e)
        {
            var dataset = new DataTable();
            Utils.DBManager.QueryAdapter($"SELECT * FROM PattyCashBook WHERE NOT(Id = {id})").Fill(dataset);

            this.pattyCashBookDataGridView.DataSource = dataset;
        }

        private void PattyCashBookDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (pattyCashBookDataGridView.SelectedRows.Count > 0) new Views.PattyCashBook(false, (int) pattyCashBookDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
