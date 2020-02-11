using PakMotors.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class PattyCashReport : Form
    {
        public PattyCashReport(int id)
        {
            InitializeComponent();

            var data = new DataSet();

            Utils.DBManager.QueryAdapter("SELECT * FROM CR_Patty_Cash_Book WHERE PattyCashBookId = " + id).Fill(data, "CR_Patty_Cash_Book");

            cr_patty_cash_book report = new cr_patty_cash_book();

            report.SetDataSource(data.Tables["CR_Patty_Cash_Book"]);

            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();
        }
    }
}
