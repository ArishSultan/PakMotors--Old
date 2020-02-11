using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Data.SqlTypes;

namespace PakMotors.Models
{
    public class Person
    {
        private string _cnic;

        public int Id { get; set; }
        
        public bool IsBuyer { get; set; }
        public bool IsSeller { get; set; }
        public bool IsWitness { get; set; }

        public string Name { get; set; }
        public string CNIC {
            get
            {
                return _cnic;
            }
            set
            {
                if (value != "" &&  value != null)
                {
                    if (value.Length < 13)
                        throw new Exception("CNIC value is invalid. Invalid Number Length not 15");

                    if (value.Length > 15)
                        throw new Exception("CNIC value is invalid. Too Long");

                    this._cnic = value;
                } else this._cnic = "";
            }
        }
        public string Cast { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Address { get; set; }
        public string Enrolment { get; set; }
        public string FatherName { get; set; }

        public List<Image> Images = new List<Image>();

        public Image Picture { get; set; }
        public Image Signature { get; set; }
        public Image ThumbPrint { get; set; }

        public Person() {
            Picture = null;
            Signature = null;
            ThumbPrint = null;
        }

        public void Fill(DataRow person)
        {
            if (person != null)
            {
                this.Id = (int) person["Id"];

                if (person["Name"] != DBNull.Value) this.Name = person["Name"].ToString();

                if (person["Cast"] != DBNull.Value) this.Cast = person["Cast"].ToString();

                if (person["CNIC"] != DBNull.Value) this.CNIC = person["CNIC"].ToString();

                if (person["Phone1"] != DBNull.Value) this.Phone1 = person["Phone1"].ToString();

                if (person["Phone2"] != DBNull.Value) this.Phone2 = person["Phone2"].ToString();

                if (person["Address"] != DBNull.Value) this.Address = person["Address"].ToString();

                if (person["Enrolment"] != DBNull.Value) this.Enrolment = person["Enrolment"].ToString();

                if (person["FatherName"] != DBNull.Value) this.FatherName = person["FatherName"].ToString();

                if (person["IsBuyer"] != DBNull.Value) this.IsBuyer = (Boolean)person["IsBuyer"];

                if (person["IsSeller"] != DBNull.Value) this.IsSeller = (Boolean)person["IsSeller"];

                if (person["IsWitness"] != DBNull.Value) this.IsWitness = (Boolean)person["IsWitness"];

                if (person["Picture"] != DBNull.Value) this.Picture = new Bitmap(new MemoryStream((byte[])(object)person["Picture"])); ;

                if (person["Signature"] != DBNull.Value) this.Signature = new Bitmap(new MemoryStream((byte[])(object)person["Signature"])); ;

                if (person["FingerPrint"] != DBNull.Value) this.ThumbPrint = new Bitmap(new MemoryStream((byte[])(object)person["FingerPrint"])); ;

                foreach (DataRow row in Utils.DBManager.Query("PersonImages", "PersonId", (int)person["Id"])) this.Images.Add(Image.FromStream(new MemoryStream((Byte[])row["Image"])));
            }
            else throw new Exception("Person was not found");
        }

        public bool Fetch(int id)
        {
            var res = Utils.DBManager.Query("Persons", "Id", id);

            // Fetch Person;
            if (res.Count > 0)
            {
                this.Fill(res[0]);
                return true;
            }

            return false;
        }

        public bool Fetch(string CNIC)
        {
            // Fetch Person;

            var res = Utils.DBManager.Query("Persons", $"CNIC = '{CNIC}'");

            // Fetch Person;
            if (res.Count > 0)
            {
                this.Fill(res[0]);
                return true;
            }

            return false;
        }

        public static void Fill(Person person, object[] array)
        {
            person.Name       = (array[0] as TextBox).Text;
            person.Cast       = (array[1] as TextBox).Text;
            person.CNIC       = (array[2] as MaskedTextBox).Text;
            person.Phone1     = (array[3] as MaskedTextBox).Text;
            person.Phone2     = (array[4] as MaskedTextBox).Text;
            person.Address    = (array[5] as TextBox).Text;
            person.FatherName = (array[6] as TextBox).Text;

            person.Picture    = (array[7] as PictureBox).Image;
            person.Signature  = (array[8] as PictureBox).Image;
            person.ThumbPrint = (array[9] as PictureBox).Image;

            person.Images.Clear();
            foreach (var control in ((System.Windows.Forms.FlowLayoutPanel)array[10]).Controls) person.Images.Add((control as PictureBox).Image);
        }

        public static void Fill(object[] array, Person person)
        {
            Console.WriteLine(person);

            (array[0] as TextBox).Text = person.Name;
            (array[1] as TextBox).Text = person.Cast;
            (array[2] as MaskedTextBox).Text = person.CNIC;
            (array[3] as MaskedTextBox).Text = person.Phone1;
            (array[4] as MaskedTextBox).Text = person.Phone2;
            (array[5] as TextBox).Text = person.Address;
            (array[6] as TextBox).Text = person.FatherName;

            (array[7] as PictureBox).Image = person.Picture;
            (array[8] as PictureBox).Image = person.Signature;
            (array[9] as PictureBox).Image = person.ThumbPrint;

            Utils.ImageHandling.InsertInList(person.Images, (System.Windows.Forms.FlowLayoutPanel)array[10]);
        }

        private static string FormatCNIC(string cnic)
        {
            cnic = cnic.Replace("-", "");
            return $"{cnic.Substring(0, 5)}-{cnic.Substring(5, 7)}-{cnic[12]}";
        }

        public static void Insert(Person person, bool buyer, bool seller, bool witness)
        {
            MemoryStream ms1, ms2, ms3;
            byte[] b1 = null, b2 = null, b3 = null;

            if (person.Picture != null)
            {
                ms1 = new System.IO.MemoryStream();
                person.Picture.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                b1 = ms1.ToArray();
            }

            if (person.Signature != null)
            {
                ms2 = new System.IO.MemoryStream();
                person.Signature.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                b2 = ms2.ToArray();
            }

            if (person.ThumbPrint != null)
            {
                ms3 = new System.IO.MemoryStream();
                person.ThumbPrint.Save(ms3, System.Drawing.Imaging.ImageFormat.Jpeg);
                b3 = ms3.ToArray();
            }

            var enrolment = (person.Enrolment == "" || person.Enrolment == null) ? (object)DBNull.Value : (object)new SqlXml(XmlReader.Create(new StringReader(person.Enrolment)));

            person.Id = Utils.DBManager.Insert(
                "Persons", 
                new string[]    { "Name",            "FatherName",      "CNIC",            "Cast",            "Phone1",          "Phone2",          "Address",          "Picture",       "Signature",     "FingerPrint",   "Enrolment",      "IsBuyer",     "IsSeller",    "IsWitness"   },
                new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.NVarChar, SqlDbType.Image, SqlDbType.Image, SqlDbType.Image, SqlDbType.Xml,    SqlDbType.Bit, SqlDbType.Bit, SqlDbType.Bit },
                new object[]    { person.Name,       person.FatherName, person.CNIC,       person.Cast,       person.Phone1,     person.Phone2,     person.Address,     b1,              b2,              b3,              enrolment,        buyer,         seller,        witness       }
            );

            Utils.DBManager.InsertAllPersonImages(person.Id, person.Images);
        }

