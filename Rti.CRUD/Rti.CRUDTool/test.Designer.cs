namespace Rti.CRUDTool
{
    partial class test
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.linkLblAdvSearch = new System.Windows.Forms.LinkLabel();
            this.rdoBtnAndSearch = new System.Windows.Forms.RadioButton();
            this.cboAdvSearchCol1 = new System.Windows.Forms.ComboBox();
            this.txtAdvSearchVal1 = new System.Windows.Forms.TextBox();
            this.txtAdvSearchVal2 = new System.Windows.Forms.TextBox();
            this.cboAdvSearchCol2 = new System.Windows.Forms.ComboBox();
            this.txtAdvSearchVal3 = new System.Windows.Forms.TextBox();
            this.cboAdvSearchCol3 = new System.Windows.Forms.ComboBox();
            this.btnAdvSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 128);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1002, 341);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 1);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(93, 1);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 2;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(93, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // linkLblAdvSearch
            // 
            this.linkLblAdvSearch.AutoSize = true;
            this.linkLblAdvSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblAdvSearch.LinkColor = System.Drawing.Color.Black;
            this.linkLblAdvSearch.Location = new System.Drawing.Point(12, 78);
            this.linkLblAdvSearch.Name = "linkLblAdvSearch";
            this.linkLblAdvSearch.Size = new System.Drawing.Size(102, 15);
            this.linkLblAdvSearch.TabIndex = 9;
            this.linkLblAdvSearch.TabStop = true;
            this.linkLblAdvSearch.Text = "Advanced Search";
            this.linkLblAdvSearch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblAdvSearch_LinkClicked);
            // 
            // rdoBtnAndSearch
            // 
            this.rdoBtnAndSearch.AutoSize = true;
            this.rdoBtnAndSearch.Location = new System.Drawing.Point(120, 78);
            this.rdoBtnAndSearch.Name = "rdoBtnAndSearch";
            this.rdoBtnAndSearch.Size = new System.Drawing.Size(48, 17);
            this.rdoBtnAndSearch.TabIndex = 10;
            this.rdoBtnAndSearch.TabStop = true;
            this.rdoBtnAndSearch.Text = "AND";
            this.rdoBtnAndSearch.UseVisualStyleBackColor = true;
            // 
            // cboAdvSearchCol1
            // 
            this.cboAdvSearchCol1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAdvSearchCol1.FormattingEnabled = true;
            this.cboAdvSearchCol1.Location = new System.Drawing.Point(200, 12);
            this.cboAdvSearchCol1.Name = "cboAdvSearchCol1";
            this.cboAdvSearchCol1.Size = new System.Drawing.Size(121, 21);
            this.cboAdvSearchCol1.TabIndex = 11;
            // 
            // txtAdvSearchVal1
            // 
            this.txtAdvSearchVal1.Location = new System.Drawing.Point(200, 51);
            this.txtAdvSearchVal1.Name = "txtAdvSearchVal1";
            this.txtAdvSearchVal1.Size = new System.Drawing.Size(100, 20);
            this.txtAdvSearchVal1.TabIndex = 12;
            // 
            // txtAdvSearchVal2
            // 
            this.txtAdvSearchVal2.Location = new System.Drawing.Point(385, 51);
            this.txtAdvSearchVal2.Name = "txtAdvSearchVal2";
            this.txtAdvSearchVal2.Size = new System.Drawing.Size(100, 20);
            this.txtAdvSearchVal2.TabIndex = 14;
            // 
            // cboAdvSearchCol2
            // 
            this.cboAdvSearchCol2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAdvSearchCol2.FormattingEnabled = true;
            this.cboAdvSearchCol2.Location = new System.Drawing.Point(385, 12);
            this.cboAdvSearchCol2.Name = "cboAdvSearchCol2";
            this.cboAdvSearchCol2.Size = new System.Drawing.Size(121, 21);
            this.cboAdvSearchCol2.TabIndex = 13;
            // 
            // txtAdvSearchVal3
            // 
            this.txtAdvSearchVal3.Location = new System.Drawing.Point(550, 51);
            this.txtAdvSearchVal3.Name = "txtAdvSearchVal3";
            this.txtAdvSearchVal3.Size = new System.Drawing.Size(100, 20);
            this.txtAdvSearchVal3.TabIndex = 16;
            // 
            // cboAdvSearchCol3
            // 
            this.cboAdvSearchCol3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAdvSearchCol3.FormattingEnabled = true;
            this.cboAdvSearchCol3.Location = new System.Drawing.Point(550, 12);
            this.cboAdvSearchCol3.Name = "cboAdvSearchCol3";
            this.cboAdvSearchCol3.Size = new System.Drawing.Size(121, 21);
            this.cboAdvSearchCol3.TabIndex = 15;
            // 
            // btnAdvSearch
            // 
            this.btnAdvSearch.Location = new System.Drawing.Point(735, 41);
            this.btnAdvSearch.Name = "btnAdvSearch";
            this.btnAdvSearch.Size = new System.Drawing.Size(75, 23);
            this.btnAdvSearch.TabIndex = 17;
            this.btnAdvSearch.Text = "Search";
            this.btnAdvSearch.UseVisualStyleBackColor = true;
            this.btnAdvSearch.Click += new System.EventHandler(this.btnAdvSearch_Click);
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 469);
            this.Controls.Add(this.btnAdvSearch);
            this.Controls.Add(this.txtAdvSearchVal3);
            this.Controls.Add(this.cboAdvSearchCol3);
            this.Controls.Add(this.txtAdvSearchVal2);
            this.Controls.Add(this.cboAdvSearchCol2);
            this.Controls.Add(this.txtAdvSearchVal1);
            this.Controls.Add(this.cboAdvSearchCol1);
            this.Controls.Add(this.rdoBtnAndSearch);
            this.Controls.Add(this.linkLblAdvSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dataGridView1);
            this.Name = "test";
            this.Text = "test";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.LinkLabel linkLblAdvSearch;
        private System.Windows.Forms.RadioButton rdoBtnAndSearch;
        private System.Windows.Forms.ComboBox cboAdvSearchCol1;
        private System.Windows.Forms.TextBox txtAdvSearchVal1;
        private System.Windows.Forms.TextBox txtAdvSearchVal2;
        private System.Windows.Forms.ComboBox cboAdvSearchCol2;
        private System.Windows.Forms.TextBox txtAdvSearchVal3;
        private System.Windows.Forms.ComboBox cboAdvSearchCol3;
        private System.Windows.Forms.Button btnAdvSearch;
    }
}