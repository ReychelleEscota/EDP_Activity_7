using ClosedXML.Excel;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Escota_Form
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            // Check if any fields are empty
            if (string.IsNullOrEmpty(textBox1.Text) ||
                event_date.Value == DateTime.MinValue ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox4.Text) ||
                purchased_date.Value == DateTime.MinValue)
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Connection string for MySQL
            string connectionString = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Prepare the INSERT query
                    string insertQuery = "INSERT INTO ticket_sales (event_name, event_date, buyer_name, ticket_price, purchase_date) " +
                                         "VALUES (@event_name, @event_date, @buyer_name, @ticket_price, @purchase_date)";

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        // Add parameters for the INSERT query
                        insertCmd.Parameters.AddWithValue("@event_name", textBox1.Text.Trim());
                        insertCmd.Parameters.AddWithValue("@event_date", event_date.Value);
                        insertCmd.Parameters.AddWithValue("@buyer_name", textBox2.Text.Trim());

                        // Parse and add ticket_price parameter
                        if (double.TryParse(textBox4.Text.Trim(), out double ticketPrice))
                        {
                            insertCmd.Parameters.AddWithValue("@ticket_price", ticketPrice);
                        }
                        else
                        {
                            MessageBox.Show("Invalid ticket price format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        insertCmd.Parameters.AddWithValue("@purchase_date", purchased_date.Value);

                        // Execute the INSERT command
                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Refresh the DataGridView with updated data
                            LoadData();

                            MessageBox.Show("New ticket added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the new ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Handle MySqlException and provide detailed error message
                    MessageBox.Show($"MySQL database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FormatException ex)
                {
                    // Handle FormatException when parsing numbers
                    MessageBox.Show($"Invalid data format: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // General exception handling for other errors
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        public void LoadData()
        {
            try
            {
                // Define the connection string
                string connectionString = "server=localhost;uid=root;pwd=Ylle_29!!!;database=concert";

                // Use MySqlConnection within a using statement
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Define the query to select data
                    string query = "SELECT * FROM ticket_sales";

                    // Use MySqlCommand to execute the query
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Use MySqlDataAdapter to fetch data and fill a DataSet
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        // Set the data source for the DataGridView
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
