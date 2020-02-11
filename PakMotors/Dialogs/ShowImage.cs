using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMotors.dialogs
{
    public partial class ShowImage : Form
    {
        public bool status = true;

        public ShowImage(Image image)
        {
            InitializeComponent();

            this.pictureBox1.Image = image;
        }

        public ShowImage()
        {
            InitializeComponent();

        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            status = true;
            this.Close();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            status = false;
            this.Close();
        }

        private void MaterialFlatButton4_Click(object sender, EventArgs e)
        {
            Utils.ImageHandling.ExportImage(pictureBox1);
        }
    }
}
