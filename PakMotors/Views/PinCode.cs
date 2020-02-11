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
    public partial class PinCode : Form
    {
        int id;
        public PinCode(int id)
        {
            InitializeComponent();

            this.id = id;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "ABCD")
            {
                var monthAccounts = new DataSet();

                Utils.DBManager.QueryAdapter($"SELECT * FROM MonthAccounts WHERE AccountId = {this.id}").Fill(monthAccounts);

                foreach (DataRow row in monthAccounts.Tables[0].Rows)
                {
                    Utils.DBManager.Delete($"DELETE FROM MonthAccountTransactions Where MonthAccountId = {row["Id"]}");
                    Utils.DBManager.Delete($"DELETE FROM MonthAccounts Where Id = {row["Id"]}");
                }

                Utils.DBManager.Delete($"DELETE FROM Accounts WHERE Id = {this.id}");
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Pin Code");
                this.Close();
            }
        }
    }
}
