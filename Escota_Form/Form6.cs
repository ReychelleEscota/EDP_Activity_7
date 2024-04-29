using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Escota_Form
{
    public partial class Register : Form
    {
        string server = "127.0.0.1";
        string uid = "root";
        string database = "concert";
        string password = "Ylle_29!!!";

        public Register()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Code related to textBox4 text changed event handler
        }

        private void title_Click(object sender, EventArgs e)
        {
            // Code related to title click event handler
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Code related to checkBox1 checked changed event handler
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            // Check if any of the input fields are empty or contain only white spaces
            if (string.IsNullOrWhiteSpace(register_username.Text) || string.IsNullOrWhiteSpace(register_password.Text) || string.IsNullOrWhiteSpace(register_conpass.Text))
            {
                MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Establish a connection to the database
            string connectionString = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create a SQL command to insert data into the 'admin' table
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO admin (username, password, confirm_password) VALUES (@username, @password, @conpassword)";
                    command.Parameters.AddWithValue("@username", register_username.Text);
                    command.Parameters.AddWithValue("@password", register_password.Text);
                    command.Parameters.AddWithValue("@conpassword", register_conpass.Text);

                    // Execute the SQL command
                    command.ExecuteNonQuery();

                    // Close the database connection
                    connection.Close();

                    // Show a success message
                    MessageBox.Show("Registered Successfully");

                    // Open the login form
                    var confirm = new Login_Form();
                    confirm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions (e.g., database connection error)
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void username_TextChanged(object sender, EventArgs e)
        {
            // Code related to the username textbox text changed event handler
        }

        // If Form6 is not meant to be nested within Register, remove the nested class
        // public partial class Form6 : Form
        // {
        // }

        private void button1_Click(object sender, EventArgs e)
        {
            // Code related to button1 click event handler
        }

        private static void username_TextChanged1(object sender, EventArgs e)
        {
            // Code related to username textbox text changed event handler
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}



