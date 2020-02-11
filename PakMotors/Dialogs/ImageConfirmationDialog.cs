using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMotors.Dialogs
{
    public partial class ImageConfirmationDialog : Form
    {
        public bool status = false;

        public ImageConfirmationDialog(Image image)
        {
            InitializeComponent();

            pictureBox1.Image = image;
        }

        public ImageConfirmationDialog()
        {
            InitializeComponent();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            this.status = true;
            this.Close();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            this.status = false;
            this.Close();
        }

        private void MaterialFlatButton3_Click(object sender, EventArgs e)
        {
            Utils.ImageHandling.ExportImage(pictureBox1);
        }
    }
}
