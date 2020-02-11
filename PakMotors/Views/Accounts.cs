using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class Accounts : Form
    {
        int id;
        private int currentId;
        private List<int> toBeDeleted = new List<int>();

        public Accounts()
        {
            InitializeComponent();

            monthAccountTransactionsDataGridView.Enabled = false;
            button3.Enabled = false;
            print.Enabled = false;
            oldRecords.Enabled = false;
        }

        private void MonthAccountTransactionsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.monthAccountTransactionsBindingSource.EndEdit();

            for(int i = 0; i < toBeDeleted.Count; i++)
            {
                Utils.DBManager.Delete("DELETE FROM MonthAccountTransactions WHERE id = " + toBeDeleted[i]);
                toBeDeleted.Clear();
            }

            foreach(DataGridViewRow row in monthAccountTransactionsDataGridView.Rows)
            {
                Utils.DBManager.Update(
                    "MonthAccountTransactions",
                    (int)row.Cells[0].Value,
                    new string[] { "DebitAmount", "DebitDate", "DebitDescription", "CreditAmount", "CreditDate", "CreditDescription", },
                    new SqlDbType[] { SqlDbType.BigInt, SqlDbType.Date, SqlDbType.Text, SqlDbType.BigInt, SqlDbType.Date, SqlDbType.Text, },
                    new object[] { row.Cells[3].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[7].Value, row.Cells[5].Value, row.Cells[6].Value }
                );
            }

            var query1 = Utils.DBManager.Query("SELECT (SUM(DebitAmount) - SUM(CreditAmount)) AS asd FROM MonthAccountTransactions WHERE MonthAccountId = " + currentId);
            var query2 = Utils.DBManager.Query("SELECT StartingBalance FROM MonthAccounts WHERE id = " + currentId);
            try
            {
                this.currentBalance.Text =  (((int)query2[0]["StartingBalance"]) + (long)query1[0]["asd"]) + "";
            }
            catch (Exception)
            {
                this.currentBalance.Text = query2[0]["StartingBalance"] + "";
            }
        }

        private void Accounts_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //LoadAccounts();

            monthAccountTransactionsBindingNavigatorSaveItem.PerformClick();

            var command = new SqlCommand("INSERT INTO MonthAccountTransactions(DebitDate, DebitDescription, DebitAmount, MonthAccountId, CreditDate, CreditDescription, CreditAmount) VALUES(@a, '', 0, " + currentId + ", @b, '', 0);");
            command.Parameters.AddWithValue("@a", DateTime.Now);
            command.Parameters.AddWithValue("@b", DateTime.Now);

            Utils.DBManager.Insert(command);

            var data = new DataTable();
            Utils.DBManager.QueryAdapter("SELECT * FROM MonthAccountTransactions WHERE MonthAccountId = " + currentId).Fill(data);
            monthAccountTransactionsDataGridView.DataSource = data;
            monthAccountTransactionsDataGridView.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new dialogs.CreateAccount().ShowDialog();

            LoadAccounts();
        }

        private void OldRecords_Click(object sender, EventArgs e)
        {
            new Dialogs.OldAccounts(id).ShowDialog();
        }

        private void LoadAccounts()
        {
            accountsList.Controls.Clear();

            var adapter = Utils.DBManager.QueryAdapter("SELECT * FROM Accounts");
            var dataSet = new DataSet();

            adapter.Fill(dataSet);

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                var button = new Button();

                button.Text = row["Name"].ToString();
                button.Width = accountsList.Width - 6;
                button.Click += (_sender, _e) =>
                {
                    button3.Enabled = true;
                    print.Enabled = true;
                    oldRecords.Enabled = true;

                    this.id = (int)row["Id"];
                    var query1 = Utils.DBManager.Query("SELECT Top 1 * FROM MonthAccounts WHERE AccountId = " + row["Id"] + " ORDER BY Id DESC");

                    if (query1.Count <= 0) return;
                    var query2 = Utils.DBManager.Query("SELECT * FROM MonthAccountTransactions WHERE MonthAccountId = " + query1[0]["Id"]);

                    currentId = (int)query1[0]["id"];

                    accountName.Text = row["name"].ToString();
                    openingBalance.Text = query1[0]["startingBalance"].ToString();


                    query1 = Utils.DBManager.Query("SELECT (SUM(DebitAmount) - SUM(CreditAmount)) AS asd FROM MonthAccountTransactions WHERE MonthAccountId = " + currentId);
                    query2 = Utils.DBManager.Query("SELECT StartingBalance FROM MonthAccounts WHERE id = " + currentId);
                    try
                    {
                        this.currentBalance.Text = (((int)query2[0]["StartingBalance"]) + (long)query1[0]["asd"]) + "";
                    }
                    catch (Exception)
                    {
                        this.currentBalance.Text = query2[0]["StartingBalance"] + "";
                    }

                    var data = new DataTable();
                    Utils.DBManager.QueryAdapter("SELECT * FROM MonthAccountTransactions WHERE MonthAccountId = " + currentId).Fill(data);

                    monthAccountTransactionsDataGridView.DataSource = data;
                    monthAccountTransactionsDataGridView.Enabled = true;

                    //var q1 = Utils.DBManager.Query("SELECT SUM(debit_amount) - SUM(credit_amount) as calc FROM Transactions WHERE accountDetailsID = " + currentId);
                    //var q2 = Utils.DBManager.Query("SELECT startingBalance FROM AccountDetails WHERE id = " + currentId);

                    ////Console.WriteLine(q1[0]["calc"]);
                    //currentBalance.Text = (((decimal)q1[0]["calc"]) + ((int)q2[0]["startingBalance"])) + "";
                };
                accountsList.Controls.Add(button);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.monthAccountTransactionsBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);

            foreach (DataGridViewRow row in monthAccountTransactionsDataGridView.Rows)
            {
                Utils.DBManager.Update(
                    "MonthAccountTransactions",
                    (int)row.Cells[0].Value,
                    new string[] { "DebitAmount", "DebitDate", "DebitDescription", "CreditAmount", "CreditDate", "CreitDescription", },
                    new SqlDbType[] { SqlDbType.Money, SqlDbType.Date, SqlDbType.Text, SqlDbType.Money, SqlDbType.Date, SqlDbType.Text, },
                    new object[] { row.Cells[3].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[6].Value, row.Cells[4].Value, row.Cells[5].Value }
                );
            }

            var query1 = Utils.DBManager.Query("SELECT (SUM(DebitAmount) - SUM(CreditAmount)) AS asd FROM MonthAccountTransactions WHERE MonthAccountId = " + currentId);
            var query2 = Utils.DBManager.Query("SELECT StartingBalance FROM MonthAccounts WHERE id = " + currentId);
            try
            {
                this.currentBalance.Text = (((int)query1[0]["asd"]) - (int)query2[0]["StartingBalance"]) + "";
            }
            catch (Exception ex)
            {
                this.currentBalance.Text = query2[0]["StartingBalance"] + "";
            }
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.S:
                    {
                        monthAccountTransactionsBindingNavigatorSaveItem.PerformClick();
                        return true;
                    }
                case Keys.Control | Keys.N:
                    {
                        button3.PerformClick();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void MonthAccountTransactionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BindingNavigatorDeleteItem_Click_1(object sender, EventArgs e)
        {
            if (monthAccountTransactionsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a row to delete");
            }
            else
            {
                foreach(DataGridViewRow row in monthAccountTransactionsDataGridView.SelectedRows)
                {
                    toBeDeleted.Add((int)row.Cells[0].Value);
                    monthAccountTransactionsDataGridView.Rows.Remove(row);
                }
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
            new AccountReportView(id).ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.id != 0)
            {
                new PinCode(this.id).ShowDialog();

                accountsList.Controls.Clear();

                var adapter = Utils.DBManager.QueryAdapter("SELECT * FROM Accounts");
                var dataSet = new DataSet();

                adapter.Fill(dataSet);

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var button = new Button();

                    button.Text = row["Name"].ToString();
                    button.Width = accountsList.Width - 6;
                    button.Click += (_sender, _e) =>
                    {
                        button3.Enabled = true;
                        print.Enabled = true;
                        oldRecords.Enabled = true;

                        this.id = (int)row["Id"];
                        var query1 = Utils.DBManager.Query("SELECT Top 1 * FROM MonthAccounts WHERE AccountId = " + row["Id"] + " ORDER BY Id DESC");

                        if (query1.Count <= 0) return;
                        var query2 = Utils.DBManager.Query("SELECT * FROM MonthAccountTransactions WHERE MonthAccountId = " + query1[0]["Id"]);

                        currentId = (int)query1[0]["id"];

                        accountName.Text = row["name"].ToString();
                        openingBalance.Text = query1[0]["startingBalance"].ToString();


                        query1 = Utils.DBManager.Query("SELECT (SUM(DebitAmount) - SUM(CreditAmount)) AS asd FROM MonthAccountTransactions WHERE MonthAccountId = " + currentId);
                        query2 = Utils.DBManager.Query("SELECT StartingBalance FROM MonthAccounts WHERE id = " + currentId);
                        try
                        {
                            this.currentBalance.Text = (((int)query2[0]["StartingBalance"]) + (long)query1[0]["asd"]) + "";
                        }
                        catch (Exception)
                        {
                            this.currentBalance.Text = query2[0]["StartingBalance"] + "";
                        }

                        var data = new DataTable();
                        Utils.DBManager.QueryAdapter("SELECT * FROM MonthAccountTransactions WHERE MonthAccountId = " + currentId).Fill(data);

                        monthAccountTransactionsDataGridView.DataSource = data;
                        monthAccountTransactionsDataGridView.Enabled = true;

                        //var q1 = Utils.DBManager.Query("SELECT SUM(debit_amount) - SUM(credit_amount) as calc FROM Transactions WHERE accountDetailsID = " + currentId);
                        //var q2 = Utils.DBManager.Query("SELECT startingBalance FROM AccountDetails WHERE id = " + currentId);

                        ////Console.WriteLine(q1[0]["calc"]);
                        //currentBalance.Text = (((decimal)q1[0]["calc"]) + ((int)q2[0]["startingBalance"])) + "";
                    };
                    accountsList.Controls.Add(button);
                }

            }
        }
    }
}
