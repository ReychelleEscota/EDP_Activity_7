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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void attendee_id_label_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
        }

        private void picture_docs_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Load the selected file into the PictureBox's ImageLocation
                textBox3.Text = openFileDialog1.FileName;
            }
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


        private void dashboard_add_Click(object sender, EventArgs e)
        {
            // Define the connection string (replace the placeholders with your own values)
            string connectionString = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";

            // Define the SQL query for inserting data
            string insertQuery = "INSERT INTO event_overview (event_name, event_date, venue) VALUES (@event_name, @event_date, @venue)";

            // Retrieve the data from the form controls
            string eventName = event_name.Text;
            DateTime eventDate = event_date.Value;
            string venue = venue_name.Text;

            // Establish a connection to the MySQL database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to execute the SQL query
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        // Add parameters to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@event_name", eventName);
                        command.Parameters.AddWithValue("@event_date", eventDate);
                        command.Parameters.AddWithValue("@venue", venue);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the data was successfully inserted
                        if (rowsAffected > 0)
                        {
                            // Show a success message to the user
                            MessageBox.Show("Event added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the data in the DataGridView to reflect the changes
                            LoadData();
                        }
                        else
                        {
                            // Show an error message to the user
                            MessageBox.Show("Failed to add event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the database operations
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string query = "SELECT * FROM event_overview";
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



    }
}
