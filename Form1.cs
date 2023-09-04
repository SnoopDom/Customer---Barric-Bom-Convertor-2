using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.XlsIO;



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
            // Load the Excel file
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(filePath);
                IWorksheet worksheet = workbook.Worksheets[0]; // Assuming data is in the first worksheet

                // Read data from the first row (row 1) to populate the combo box
                int rowNumber = 0; // First row
                IRange dataRange = worksheet.Rows[rowNumber];
                List<string> comboItems = new List<string>();

                foreach (var cell in dataRange)
                {
                    comboItems.Add(cell.DisplayText);
                }

                // Populate the combo box with the extracted data
                DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)dataGridView.Columns["dataGridViewComboBoxColumn"];
                comboBoxColumn.Items.AddRange(comboItems.ToArray());

                // Close the Excel file
                workbook.Close();
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

                // You can also perform further operations with the selected file if needed
                // For example, you can load and process the Excel file using Syncfusion XlsIO
                // For simplicity, this code only sets the file path in the TextBox.
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
            // Check if the clicked cell is in the "Delete Row" column
            if (e.ColumnIndex == dataGridView.Columns["btnDeleteRowColumn"].Index && e.RowIndex >= 0)
            {
                // Check if a value is selected in the ComboBoxColumn
                DataGridViewComboBoxCell comboBoxCell = dataGridView.Rows[e.RowIndex].Cells["dataGridViewComboBoxColumn"] as DataGridViewComboBoxCell;
                if (comboBoxCell != null && comboBoxCell.Value != null && !string.IsNullOrEmpty(comboBoxCell.Value.ToString()))
                {
                    // Delete the row corresponding to the clicked cell
                    dataGridView.Rows.RemoveAt(e.RowIndex);
                }
                else
                {
                    // Display an error message
                    MessageBox.Show("Please select a search criteria before deleting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProcessData_Click(object sender, EventArgs e)
        {
            // Check if there is a selected row
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a row before processing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected search criteria and cell range
            string criteria = dataGridView.CurrentRow.Cells["dataGridViewComboBoxColumn"].Value?.ToString();
            string cellRange = dataGridView.CurrentRow.Cells["enterCellDataColumn"].Value?.ToString();

            // Check if criteria or cellRange is null or empty
            if (string.IsNullOrEmpty(criteria) || string.IsNullOrEmpty(cellRange))
            {
                MessageBox.Show("Please fill in both search criteria and cell range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Load the source Excel file
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook sourceWorkbook = application.Workbooks.Open(bomFilePath.Text);
                IWorksheet sourceWorksheet = sourceWorkbook.Worksheets[0];

                // Create a new Excel workbook
                IWorkbook newWorkbook = application.Workbooks.Create();
                IWorksheet newWorksheet = newWorkbook.Worksheets[0];

                // Get the data from the specified cell range
                IRange bomDataRange = sourceWorksheet.Range[cellRange];

                // Set the data in the new workbook
                for (int i = 0; i < bomDataRange.Text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                {
                    newWorksheet.SetValue(i + 1, 1, bomDataRange.Text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[i]);
                }

                //Save the new workbooks
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    newWorkbook.SaveAs(saveFileDialog.FileName);
                }

                // Close workbooks
                newWorkbook.Close();
                sourceWorkbook.Close();
                MessageBox.Show("Data processing completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
