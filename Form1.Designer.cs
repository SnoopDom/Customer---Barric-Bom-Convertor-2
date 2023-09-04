namespace Customer___Barric_Bom_Convertor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bomFilePath = new System.Windows.Forms.RichTextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ppFilePath = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.browseButton = new System.Windows.Forms.Button();
            this.btnLoadPP = new System.Windows.Forms.Button();
            this.btnProcessData = new System.Windows.Forms.Button();
            this.dataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.enterCellDataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteRowColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bomFilePath
            // 
            this.bomFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bomFilePath.Location = new System.Drawing.Point(146, 3);
            this.bomFilePath.Name = "bomFilePath";
            this.bomFilePath.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.bomFilePath.Size = new System.Drawing.Size(314, 24);
            this.bomFilePath.TabIndex = 1;
            this.bomFilePath.Text = "";
            this.bomFilePath.UseWaitCursor = true;
            this.bomFilePath.WordWrap = false;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewComboBoxColumn,
            this.enterCellDataColumn,
            this.btnDeleteRowColumn});
            this.dataGridView.Location = new System.Drawing.Point(12, 80);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.Size = new System.Drawing.Size(460, 89);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 105);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // ppFilePath
            // 
            this.ppFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ppFilePath.Location = new System.Drawing.Point(146, 33);
            this.ppFilePath.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ppFilePath.Name = "ppFilePath";
            this.ppFilePath.Size = new System.Drawing.Size(314, 25);
            this.ppFilePath.TabIndex = 5;
            this.ppFilePath.Text = "";
            this.ppFilePath.UseWaitCursor = true;
            this.ppFilePath.WordWrap = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 817F));
            this.tableLayoutPanel2.Controls.Add(this.ppFilePath, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.bomFilePath, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.browseButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLoadPP, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(460, 66);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(3, 3);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(137, 25);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "Browse for Customer Bom";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // btnLoadPP
            // 
            this.btnLoadPP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadPP.Location = new System.Drawing.Point(3, 33);
            this.btnLoadPP.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnLoadPP.Name = "btnLoadPP";
            this.btnLoadPP.Size = new System.Drawing.Size(137, 25);
            this.btnLoadPP.TabIndex = 4;
            this.btnLoadPP.Text = "Browse for Progress Plus Directory";
            this.btnLoadPP.UseVisualStyleBackColor = true;
            this.btnLoadPP.Click += new System.EventHandler(this.btnLoadPP_Click);
            // 
            // btnProcessData
            // 
            this.btnProcessData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcessData.Location = new System.Drawing.Point(12, 176);
            this.btnProcessData.Name = "btnProcessData";
            this.btnProcessData.Size = new System.Drawing.Size(460, 23);
            this.btnProcessData.TabIndex = 8;
            this.btnProcessData.Text = "Magic Button";
            this.btnProcessData.UseVisualStyleBackColor = true;
            this.btnProcessData.Click += new System.EventHandler(this.btnProcessData_Click);
            // 
            // dataGridViewComboBoxColumn
            // 
            this.dataGridViewComboBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewComboBoxColumn.DataPropertyName = "1";
            this.dataGridViewComboBoxColumn.FillWeight = 60F;
            this.dataGridViewComboBoxColumn.HeaderText = "Search Criteria";
            this.dataGridViewComboBoxColumn.Name = "dataGridViewComboBoxColumn";
            this.dataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // enterCellDataColumn
            // 
            this.enterCellDataColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.NullValue = null;
            this.enterCellDataColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.enterCellDataColumn.FillWeight = 20F;
            this.enterCellDataColumn.HeaderText = "Cell Data";
            this.enterCellDataColumn.Name = "enterCellDataColumn";
            this.enterCellDataColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // btnDeleteRowColumn
            // 
            this.btnDeleteRowColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.btnDeleteRowColumn.FillWeight = 20F;
            this.btnDeleteRowColumn.HeaderText = "Delete Row";
            this.btnDeleteRowColumn.Name = "btnDeleteRowColumn";
            this.btnDeleteRowColumn.ReadOnly = true;
            this.btnDeleteRowColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnDeleteRowColumn.Text = "Delete";
            this.btnDeleteRowColumn.UseColumnTextForButtonValue = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 211);
            this.Controls.Add(this.btnProcessData);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 800);
            this.MinimumSize = new System.Drawing.Size(250, 225);
            this.Name = "Form1";
            this.Text = "Barric Bom Converter";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        public System.Windows.Forms.RichTextBox bomFilePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.RichTextBox ppFilePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button btnLoadPP;
        private System.Windows.Forms.Button btnProcessData;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enterCellDataColumn;
        private System.Windows.Forms.DataGridViewButtonColumn btnDeleteRowColumn;
    }
}

