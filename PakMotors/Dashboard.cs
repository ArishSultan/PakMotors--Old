using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PakMotors
{
    // DESKTOP-HCB17EO
    public partial class Dashboard : Form
    {
        private int pattycashId = 0;
        private object Temp;

        public Dashboard()
        {
            InitializeComponent();

            DateTime limit = new DateTime(2021, 7, 1);
            TimeSpan timeSpan = limit.Subtract(DateTime.Now);
            if (timeSpan.Days <= 0)
            {
                MessageBox.Show("30 Day usage limit is over, you can\'t use this software now", "Error", MessageBoxButtons.OK);
                System.Environment.Exit(0);
            }

            this.button5.PerformClick();
            this.WindowState = FormWindowState.Maximized;

            panel3.Location = new Point()
            {
                X = panel2.Width / 2 - panel3.Width / 2,
                Y = panel2.Height / 2 - panel3.Height / 2
            };
        }

        private void button3_Click(object sender, EventArgs e) { new Views.Cars().Show(); }
        private void button2_Click(object sender, EventArgs e) { new Utils.CreditSales().Show(); }
        private void button1_Click(object sender, EventArgs e) { new Views.Persons().Show();  }
        private void button4_Click(object sender, EventArgs e) { new Utils.CashSales().Show(); }
        private void Button6_Click(object sender, EventArgs e) { new Views.Accounts().Show(); }

        private void Button5_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();

            var data = new DataSet();

            var d = Utils.DBManager.QueryAdapter("SELECT * FROM Cars WHERE CreditSaleFlag = 1 OR CashSaleFlag = 1");
            d.Fill(data);

            foreach (DataRow row in data.Tables[0].Rows)
            {
                var _data = new DataSet();

                Utils.DBManager.QueryAdapter("SELECT Top 1 * FROM CarTransactions WHERE (IsRecieved is NULL OR IsRecieved = 0) AND CarId = " + row["Id"]).Fill(_data);

                try
                {
                    if (_data.Tables[0].Rows.Count == 0) continue;

                    else if ((DateTime.Now - (DateTime)(_data.Tables[0].Rows[0]["Date"])).Days * -1 <= 10)
                    {
                        var label = new Label();

                        var b = new DataSet();
                        Utils.DBManager.QueryAdapter("SELECT * FROM Persons WHERE Id = " + row["BuyerId"]).Fill(b);

                        var days = -(DateTime.Now - (DateTime)(_data.Tables[0].Rows[0]["Date"])).Days;
                        var daysLeftString = "has to pay " + _data.Tables[0].Rows[0]["Amount"] + " (" + days + ") Days left";

                        label.Text = "SOLD ON " + ((Boolean)row["CashSaleFlag"] ? "CASH" : "CREDIT") + "\n" +
                                     "Name: " + b.Tables[0].Rows[0]["Name"] + "\n" +
                                     "Phone #: " + b.Tables[0].Rows[0]["Phone1"] + "\n" +
                                     daysLeftString;

                        label.Height = 80;
                        label.AutoSize = false;
                        label.Font = new Font("Roboto", 10);
                        label.Width = flowLayoutPanel1.Width - 25;

                        label.Click += (_sender, _e) =>
                        {
                            if (((Boolean)row["CashSaleFlag"]))
                                new Dialogs.NewCashSaleForm((int)row["Id"]).ShowDialog();
                            else new Dialogs.NewCreditSaleForm((int)row["Id"]).ShowDialog();
                        };

                        label.BorderStyle = BorderStyle.FixedSingle;

                        if (Math.Abs(days) <= 3) flowLayoutPanel4.Controls.Add(label);
                        else if (days < -3) flowLayoutPanel3.Controls.Add(label);
                        else flowLayoutPanel1.Controls.Add(label);
                    }
                }
                catch(Exception)
                {

                }
            }

            var q = Utils.DBManager.Query("SELECT * FROM Accounts");

            foreach (DataRow row in q)
            {
                var q2 = Utils.DBManager.Query($"SELECT Top 1 * FROM MonthAccounts WHERE AccountId = {row["Id"]} AND ClosingBalance is NULL");

                foreach (DataRow _row in q2)
                {
                    var q3 = Utils.DBManager.Query("SELECT SUM(DebitAmount) - SUM(CreditAmount) as calc FROM MonthAccountTransactions WHERE MonthAccountId = " + _row["Id"]);

                    long sum = 0;

                    try
                    {
                        sum = ((long)q3[0]["calc"]);
                    }
                    catch (Exception)
                    {
                        sum = 0;
                    }

                    long closing = ((int)_row["StartingBalance"]) + sum;


                    var label = new Label
                    {
                        Text = "Account: " + row["Name"] + "\n" +
                                    "Current Balance: " + closing + "\n",

                        Height = 52,
                        AutoSize = false,
                        Font = new Font("Roboto", 10),
                        Width = flowLayoutPanel1.Width - 8,

                        BorderStyle = BorderStyle.FixedSingle
                    };
                    flowLayoutPanel2.Controls.Add(label);
                }
            }
        }

        private void Panel2_SizeChanged(object sender, EventArgs e)
        {
            panel3.Location = new Point()
            {
                X = panel2.Width / 2 - panel3.Width / 2,
                Y = panel2.Height / 2 - panel3.Height / 2
            };
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet1.Sales' table. You can move, or remove it, as needed.
            //this.salesTableAdapter.Fill(this.pakMotorsDataSet1.Sales);
            this.button5.PerformClick();

            // Manage Accounts

            var q = Utils.DBManager.Query("SELECT * FROM Accounts");

            foreach (DataRow row in q)
            {
                var q2 = Utils.DBManager.Query($"SELECT Top 1 * FROM MonthAccounts WHERE AccountId = {row["Id"]} AND ClosingBalance is NULL");

                foreach (DataRow _row in q2)
                {
                    if (DateTime.Compare(DateTime.Now, DateTime.Parse(_row["EndDate"].ToString())) > 0)
                    {
                        var q3 = Utils.DBManager.Query("SELECT SUM(DebitAmount) - SUM(CreditAmount) as calc FROM MonthAccountTransactions WHERE MonthAccountId = " + _row["Id"]);

                        long sum = 0;

                        try
                        {
                            sum = ((long)q3[0]["calc"]);
                        }
                        catch (Exception)
                        {
                            sum = 0;
                        }

                        long closing = ((int)_row["StartingBalance"]) + sum;


                        Utils.DBManager.Update(new SqlCommand($"UPDATE MonthAccounts SET ClosingBalance = {closing} WHERE Id = {_row["Id"]}"));

                        var command = new SqlCommand("INSERT INTO MonthAccounts(AccountId, StartDate, StartingBalance, EndDate) VALUES(@a, @b, " + closing + ", @c)");
                        var time = DateTime.Parse(_row["EndDate"].ToString());
                        command.Parameters.AddWithValue("@a", row["Id"]);
                        command.Parameters.AddWithValue("@b", time);
                        time = time.AddMonths(1);
                        time = new DateTime(time.Year, time.Month, 1);
                        command.Parameters.AddWithValue("@c", time);

                        Utils.DBManager.Insert(command);
                    }
                }
            }

            var res = Utils.DBManager.Query("SELECT Top 1 * FROM PattyCashBook Order By Id DESC");
            if (res.Count > 0)
            {
                var _res = res[0];
                var d = DateTime.Now;
                var d2 = new DateTime(d.Year, d.Month, d.Day);

                if ((d2 - DateTime.Parse(_res["Date"].ToString())).TotalDays > 0.7)
                {
                    Console.WriteLine((DateTime.Now - DateTime.Parse(_res["Date"].ToString())).TotalDays);

                    this.pattycashId = Utils.DBManager.Insert(
                        "PattyCashBook",
                        new string[] { "Date" },
                        new SqlDbType[] { SqlDbType.DateTime },
                        new object[] { DateTime.Now }
                    );
                }

                else
                {
                    this.pattycashId = (int)_res["Id"];
                }
            }
            else
            {
                this.pattycashId = Utils.DBManager.Insert(
                    "PattyCashBook",
                    new string[] { "Date" },
                    new SqlDbType[] { SqlDbType.DateTime },
                    new object[] { DateTime.Now }
                );
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            new Dialogs.NewCreditSaleForm().ShowDialog();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            new Dialogs.NewCashSaleForm().ShowDialog();
        }

        private void Dashboard_Enter(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {

            switch (keys)
            {
                case Keys.Control | Keys.F:
                    {
                        new Dialogs.GeneralSearch().ShowDialog();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            new Views.PattyCashBook(true, pattycashId).Show();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            new Views.RecyclePersons().Show();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            new Views.RecycleCars().Show();
        }
    }
}