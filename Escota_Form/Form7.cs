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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

namespace Escota_Form
{
    public partial class Form7 : Form
    {
        private MySqlConnection connection;

        public Form7()
        {
            InitializeComponent();
            string connectionString = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";
            connection = new MySqlConnection(connectionString);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dashboard_D_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void dashboard_add_Click(object sender, EventArgs e)
        {
            if (attendee_id.Text == "" || textBox1.Text == "" || textBox2.Text == "" ||
              comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Please fill all the blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    // Check if user ID already exists
                    string checksUserId = "SELECT COUNT(*) FROM attendee WHERE attendee_id = @UserId";
                    using (MySqlCommand checkuserid = new MySqlCommand(checksUserId, connection))
                    {
                        checkuserid.Parameters.AddWithValue("@UserId", attendee_id.Text.Trim());
                        int count = Convert.ToInt32(checkuserid.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("User ID: " + attendee_id.Text.Trim() + " already exists",
                                            "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method to prevent further execution
                        }
                    }

                    // If user ID doesn't exist, proceed with insertion
                    DateTime today = DateTime.Today;
                    string insertData = "INSERT INTO attendee " +
                    "(attendee_id, attendee_name, attendee_email, attendee_seats, artist, attendee_status) " +
                    "VALUES (@attendee_id, @attendee_name, @attendee_email, @attendee_seats, @artist, @attendee_status)";

                    using (MySqlCommand cmd = new MySqlCommand(insertData, connection))
                    {
                        cmd.Parameters.AddWithValue("@attendee_id", attendee_id.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_name", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_email", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_seats", comboBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_status", comboBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@artist", comboBox3.Text.Trim());
                        //  cmd.Parameters.AddWithValue("@Dateinsert", today);



                        // Execute the command
                        cmd.ExecuteNonQuery();

                        // Refresh the DataGridView with updated data
                        LoadData();

                        MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }


        }

        private void LoadData()
        {
            try
            {
                string connectionString = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM attendee";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        dataGridView1.DataSource = dataSet.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dashboard_update_Click(object sender, EventArgs e)

        {
            if (attendee_id.Text == "" || textBox1.Text == "" || textBox2.Text == "" ||
     comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Please fill all the blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    // Check if user ID already exists
                    string checksUserId = "SELECT COUNT(*) FROM attendee WHERE attendee_id = @attendee_id";
                    using (MySqlCommand checkuserid = new MySqlCommand(checksUserId, connection))
                    {
                        checkuserid.Parameters.AddWithValue("@attendee_id", attendee_id.Text.Trim());
                        int count = Convert.ToInt32(checkuserid.ExecuteScalar());
                        if (count == 0)
                        {
                            MessageBox.Show("attendee_id: " + attendee_id.Text.Trim() + " does not exist",
                                            "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method to prevent further execution
                        }
                    }

                    // If user ID exists, proceed with update
                    DateTime today = DateTime.Today;
                    string updateData = "UPDATE attendee SET " +
                                        "attendee_name = @attendee_name, " +
                                        "attendee_email = @attendee_email, " +
                                        "attendee_seats = @attendee_seats, " +
                                        "attendee_status = @attendee_status, " +
                                        "artist = @artist " +
                                        //   "date_insert = @Dateinsert " +
                                        "WHERE attendee_id = @attendee_id";

                    using (MySqlCommand cmd = new MySqlCommand(updateData, connection))
                    {
                        cmd.Parameters.AddWithValue("@attendee_id", attendee_id.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_name", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_email", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_seats", comboBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@attendee_status", comboBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@artist", comboBox3.Text.Trim());
                        //  cmd.Parameters.AddWithValue("@Dateinsert", today);

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Refresh the DataGridView with updated data
                            LoadData();

                            MessageBox.Show("Update successful!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update  with ID: " + attendee_id.Text.Trim(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

            }
        }


        private void LoadData1()
        {
            try
            {
                string connectionString = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM attendee";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        dataGridView1.DataSource = dataSet.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Load the selected file into the PictureBox's ImageLocation
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog to prompt the user for a save location
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Save the Excel file";

            // If the user selects a location and filename
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Create a new XLWorkbook
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Add a new worksheet
                    var worksheet = workbook.Worksheets.Add("Data");

                    // Loop through DataGridView and add data to the worksheet
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        // Add the column headers to the worksheet
                        worksheet.Cell(1, i + 1).Value = dataGridView1.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            // Convert the cell value to the appropriate type (string, int, etc.)
                            object cellValue = dataGridView1.Rows[i].Cells[j].Value;
                            worksheet.Cell(i + 2, j + 1).Value = Convert.ToString(cellValue);
                        }
                    }

                    // Save the workbook to the file specified by the user
                    workbook.SaveAs(saveFileDialog.FileName);
                }

                // Show a message to the user indicating the file has been saved
                MessageBox.Show($"Data exported successfully to {saveFileDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
