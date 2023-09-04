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

        public Form1()
        {
            InitializeComponent();
        }

        private void PopulateComboBoxFromExcel(string filePath)
        {
            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

                List<string> comboItems = new List<string>();

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
            List<string> selectedHeaders = new List<string>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewComboBoxCell comboBoxCell = row.Cells["dataGridViewComboBoxColumn"] as DataGridViewComboBoxCell;
                if (comboBoxCell != null && comboBoxCell.Value != null && !string.IsNullOrEmpty(comboBoxCell.Value.ToString()))
                {
                    selectedHeaders.Add(comboBoxCell.Value.ToString());
                }
            }

            if (selectedHeaders.Count == 0)
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

                    // Format the header row
                    for (int i = 0; i < selectedHeaders.Count; i++)
                    {
                        newWorksheet.Cells[1, i + 1].Value = selectedHeaders[i];
                    }

                    // Get the data from the specified cell range
                    var bomDataRange = sourceWorksheet.Cells[cellRange];

                    int rowNum = 2; // Start from the second row (after headers)
                    foreach (var cell in bomDataRange)
                    {
                        int columnNum = cell.Start.Column;
                        string header = newWorksheet.Cells[1, columnNum]?.Value?.ToString();

                        if (selectedHeaders.Contains(header))
                        {
                            newWorksheet.Cells[rowNum, selectedHeaders.IndexOf(header) + 1].Value = cell.Text;
                        }
                    }

                    // AutoFit columns
                    newWorksheet.Cells.AutoFitColumns();

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


        private void ProcessData(string cellRange, List<string> selectedHeaders)
        {
            using (var package = new ExcelPackage(new FileInfo(bomFilePath.Text)))
            {
                var sourceWorksheet = package.Workbook.Worksheets[0];

                using (var newPackage = new ExcelPackage())
                {
                    var newWorksheet = newPackage.Workbook.Worksheets.Add("Sheet1");

                    // Add selected headers to row 1
                    int headerColumn = 1;
                    foreach (var header in selectedHeaders)
                    {
                        newWorksheet.Cells[1, headerColumn].Value = header;
                        headerColumn++;
                    }

                    // Get the data from the specified cell range
                    var bomDataRange = sourceWorksheet.Cells[cellRange];

                    int rowNum = 2; // Start from the second row
                    foreach (var cell in bomDataRange)
                    {
                        int columnNum = cell.Start.Column;
                        string header = newWorksheet.Cells[1, columnNum]?.Value?.ToString();

                        if (selectedHeaders.Contains(header))
                        {
                            newWorksheet.Cells[rowNum, columnNum].Value = cell.Text;
                        }
                    }

                    // Save the data to a new Excel file
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        newPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                        MessageBox.Show("Data processing completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data processing canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}