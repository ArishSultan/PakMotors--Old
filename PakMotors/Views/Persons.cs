using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PakMotors.Views
{
    public partial class Persons : Form
    {
        public static Models.Person RecentPerson = null;

        private bool mode = false;

        private object temp;

        public Persons()
        {
            InitializeComponent();
            searchOptions.SelectedIndex = 0;
            temp = personsDataGridView.DataSource;
            this.WindowState = FormWindowState.Maximized;

            IdentifyLabel.TextChanged += (sender, e) =>
            {
                if (IdentifyLabel.Text == "Person is Found")
                {
                    var dataTable = new DataTable();

                    int id = int.Parse(label3.Text);
                    Utils.DBManager.QueryAdapter($"SELECT * FROM Persons WHERE Id = {id}").Fill(dataTable);
                    personsDataGridView.DataSource = dataTable;
                }
            };
        }

        public Persons(bool mode): this()
        {
            this.mode = mode;
            button2.Enabled = !mode;
        }

        private void PersonsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.personsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pakMotorsDataSet);
        }

        private void Persons_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pakMotorsDataSet.Persons' table. You can move, or remove it, as needed.
            this.personsTableAdapter.Fill(this.pakMotorsDataSet.Persons);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new Dialogs.CreatePerson().ShowDialog();
            this.personsTableAdapter.Fill(this.pakMotorsDataSet.Persons);
        }

        private void PersonsDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (mode)
            {
                Models.Person per = new Models.Person();
                per.Fetch((int)personsDataGridView.SelectedRows[0].Cells[0].Value);
                RecentPerson = per;

                this.Close();
            }
            else
            {
                if (personsDataGridView.SelectedRows.Count > 0) new Dialogs.CreatePerson((int)personsDataGridView.SelectedRows[0].Cells[0].Value).ShowDialog();
                this.personsTableAdapter.Fill(this.pakMotorsDataSet.Persons);
            }
        }

        private void IdentifyButton_Click(object sender, EventArgs e)
        {
            if (IdentifyButton.Text == "Identify")
            {
                if (Utils.DigitalPersonaUtil._.IsAvailable())
                {
                    IdentifyButton.Text = "Cancel";
                    Utils.DigitalPersonaUtil._.StartIdentificationAndFill1(IdentifyLabel, this, personsDataGridView, label3);
                }
            }
            else
            {
                IdentifyLabel.Text = "";
                IdentifyButton.Text = "Identify";

                Utils.DigitalPersonaUtil._.StopAllActivities();
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                this.personsDataGridView.DataSource = temp;
                this.personsTableAdapter.Fill(this.pakMotorsDataSet.Persons);
            }
            else
            {
                var item = searchOptions.SelectedItem.ToString();

                string query;

                if (item == "Phone")
                {
                    query = $"SELECT * FROM Persons WHERE Phone1 Like '" + searchBox.Text + "%' or Phone2 Like '" + searchBox.Text + "%'";
                }
                else
                {
                    query = $"SELECT * FROM Persons WHERE {item} Like '%" + searchBox.Text + "%'";
                }


                var dataAdapter = Utils.DBManager.QueryAdapter(query);
                var table = new DataTable();

                dataAdapter.Fill(table);

                this.personsDataGridView.DataSource = table;
            }
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Control | Keys.S:
                {
                    personsBindingNavigatorSaveItem.PerformClick();
                    return true;
                }
                case Keys.Control | Keys.R:
                {
                    searchBox.Text = "";

                    this.personsDataGridView.DataSource = temp;
                    this.personsTableAdapter.Fill(this.pakMotorsDataSet.Persons);
                    return true;
                }
                case Keys.Control | Keys.F:
                {
                    searchBox.Focus();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void Label3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in personsDataGridView.SelectedRows)
            {
                Utils.DBManager.Update(new SqlCommand($"Update Persons Set IsDeleted = 1 WHERE id = {row.Cells[0].Value}"));
            }

            this.personsTableAdapter.Fill(this.pakMotorsDataSet.Persons);
        }
    }
}
