using System;
using DPUruNet;
using System.Drawing;
using PakMotors.utils;
using System.Windows.Forms;


namespace PakMotors
{
    public partial class Form1 : Form
    {
        private DigitalPersonaUtil fingerPrintUtil = new DigitalPersonaUtil();

        private Reader reader = ReaderCollection.GetReaders()[0];
        utils.WebCam web;

        public Form1()
        {
            InitializeComponent();
             web = new utils.WebCam();

            web.InitializeWebCam(ref pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //web.Start();

            //System.IO.File.WriteAllText(@"D:\\mytext2.txt", Fmd.SerializeXml(Fmd.DeserializeXml(System.IO.File.ReadAllText(@"D:\\mytext.txt"))));
            //fingerPrintUtil.StartEnrollmentMode(pictureBox1, label1, this);
            //fingerPrintUtil.IdentifyPerson(label1, Fmd.DeserializeXml(System.IO.File.ReadAllText(@"D:\\mytext.txt")), this);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //fingerPrintUtil.StopAllActivities();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
