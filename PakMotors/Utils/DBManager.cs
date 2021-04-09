using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PakMotors.Utils
{
    public class DBManager
    {
        private static DataSet TempDataSet;
        private static SqlConnection Connection;
        private static SqlDataAdapter DataAdapter;

        public static void CloseConnection()
        {
            Connection.Close();
        }
        
        static DBManager()
        {
            TempDataSet = new DataSet();
            DataAdapter = new SqlDataAdapter();
            //"Data Source=DESKTOP-GP6DFF6\SQLEXPRESS;Initial Catalog=PakMotors;Integrated Security=True"
            //"Data Source=DESKTOP-R2RQCNB\MSSQLSERVER;Initial Catalog=PakMotors;Integrated Security=True"
            //"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;"

            //Connection = new SqlConnection(@"Data Source=ARISH-PC\MSSQLSERVER;Initial Catalog=PakMotors;Integrated Security=True");
            Connection = new SqlConnection(@"Data Source=ZAIN-PC\SQLEXPRESS01;Initial Catalog=PakMotors;Integrated Security=True");
        }

        public static int Insert(string tableName, string[] columns, SqlDbType[] types, object[] values)
        {
            if (columns.Length == values.Length)
            {
                var command = new SqlCommand($"INSERT INTO {tableName}", Connection);

                command.CommandText += $"({columns[0]}";
                for (int i = 1; i < columns.Length; i++)
                    command.CommandText += $", {columns[i]}";

                command.CommandText += ") output INSERTED.Id VALUES";

                command.CommandText += "(@value0";
                command.Parameters.Add("@value0", types[0]);

                if (types[0] == SqlDbType.VarChar && values[0] != null)
                {
                    var str = values[0].ToString();
                    if (str.Length > 1)
                    {
                        str = char.ToUpper(str[0]) + str.Substring(1);
                        values[0] = (object)str;
                    }
                }
                command.Parameters["@value0"].Value = values[0];

                for (int i = 1; i < values.Length; i++)
                {

                    command.CommandText += $", @value{i}";

                    command.Parameters.Add($"@value{i}", types[i]);

                    if (types[i] == SqlDbType.VarChar && values[i] != null)
                    {
                        var str = values[i].ToString();
                        if (str.Length > 1)
                        {
                            str = char.ToUpper(str[0]) + str.Substring(1);
                            values[i] = (object)str;
                        }
                    }
                    command.Parameters[$"@value{i}"].Value = (values[i] != null) ? values[i] : (object) DBNull.Value;
                }

                command.CommandText += ");";

                Connection.Open();
                int id = (int)command.ExecuteScalar();
                Connection.Close();

                return id;
            }

            return 0;
        }

        public static void Update(string tableName, int id, string[] columns, SqlDbType[] types, object[] values)
        {
            if (columns.Length == values.Length)
            {
                var command = new SqlCommand($"UPDATE {tableName} SET ", Connection);

                command.CommandText += $"{columns[0]} = @value0";
                command.Parameters.Add("@value0", types[0]);

                if (types[0] == SqlDbType.VarChar && values[0] != null)
                {
                    var str = values[0].ToString();
                    if (str.Length > 1)
                    {
                        str = char.ToUpper(str[0]) + str.Substring(1);
                        values[0] = (object)str;
                    }
                }

                command.Parameters["@value0"].Value = values[0];

                for (int i = 1; i < columns.Length; i++)
                {
                    command.CommandText += $", {columns[i]} = @value{i}";
                    command.Parameters.Add($"@value{i}", types[i]);

                    if (types[i] == SqlDbType.VarChar && values[i] != null)
                    {
                        var str = values[i].ToString();
                        if (str.Length > 1)
                        {
                            str = char.ToUpper(str[0]) + str.Substring(1);
                            values[i] = (object)str;
                        }
                    }
                    command.Parameters[$"@value{i}"].Value = (values[i] != null) ? values[i] : (object)DBNull.Value;
                }
                command.CommandText += $" WHERE Id = {id};";

                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }

        public static void InsertAllImages(int id, List<Image> images)
        {
            if (images.Count > 0)
            {
                var command = new SqlCommand($"INSERT INTO CarImages(Image, CarId) VALUES(@image{0}, {id})", Connection);
                var ms = new System.IO.MemoryStream();
                images[0].Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                command.Parameters.AddWithValue($"@image{0}", ms.ToArray());

                for (int i = 1; i < images.Count; i++)
                {
                    command.CommandText += $", (@image{i}, {id})";

                    ms = new System.IO.MemoryStream();
                    images[i].Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    command.Parameters.AddWithValue($"@image{i}", ms.ToArray());
                }

                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }

        public static void InsertAllPersonImages(int id, List<Image> images)
        {
            if (images.Count > 0)
            {
                var command = new SqlCommand($"INSERT INTO PersonImages(Image, PersonId) VALUES(@image{0}, {id})", Connection);
                var ms = new System.IO.MemoryStream();
                images[0].Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                command.Parameters.AddWithValue($"@image{0}", ms.ToArray());

                for (int i = 1; i < images.Count; i++)
                {
                    command.CommandText += $", (@image{i}, {id})";

                    ms = new System.IO.MemoryStream();
                    images[i].Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    command.Parameters.AddWithValue($"@image{i}", ms.ToArray());
                }

                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }

        public static void DeleteAllImages(int id)
        {
            var command = new SqlCommand($"DELETE FROM CarImages WHERE CarId = {id}", Connection);

            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }
        public static void DeleteAllPersonImages(int id)
        {
            var command = new SqlCommand($"DELETE FROM PersonImages WHERE PersonId = {id}", Connection);

            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public static void InsertAllTransactions(int id, List<Models.Car.Transaction> transactions)
        {
            if (transactions.Count > 0)
            {
                var command = new SqlCommand($"INSERT INTO CarTransactions(Amount, Date, IsRecieved, Note, CarId) VALUES(@amount{0}, @date{0}, @isRecieved{0}, @note{0}, {id})", Connection);
                command.Parameters.AddWithValue($"@amount{0}", transactions[0].Amount);
                command.Parameters.AddWithValue($"@date{0}", transactions[0].Date);
                command.Parameters.AddWithValue($"@isRecieved{0}", transactions[0].IsRecieved);
                command.Parameters.AddWithValue($"@note{0}", transactions[0].Note);

                for (int i = 1; i < transactions.Count; i++)
                {
                    command.CommandText += $", (@amount{i}, @date{i}, @isRecieved{i}, @note{i}, {id})";

                    command.Parameters.AddWithValue($"@amount{i}", transactions[i].Amount);
                    command.Parameters.AddWithValue($"@date{i}", transactions[i].Date);
                    command.Parameters.AddWithValue($"@isRecieved{i}", transactions[i].IsRecieved);
                    command.Parameters.AddWithValue($"@note{i}", transactions[i].Note);
                }

                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }

        public static void DeleteAllTransactions(int id)
        {
            var command = new SqlCommand($"DELETE FROM CarTransactions WHERE CarId = {id}", Connection);

            Connection.Open();
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public static DataRowCollection Query(string tableName, string column, int id)
        {
            TempDataSet = new DataSet();
            QueryAdapter(tableName, $"{column} = {id}").Fill(TempDataSet);

            return TempDataSet.Tables[0].Rows;
        }

        public static DataRowCollection Query(string tableName, string whereClause)
        {
            TempDataSet = new DataSet();
            QueryAdapter(tableName, whereClause).Fill(TempDataSet);

            return TempDataSet.Tables[0].Rows;
        }

        public static SqlDataAdapter QueryAdapter(string tableName, string whereClause)
        {
            Connection.Open();
            var DataAdapter = new SqlDataAdapter($"SELECT * FROM {tableName} WHERE {whereClause}", Connection);
            Connection.Close();

            return DataAdapter;
        }

        public static SqlDataAdapter QueryAdapter(SqlCommand command)
        {
            Connection.Open();
            command.Connection = Connection;
            var DataAdapter = new SqlDataAdapter(command);
            Connection.Close();

            return DataAdapter;
        }

        public static bool Exists(string query)
        {
            try
            {
                return Query(query).Count > 0;
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Unable to Interact with the Database due to " + ex, "Error");
                return false;
            }
        }

        public static SqlDataAdapter QueryAdapter(string query)
        {
            try
            {
                Connection.Open();
                var adapter = new SqlDataAdapter(query, Connection);
                Connection.Close();

                return adapter;
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Unable to Interact with the Database due to " + ex, "Error");
                return null;
            }
        }

        public static DataRowCollection Query(string query)
        {
            try
            {
                Connection.Open();
                TempDataSet = new DataSet();
                DataAdapter = new SqlDataAdapter(query, Connection);
                DataAdapter.Fill(TempDataSet);
                Connection.Close();

                return TempDataSet.Tables[0].Rows;
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Unable to Interact with the Database due to " + ex, "Error");
                return null;
            }
        }

        public static void Insert(SqlCommand command)
        {
            try
            {
                Connection.Open();
                command.Connection = Connection;
                command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Unable to Interact with the Database due to " + ex, "Error");
            }
        }

        public static void Update(SqlCommand command)
        {
            try
            {
                Connection.Open();
                command.Connection = Connection;
                command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Unable to Interact with the Database due to " + ex, "Error");
            }
        }

        public static void Delete(string query)
        {
            try
            {
                Connection.Open();
                new SqlCommand(query, Connection).ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                Connection.Close();
                MessageBox.Show("Unable to Interact with the Database due to " + ex, "Error");
            }
        }
    }
}
