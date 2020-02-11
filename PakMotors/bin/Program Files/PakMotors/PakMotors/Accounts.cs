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

namespace PakMotors
{
    public partial class Accounts : Form
    {
        private int selectedID;

        public Accounts()
        {
            InitializeComponent();

            var dataTable = new DataSet();
            utils.DBManager.QueryAdapter("SELECT * FROM Accounts").Fill(dataTable);

            foreach (DataRow row in dataTable.Tables[0].Rows)
            {
                var button = new Button();

                button.Width = 231 - 5;
                button.Text = row["account_name"].ToString();
                button.Click += (sender, e) =>
                {
                    selectedID = (int)row["account_id"];
                    var res = utils.DBManager.Query("SELECT * FROM MonthAccount WHERE account_id = " + row["account_id"]);

                    account_name.Text = button.Text;
                    starting_balance.Text = res[0]["starting_balance"].ToString();
                    utils.DBManager.QueryAdapter("SELECT * FROM Account_Transactions WHERE month_account_id = " + res[0]["id"]).Fill(this.pakMotorsDataSet.Account_Transactions);
                };

                listView1.Controls.Add(button);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new dialogs.CreateAccount().ShowDialog();
            var dataTable = new DataSet();
            utils.DBManager.QueryAdapter("SELECT * FROM Accounts").Fill(dataTable);

            foreach (DataRow row in dataTable.Tables[0].Rows)
            {
                var button = new Button();

                button.Width = 231 - 5;
                button.Text = row["account_name"].ToString();
                button.Click += (sender1, e1) =>
                {
                    selectedID = (int)row["account_id"];
                    var res = utils.DBManager.Query("SELECT * FROM MonthAccount WHERE account_id = " + row["account_id"]);

                    account_name.Text = button.Text;
                    starting_balance.Text = res[0]["starting_balance"].ToString();
                    utils.DBManager.QueryAdapter("SELECT * FROM Account_Transactions WHERE month_account_id = " + res[0]["id"]).Fill(this.pakMotorsDataSet.Account_Transactions);
                };

                listView1.Controls.Add(button);
            }
        }

        private void account_TransactionsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.account_TransactionsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);

            foreach (DataGridViewRow row in this.account_TransactionsDataGridView.Rows)
            {
                Console.WriteLine(row.Cells[0].Value + ", " + row.Cells[1].Value + ", " + row.Cells[2].Value);

                var command = new SqlCommand("UPDATE Account_Transactions SET account_id = @a");
                command.Parameters.AddWithValue("@a", selectedID);

                utils.DBManager.Update(command);
            }
            this.account_TransactionsTableAdapter.Fill(this.pakMotorsDataSet.Account_Transactions);
        }

        private void Accounts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.Account_Transactions' table. You can move, or remove it, as needed.
            //this.account_TransactionsTableAdapter.Fill(this.pakMotorsDataSet.Account_Transactions);

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
