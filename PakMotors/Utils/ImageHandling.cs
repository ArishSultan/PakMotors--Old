using System.Drawing;
using PakMotors.Dialogs;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PakMotors.Utils
{
    class ImageHandling
    {
        private static CameraDialog camera = new CameraDialog();

        public static void InsertInList(List<Image> images, FlowLayoutPanel list)
        {
            foreach (Image image in images)
            {
                var pictureBox = new PictureBox();

                pictureBox.Width = 117;
                pictureBox.Height = 114;

                pictureBox.Image = (Image)image.Clone();
                pictureBox.Click += (sender, e) =>
                {
                    var dialog = new dialogs.ShowImage((sender as PictureBox).Image);

                    dialog.ShowDialog();

                    if (!dialog.status)
                    {
                        list.Controls.Remove(sender as Control);
                    }
                };
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                list.Controls.Add(pictureBox);
            }
        }

        public static void InsertInList(string[] imagePaths, FlowLayoutPanel list)
        {
            List<Image> images = new List<Image>();

            foreach (string imagePath in imagePaths)
                images.Add(new Bitmap(imagePath));

            InsertInList(images, list);
        }

        public static FileDialog getImageFilePicker(bool  multipleFilesFlag)
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.Title            = "Browse a Picture";
            fileDialog.Filter           = "Images (*.bmp;*.jpg;*.gif,*.png,*.tiff, *.jpeg)|*.bmp;*.jpg;*.gif;*.png;*.tiff,*.jpeg";
            fileDialog.DefaultExt       = "png";
            fileDialog.Multiselect      = multipleFilesFlag;
            fileDialog.InitialDirectory = @"C:\";

            return fileDialog;
        }

        public static void AddImageWithFilePicker(PictureBox destination)
        {
            if (destination != null)
            {
                var filePicker = new OpenFileDialog();

                filePicker.Title = "Browse a Picture";
                filePicker.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF";
                filePicker.DefaultExt = "png";
                filePicker.Multiselect = false;
                filePicker.InitialDirectory = @"C:\";

                var result = filePicker.ShowDialog();

                if (result == DialogResult.OK)
                    destination.Image = new Bitmap(filePicker.FileName);
            }
        }

        public static void AddImageWithCamera(PictureBox destination)
        {
            if (destination != null)
            {
                camera.ShowDialog();

                if (camera.images.Count > 0)
                    destination.Image = camera.images[camera.images.Count - 1];

                camera.images.Clear();
            }
        }

        public static void ExportImage(PictureBox pictureBox)
        {
            var sfd = new SaveFileDialog();

            sfd.Filter = "*.jpg|*.jpg";

            var res = sfd.ShowDialog();

            if (res == DialogResult.OK)
            {
                pictureBox.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Saved Image");
            }
        }
    }
}
