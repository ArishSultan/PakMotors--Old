using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PakMotors.Models
{
    public class Car
    {
        // Stock Entries
        public int Id { get; set; }
        public int PurchasedFrom { get; set; }
        public int PurchasedFromWitness { get; set; }
        public string Sr { get; set; }
        public long PurchaseAmount { get; set; }
        public string PBO { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string HorsePower { get; set; }
        public string RegistrationNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public bool InvoiceRecieved { get; set; }
        public bool InvoiceDelivered { get; set; }
        public DateTime RecievedDate { get; set; }

        public List<Image> Images = new List<Image>();

        public bool CashSaleFlag { get; set; }
        public bool CreditSaleFlag { get; set; }
        public Image CreditPlanDoc { get; set; }

        public bool InvoiceFlag { get; set; }
        public bool RecievedFlag { get; set; }

        // Sales Entries
        public string Note { get; set; }
        public string Buyer { get; set; }
        public bool WarrantyBookRecieved { get; set; }
        public bool WarrantyBookDelivered { get; set; }
        public string NoteSecondary { get; set; }
        public long TotalAmount { get; set; }


        // Links
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int Witness1Id { get; set; }
        public int Witness2Id { get; set; }

        public List<Transaction> CarTransactions = new List<Transaction>();

        public string InvoiceName { get; set; }

        public class Transaction
        {
            public object Amount { get; set; }
            public object Date { get; set; }
            public object IsRecieved { get; set; }
            public object Note { get; set; }
        }

        public Car()
        {
            this.InvoiceRecieved = false;
            this.InvoiceDate = DateTime.Now;
            this.RecievedDate = DateTime.Now;
        }


        public static void Insert(Car car)
        {
            var buyerId = car.BuyerId;
            var sellerId = car.SellerId;
            var witness1Id = car.Witness1Id;
            var witness2Id = car.Witness2Id;

            car.Id = Utils.DBManager.Insert(
                "Cars",
                new string[]    { "Name",            "Model",           "ChassisNo",       "EngineNo",        "RegistrationNo",   "HorsePower",      "Color",           "PBO",             "InvoiceDate",                                              "InvoiceRecieved",   "RecievedDate",                                               "TotalAmount",   "BuyerId",     "SellerId",    "Witness1Id",   "Witness2Id",  "CashSaleFlag",   "CreditSaleFlag",   "Note",         "InvoiceDelivered",   "sr",              "PurchaseAmount",  "InvoiceName",      "NoteSecondary",   "Buyer",           "WarrantyBookRecieved",   "WarrantyBookDelivered",   "InvoiceDateFlag", "InvoiceRecievedFlag", "PurchasedFrom", "PurchasedFromWitness" },
                new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,  SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Date,                                             SqlDbType.Bit,       SqlDbType.Date,                                               SqlDbType.Money, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int,  SqlDbType.Int, SqlDbType.Bit,    SqlDbType.Bit,      SqlDbType.Text, SqlDbType.Bit,        SqlDbType.VarChar, SqlDbType.BigInt,   SqlDbType.VarChar, SqlDbType.Text,    SqlDbType.VarChar, SqlDbType.Bit,            SqlDbType.Bit,             SqlDbType.Bit,     SqlDbType.Bit, SqlDbType.Int, SqlDbType.Int },
                new object[]    { car.Name,          car.Model,         car.ChassisNo,     car.EngineNo,      car.RegistrationNo, car.HorsePower,    car.Color,         car.PBO,           (car.InvoiceFlag ? (object)car.InvoiceDate : DBNull.Value), car.InvoiceRecieved, (car.RecievedFlag ? (object)car.RecievedDate : DBNull.Value), car.TotalAmount, buyerId,       sellerId,      witness1Id,     witness2Id,    car.CashSaleFlag, car.CreditSaleFlag, car.Note,       car.InvoiceDelivered, car.Sr,            car.PurchaseAmount, car.InvoiceName,   car.NoteSecondary, car.Buyer,         car.WarrantyBookRecieved, car.WarrantyBookDelivered, car.InvoiceFlag,   car.RecievedFlag, car.PurchasedFrom, car.PurchasedFromWitness     }
            );

            Utils.DBManager.InsertAllImages(car.Id, car.Images);
            Utils.DBManager.InsertAllTransactions(car.Id, car.CarTransactions);
        }

        public static void Update(Car car)
        {
            var buyerId = car.BuyerId;
            var sellerId = car.SellerId;
            var witness1Id = car.Witness1Id;
            var witness2Id = car.Witness2Id;

            Utils.DBManager.Update(
                "Cars",
                car.Id,
                new string[]    { "Name",            "Model",           "ChassisNo",       "EngineNo",        "RegistrationNo",   "HorsePower",      "Color",           "PBO",             "InvoiceDate",                                            "InvoiceRecieved",   "RecievedDate",                                               "TotalAmount",   "BuyerId",     "SellerId",    "Witness1Id",  "Witness2Id",  "Note",         "CashSaleFlag",   "CreditSaleFlag",   "InvoiceDelivered",   "sr",              "PurchaseAmount",   "InvoiceName",    "NoteSecondary",    "Buyer", "WarrantyBookRecieved", "WarrantyBookDelivered", "InvoiceDateFlag", "InvoiceRecievedFlag", "PurchasedFrom", "PurchasedFromWitness" },
                new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,  SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Date,                                           SqlDbType.Bit,       SqlDbType.Date,                                               SqlDbType.Money, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Text, SqlDbType.Bit,    SqlDbType.Bit,      SqlDbType.Bit,        SqlDbType.VarChar, SqlDbType.BigInt,   SqlDbType.VarChar, SqlDbType.Text,    SqlDbType.VarChar, SqlDbType.Bit, SqlDbType.Bit,  SqlDbType.Bit, SqlDbType.Bit, SqlDbType.Int, SqlDbType.Int },
                new object[]    { car.Name,          car.Model,         car.ChassisNo,     car.EngineNo,      car.RegistrationNo, car.HorsePower,    car.Color,         car.PBO,           (car.InvoiceFlag? (object)car.InvoiceDate: DBNull.Value), car.InvoiceRecieved, (car.RecievedFlag ? (object)car.RecievedDate : DBNull.Value), car.TotalAmount, buyerId,       sellerId,      witness1Id,    witness2Id,    car.Note,       car.CashSaleFlag, car.CreditSaleFlag, car.InvoiceDelivered, car.Sr,            car.PurchaseAmount, car.InvoiceName,   car.NoteSecondary, car.Buyer,         car.WarrantyBookRecieved, car.WarrantyBookDelivered, car.InvoiceFlag, car.RecievedFlag, car.PurchasedFrom, car.PurchasedFromWitness }
            );

            Utils.DBManager.DeleteAllImages(car.Id);
            Utils.DBManager.InsertAllImages(car.Id, car.Images);

            Utils.DBManager.DeleteAllTransactions(car.Id);
            Utils.DBManager.InsertAllTransactions(car.Id, car.CarTransactions);
        }

        private void Fill(DataRow car)
        {
            if (car != null)
            {
                this.Id = (int) car["Id"];

                // String Fields
                if (car["sr"] != DBNull.Value) this.Sr = car["sr"].ToString();
                if (car["PurchaseAmount"] != DBNull.Value) this.PurchaseAmount = (long)car["PurchaseAmount"];
                if (car["PBO"] != DBNull.Value) this.PBO = car["PBO"].ToString();
                if (car["Name"] != DBNull.Value) this.Name = car["Name"].ToString();
                if (car["Model"] != DBNull.Value) this.Model = car["Model"].ToString();
                if (car["Color"] != DBNull.Value) this.Color = car["Color"].ToString();
                if (car["ChassisNo"] != DBNull.Value) this.ChassisNo = car["ChassisNo"].ToString();
                if (car["EngineNo"] != DBNull.Value) this.EngineNo = car["EngineNo"].ToString();
                if (car["HorsePower"] != DBNull.Value) this.HorsePower = car["HorsePower"].ToString();
                if (car["RegistrationNo"] != DBNull.Value) this.RegistrationNo = car["RegistrationNo"].ToString();
                if (car["InvoiceName"] != DBNull.Value) this.InvoiceName = car["InvoiceName"].ToString();

                if (car["InvoiceRecieved"] != DBNull.Value) this.InvoiceRecieved = (Boolean)car["InvoiceRecieved"];
                if (car["InvoiceDelivered"] != DBNull.Value) this.InvoiceDelivered = (Boolean)car["InvoiceDelivered"];

                // Date Fields
                if (car["InvoiceDate"] != DBNull.Value) this.InvoiceDate = DateTime.Parse(car["InvoiceDate"].ToString());
                if (car["RecievedDate"] != DBNull.Value) this.RecievedDate = DateTime.Parse(car["RecievedDate"].ToString());
                if (car["NoteSecondary"] != DBNull.Value) this.NoteSecondary = car["NoteSecondary"].ToString();

                if (car["Buyer"] != DBNull.Value) this.NoteSecondary = car["NoteSecondary"].ToString();
                if (car["WarrantyBookRecieved"] != DBNull.Value) this.WarrantyBookRecieved = (Boolean)car["WarrantyBookRecieved"];
                if (car["WarrantyBookDelivered"] != DBNull.Value) this.WarrantyBookDelivered = (Boolean)car["WarrantyBookDelivered"];

                if (car["TotalAmount"] != DBNull.Value) this.TotalAmount = (long)car["TotalAmount"];
                if (car["BuyerId"] != DBNull.Value) this.BuyerId = (int)car["BuyerId"];
                if (car["SellerId"] != DBNull.Value) this.SellerId = (int)car["SellerId"];
                if (car["Witness1Id"] != DBNull.Value) this.Witness1Id = (int)car["Witness1Id"];
                if (car["Witness2Id"] != DBNull.Value) this.Witness2Id = (int)car["Witness2Id"];
                if (car["PurchasedFrom"] != DBNull.Value) this.PurchasedFrom = (int)car["PurchasedFrom"];
                if (car["PurchasedFromWitness"] != DBNull.Value) this.PurchasedFromWitness = (int)car["PurchasedFromWitness"];
                if (car["CashSaleFlag"] != DBNull.Value) this.CashSaleFlag = (Boolean)car["CashSaleFlag"];
                if (car["CreditSaleFlag"] != DBNull.Value) this.CreditSaleFlag = (Boolean)car["CreditSaleFlag"];
                if (car["Note"] != DBNull.Value) this.Note = car["Note"].ToString();
                if (car["InvoiceDateFlag"] != DBNull.Value) this.InvoiceFlag = (Boolean)car["InvoiceDateFlag"];
                if (car["InvoiceRecievedFlag"] != DBNull.Value) this.RecievedFlag = (Boolean)car["InvoiceRecievedFlag"];
                if (car["CreditPlanDoc"] != DBNull.Value) this.CreditPlanDoc = (Image)Image.FromStream(new MemoryStream((Byte[])car["CreditPlanDoc"]));
            }
            else throw new Exception($"Car was not found");

            foreach (DataRow row in Utils.DBManager.Query("CarImages", "CarId", (int) car["Id"])) this.Images.Add(Image.FromStream(new MemoryStream((Byte[])row["Image"])));

            foreach (DataRow row in Utils.DBManager.Query("CarTransactions", "CarId", (int)car["Id"]))
            {
                this.CarTransactions.Add(new Car.Transaction
                {
                    Amount = row["Amount"],
                    Date = row["Date"],
                    IsRecieved = row["IsRecieved"],
                    Note = row["Note"]
                });
            }
        }

        public bool Fetch(int id)
        {

            var res = Utils.DBManager.Query("Cars", "Id", id);

            if (res.Count > 0)
            {
                this.Fill(res[0]);

                return true;
            }

            return false;
        }

        public bool FetchStock(string column, string value)
        {
            var res = Utils.DBManager.Query("Cars", $"{column} = '{value}' AND CashSaleFlag = 0 AND CreditSaleFlag = 0");

            if (res.Count > 0)
            {
                this.Fill(res[0]);

                return true;
            }

            return false;
        }

        public static void FillStock(object[] toBeFilled, Car car)
        {
            ((System.Windows.Forms.TextBox)toBeFilled[0]).Text = car.Sr;
            ((System.Windows.Forms.TextBox)toBeFilled[1]).Text = car.PurchaseAmount + "";
            ((System.Windows.Forms.TextBox)toBeFilled[2]).Text = car.PBO;
            ((System.Windows.Forms.TextBox)toBeFilled[3]).Text = car.Model;
            ((System.Windows.Forms.TextBox)toBeFilled[4]).Text = car.Color;
            ((System.Windows.Forms.TextBox)toBeFilled[5]).Text = car.EngineNo;
            ((System.Windows.Forms.TextBox)toBeFilled[6]).Text = car.Name;
            ((System.Windows.Forms.TextBox)toBeFilled[7]).Text = car.ChassisNo;
            ((System.Windows.Forms.CheckBox)toBeFilled[8]).Checked = car.InvoiceRecieved;
            ((System.Windows.Forms.DateTimePicker)toBeFilled[9]).Value = car.InvoiceDate;
            ((System.Windows.Forms.DateTimePicker)toBeFilled[10]).Value = car.RecievedDate;
            ((System.Windows.Forms.TextBox)toBeFilled[11]).Text = car.HorsePower + "";
            ((System.Windows.Forms.TextBox)toBeFilled[12]).Text = car.RegistrationNo;

            ((System.Windows.Forms.FlowLayoutPanel)toBeFilled[13]).Controls.Clear();
            Utils.ImageHandling.InsertInList(car.Images, (System.Windows.Forms.FlowLayoutPanel)toBeFilled[13]);

            ((System.Windows.Forms.CheckBox)toBeFilled[14]).Checked = car.InvoiceDelivered;
            ((System.Windows.Forms.CheckBox)toBeFilled[15]).Checked = car.InvoiceFlag;
            ((System.Windows.Forms.CheckBox)toBeFilled[16]).Checked = car.RecievedFlag;
            ((System.Windows.Forms.TextBox)toBeFilled[17]).Text = car.InvoiceName;
            ((System.Windows.Forms.TextBox)toBeFilled[18]).Text = car.NoteSecondary;
            ((System.Windows.Forms.CheckBox)toBeFilled[19]).Checked = car.WarrantyBookDelivered;
            ((System.Windows.Forms.CheckBox)toBeFilled[20]).Checked = car.WarrantyBookRecieved;
        }

        public static void Fill(object[] toBeFilled, Car car)
        {
            FillStock(toBeFilled, car);
            ((System.Windows.Forms.TextBox)toBeFilled[21]).Text = car.TotalAmount + "";

            if (car.CarTransactions.Count > 0)
                ((System.Windows.Forms.DateTimePicker)toBeFilled[22]).Value = (DateTime)car.CarTransactions[0].Date;

            ((System.Windows.Forms.TextBox)toBeFilled[23]).Text = car.Note;

            if (toBeFilled.Length > 24) ((System.Windows.Forms.PictureBox)toBeFilled[24]).Image = car.CreditPlanDoc;
        }

        public static void FillStock(Car car, object[] toBeFilled)
        {
            car.Sr = ((System.Windows.Forms.TextBox)toBeFilled[0]).Text;
            car.PurchaseAmount = int.Parse((((System.Windows.Forms.TextBox)toBeFilled[1]).Text == "") ? "0" : ((System.Windows.Forms.TextBox)toBeFilled[1]).Text);
            car.PBO = ((System.Windows.Forms.TextBox)toBeFilled[2]).Text;
            car.Model = ((System.Windows.Forms.TextBox)toBeFilled[3]).Text;
            car.Color = ((System.Windows.Forms.TextBox)toBeFilled[4]).Text;
            car.EngineNo = ((System.Windows.Forms.TextBox)toBeFilled[5]).Text;
            car.Name = ((System.Windows.Forms.TextBox)toBeFilled[6]).Text;
            car.ChassisNo = ((System.Windows.Forms.TextBox)toBeFilled[7]).Text;
            car.InvoiceRecieved = ((System.Windows.Forms.CheckBox)toBeFilled[8]).Checked;
            car.InvoiceDate = ((System.Windows.Forms.DateTimePicker)toBeFilled[9]).Value;
            car.RecievedDate = ((System.Windows.Forms.DateTimePicker)toBeFilled[10]).Value;
            car.HorsePower = ((System.Windows.Forms.TextBox)toBeFilled[11]).Text;
            car.RegistrationNo = ((System.Windows.Forms.TextBox)toBeFilled[12]).Text;

            car.Images.Clear();
            foreach (var control in ((System.Windows.Forms.FlowLayoutPanel)toBeFilled[13]).Controls) car.Images.Add((control as PictureBox).Image);
            car.InvoiceDelivered = ((System.Windows.Forms.CheckBox)toBeFilled[14]).Checked;
            car.InvoiceFlag = ((System.Windows.Forms.CheckBox)toBeFilled[15]).Checked;
            car.RecievedFlag = ((System.Windows.Forms.CheckBox)toBeFilled[16]).Checked;
            car.InvoiceName = ((System.Windows.Forms.TextBox)toBeFilled[17]).Text;
            car.NoteSecondary = ((System.Windows.Forms.TextBox)toBeFilled[18]).Text;
            car.WarrantyBookDelivered = ((System.Windows.Forms.CheckBox)toBeFilled[19]).Checked;
            car.WarrantyBookRecieved = ((System.Windows.Forms.CheckBox)toBeFilled[20]).Checked;
        }

        public static void Fill(Car car, object[] toBeFilled)
        {
            FillStock(car, toBeFilled);

            car.TotalAmount = long.Parse((((System.Windows.Forms.TextBox)toBeFilled[21]).Text == "")? "0": ((System.Windows.Forms.TextBox)toBeFilled[21]).Text);

            car.Note = ((System.Windows.Forms.TextBox)toBeFilled[22]).Text;
            if (toBeFilled.Length > 24) car.CreditPlanDoc = ((System.Windows.Forms.PictureBox)toBeFilled[24]).Image;
        }
    }
}
