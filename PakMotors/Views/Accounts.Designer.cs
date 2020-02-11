namespace PakMotors.Views
{
    partial class Accounts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Accounts));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pakMotorsDataSet = new PakMotors.PakMotorsDataSet();
            this.monthAccountTransactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.monthAccountTransactionsTableAdapter = new PakMotors.PakMotorsDataSetTableAdapters.MonthAccountTransactionsTableAdapter();
            this.tableAdapterManager = new PakMotors.PakMotorsDataSetTableAdapters.TableAdapterManager();
            this.monthAccountTransactionsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.monthAccountTransactionsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.monthAccountTransactionsDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.accountsList = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.currentBalance = new System.Windows.Forms.Label();
            this.openingBalance = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.accountName = new System.Windows.Forms.Label();
            this.oldRecords = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.print = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pakMotorsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthAccountTransactionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthAccountTransactionsBindingNavigator)).BeginInit();
            this.monthAccountTransactionsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthAccountTransactionsDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pakMotorsDataSet
            // 
            this.pakMotorsDataSet.DataSetName = "PakMotorsDataSet";
            this.pakMotorsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // monthAccountTransactionsBindingSource
            // 
            this.monthAccountTransactionsBindingSource.DataMember = "MonthAccountTransactions";
            this.monthAccountTransactionsBindingSource.DataSource = this.pakMotorsDataSet;
            // 
            // monthAccountTransactionsTableAdapter
            // 
            this.monthAccountTransactionsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AccountsTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CarImagesTableAdapter = null;
            this.tableAdapterManager.CarsTableAdapter = null;
            this.tableAdapterManager.CarTransactionsTableAdapter = null;
            this.tableAdapterManager.MonthAccountsTableAdapter = null;
            this.tableAdapterManager.MonthAccountTransactionsTableAdapter = this.monthAccountTransactionsTableAdapter;
            this.tableAdapterManager.PattyCashBookTableAdapter = null;
            this.tableAdapterManager.PattyCashBookTransactionsTableAdapter = null;
            this.tableAdapterManager.PersonsTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = PakMotors.PakMotorsDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // monthAccountTransactionsBindingNavigator
            // 
            this.monthAccountTransactionsBindingNavigator.AddNewItem = null;
            this.monthAccountTransactionsBindingNavigator.BindingSource = this.monthAccountTransactionsBindingSource;
            this.monthAccountTransactionsBindingNavigator.CountItem = null;
            this.monthAccountTransactionsBindingNavigator.DeleteItem = null;
            this.monthAccountTransactionsBindingNavigator.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.monthAccountTransactionsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorDeleteItem,
            this.monthAccountTransactionsBindingNavigatorSaveItem});
            this.monthAccountTransactionsBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.monthAccountTransactionsBindingNavigator.MoveFirstItem = null;
            this.monthAccountTransactionsBindingNavigator.MoveLastItem = null;
            this.monthAccountTransactionsBindingNavigator.MoveNextItem = null;
            this.monthAccountTransactionsBindingNavigator.MovePreviousItem = null;
            this.monthAccountTransactionsBindingNavigator.Name = "monthAccountTransactionsBindingNavigator";
            this.monthAccountTransactionsBindingNavigator.PositionItem = null;
            this.monthAccountTransactionsBindingNavigator.Size = new System.Drawing.Size(1363, 27);
            this.monthAccountTransactionsBindingNavigator.TabIndex = 0;
            this.monthAccountTransactionsBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.BindingNavigatorDeleteItem_Click_1);
            // 
            // monthAccountTransactionsBindingNavigatorSaveItem
            // 
            this.monthAccountTransactionsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.monthAccountTransactionsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("monthAccountTransactionsBindingNavigatorSaveItem.Image")));
            this.monthAccountTransactionsBindingNavigatorSaveItem.Name = "monthAccountTransactionsBindingNavigatorSaveItem";
            this.monthAccountTransactionsBindingNavigatorSaveItem.Size = new System.Drawing.Size(29, 24);
            this.monthAccountTransactionsBindingNavigatorSaveItem.Text = "Save Data";
            this.monthAccountTransactionsBindingNavigatorSaveItem.Click += new System.EventHandler(this.MonthAccountTransactionsBindingNavigatorSaveItem_Click);
            // 
            // monthAccountTransactionsDataGridView
            // 
            this.monthAccountTransactionsDataGridView.AllowUserToAddRows = false;
            this.monthAccountTransactionsDataGridView.AutoGenerateColumns = false;
            this.monthAccountTransactionsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.monthAccountTransactionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.monthAccountTransactionsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn6});
            this.monthAccountTransactionsDataGridView.DataSource = this.monthAccountTransactionsBindingSource;
            this.monthAccountTransactionsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthAccountTransactionsDataGridView.Location = new System.Drawing.Point(0, 27);
            this.monthAccountTransactionsDataGridView.Name = "monthAccountTransactionsDataGridView";
            this.monthAccountTransactionsDataGridView.RowHeadersWidth = 51;
            this.monthAccountTransactionsDataGridView.RowTemplate.Height = 24;
            this.monthAccountTransactionsDataGridView.Size = new System.Drawing.Size(1363, 566);
            this.monthAccountTransactionsDataGridView.TabIndex = 1;
            this.monthAccountTransactionsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MonthAccountTransactionsDataGridView_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.accountsList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 693);
            this.panel1.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.DodgerBlue;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::PakMotors.Properties.Resources.outline_remove_white_18dp;
            this.button4.Location = new System.Drawing.Point(191, 558);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(54, 54);
            this.button4.TabIndex = 14;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::PakMotors.Properties.Resources.baseline_add_white_18dp1;
            this.button1.Location = new System.Drawing.Point(191, 626);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 54);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // accountsList
            // 
            this.accountsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountsList.Location = new System.Drawing.Point(0, 0);
            this.accountsList.Name = "accountsList";
            this.accountsList.Size = new System.Drawing.Size(258, 693);
            this.accountsList.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.currentBalance);
            this.panel3.Controls.Add(this.openingBalance);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.accountName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1363, 100);
            this.panel3.TabIndex = 7;
            // 
            // currentBalance
            // 
            this.currentBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentBalance.Location = new System.Drawing.Point(1168, 55);
            this.currentBalance.Name = "currentBalance";
            this.currentBalance.Size = new System.Drawing.Size(183, 23);
            this.currentBalance.TabIndex = 4;
            this.currentBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openingBalance
            // 
            this.openingBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openingBalance.Location = new System.Drawing.Point(1168, 9);
            this.openingBalance.Name = "openingBalance";
            this.openingBalance.Size = new System.Drawing.Size(183, 23);
            this.openingBalance.TabIndex = 3;
            this.openingBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1045, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current Balance:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1045, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Opening Balance:";
            // 
            // accountName
            // 
            this.accountName.AutoSize = true;
            this.accountName.Font = new System.Drawing.Font("Google Sans", 24F);
            this.accountName.Location = new System.Drawing.Point(6, 23);
            this.accountName.Name = "accountName";
            this.accountName.Size = new System.Drawing.Size(174, 50);
            this.accountName.TabIndex = 0;
            this.accountName.Text = "Account";
            // 
            // oldRecords
            // 
            this.oldRecords.Location = new System.Drawing.Point(15, 25);
            this.oldRecords.Name = "oldRecords";
            this.oldRecords.Size = new System.Drawing.Size(165, 23);
            this.oldRecords.TabIndex = 6;
            this.oldRecords.Text = "View Old Records";
            this.oldRecords.UseVisualStyleBackColor = true;
            this.oldRecords.Click += new System.EventHandler(this.OldRecords_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.oldRecords);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 693);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1621, 67);
            this.panel6.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.print);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.monthAccountTransactionsDataGridView);
            this.panel2.Controls.Add(this.monthAccountTransactionsBindingNavigator);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1363, 593);
            this.panel2.TabIndex = 10;
            // 
            // print
            // 
            this.print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.print.Location = new System.Drawing.Point(1134, -2);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(105, 29);
            this.print.TabIndex = 7;
            this.print.Text = "print";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.Print_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1245, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 27);
            this.button3.TabIndex = 6;
            this.button3.Text = "add_new";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1621, 693);
            this.panel4.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(258, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1363, 693);
            this.panel5.TabIndex = 11;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DebitDate";
            dataGridViewCellStyle1.Format = "D";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn3.HeaderText = "DebitDate";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DebitDescription";
            this.dataGridViewTextBoxColumn4.HeaderText = "DebitDescription";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DebitAmount";
            this.dataGridViewTextBoxColumn2.HeaderText = "DebitAmount";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "MonthAccountId";
            this.dataGridViewTextBoxColumn5.HeaderText = "MonthAccountId";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "CreditDate";
            dataGridViewCellStyle2.Format = "D";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn7.HeaderText = "CreditDate";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "CreditDescription";
            this.dataGridViewTextBoxColumn8.HeaderText = "CreditDescription";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "CreditAmount";
            this.dataGridViewTextBoxColumn6.HeaderText = "CreditAmount";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1621, 760);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel6);
            this.Name = "Accounts";
            this.Text = "Accounts";
            this.Load += new System.EventHandler(this.Accounts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pakMotorsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthAccountTransactionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthAccountTransactionsBindingNavigator)).EndInit();
            this.monthAccountTransactionsBindingNavigator.ResumeLayout(false);
            this.monthAccountTransactionsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthAccountTransactionsDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PakMotorsDataSet pakMotorsDataSet;
        private System.Windows.Forms.BindingSource monthAccountTransactionsBindingSource;
        private PakMotorsDataSetTableAdapters.MonthAccountTransactionsTableAdapter monthAccountTransactionsTableAdapter;
        private PakMotorsDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator monthAccountTransactionsBindingNavigator;
        private System.Windows.Forms.ToolStripButton monthAccountTransactionsBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView monthAccountTransactionsDataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label currentBalance;
        private System.Windows.Forms.Label openingBalance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label accountName;
        private System.Windows.Forms.Button oldRecords;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.FlowLayoutPanel accountsList;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}