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
            // Set EPPlus license context to non-commercial
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

            using (var package = new ExcelPackage(new FileInfo(bomFilePath.Text)))
            {
                var sourceWorksheet = package.Workbook.Worksheets[0];

                using (var newPackage = new ExcelPackage())
                {
                    var newWorksheet = newPackage.Workbook.Worksheets.Add("Sheet1");

                    // Format the header row in the new worksheet
                    foreach (var kvp in headerToDataRange)
                    {
                        newWorksheet.Cells[1, headerToDataRange.Keys.ToList().IndexOf(kvp.Key) + 1].Value = kvp.Key;
                    }

                    // Get the data from the specified cell range and populate the new worksheet
                    foreach (var kvp in headerToDataRange)
                    {
                        var bomDataRange = sourceWorksheet.Cells[kvp.Value];
                        int rowNum = 2; // Start from the second row (after headers)

                        foreach (var cell in bomDataRange)
                        {
                            newWorksheet.Cells[rowNum, headerToDataRange.Keys.ToList().IndexOf(kvp.Key) + 1].Value = cell.Text;
                            rowNum++;
                        }
                    }

                    // AutoFit columns in the new worksheet
                    newWorksheet.Cells.AutoFitColumns();

                    // Save the new Excel file
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        newPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                    }

                    MessageBox.Show("Data processing and formatting completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
