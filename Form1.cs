using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Customer___Barric_Bom_Convertor
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> headerToDataRange = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void PopulateComboBoxFromExcel(string filePath)
        {
            // Set the licensing context for EPPlus to non-commercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Load the selected Excel file
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

                List<string> comboItems = new List<string>();

                // Extract header row data from the worksheet
                foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    comboItems.Add(cell.Text);
                }

                // Populate the combo box with the extracted data
                DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)dataGridView.Columns["dataGridViewComboBoxColumn"];
                comboBoxColumn.Items.AddRange(comboItems.ToArray());
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path and display it in the filePathTextBox
                bomFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnLoadPP_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Call the method to populate the ComboBox with data from the selected Excel file
                PopulateComboBoxFromExcel(selectedFilePath);

                // Set the selected file path in the TextBox
                ppFilePath.Text = selectedFilePath;
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["btnDeleteRowColumn"].Index && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboBoxCell = dataGridView.Rows[e.RowIndex].Cells["dataGridViewComboBoxColumn"] as DataGridViewComboBoxCell;
                if (comboBoxCell != null && comboBoxCell.Value != null && !string.IsNullOrEmpty(comboBoxCell.Value.ToString()))
                {
                    // Remove the selected row if a search criteria is selected
                    dataGridView.Rows.RemoveAt(e.RowIndex);
                }
                else
                {
                    MessageBox.Show("Please select a search criteria before deleting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProcessData_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a row before processing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string cellRange = dataGridView.CurrentRow.Cells["enterCellDataColumn"].Value?.ToString();

            if (string.IsNullOrEmpty(cellRange))
            {
                MessageBox.Show("Please fill in the cell range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected headers from the DataGridViewComboBoxColumn
            headerToDataRange.Clear();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewComboBoxCell comboBoxCell = row.Cells["dataGridViewComboBoxColumn"] as DataGridViewComboBoxCell;
                if (comboBoxCell != null && comboBoxCell.Value != null && !string.IsNullOrEmpty(comboBoxCell.Value.ToString()))
                {
                    // Store the mapping of header to data range
                    headerToDataRange[comboBoxCell.Value.ToString()] = row.Cells["enterCellDataColumn"].Value?.ToString();
                }
            }

            if (headerToDataRange.Count == 0)
            {
                MessageBox.Show("Please select at least one header.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Open the existing Excel file using a file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx";
            openFileDialog.Title = "Select an existing Excel file to append data";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string existingFilePath = openFileDialog.FileName;

                try
                {
                    using (var existingPackage = new ExcelPackage(new FileInfo(existingFilePath)))
                    {
                        var existingWorksheet = existingPackage.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Customer BOM Information");

                        if (existingWorksheet == null)
                        {
                            // Create a new worksheet if it doesn't exist
                            existingWorksheet = existingPackage.Workbook.Worksheets.Add("Customer BOM Information");
                        }
                        else
                        {
                            // Clear all the data in the "Customer BOM Information" sheet
                            var rowsWithData = existingWorksheet.Cells["2:1048576"].Where(cell => !string.IsNullOrEmpty(cell.Text));
                            foreach (var cell in rowsWithData)
                            {
                                cell.Clear();
                            }
                        }

                        // Load the source Excel file
                        using (var sourcePackage = new ExcelPackage(new FileInfo(bomFilePath.Text)))
                        {
                            var sourceWorksheet = sourcePackage.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

                            // Add selected headers to the existing worksheet
                            foreach (var kvp in headerToDataRange)
                            {
                                // Copy headers from the source worksheet to the existing worksheet
                                existingWorksheet.Cells[1, headerToDataRange.Keys.ToList().IndexOf(kvp.Key) + 1].Value = kvp.Key;
                            }

                            // Get the data from the specified cell range and populate the new worksheet
                            foreach (var kvp in headerToDataRange)
                            {
                                var bomDataRange = sourceWorksheet.Cells[kvp.Value];
                                int rowNum = 2; // Start from the second row (after headers)

                                foreach (var cell in bomDataRange)
                                {
                                    existingWorksheet.Cells[rowNum, headerToDataRange.Keys.ToList().IndexOf(kvp.Key) + 1].Value = cell.Text;
                                    rowNum++;
                                }
                            }

                            // AutoFit columns in the existing worksheet
                            existingWorksheet.Cells.AutoFitColumns();

                            // Save the existing Excel file with the added data and selected headers
                            existingPackage.Save();

                            // After processing and appending data, search for matches in the "Stock" sheet
                            var stockWorksheet = existingPackage.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Stock");
                            if (stockWorksheet != null)
                            {
                                // Assuming the "Description" and "Part Number" columns are in the first row
                                var headerRow = stockWorksheet.Cells["1:1"];

                                int descriptionColumnIndex = -1; // Initialize to an invalid index
                                int partNumberColumnIndex = -1;  // Initialize to an invalid index

                                // Find the columns containing "Description" and "Part Number" headers
                                foreach (var cell in headerRow)
                                {
                                    if (cell.Text == "Description")
                                    {
                                        descriptionColumnIndex = cell.Start.Column;
                                    }
                                    else if (cell.Text == "Part No")
                                    {
                                        partNumberColumnIndex = cell.Start.Column;
                                    }

                                    // If both columns are found, exit the loop
                                    if (descriptionColumnIndex != -1 && partNumberColumnIndex != -1)
                                    {
                                        break;
                                    }
                                }

                                // Check if both columns were found
                                if (descriptionColumnIndex != -1 && partNumberColumnIndex != -1)
                                {
                                    // Try to find the "Inferred Data" sheet or create it if it doesn't exist
                                    var inferredDataWorksheet = existingPackage.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Inferred Data");
                                    if (inferredDataWorksheet == null)
                                    {
                                        inferredDataWorksheet = existingPackage.Workbook.Worksheets.Add("Inferred Data");
                                        inferredDataWorksheet.Cells[1, 1].Value = "Part No";
                                        inferredDataWorksheet.Cells[1, 2].Value = "Description";
                                    }

                                    // Loop through rows in the "Customer BOM Information" sheet
                                    for (int row = 2; row <= existingWorksheet.Dimension.End.Row; row++)
                                    {
                                        // Get the value in the "Description" column for the current row
                                        string descriptionValue = existingWorksheet.Cells[row, descriptionColumnIndex].Text;

                                        // Search for a match in the "Stock" sheet's "Description" column
                                        var matchingCell = stockWorksheet.Cells[2, descriptionColumnIndex, stockWorksheet.Dimension.End.Row, descriptionColumnIndex].FirstOrDefault(cell => cell.Text == descriptionValue);

                                        // If a match is found, write the "Part Number" value to the "Inferred Data" sheet
                                        if (matchingCell != null)
                                        {
                                            // Get the "Part Number" value from the corresponding row in "Stock" sheet
                                            string partNumberValue = stockWorksheet.Cells[matchingCell.Start.Row, partNumberColumnIndex].Text;

                                            // Write the matched "Part Number" and "Description" values to the "Inferred Data" sheet
                                            inferredDataWorksheet.Cells[row, 1].Value = partNumberValue;
                                            inferredDataWorksheet.Cells[row, 2].Value = descriptionValue;
                                        }
                                    }

                                    // Save the existing Excel file with the added "Inferred Data" sheet
                                    existingPackage.Save();
                                }
                                else
                                {
                                    MessageBox.Show("Could not find 'Description' and 'Part Number' columns in the 'Stock' sheet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Error accessing the Excel files. Please make sure the files are not open in another application.\nDetails: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while processing and appending the data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
