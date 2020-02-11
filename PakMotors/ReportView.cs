using System;
using System.Data;
using PakMotors.Reporting;
using System.Windows.Forms;

namespace PakMotors
{
    public partial class ReportView : Form
    {
        private int id;
        private bool flag;
        private DataSet data = new DataSet();

        public ReportView(int id, bool flag)
        {
            InitializeComponent();

            this.id = id;

            comboBox2.Items.Add("Bayana Recipt");
            comboBox2.Items.Add("Bayana Recipt (English)");
            comboBox2.Items.Add("Delivery Order");
            comboBox2.Items.Add("Transfer Letter");
            comboBox2.Items.Add("Sales Recipt");
            comboBox2.Items.Add("Sales Recipt (English)");
            comboBox2.Items.Add("Iqraar Naama");
            comboBox2.Items.Add("Bank Quotation");
            comboBox2.Items.Add("Credit Plan");

            comboBox2.SelectedIndex = 0;

            var adapter = Utils.DBManager.QueryAdapter($"SELECT * from CR_Data2 where car_sr = " + id);
            adapter.Fill(data, "CR_Data2");

            cr_bayana_receipt report = new cr_bayana_receipt();
            report.SetDataSource(data.Tables["CR_Data2"]);

            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();

            button1.Visible = flag;
        }

        public ReportView(int id, bool flag, bool flag2)
        {
            InitializeComponent();

            this.id = id;
            this.flag = flag2;

            comboBox2.Items.Add("Sales Recipt");
            comboBox2.Items.Add("Sales Recipt (English)");

            comboBox2.SelectedIndex = 0;

            var adapter = Utils.DBManager.QueryAdapter($"SELECT * from CR_DATA3 where Id = " + id);
            adapter.Fill(data, "CR_DATA3");

            cr_sales_receipt_blank report = new cr_sales_receipt_blank();
            report.SetDataSource(data.Tables["CR_DATA3"]);

            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();

            button1.Visible = flag;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var res = comboBox2.SelectedItem.ToString();

            if (res == "Bayana Recipt")
            {
                cr_bayana_receipt report = new cr_bayana_receipt();
                report.SetDataSource(data.Tables["CR_Data2"]);
                Console.WriteLine(report);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }

            if (res == "Bayana Recipt (English)")
            {
                cr_bayana_receipt_eng report = new cr_bayana_receipt_eng();
                report.SetDataSource(data.Tables["CR_Data2"]);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            if (res == "Delivery Order")
            {
                cr_delivery_order report = new cr_delivery_order();
                report.SetDataSource(data.Tables["CR_Data2"]);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            if (res == "Transfer Letter")
            {
                cr_transfer_letter report = new cr_transfer_letter();
                report.SetDataSource(data.Tables["CR_Data2"]);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            if (res == "Sales Recipt")
            {
                if (flag)
                {
                    cr_sales_receipt_blank report = new cr_sales_receipt_blank();
                    report.SetDataSource(data.Tables["CR_DATA3"]);

                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    cr_sales_receipt report = new cr_sales_receipt();
                    report.SetDataSource(data.Tables["CR_Data2"]);

                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.Refresh();
                }
            }
            if (res == "Sales Recipt (English)")
            {
                if (flag)
                {
                    cr_sale_receipt_eng_Blanks report = new cr_sale_receipt_eng_Blanks();
                    report.SetDataSource(data.Tables["CR_DATA3"]);

                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    cr_sale_receipt_eng report = new cr_sale_receipt_eng();
                    report.SetDataSource(data.Tables["CR_Data2"]);

                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.Refresh();
                }
            }
            if (res == "Iqraar Naama")
            {
                cr_IqraarNama report = new cr_IqraarNama();
                report.SetDataSource(data.Tables["CR_Data2"]);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            if (res == "Bank Quotation")
            {
                cr_bank_quotation report = new cr_bank_quotation();
                report.SetDataSource(data.Tables["CR_Data2"]);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            if (res == "Credit Plan")
            {
                var data = new DataSet();

                cr_carTransactions report = new cr_carTransactions();
                Utils.DBManager.QueryAdapter("SELECT * FROM CR_DATA_CarTransactions WHERE Id = " + id).Fill(data, "CR_DATA_CarTransactions");

                Console.WriteLine(id);
                Console.WriteLine(id);
                Console.WriteLine(id);
                Console.WriteLine(id);

                report.SetDataSource(data.Tables["CR_DATA_CarTransactions"]);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
        }

        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < comboBox2.Items.Count-1)
            {
                comboBox2.SelectedIndex += 1;
            }
            else new Dialogs.NewCreditSaleForm(id).ShowDialog();
        }
    }
}
