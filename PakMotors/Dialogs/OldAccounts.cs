using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors.Dialogs
{
    public partial class OldAccounts : Form
    {
        public OldAccounts()
        {
            InitializeComponent();
        }

        public OldAccounts(int id): this()
        {
            Console.WriteLine(id);

            var dataSet = new DataTable();
             
            Utils.DBManager.QueryAdapter("SELECT * FROM OldAccounts WHERE Id = " + id).Fill(dataSet);
            this.oldAccountsDataGridView.DataSource = dataSet;
        }

        private void OldAccounts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.OldAccounts' table. You can move, or remove it, as needed.
        }

        private void OldAccountsDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new OldAccountDetails((int)this.oldAccountsDataGridView.SelectedRows[0].Cells[1].Value).ShowDialog();
        }
    }
}
