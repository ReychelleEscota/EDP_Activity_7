using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Escota_Form
{
    internal class attendee_add
    {
        private MySqlConnection connection;

        public int attendee_id { set; get; }
        public string attendee_name { set; get; }
        public string attendee_email { set; get; }
        public string attendee_seats { set; get; }
        public string attendee_status { set; get; }
        public int artist { set; get; }

        public attendee_add()
        {
            connection = new MySqlConnection("server=localhost;uid=root;pwd=Ylle_29!!!;database=concert");
        }

        public List<attendee_add> DataUsers(string email)
        {
            List<attendee_add> userDataList = new List<attendee_add>();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT * FROM Profile WHERE date_delete IS NULL";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        attendee_add userData = new attendee_add
                        {
                            attendee_id = Convert.ToInt32(reader["attendee_id"]),
                            attendee_name = reader["attendee_name"].ToString(),
                            attendee_email = reader["attendee_email"].ToString(),
                            attendee_seats = reader["attendee_seats"].ToString(),
                            attendee_status = reader["attendee_status"] != DBNull.Value ? reader["attendee_status"].ToString() : "",
                            artist = Convert.ToInt32(reader["artist"]),
                        };

                        userDataList.Add(userData);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting Database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return userDataList;
        }
    }
}
