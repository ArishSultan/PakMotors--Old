using System;
using System.Windows.Forms;

namespace PakMotors.dialogs
{
    public partial class EnrollmentDialog : Form
    {
        public string Result;

        public EnrollmentDialog()
        {
            InitializeComponent();

            label2.TextChanged += (sender, e) => Result = label2.Text;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Utils.DigitalPersonaUtil._.__EnrolmentWorker(pictureBox1, label1, label2, this);
        }
    }
}
