using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Escota_Form
{
    public partial class Password_Recovery : Form
    {
        public Password_Recovery()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(recovery_username.Text) || string.IsNullOrWhiteSpace(recovery_password.Text))
            {
                MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the event handler to prevent further execution
            }
            else
            {
                string path = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";

                MySqlConnection conn = new MySqlConnection(path);
                conn.Open();
                MySqlCommand command = conn.CreateCommand();

                // Check if the username exists in the database
                command.CommandText = "SELECT COUNT(*) FROM admin WHERE username = @username";
                command.Parameters.AddWithValue("@username", recovery_username.Text);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                {
                    MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                    return;
                }

                // Update the password for the user
                command.CommandText = "UPDATE admin SET password = @password, confirm_password = @conpassword WHERE username = @username";
                command.Parameters.Clear(); // Clear previous parameters
                command.Parameters.AddWithValue("@password", recovery_password.Text);
                command.Parameters.AddWithValue("@conpassword", recovery_conpass.Text);
                command.Parameters.AddWithValue("@username", recovery_username.Text);
                command.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Password updated successfully.");

                var confirm = new Login_Form(); // Instantiate Form2
                confirm.Show(); // Show Form2
                this.Hide(); // Hide the current form
            }
        }

            private void label3_Click(object sender, EventArgs e)
            {

            }

            private void textBox2_TextChanged(object sender, EventArgs e)
            {

            }

            private void pictureBox1_Click(object sender, EventArgs e)
            {

            }

            private void label4_Click(object sender, EventArgs e)
            {

            }
        }
    } 

