using System.Data;
using PakMotors.Reporting;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class AccountReportView : Form
    {
        public AccountReportView(int id)
        {
            InitializeComponent();

            var data = new DataSet();

            Utils.DBManager.QueryAdapter("SELECT * FROM CR__DATA_Accounts WHERE Id = " + id).Fill(data, "CR__DATA_Accounts");

            cr_accounts account = new cr_accounts();

            account.SetDataSource(data.Tables["CR__DATA_Accounts"]);

            crystalReportViewer1.ReportSource = account;
            crystalReportViewer1.Refresh();
        }
    }
}
