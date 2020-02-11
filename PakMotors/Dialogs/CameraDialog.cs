using System;
using AForge.Video;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using System.Collections.Generic;

namespace PakMotors.Dialogs
{
    public partial class CameraDialog : Form
    {
        private FilterInfoCollection devices;
        private VideoCaptureDevice videoSource;

        public List<System.Drawing.Image> images = new List<System.Drawing.Image>();

        public CameraDialog()
        {
            InitializeComponent();

            devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in devices)
            {
                comboBox1.Items.Add(device.Name);
            }

            if (devices.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                videoSource = new VideoCaptureDevice();
                videoSource = new VideoCaptureDevice(devices[comboBox1.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(handler);
            }
            else
            {
                MessageBox.Show("No Camera is Attached");
                this.Close();
            }
        }

        private void handler(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Image image = (System.Drawing.Image)pictureBox1.Image.Clone();

                var dialog = new ImageConfirmationDialog(image);

                dialog.ShowDialog();

                if (dialog.status)
                {
                    images.Add(image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Camera is Loading, Please Wait " + ex);
            }

            
        }

        private void CameraDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            videoSource.Stop();
        }

        private void CameraDialog_Load(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(devices[comboBox1.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(handler);
            videoSource.Start();
        }
    }
}
