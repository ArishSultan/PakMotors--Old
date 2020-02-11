using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors.Dialogs
{
    public partial class OldAccountDetails : Form
    {
        private int id;

        public OldAccountDetails(int id)
        {
            this.id = id;

            InitializeComponent();
        }

        private void OldAccountDetails_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.Old_Details' table. You can move, or remove it, as needed.
            //this.old_DetailsTableAdapter.Fill(this.pakMotorsDataSet.Old_Details);

            var dataSet = new DataTable();
            Utils.DBManager.QueryAdapter("SELECT * FROM MonthAccountTransactions WHERE MonthAccountId = " + id).Fill(dataSet);

            this.old_DetailsDataGridView.DataSource = dataSet;
        }
    }
}
