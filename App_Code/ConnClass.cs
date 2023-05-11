using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SignalRChat
{
    public class ConnClass
    {
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter sda;
        public SqlDataReader sdr;
        public DataSet ds = new DataSet();
       // public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        public SqlConnection con = new SqlConnection();

        string connectionstring = "server=localhost;database='Chat2Fun';integrated security=true";
           

public bool IsExist(string Query)
        {
            // Dungchung.cnn.Open();
            // MessageBox.Show("ket noi thanh cong");

            con.ConnectionString = connectionstring;

            bool check = false;
            using (cmd = new SqlCommand(Query, con))
            {
                con.Open();
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                    check = true;
            }
            sdr.Close();
            con.Close();
            return check;

        }
        public string GenerateID()
        {
            long currentMilis = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            return (currentMilis % 10000000).ToString();
        }
        public string GetUserColData(string Username, string colData)
        {
            string dataReturn = "";
            if (Username != null)
            {
                string query = "select " + colData + " from UserData where UserName='" + Username + "'";

                dataReturn = GetColumnVal(query, colData);

            }

            return dataReturn;
        }

        public string GetUserColDataById(string id, string colData)
        {
            string dataReturn = "";
            if (id != null)
            {
                string query = "select " + colData + " from UserData where UID='" + id + "'";

                dataReturn = GetColumnVal(query, colData);

            }

            return dataReturn;
        }

        public string GetMessageDataById(string id, string colData)
        {
            string dataReturn = "";
            if (id != null)
            {
                string query = "select " + colData + " from Messages where MessageID='" + id + "'";

                dataReturn = GetColumnVal(query, colData);

            }

            return dataReturn;
        }

        public bool ExecuteQuery(string Query)
        {
            // Dungchung.cnn.Open();
            // MessageBox.Show("ket noi thanh cong");

            con.ConnectionString = connectionstring;

            int j = 0;
            using (cmd = new SqlCommand(Query, con))
            {
                con.Open();
                j = cmd.ExecuteNonQuery();
                con.Close();
            }

            if (j > 0)
                return true;
            else
                return false;

        }

        public List<Messages> ReadMessage()
        {
            List<Messages> CurrentMessage = new List<Messages>();
            string connectionString = this.connectionstring;

            // Create a connection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the connection

                string sqlQuery = "SELECT * FROM Messages"; // Replace "YourTable" with the actual table name

                // Create a command object with the SQL query and connection
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Execute the command and retrieve the data
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Loop through the rows of data
                        while (reader.Read())
                        {
                            string userName = GetUserColDataById(reader.GetString(2), "Username");
                            string userImage = GetUserColDataById(reader.GetString(2), "Avatar");
                            string message = reader.GetString(3);
                            string sendTime = reader.GetDateTime(4).ToString();

                            CurrentMessage.Add(new Messages(userName, message, sendTime, userImage));

                        }
                    }
                }

                connection.Close(); // Close the connection
            }

            return CurrentMessage;
        }

        public string GetColumnVal(string Query, string ColumnName)
        {
            con.ConnectionString = connectionstring;
            string RetVal = "";
            using (cmd = new SqlCommand(Query, con))
            {
                con.Open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    RetVal = sdr[ColumnName].ToString();
                    break;
                }
                sdr.Close();
                con.Close();
            }

            return RetVal;
        }

       

    }
}