using System;
using DPUruNet;
using System.Data;
using System.Drawing;
using PakMotors.dialogs;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace PakMotors.Utils
{
    class DigitalPersonaUtil
    {
        public static DigitalPersonaUtil _;

        private bool IsReaderOpened;
        private Reader FingerPrintReader;
        private EnrollmentDialog EnrolmentDialog;

        static DigitalPersonaUtil() { _ = new DigitalPersonaUtil(); }
        private DigitalPersonaUtil() {
            this.IsReaderOpened = false;
            this.EnrolmentDialog = new dialogs.EnrollmentDialog();
        }

        private void SetUpReader()
        {
            if (IsReaderOpened) throw new Exception("FingerPrint reader is still in use, Please finish all the other tasks!");

            FingerPrintReader = ReaderCollection.GetReaders()[0];

            FingerPrintReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE); IsReaderOpened = true;
            FingerPrintReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, FingerPrintReader.Capabilities.Resolutions[0]);
        }

        public bool IsAvailable()
        {
            return ReaderCollection.GetReaders().Count > 0 && !IsReaderOpened;
        }

        public void StartCapturing(PictureBox pictureBox, Form form)
        {
            try
            {
                this.SetUpReader();

                FingerPrintReader.On_Captured += new Reader.CaptureCallback(captureResult =>
                {
                    foreach (Fid.Fiv fiv in captureResult.Data.Views)
                        SendMessage(Action.CaptureModeApplyImage, pictureBox, form, GenerateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show("An Error was Occurred in FingerPrint Device " + ex);
            }
        }

        public void StartEnrolement(Models.Person person)
        {
            EnrolmentDialog.ShowDialog();

            if (EnrolmentDialog.Result != null || EnrolmentDialog.Result != "") { person.Enrolment = EnrolmentDialog.Result; Console.WriteLine(EnrolmentDialog.Result); Console.WriteLine(EnrolmentDialog.Result); Console.WriteLine(EnrolmentDialog.Result); };
        }
        public void __EnrolmentWorker(PictureBox pictureBox, Label message, Label result, Form form)
        {
            try
            {
                var count = 0;
                var preEnrolmentFMD = new List<Fmd>();

                SetUpReader();

                message.Text = "Place Thumb or Finger on Reader";

                FingerPrintReader.On_Captured += new Reader.CaptureCallback(captureResult => {
                    try
                    {
                        count++; // It Counts the number of Times Captured.
                        
                        foreach (Fid.Fiv fiv in captureResult.Data.Views)
                            if (count < 4) SendMessage(Action.CaptureModeApplyImage, pictureBox, form, GenerateBitmap(fiv.RawImage, fiv.Width, fiv.Height));

                        DataResult<Fmd> conversionResult = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                        SendMessage(Action.EnrollmentMode, pictureBox, message, "An Impression Was Detected, Place again", form);

                        if (conversionResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                            throw new Exception("Error Occurred, Unable to Extract Features from Finger Impression");

                        preEnrolmentFMD.Add(conversionResult.Data);

                        if (count >= 1)
                        {
                            DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preEnrolmentFMD);

                            if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                            {
                                SendMessage(Action.EnrollmentMode, pictureBox, message, "An enrollment FMD was successfully created. and Process is Paused", form);
                                StopAllActivities();

                                var serializedFMD = Fmd.SerializeXml(resultEnrollment.Data);

                                SendMessage(Action.EnrollmentMode, pictureBox, result, serializedFMD, form);
                                return;
                            }
                            else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                            {
                                SendMessage(Action.EnrollmentMode, pictureBox, message, "Enrollment was unsuccessful. Please try again.", form);
                                count = 0;
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Occured while Enrolment");
                    }
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        public void StartIdentificationAndFill(Label label, Form form, DataGridView view, string type)
        {
            string query;

            try
            {
                SetUpReader();

                label.Text = "Please Place a Finger to start Identification";

                FingerPrintReader.On_Captured += new Reader.CaptureCallback(captureResult =>
                {
                    SendMessage1(Action.IdentificationMode, label, "Impression detected, Now Matching...", form);

                    DataResult<Fmd> result = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                    var dataSet = new DataSet();
                    Utils.DBManager.QueryAdapter("SELECT Id, Enrolment FROM Persons").Fill(dataSet);

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        try
                        {
                            var identifyResult = Comparison.Compare(result.Data, 0, Fmd.DeserializeXml("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + row["Enrolment"].ToString()), 0);

                            if (identifyResult.Score < 10000)
                            {
                                var dataTable = new DataTable();
                                Utils.DBManager.QueryAdapter($"SELECT * FROM Persons WHERE Id = {row["Id"]}").Fill(dataTable);

                                view.DataSource = dataTable;

                                SendMessage1(Action.IdentificationMode, label, "Person is Found", form);
                                return;
                            }
                        }
                        catch (Exception) { }
                    }

                    SendMessage1(Action.IdentificationMode, label, "No Match was found.", form);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while Identification: " + ex);
            }
        }

        public void StartIdentificationAndFill1(Label label, Form form, DataGridView view, Label label2)
        {
            try
            {
                SetUpReader();

                label.Text = "Please Place a Finger to start Identification";

                FingerPrintReader.On_Captured += new Reader.CaptureCallback(captureResult =>
                {
                    SendMessage1(Action.IdentificationMode, label, "Impression detected, Now Matching...", form);

                    DataResult<Fmd> result = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                    var dataSet = new DataSet();
                    Utils.DBManager.QueryAdapter("SELECT * FROM Persons").Fill(dataSet);

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        if (row["Enrolment"] != DBNull.Value)
                        {

                            var identifyResult = Comparison.Compare(result.Data, 0, Fmd.DeserializeXml(row["Enrolment"].ToString()), 0);

                            if (identifyResult.Score < 10000)
                            {
                                Console.WriteLine(row["Id"]);

                                SendMessage1(Action.IdentificationMode, label2, row["Id"].ToString(), form);
                                SendMessage1(Action.IdentificationMode, label, "Person is Found", form);
                                return;
                            }
                        }
                    }

                    SendMessage1(Action.IdentificationMode, label, "No Match was found.", form);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while Identification: " + ex);
            }
        }

        public void StartIdentificationAndFill(Label label, Form form, Models.Person person, object[] filler)
        {
            try
            {
                SetUpReader();

                label.Text = "Please Place a Finger to start Identification";

                FingerPrintReader.On_Captured += new Reader.CaptureCallback(captureResult =>
                {
                    SendMessage1(Action.IdentificationMode, label, "Impression detected, Now Matching...", form);

                    DataResult<Fmd> result = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                    var dataSet = new DataSet();
                    Utils.DBManager.QueryAdapter("SELECT * FROM Persons").Fill(dataSet);

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        if (row["Enrolment"] != DBNull.Value)
                        {
                            var identifyResult = Comparison.Compare(result.Data, 0, Fmd.DeserializeXml(row["Enrolment"].ToString()), 0);

                            if (identifyResult.Score < 10000)
                            {
                                person.Fill(row);

                                SendMessage1(Action.IdentificationMode, label, "Person is Found", form);
                                return;
                            }
                        }
                    }

                    SendMessage1(Action.IdentificationMode, label, "No Match was found.", form);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while Identification: " + ex);
            }
        }

        public void StartCapturingMode(PictureBox pictureBox, System.Windows.Forms.Form form)
        {
            FingerPrintReader = ReaderCollection.GetReaders()[0];

            if (IsReaderOpened) throw new Exception("Reader was already Opened by another process.");

            FingerPrintReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE); IsReaderOpened = true;
            FingerPrintReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, FingerPrintReader.Capabilities.Resolutions[0]);

            FingerPrintReader.On_Captured += new Reader.CaptureCallback((captureResult) =>
            {
                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    SendMessage(Action.CaptureModeApplyImage, pictureBox, form, GenerateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                }
            });
        }

        public void StartEnrollmentMode(System.Windows.Forms.PictureBox pictureBox, System.Windows.Forms.Label label, System.Windows.Forms.Form form, string cnic, string type)
        {
            int count = 0;
            var preenrollmentFmds = new List<Fmd>();
            label.Text = "Place Finger";

            FingerPrintReader.On_Captured += new Reader.CaptureCallback((captureResult) =>
            {
                try
                {
                    if (count >= 1)
                    {
                        DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                        if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                        {
                            SendMessage(Action.EnrollmentMode, pictureBox, label, "An enrollment FMD was successfully created. and Process is Paused", form);
                            StopAllActivities();

                            var data = Fmd.SerializeXml(resultEnrollment.Data);

                            var dataAdapter = Utils.DBManager.QueryAdapter("Select * FROM " + type + "_Enrolements WHERE " + type.ToLower() + "_cnic = '" + cnic + "';");
                            var dataSet = new DataSet();

                            dataAdapter.Fill(dataSet);
                            SqlCommand command;
                            if (dataSet.Tables[0].Rows.Count > 0)
                            {
                                Utils.DBManager.Update(new SqlCommand("UPDATE " + type + "_Enrolements SET " + type.ToLower() + "_enrolement = '" + data + "' WHERE " + type.ToLower() + "_cnic = '" + cnic + "';"));
                            }
                            else
                            {
                                Utils.DBManager.Insert(new SqlCommand("INSERT INTO " + type + "_Enrolements(" + type.ToLower() + "_cnic, " + type.ToLower() + "_enrolement) VALUES('" + cnic + "', '" + data + "');"));
                            }

                            return;
                        }
                        else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                        {
                            SendMessage(Action.EnrollmentMode, pictureBox, label, "Enrollment was unsuccessful.Please try again.", form);
                            count = 0;
                            return;
                        }
                    }

                    SendMessage(Action.EnrollmentMode, pictureBox, label, "Now place finger on the reader", form);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Has occured " + ex);
                }
            });
        }

        public void IdentifyPerson(System.Windows.Forms.Label label, System.Windows.Forms.Form form, string type)
        {
            try
            {
                FingerPrintReader = ReaderCollection.GetReaders()[0];

                label.Text = "Waiting For Response";

                if (IsReaderOpened) throw new Exception("Reader was already Opened by another process.");

                FingerPrintReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE); IsReaderOpened = true;
                FingerPrintReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, FingerPrintReader.Capabilities.Resolutions[0]);

                FingerPrintReader.On_Captured += new Reader.CaptureCallback((captureResult) =>
                {
                    SendMessage1(Action.IdentificationMode, label, "Impression detected, Now Matching...", form);

                    DataResult<Fmd> result = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                    var dataAdapter = Utils.DBManager.QueryAdapter("Select * FROM " + type + "_Enrolements;");
                    var dataSet = new DataSet();

                    dataAdapter.Fill(dataSet);

                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        CompareResult identifyResult = Comparison.Compare(result.Data, 0, Fmd.DeserializeXml(dataSet.Tables[0].Rows[i][type.ToLower() + "_enrolement"].ToString()), 0);
                        if (identifyResult.Score < 10000)
                        {
                            SendMessage1(Action.IdentificationMode, label, dataSet.Tables[0].Rows[i][type.ToLower() + "_cnic"].ToString(), form);
                            StopAllActivities();
                            return;
                        }
                    }
                    SendMessage1(Action.IdentificationMode, label, "Didn't Found any match", form);
                    StopAllActivities();
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public void IdentifyPerson(System.Windows.Forms.Label label, System.Windows.Forms.Label label2, System.Windows.Forms.Form form, Fmd fmd)
        {
            try
            {
                FingerPrintReader = ReaderCollection.GetReaders()[0];

                label.Text = "Waiting For Response";

                if (IsReaderOpened) throw new Exception("Reader was already Opened by another process.");

                FingerPrintReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE); IsReaderOpened = true;
                FingerPrintReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, FingerPrintReader.Capabilities.Resolutions[0]);

                FingerPrintReader.On_Captured += new Reader.CaptureCallback((captureResult) =>
                {
                    SendMessage1(Action.IdentificationMode, label, "Impression detected, Now Matching...", form);

                    DataResult<Fmd> result = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                    CompareResult identifyResult = Comparison.Compare(result.Data, 0, fmd, 0);
                    if (identifyResult.Score < 10000)
                    {
                        SendMessage1(Action.IdentificationMode, label2, "Found", form);
                        return;
                    }
                    SendMessage1(Action.IdentificationMode, label2, "Didn't Found any match", form);
                });
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StopAllActivities()
        {
            try
            {
                FingerPrintReader = ReaderCollection.GetReaders()[0];

                FingerPrintReader.Dispose(); IsReaderOpened = false;
            }
            catch(Exception)
            {

            }
        }


        /// <summary>
        /// The Digital Persona API returns the Captured Image in the form of an array
        /// of bytes (a Raw Image). No System library is available to draw a raw Image
        /// to Screen So, We need to convert that raw bytes to a structured image, the
        /// Bitmap is the most lightweight structure and close to raw image, So this
        /// method is created to do so.
        /// </summary>
        /// 
        /// <param name="bytes">stores the raw image array</param>
        /// <param name="width">stores the width of the image</param>
        /// <param name="height">stores the height of the image</param>
        /// 
        /// <returns>bitmap image</returns> 
        ///
        public static Bitmap GenerateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++) rgbBytes[(i * 3) + 2] = rgbBytes[(i * 3) + 1] = rgbBytes[(i * 3)] = bytes[i];

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        #region CaptureThreadMessagePassing
        private enum Action
        {
            CaptureModeApplyImage,
            EnrollmentMode,
            IdentificationMode
        }
        private delegate void CaptureMessageCallback(Action action, object requiredFor, object form, object payload);
        private void SendMessage(Action action, object requiredFor, object form, object payload)
        {
            switch (action)
            {
                case Action.CaptureModeApplyImage:
                    if ((requiredFor as System.Windows.Forms.PictureBox).InvokeRequired)
                    {
                        var callback = new CaptureMessageCallback(SendMessage);
                        (form as System.Windows.Forms.Form).Invoke(callback, new object[] { action, requiredFor, form, payload });
                    }
                    else
                    {
                        (requiredFor as System.Windows.Forms.PictureBox).Image = (Bitmap)payload;
                        (requiredFor as System.Windows.Forms.PictureBox).Refresh();
                    }
                    break;
            }
        }
        #endregion

        #region EnrollmentThreadMessagePassing
        private delegate void EnrollmentMessageCallback(Action action, object requiredFor, object label, object message, object form);
        private void SendMessage(Action action, object requiredFor, object label, object message, object form)
        {
            switch (action)
            {
                case Action.EnrollmentMode:
                    if ((requiredFor as System.Windows.Forms.PictureBox).InvokeRequired)
                    {
                        var callback = new EnrollmentMessageCallback(SendMessage);
                        (form as System.Windows.Forms.Form).Invoke(callback, new object[] { action, requiredFor, label, message, form });
                    }
                    else
                    {
                        (label as System.Windows.Forms.Label).Text = (string)message;
                    }
                    break;
            }
        }
        #endregion

        private delegate void IdentificationMessageCallback(Action action, System.Windows.Forms.Label label, object message, object form);
        private void SendMessage1(Action action, System.Windows.Forms.Label label, object message, object form)
        {
            switch (action)
            {
                case Action.IdentificationMode:
                    if (label.InvokeRequired)
                    {
                        var callback = new IdentificationMessageCallback(SendMessage1);
                        (form as System.Windows.Forms.Form).Invoke(callback, new object[] { action, label, message, form });
                    }
                    else
                    {
                        (label as System.Windows.Forms.Label).Text = (string)message;
                    }
                    break;
            }
        }
    }
}
