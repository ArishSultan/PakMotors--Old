using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PakMotors.dialogs
{
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void create_update_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || ammount.Text == "")
            {
                MessageBox.Show("Please Fill All Fields");
            }

            var command = new SqlCommand("INSERT INTO Accounts(Name) Values('" + name.Text + "');");

            Utils.DBManager.Insert(command);

            var res = (int)Utils.DBManager.Query("SELECT TOP 1 * FROM Accounts ORDER BY Id DESC")[0]["Id"];

            command = new SqlCommand("INSERT INTO MonthAccounts(AccountId, StartDate, StartingBalance, EndDate) VALUES(@a, @b, " + ammount.Text + ", @c)");
            var time = DateTime.Now;
            command.Parameters.AddWithValue("@a", res);
            command.Parameters.AddWithValue("@b", time);
            time = time.AddMonths(1);
            time = new DateTime(time.Year, time.Month, 1);
            command.Parameters.AddWithValue("@c", time);

            Utils.DBManager.Insert(command);

            this.Close();
        }
    }
}
