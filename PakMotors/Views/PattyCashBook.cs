using System;
using System.Data;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class PattyCashBook : Form
    {
        private int Id;
        private DateTime date = DateTime.Now;

        public PattyCashBook(bool b, int id)
        {
            this.Id = id;

            InitializeComponent();

            panel1.Visible = b;
            this.currentBalance.Text = "0";
            pattyCashBookTransactionsDataGridView.Enabled = b;
            button2.Enabled = button1.Enabled = b;
            pattyCashBookTransactionsBindingNavigator.Visible = b;

            var query1 = Utils.DBManager.Query("SELECT SUM(Amount) as asd FROM PattyCashBookTransactions WHERE PattyCashBookId = " + this.Id);
            try
            {
                this.currentBalance.Text = ((long)query1[0]["asd"]) + "";
            }
            catch (Exception)
            {
                this.currentBalance.Text = "0";
            }
        }

        private void PattyCashBookTransactionsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pattyCashBookTransactionsBindingSource.EndEdit();

            foreach (DataGridViewRow row in pattyCashBookTransactionsDataGridView.Rows)
            {
                Utils.DBManager.Update(
                    "PattyCashBookTransactions",
                    (int)row.Cells[0].Value,
                    new string[] { "Amount", "Description", "PattyCashBookId" },
                    new SqlDbType[] { SqlDbType.BigInt, SqlDbType.Text, SqlDbType.Int },
                    new object[] { row.Cells[2].Value, row.Cells[1].Value, this.Id }
                );
            }

            var query1 = Utils.DBManager.Query("SELECT SUM(Amount) as asd FROM PattyCashBookTransactions WHERE PattyCashBookId = " + this.Id);
            try
            {
                this.currentBalance.Text = ((long)query1[0]["asd"]) + "";
            }
            catch (Exception)
            {
                this.currentBalance.Text = "0";
            }
        }

        private void PattyCashBook_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.PattyCashBookTransactions' table. You can move, or remove it, as needed.
            var data = new DataTable();
            Utils.DBManager.QueryAdapter("SELECT * FROM PattyCashBookTransactions WHERE PattyCashBookId = " + this.Id).Fill(data);
            pattyCashBookTransactionsDataGridView.DataSource = data;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            new Views.OldPattyCashBooks(Id).ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (pattyCashBookTransactionsDataGridView.SelectedRows.Count > 0)
            {
                Utils.DBManager.Delete("DELETE FROM PattyCashBookTransactions WHERE Id = " + (int)pattyCashBookTransactionsDataGridView.SelectedRows[0].Cells[0].Value);

                var data = new DataTable();
                Utils.DBManager.QueryAdapter("SELECT * FROM PattyCashBookTransactions WHERE PattyCashBookId = " + this.Id).Fill(data);
                pattyCashBookTransactionsDataGridView.DataSource = data;
            }
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {

            switch (keys)
            {
                case Keys.Control | Keys.N:
                    {
                        button1.PerformClick();
                        return true;
                    }
                case Keys.Control | Keys.S:
                    {
                        pattyCashBookTransactionsBindingNavigatorSaveItem.PerformClick();
                        return true;
                    }

                case Keys.Control | Keys.P:
                    {
                        button4.PerformClick();
                        return true;
                    }
                case Keys.Delete:
                    {
                        button2.PerformClick();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pattyCashBookTransactionsBindingNavigatorSaveItem.PerformClick();

            Utils.DBManager.Insert(
                "PattyCashBookTransactions",
                new string[] { "Amount", "Description", "PattyCashBookId" },
                new SqlDbType[] { SqlDbType.BigInt, SqlDbType.Text, SqlDbType.Int },
                new object[] { 0, "", this.Id }
            );

            var data = new DataTable();
            Utils.DBManager.QueryAdapter("SELECT * FROM PattyCashBookTransactions WHERE PattyCashBookId = " + this.Id).Fill(data);
            pattyCashBookTransactionsDataGridView.DataSource = data;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            new Views.PattyCashReport(Id).ShowDialog();
        }
    }
}
