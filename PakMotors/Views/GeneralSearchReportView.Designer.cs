
namespace PakMotors.Views
{
    partial class GeneralSearchReportView
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
            this.generalSearchReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // generalSearchReportViewer
            // 
            this.generalSearchReportViewer.ActiveViewIndex = -1;
            this.generalSearchReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.generalSearchReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.generalSearchReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalSearchReportViewer.Location = new System.Drawing.Point(0, 0);
            this.generalSearchReportViewer.Name = "generalSearchReportViewer";
            this.generalSearchReportViewer.Size = new System.Drawing.Size(1329, 617);
            this.generalSearchReportViewer.TabIndex = 0;
            this.generalSearchReportViewer.Load += new System.EventHandler(this.generalSearchReportViewer_Load);
            // 
            // GeneralSearchReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 617);
            this.Controls.Add(this.generalSearchReportViewer);
            this.Name = "GeneralSearchReportView";
            this.Text = "GeneralSearchReportView";
            this.Load += new System.EventHandler(this.GeneralSearchReportView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer generalSearchReportViewer;
    }
}