        public  static void Update(Person person)
        {
            MemoryStream ms1, ms2, ms3;
            byte[] b1 = null, b2 = null, b3 = null;

            if (person.Picture != null)
            {
                Console.WriteLine("Here");
                ms1 = new System.IO.MemoryStream();
                person.Picture.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                b1 = ms1.ToArray();
            }

            if (person.Signature != null)
            {
                Console.WriteLine("Here");
                ms2 = new System.IO.MemoryStream();
                person.Signature.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                b2 = ms2.ToArray();
            }

            if (person.ThumbPrint != null)
            {
                Console.WriteLine("Here");
                ms3 = new System.IO.MemoryStream();
                person.ThumbPrint.Save(ms3, System.Drawing.Imaging.ImageFormat.Jpeg);
                b3 = ms3.ToArray();
            }

            var enrolment = (person.Enrolment == "" || person.Enrolment == null) ? (object)DBNull.Value : (object)new SqlXml(XmlReader.Create(new StringReader(person.Enrolment)));

            Utils.DBManager.Update(
                "Persons",
                person.Id,
                new string[]    { "Name",            "FatherName",      "CNIC",            "Cast",            "Phone1",          "Phone2",          "Address",         "Picture",       "Signature",     "FingerPrint",   "Enrolment",      "IsBuyer",      "IsSeller",      "IsWitness"      },
                new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Image, SqlDbType.Image, SqlDbType.Image, SqlDbType.Xml,    SqlDbType.Bit,  SqlDbType.Bit,   SqlDbType.Bit    },
                new object[]    { person.Name,       person.FatherName, person.CNIC,       person.Cast,       person.Phone1,     person.Phone2,     person.Address,    b1,              b2,              b3,              enrolment,        person.IsBuyer, person.IsSeller, person.IsWitness }
            );

            Utils.DBManager.DeleteAllPersonImages(person.Id);
            Utils.DBManager.InsertAllPersonImages(person.Id, person.Images);
        }

        public override string ToString()
        {
            return $"Name:       {Name}\n" +
                   $"FatherName: {FatherName}\n" +
                   $"CNIC:       {CNIC}\n" +
                   $"Cast:       {Cast}\n" +
                   $"Phone1:     {Phone1}\n" +
                   $"Phone1:     {Phone2}\n" +
                   $"Address:    {Address}\n" +
                   $"Enrolment:  {Enrolment}";
        }
    }
}
