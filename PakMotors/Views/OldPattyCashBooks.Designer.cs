namespace PakMotors.Views
{
    partial class OldPattyCashBooks
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pakMotorsDataSet = new PakMotors.PakMotorsDataSet();
            this.pattyCashBookBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pattyCashBookTableAdapter = new PakMotors.PakMotorsDataSetTableAdapters.PattyCashBookTableAdapter();
            this.tableAdapterManager = new PakMotors.PakMotorsDataSetTableAdapters.TableAdapterManager();
            this.pattyCashBookDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pakMotorsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pattyCashBookBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pattyCashBookDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // pakMotorsDataSet
            // 
            this.pakMotorsDataSet.DataSetName = "PakMotorsDataSet";
            this.pakMotorsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pattyCashBookBindingSource
            // 
            this.pattyCashBookBindingSource.DataMember = "PattyCashBook";
            this.pattyCashBookBindingSource.DataSource = this.pakMotorsDataSet;
            // 
            // pattyCashBookTableAdapter
            // 
            this.pattyCashBookTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AccountsTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CarImagesTableAdapter = null;
            this.tableAdapterManager.CarsTableAdapter = null;
            this.tableAdapterManager.CarTransactionsTableAdapter = null;
            this.tableAdapterManager.MonthAccountsTableAdapter = null;
            this.tableAdapterManager.MonthAccountTransactionsTableAdapter = null;
            this.tableAdapterManager.PattyCashBookTableAdapter = this.pattyCashBookTableAdapter;
            this.tableAdapterManager.PattyCashBookTransactionsTableAdapter = null;
            //this.tableAdapterManager. = null;
            this.tableAdapterManager.UpdateOrder = PakMotors.PakMotorsDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // pattyCashBookDataGridView
            // 
            this.pattyCashBookDataGridView.AllowUserToAddRows = false;
            this.pattyCashBookDataGridView.AutoGenerateColumns = false;
            this.pattyCashBookDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pattyCashBookDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pattyCashBookDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.pattyCashBookDataGridView.DataSource = this.pattyCashBookBindingSource;
            this.pattyCashBookDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pattyCashBookDataGridView.Location = new System.Drawing.Point(0, 0);
            this.pattyCashBookDataGridView.Name = "pattyCashBookDataGridView";
            this.pattyCashBookDataGridView.RowHeadersWidth = 51;
            this.pattyCashBookDataGridView.RowTemplate.Height = 24;
            this.pattyCashBookDataGridView.Size = new System.Drawing.Size(507, 553);
            this.pattyCashBookDataGridView.TabIndex = 1;
            this.pattyCashBookDataGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PattyCashBookDataGridView_RowHeaderMouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Date";
            dataGridViewCellStyle3.Format = "D";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Date";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // OldPattyCashBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 553);
            this.Controls.Add(this.pattyCashBookDataGridView);
            this.Name = "OldPattyCashBooks";
            this.Text = "OldPattyCashBooks";
            this.Load += new System.EventHandler(this.OldPattyCashBooks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pakMotorsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pattyCashBookBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pattyCashBookDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PakMotorsDataSet pakMotorsDataSet;
        private System.Windows.Forms.BindingSource pattyCashBookBindingSource;
        private PakMotorsDataSetTableAdapters.PattyCashBookTableAdapter pattyCashBookTableAdapter;
        private PakMotorsDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView pattyCashBookDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}