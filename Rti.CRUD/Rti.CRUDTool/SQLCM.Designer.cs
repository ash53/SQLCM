namespace Rti.CRUDTool
{
    partial class SQLCM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLCM));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Insert");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Read");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Update");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Delete");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("About");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("History");
            this.tabCrudTool = new System.Windows.Forms.TabControl();
            this.tabCrudPage = new System.Windows.Forms.TabPage();
            this.gbLowerBtn = new System.Windows.Forms.GroupBox();
            this.lblCrudAdmin = new System.Windows.Forms.Label();
            this.btnCrudInsertSave = new System.Windows.Forms.Button();
            this.lblFoundRec = new System.Windows.Forms.Label();
            this.lblEditable = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblRequired = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRed = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.dgvLoadTable = new System.Windows.Forms.DataGridView();
            this.grpBoxSelectDB = new System.Windows.Forms.GroupBox();
            this.btnCRUDLoadTable = new System.Windows.Forms.Button();
            this.cboSelectTable = new System.Windows.Forms.ComboBox();
            this.lblSelectTable = new System.Windows.Forms.Label();
            this.cboCrudSearchColumn = new System.Windows.Forms.ComboBox();
            this.cboSelectUser = new System.Windows.Forms.ComboBox();
            this.lblSelectUser = new System.Windows.Forms.Label();
            this.lblCrudSearch = new System.Windows.Forms.Label();
            this.cboSelectDB = new System.Windows.Forms.ComboBox();
            this.lblSelectDB = new System.Windows.Forms.Label();
            this.btnCrudSearch = new System.Windows.Forms.Button();
            this.lblSelectRecordno = new System.Windows.Forms.Label();
            this.cboRecordPerPage = new System.Windows.Forms.ComboBox();
            this.txtCrudSearch = new System.Windows.Forms.TextBox();
            this.tabViewAbout = new System.Windows.Forms.TabPage();
            this.lblAbout = new System.Windows.Forms.Label();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.grpBoxHistory = new System.Windows.Forms.GroupBox();
            this.rdoBtnLive = new System.Windows.Forms.RadioButton();
            this.rdoBtnTest = new System.Windows.Forms.RadioButton();
            this.lblFoundHistoryRec = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboHistoryRecNo = new System.Windows.Forms.ComboBox();
            this.cboHistorySearchCol = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHistorySearch = new System.Windows.Forms.Button();
            this.txtHistorySearch = new System.Windows.Forms.TextBox();
            this.cboAction = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.btnViewHistory = new System.Windows.Forms.Button();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.treeViewMenu = new System.Windows.Forms.TreeView();
            this.tabCrudTool.SuspendLayout();
            this.tabCrudPage.SuspendLayout();
            this.gbLowerBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadTable)).BeginInit();
            this.grpBoxSelectDB.SuspendLayout();
            this.tabViewAbout.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.grpBoxHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCrudTool
            // 
            this.tabCrudTool.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCrudTool.Controls.Add(this.tabCrudPage);
            this.tabCrudTool.Controls.Add(this.tabViewAbout);
            this.tabCrudTool.Controls.Add(this.tabHistory);
            this.tabCrudTool.Location = new System.Drawing.Point(105, 0);
            this.tabCrudTool.Name = "tabCrudTool";
            this.tabCrudTool.SelectedIndex = 0;
            this.tabCrudTool.Size = new System.Drawing.Size(882, 581);
            this.tabCrudTool.TabIndex = 0;
            // 
            // tabCrudPage
            // 
            this.tabCrudPage.BackColor = System.Drawing.Color.White;
            this.tabCrudPage.Controls.Add(this.gbLowerBtn);
            this.tabCrudPage.Controls.Add(this.dgvLoadTable);
            this.tabCrudPage.Controls.Add(this.grpBoxSelectDB);
            this.tabCrudPage.Location = new System.Drawing.Point(4, 22);
            this.tabCrudPage.Name = "tabCrudPage";
            this.tabCrudPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabCrudPage.Size = new System.Drawing.Size(874, 555);
            this.tabCrudPage.TabIndex = 0;
            this.tabCrudPage.Text = "crudPage";
            // 
            // gbLowerBtn
            // 
            this.gbLowerBtn.Controls.Add(this.lblCrudAdmin);
            this.gbLowerBtn.Controls.Add(this.btnCrudInsertSave);
            this.gbLowerBtn.Controls.Add(this.lblFoundRec);
            this.gbLowerBtn.Controls.Add(this.lblEditable);
            this.gbLowerBtn.Controls.Add(this.btnDelete);
            this.gbLowerBtn.Controls.Add(this.lblRequired);
            this.gbLowerBtn.Controls.Add(this.btnCancel);
            this.gbLowerBtn.Controls.Add(this.lblRed);
            this.gbLowerBtn.Controls.Add(this.lblGreen);
            this.gbLowerBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbLowerBtn.Location = new System.Drawing.Point(3, 457);
            this.gbLowerBtn.Name = "gbLowerBtn";
            this.gbLowerBtn.Size = new System.Drawing.Size(868, 95);
            this.gbLowerBtn.TabIndex = 18;
            this.gbLowerBtn.TabStop = false;
            // 
            // lblCrudAdmin
            // 
            this.lblCrudAdmin.AutoSize = true;
            this.lblCrudAdmin.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCrudAdmin.ForeColor = System.Drawing.Color.Maroon;
            this.lblCrudAdmin.Location = new System.Drawing.Point(351, 36);
            this.lblCrudAdmin.Name = "lblCrudAdmin";
            this.lblCrudAdmin.Size = new System.Drawing.Size(208, 15);
            this.lblCrudAdmin.TabIndex = 17;
            this.lblCrudAdmin.Text = "**You are logged in as CRUD admin";
            this.lblCrudAdmin.Visible = false;
            // 
            // btnCrudInsertSave
            // 
            this.btnCrudInsertSave.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnCrudInsertSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrudInsertSave.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrudInsertSave.ForeColor = System.Drawing.Color.White;
            this.btnCrudInsertSave.Location = new System.Drawing.Point(30, 40);
            this.btnCrudInsertSave.Name = "btnCrudInsertSave";
            this.btnCrudInsertSave.Size = new System.Drawing.Size(75, 25);
            this.btnCrudInsertSave.TabIndex = 8;
            this.btnCrudInsertSave.Text = "Save";
            this.btnCrudInsertSave.UseVisualStyleBackColor = false;
            this.btnCrudInsertSave.Visible = false;
            this.btnCrudInsertSave.Click += new System.EventHandler(this.btnCrudInsertSave_Click);
            // 
            // lblFoundRec
            // 
            this.lblFoundRec.AutoSize = true;
            this.lblFoundRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoundRec.Location = new System.Drawing.Point(17, 18);
            this.lblFoundRec.Name = "lblFoundRec";
            this.lblFoundRec.Size = new System.Drawing.Size(0, 15);
            this.lblFoundRec.TabIndex = 9;
            this.lblFoundRec.Visible = false;
            // 
            // lblEditable
            // 
            this.lblEditable.AutoSize = true;
            this.lblEditable.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditable.Location = new System.Drawing.Point(760, 45);
            this.lblEditable.Name = "lblEditable";
            this.lblEditable.Size = new System.Drawing.Size(70, 15);
            this.lblEditable.TabIndex = 16;
            this.lblEditable.Text = "- Do not edit";
            this.lblEditable.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(39, 40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblRequired
            // 
            this.lblRequired.AutoSize = true;
            this.lblRequired.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequired.Location = new System.Drawing.Point(760, 19);
            this.lblRequired.Name = "lblRequired";
            this.lblRequired.Size = new System.Drawing.Size(88, 15);
            this.lblRequired.TabIndex = 15;
            this.lblRequired.Text = "- Required Field";
            this.lblRequired.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(120, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRed.ForeColor = System.Drawing.Color.LightCoral;
            this.lblRed.Location = new System.Drawing.Point(723, 44);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(30, 16);
            this.lblRed.TabIndex = 14;
            this.lblRed.Text = "Red";
            this.lblRed.Visible = false;
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreen.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblGreen.Location = new System.Drawing.Point(723, 18);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(42, 16);
            this.lblGreen.TabIndex = 13;
            this.lblGreen.Text = "Green";
            this.lblGreen.Visible = false;
            // 
            // dgvLoadTable
            // 
            this.dgvLoadTable.AllowUserToDeleteRows = false;
            this.dgvLoadTable.AllowUserToOrderColumns = true;
            this.dgvLoadTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoadTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoadTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvLoadTable.Location = new System.Drawing.Point(3, 128);
            this.dgvLoadTable.Name = "dgvLoadTable";
            this.dgvLoadTable.Size = new System.Drawing.Size(875, 323);
            this.dgvLoadTable.TabIndex = 10;
            this.dgvLoadTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoadTable_CellClick);
            this.dgvLoadTable.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLoadTable_ColumnHeaderMouseClick);
            this.dgvLoadTable.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLoadTable_DataBindingComplete);
            this.dgvLoadTable.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvLoadTable_DataError);
            this.dgvLoadTable.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvLoadTable_RowPostPaint);
            // 
            // grpBoxSelectDB
            // 
            this.grpBoxSelectDB.Controls.Add(this.btnCRUDLoadTable);
            this.grpBoxSelectDB.Controls.Add(this.cboSelectTable);
            this.grpBoxSelectDB.Controls.Add(this.lblSelectTable);
            this.grpBoxSelectDB.Controls.Add(this.cboCrudSearchColumn);
            this.grpBoxSelectDB.Controls.Add(this.cboSelectUser);
            this.grpBoxSelectDB.Controls.Add(this.lblSelectUser);
            this.grpBoxSelectDB.Controls.Add(this.lblCrudSearch);
            this.grpBoxSelectDB.Controls.Add(this.cboSelectDB);
            this.grpBoxSelectDB.Controls.Add(this.lblSelectDB);
            this.grpBoxSelectDB.Controls.Add(this.btnCrudSearch);
            this.grpBoxSelectDB.Controls.Add(this.lblSelectRecordno);
            this.grpBoxSelectDB.Controls.Add(this.cboRecordPerPage);
            this.grpBoxSelectDB.Controls.Add(this.txtCrudSearch);
            this.grpBoxSelectDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBoxSelectDB.Location = new System.Drawing.Point(3, 3);
            this.grpBoxSelectDB.Name = "grpBoxSelectDB";
            this.grpBoxSelectDB.Size = new System.Drawing.Size(868, 119);
            this.grpBoxSelectDB.TabIndex = 0;
            this.grpBoxSelectDB.TabStop = false;
            // 
            // btnCRUDLoadTable
            // 
            this.btnCRUDLoadTable.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnCRUDLoadTable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCRUDLoadTable.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCRUDLoadTable.ForeColor = System.Drawing.Color.White;
            this.btnCRUDLoadTable.Location = new System.Drawing.Point(373, 51);
            this.btnCRUDLoadTable.Name = "btnCRUDLoadTable";
            this.btnCRUDLoadTable.Size = new System.Drawing.Size(75, 29);
            this.btnCRUDLoadTable.TabIndex = 6;
            this.btnCRUDLoadTable.Text = "Load Table";
            this.btnCRUDLoadTable.UseVisualStyleBackColor = false;
            this.btnCRUDLoadTable.Click += new System.EventHandler(this.btnCRUDLoadTable_Click);
            // 
            // cboSelectTable
            // 
            this.cboSelectTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectTable.FormattingEnabled = true;
            this.cboSelectTable.Location = new System.Drawing.Point(91, 51);
            this.cboSelectTable.Name = "cboSelectTable";
            this.cboSelectTable.Size = new System.Drawing.Size(125, 21);
            this.cboSelectTable.TabIndex = 5;
            this.cboSelectTable.SelectedIndexChanged += new System.EventHandler(this.cboSelectTable_SelectedIndexChanged);
            this.cboSelectTable.Click += new System.EventHandler(this.cboSelectTable_Click);
            // 
            // lblSelectTable
            // 
            this.lblSelectTable.AutoSize = true;
            this.lblSelectTable.Location = new System.Drawing.Point(3, 51);
            this.lblSelectTable.Name = "lblSelectTable";
            this.lblSelectTable.Size = new System.Drawing.Size(67, 13);
            this.lblSelectTable.TabIndex = 4;
            this.lblSelectTable.Text = "Select Table";
            // 
            // cboCrudSearchColumn
            // 
            this.cboCrudSearchColumn.FormattingEnabled = true;
            this.cboCrudSearchColumn.Location = new System.Drawing.Point(690, 19);
            this.cboCrudSearchColumn.Name = "cboCrudSearchColumn";
            this.cboCrudSearchColumn.Size = new System.Drawing.Size(161, 21);
            this.cboCrudSearchColumn.TabIndex = 7;
            this.cboCrudSearchColumn.Click += new System.EventHandler(this.cboCrudSearchColumn_Click);
            // 
            // cboSelectUser
            // 
            this.cboSelectUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectUser.FormattingEnabled = true;
            this.cboSelectUser.Location = new System.Drawing.Point(310, 19);
            this.cboSelectUser.Name = "cboSelectUser";
            this.cboSelectUser.Size = new System.Drawing.Size(138, 21);
            this.cboSelectUser.TabIndex = 3;
            this.cboSelectUser.SelectedIndexChanged += new System.EventHandler(this.cboSelectUser_SelectedIndexChanged);
            this.cboSelectUser.Click += new System.EventHandler(this.cboSelectUser_Click);
            // 
            // lblSelectUser
            // 
            this.lblSelectUser.AutoSize = true;
            this.lblSelectUser.Location = new System.Drawing.Point(222, 22);
            this.lblSelectUser.Name = "lblSelectUser";
            this.lblSelectUser.Size = new System.Drawing.Size(62, 13);
            this.lblSelectUser.TabIndex = 2;
            this.lblSelectUser.Text = "Select User";
            // 
            // lblCrudSearch
            // 
            this.lblCrudSearch.AutoSize = true;
            this.lblCrudSearch.Location = new System.Drawing.Point(551, 22);
            this.lblCrudSearch.Name = "lblCrudSearch";
            this.lblCrudSearch.Size = new System.Drawing.Size(128, 13);
            this.lblCrudSearch.TabIndex = 6;
            this.lblCrudSearch.Text = "Select Column To Search";
            // 
            // cboSelectDB
            // 
            this.cboSelectDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectDB.FormattingEnabled = true;
            this.cboSelectDB.Location = new System.Drawing.Point(91, 19);
            this.cboSelectDB.Name = "cboSelectDB";
            this.cboSelectDB.Size = new System.Drawing.Size(125, 21);
            this.cboSelectDB.TabIndex = 1;
            this.cboSelectDB.SelectedIndexChanged += new System.EventHandler(this.cboSelectDB_SelectedIndexChanged);
            // 
            // lblSelectDB
            // 
            this.lblSelectDB.AutoSize = true;
            this.lblSelectDB.Location = new System.Drawing.Point(3, 19);
            this.lblSelectDB.Name = "lblSelectDB";
            this.lblSelectDB.Size = new System.Drawing.Size(86, 13);
            this.lblSelectDB.TabIndex = 0;
            this.lblSelectDB.Text = "Select Database";
            // 
            // btnCrudSearch
            // 
            this.btnCrudSearch.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnCrudSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrudSearch.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrudSearch.ForeColor = System.Drawing.Color.White;
            this.btnCrudSearch.Location = new System.Drawing.Point(776, 50);
            this.btnCrudSearch.Name = "btnCrudSearch";
            this.btnCrudSearch.Size = new System.Drawing.Size(75, 29);
            this.btnCrudSearch.TabIndex = 5;
            this.btnCrudSearch.Text = "Search";
            this.btnCrudSearch.UseVisualStyleBackColor = false;
            this.btnCrudSearch.Click += new System.EventHandler(this.btnCrudSearch_Click);
            // 
            // lblSelectRecordno
            // 
            this.lblSelectRecordno.AutoSize = true;
            this.lblSelectRecordno.Location = new System.Drawing.Point(222, 51);
            this.lblSelectRecordno.Name = "lblSelectRecordno";
            this.lblSelectRecordno.Size = new System.Drawing.Size(80, 13);
            this.lblSelectRecordno.TabIndex = 2;
            this.lblSelectRecordno.Text = "Show Records ";
            // 
            // cboRecordPerPage
            // 
            this.cboRecordPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRecordPerPage.FormattingEnabled = true;
            this.cboRecordPerPage.Items.AddRange(new object[] {
            "500",
            "1000",
            "1500",
            "2500",
            "3500",
            "5000",
            "All"});
            this.cboRecordPerPage.Location = new System.Drawing.Point(310, 51);
            this.cboRecordPerPage.Name = "cboRecordPerPage";
            this.cboRecordPerPage.Size = new System.Drawing.Size(57, 21);
            this.cboRecordPerPage.TabIndex = 3;
            // 
            // txtCrudSearch
            // 
            this.txtCrudSearch.Location = new System.Drawing.Point(554, 50);
            this.txtCrudSearch.Name = "txtCrudSearch";
            this.txtCrudSearch.Size = new System.Drawing.Size(182, 20);
            this.txtCrudSearch.TabIndex = 4;
            // 
            // tabViewAbout
            // 
            this.tabViewAbout.BackColor = System.Drawing.Color.White;
            this.tabViewAbout.Controls.Add(this.lblAbout);
            this.tabViewAbout.Location = new System.Drawing.Point(4, 22);
            this.tabViewAbout.Name = "tabViewAbout";
            this.tabViewAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabViewAbout.Size = new System.Drawing.Size(874, 555);
            this.tabViewAbout.TabIndex = 1;
            this.tabViewAbout.Text = "About";
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbout.Location = new System.Drawing.Point(17, 17);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(761, 290);
            this.lblAbout.TabIndex = 0;
            this.lblAbout.Text = resources.GetString("lblAbout.Text");
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.grpBoxHistory);
            this.tabHistory.Controls.Add(this.dgvHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(874, 555);
            this.tabHistory.TabIndex = 2;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // grpBoxHistory
            // 
            this.grpBoxHistory.Controls.Add(this.rdoBtnLive);
            this.grpBoxHistory.Controls.Add(this.rdoBtnTest);
            this.grpBoxHistory.Controls.Add(this.lblFoundHistoryRec);
            this.grpBoxHistory.Controls.Add(this.label2);
            this.grpBoxHistory.Controls.Add(this.cboHistoryRecNo);
            this.grpBoxHistory.Controls.Add(this.cboHistorySearchCol);
            this.grpBoxHistory.Controls.Add(this.label1);
            this.grpBoxHistory.Controls.Add(this.btnHistorySearch);
            this.grpBoxHistory.Controls.Add(this.txtHistorySearch);
            this.grpBoxHistory.Controls.Add(this.cboAction);
            this.grpBoxHistory.Controls.Add(this.lblAction);
            this.grpBoxHistory.Controls.Add(this.btnViewHistory);
            this.grpBoxHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBoxHistory.Location = new System.Drawing.Point(0, 0);
            this.grpBoxHistory.Name = "grpBoxHistory";
            this.grpBoxHistory.Size = new System.Drawing.Size(874, 98);
            this.grpBoxHistory.TabIndex = 4;
            this.grpBoxHistory.TabStop = false;
            // 
            // rdoBtnLive
            // 
            this.rdoBtnLive.AutoSize = true;
            this.rdoBtnLive.Location = new System.Drawing.Point(65, 57);
            this.rdoBtnLive.Name = "rdoBtnLive";
            this.rdoBtnLive.Size = new System.Drawing.Size(45, 17);
            this.rdoBtnLive.TabIndex = 16;
            this.rdoBtnLive.TabStop = true;
            this.rdoBtnLive.Text = "Live";
            this.rdoBtnLive.UseVisualStyleBackColor = true;
            // 
            // rdoBtnTest
            // 
            this.rdoBtnTest.AutoSize = true;
            this.rdoBtnTest.Location = new System.Drawing.Point(13, 57);
            this.rdoBtnTest.Name = "rdoBtnTest";
            this.rdoBtnTest.Size = new System.Drawing.Size(46, 17);
            this.rdoBtnTest.TabIndex = 15;
            this.rdoBtnTest.TabStop = true;
            this.rdoBtnTest.Text = "Test";
            this.rdoBtnTest.UseVisualStyleBackColor = true;
            // 
            // lblFoundHistoryRec
            // 
            this.lblFoundHistoryRec.AutoSize = true;
            this.lblFoundHistoryRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoundHistoryRec.Location = new System.Drawing.Point(377, 63);
            this.lblFoundHistoryRec.Name = "lblFoundHistoryRec";
            this.lblFoundHistoryRec.Size = new System.Drawing.Size(0, 16);
            this.lblFoundHistoryRec.TabIndex = 14;
            this.lblFoundHistoryRec.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Show Records :";
            // 
            // cboHistoryRecNo
            // 
            this.cboHistoryRecNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHistoryRecNo.FormattingEnabled = true;
            this.cboHistoryRecNo.Items.AddRange(new object[] {
            "500",
            "1000",
            "1500",
            "2500",
            "3500",
            "5000",
            "All"});
            this.cboHistoryRecNo.Location = new System.Drawing.Point(294, 53);
            this.cboHistoryRecNo.Name = "cboHistoryRecNo";
            this.cboHistoryRecNo.Size = new System.Drawing.Size(57, 21);
            this.cboHistoryRecNo.TabIndex = 13;
            // 
            // cboHistorySearchCol
            // 
            this.cboHistorySearchCol.FormattingEnabled = true;
            this.cboHistorySearchCol.Location = new System.Drawing.Point(704, 19);
            this.cboHistorySearchCol.Name = "cboHistorySearchCol";
            this.cboHistorySearchCol.Size = new System.Drawing.Size(161, 21);
            this.cboHistorySearchCol.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(565, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select Column To Search";
            // 
            // btnHistorySearch
            // 
            this.btnHistorySearch.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnHistorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHistorySearch.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorySearch.ForeColor = System.Drawing.Color.White;
            this.btnHistorySearch.Location = new System.Drawing.Point(790, 50);
            this.btnHistorySearch.Name = "btnHistorySearch";
            this.btnHistorySearch.Size = new System.Drawing.Size(75, 29);
            this.btnHistorySearch.TabIndex = 9;
            this.btnHistorySearch.Text = "Search";
            this.btnHistorySearch.UseVisualStyleBackColor = false;
            this.btnHistorySearch.Click += new System.EventHandler(this.btnHistorySearch_Click);
            // 
            // txtHistorySearch
            // 
            this.txtHistorySearch.Location = new System.Drawing.Point(568, 50);
            this.txtHistorySearch.Name = "txtHistorySearch";
            this.txtHistorySearch.Size = new System.Drawing.Size(182, 20);
            this.txtHistorySearch.TabIndex = 8;
            // 
            // cboAction
            // 
            this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAction.FormattingEnabled = true;
            this.cboAction.Items.AddRange(new object[] {
            "CRUDTRACKINGs",
            "QUERYTRACINGs"});
            this.cboAction.Location = new System.Drawing.Point(114, 19);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(147, 21);
            this.cboAction.TabIndex = 1;
            this.cboAction.SelectedIndexChanged += new System.EventHandler(this.cboAction_SelectedIndexChanged);
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(10, 19);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(97, 16);
            this.lblAction.TabIndex = 0;
            this.lblAction.Text = "Choose Table:";
            // 
            // btnViewHistory
            // 
            this.btnViewHistory.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnViewHistory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnViewHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewHistory.ForeColor = System.Drawing.Color.White;
            this.btnViewHistory.Location = new System.Drawing.Point(276, 19);
            this.btnViewHistory.Name = "btnViewHistory";
            this.btnViewHistory.Size = new System.Drawing.Size(75, 23);
            this.btnViewHistory.TabIndex = 2;
            this.btnViewHistory.Text = "View";
            this.btnViewHistory.UseVisualStyleBackColor = false;
            this.btnViewHistory.Click += new System.EventHandler(this.btnViewHistory_Click);
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToOrderColumns = true;
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(0, 104);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.Size = new System.Drawing.Size(874, 451);
            this.dgvHistory.TabIndex = 3;
            this.dgvHistory.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvHistory_ColumnHeaderMouseClick);
            this.dgvHistory.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvHistory_DataBindingComplete);
            this.dgvHistory.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvHistory_RowPostPaint);
            // 
            // treeViewMenu
            // 
            this.treeViewMenu.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.treeViewMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeViewMenu.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewMenu.ForeColor = System.Drawing.SystemColors.Window;
            this.treeViewMenu.LineColor = System.Drawing.Color.White;
            this.treeViewMenu.Location = new System.Drawing.Point(0, 0);
            this.treeViewMenu.Name = "treeViewMenu";
            treeNode1.Name = "NodeCrudInsert";
            treeNode1.Text = "Insert";
            treeNode2.Name = "NodeCrudRead";
            treeNode2.Text = "Read";
            treeNode3.Name = "NodeCrudUpdate";
            treeNode3.Text = "Update";
            treeNode4.Name = "NodeCrudDelete";
            treeNode4.Text = "Delete";
            treeNode5.Name = "abt";
            treeNode5.Text = "About";
            treeNode6.Name = "nodeViewHistory";
            treeNode6.Text = "History";
            this.treeViewMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeViewMenu.Size = new System.Drawing.Size(103, 581);
            this.treeViewMenu.TabIndex = 11;
            // 
            // SQLCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 581);
            this.Controls.Add(this.treeViewMenu);
            this.Controls.Add(this.tabCrudTool);
            this.Name = "SQLCM";
            this.Text = "SQLCM";
            this.tabCrudTool.ResumeLayout(false);
            this.tabCrudPage.ResumeLayout(false);
            this.gbLowerBtn.ResumeLayout(false);
            this.gbLowerBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadTable)).EndInit();
            this.grpBoxSelectDB.ResumeLayout(false);
            this.grpBoxSelectDB.PerformLayout();
            this.tabViewAbout.ResumeLayout(false);
            this.tabViewAbout.PerformLayout();
            this.tabHistory.ResumeLayout(false);
            this.grpBoxHistory.ResumeLayout(false);
            this.grpBoxHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCrudTool;
        private System.Windows.Forms.TabPage tabCrudPage;
        private System.Windows.Forms.TabPage tabViewAbout;
        private System.Windows.Forms.TreeView treeViewMenu;
        private System.Windows.Forms.GroupBox grpBoxSelectDB;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.Label lblSelectDB;
        private System.Windows.Forms.Label lblSelectTable;
        private System.Windows.Forms.Button btnCRUDLoadTable;
        private System.Windows.Forms.ComboBox cboRecordPerPage;
        private System.Windows.Forms.Label lblSelectRecordno;
        private System.Windows.Forms.Button btnCrudSearch;
        private System.Windows.Forms.TextBox txtCrudSearch;
        private System.Windows.Forms.ComboBox cboCrudSearchColumn;
        private System.Windows.Forms.Label lblCrudSearch;
        private System.Windows.Forms.Button btnCrudInsertSave;
        private System.Windows.Forms.Label lblFoundRec;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.Label lblEditable;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Label lblCrudAdmin;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.ComboBox cboAction;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.GroupBox gbLowerBtn;
        private System.Windows.Forms.GroupBox grpBoxHistory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboHistoryRecNo;
        private System.Windows.Forms.ComboBox cboHistorySearchCol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHistorySearch;
        private System.Windows.Forms.TextBox txtHistorySearch;
        private System.Windows.Forms.Label lblFoundHistoryRec;
        private System.Windows.Forms.RadioButton rdoBtnLive;
        private System.Windows.Forms.RadioButton rdoBtnTest;
        public System.Windows.Forms.ComboBox cboSelectUser;
        public System.Windows.Forms.ComboBox cboSelectDB;
        public System.Windows.Forms.ComboBox cboSelectTable;
        public System.Windows.Forms.DataGridView dgvLoadTable;

    }
}

