using System;
using System.Linq;
using System.Windows.Forms;
using Rti.DataModel;
using System.Drawing;
using System.Data.Entity;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Data.Linq.SqlClient;
using Rti.EncryptionLib;
using System.Configuration;
using System.Data.Entity.Infrastructure;


namespace Rti.CRUDTool
{
    public partial class SQLCM : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        CadAdminModelContainer cadContext = new CadAdminModelContainer();
        RtTransBrokerAdminModelContainer rtContext = new RtTransBrokerAdminModelContainer();
        TowerModelContainer twrContext = new TowerModelContainer();
        TowerModelContainer twrTest = new TowerModelContainer(); //Default TowerModelContainer
        public BindingSource crudBindingSource = new BindingSource(); //Bindingsource for dgvLoadTable
        BindingSource HistoryBinding = new BindingSource(); //Bindingsource for dgvHistory
        CheckBox chkbox = new CheckBox();
        static int crudCount = 0; //keeps count if user has already entered credentials for accessing CRUDRULES table
        
        public SQLCM()
        {
            InitializeComponent();
            //removes the header of the tab control
            tabCrudTool.Appearance = TabAppearance.FlatButtons;
            tabCrudTool.ItemSize = new Size(0, 1);
            tabCrudTool.SizeMode = TabSizeMode.Fixed;
            //When the app loads up for the first time, initially select the tabPage for CRUD operation
            tabCrudTool.SelectedTab = tabCrudPage;
            //Node mouse click event for treeViewMenu
            this.treeViewMenu.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeViewMenu_NodeMouseClick);
            this.dgvLoadTable.EditingControlShowing += HandleEditShowing;
            this.dgvLoadTable.CellValueChanged += new DataGridViewCellEventHandler(dgvLoadTable_CellValueChanged);
            

            try
            {
                //load database names from table CRUDRULES in cboSelectDB
                cboSelectDB.Items.Clear(); //Clears the item with each click, so that same items are not loaded again
                var cboDbItem = (from a in twrTest.CRUDRULES where a.DBNAME != null select a.DBNAME).Distinct().OrderBy(x => x);
                foreach (var dbName in cboDbItem)
                    cboSelectDB.Items.Add(dbName);
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }
        //end of CRUDTool() function
        /**********************************************************************************************************/

        //function for adding item to cboSelectUser when a database is selected in cboSelectDB
        private void cboSelectDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboSelectUser.SelectedIndex = -1; //clears combobox text upon new database selection
                cboSelectUser.Items.Clear();
                //Loads items in cboSelectUser when database in cboSelectDB is selected
                var cboSelectUserItem = (from a in twrTest.CRUDRULES where a.RTISCHEMA != null && a.DBNAME == cboSelectDB.Text select a.RTISCHEMA).Distinct().OrderBy(x => x);
                foreach (var schemaName in cboSelectUserItem)
                    cboSelectUser.Items.Add(schemaName);
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }

        }
        //end of cboSelectDB_SelectedIndexChanged()
        /*********************************************************************************/

        //function for adding items in cboSelectTable according to the selected user
        private void cboSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboSelectTable.SelectedIndex = -1; //clears combobox text upon new selection
                cboSelectTable.Items.Clear();
                //loads tablenames in cmbSelectTable upon selecting DB name and user name
                var cboSelectTableItem = (from a in twrTest.CRUDRULES
                                          where a.RTITABLE != null && a.DBNAME == cboSelectDB.Text && a.RTISCHEMA == cboSelectUser.Text
                                          && a.RTITABLE !="CRUDTRACKINGs" && a.RTITABLE !="QUERYTRACINGs"
                                          select a.RTITABLE).Distinct().OrderBy(x => x);
                foreach (var tableName in cboSelectTableItem)
                    cboSelectTable.Items.Add(tableName);
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }

        }//end of cboSelectUser_SelectedIndexChanged()
        /*****************************************************************************/

        //notify user to select DB first before selecting user name
        private void cboSelectUser_Click(object sender, EventArgs e)
        {

            if (cboSelectDB.SelectedIndex <= -1)
                MessageBox.Show("Please select database first");
        }//end of cboSelectUser_Click()
        /****************************************************************************/

        //notify user to select DB/user first before selecting table name
        private void cboSelectTable_Click(object sender, EventArgs e)
        {

            if (cboSelectDB.SelectedIndex <= -1 || cboSelectUser.SelectedIndex <= -1)
                MessageBox.Show("Please select database or user first");
        }//end of cboSelectTable_Click()
        /****************************************************************************/

        //notify user to load a table first before selecting column for search
        private void cboCrudSearchColumn_Click(object sender, EventArgs e)
        {

            if (cboSelectTable.SelectedIndex <= -1)
                MessageBox.Show("Please select a table and load it first");
        }//end of cboCrudSearchColumn_Click()
        /****************************************************************************/

        //This function displays serial number in datagridview dgvLoadTable
        private void dgvLoadTable_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvLoadTable.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }//end of dgvLoadTable_RowPostPaint()
        /****************************************************************************/

        //Function for loading data in datagridview dgvLoadTable
         private void crudLoadData(string RecordNum)
        {
            var context = new object();
            var TableName = cboSelectTable.Text.ToString();

            if (cboSelectUser.Text.ToString() == "CADADMIN" || cboSelectUser.Text.ToString() == "CADUSER")
            {
                cadContext = new CadAdminModelContainer();
                //CADADMIN@DEVDB01
                if (cboSelectDB.Text.ToString() == "DEVDB01")
                    cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerTest"].ConnectionString;
                //CADUSER@RTIDB01
                else if (cboSelectDB.Text.ToString() == "RTIDB01")
                    cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerLive"].ConnectionString;
                context = cadContext;
            }
            else if (cboSelectUser.Text.ToString() == "RTTRANSBROKERADMIN" || cboSelectUser.Text.ToString() == "RTTRANSBROKER")
            {
                rtContext = new RtTransBrokerAdminModelContainer();
                //RTTRANSBROKERADMIN@DEVDB01
                if (cboSelectDB.Text.ToString() == "DEVDB01")
                    rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerTest"].ConnectionString;
                //RTTRANSBROKER@RTIDB01
                else if (cboSelectDB.Text.ToString() == "RTIDB01")
                    rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerLive"].ConnectionString;
                context = rtContext;
            }
            else if (cboSelectUser.Text.ToString() == "TOWER" || cboSelectUser.Text.ToString() == "TOWERPROD")
            {
                twrContext = new TowerModelContainer();
                //TOWER@RTITSTA
                if (cboSelectDB.Text.ToString() == "RTITSTA")
                    twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                //TOWERPROD@TWRPROD.RTI.COM
                else if (cboSelectDB.Text.ToString() == "TWRPROD.RTI.COM")
                    twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                context = twrContext;
            }

            try
            {
            var truncatedData = new object();
            var rawData = context.GetType().GetProperty(TableName).GetValue(context, null);
            if (RecordNum == "All")
            {
                truncatedData = ((IQueryable<object>)rawData).ToList();
            }
            else
            {
                int RecNo = Int32.Parse(RecordNum);
                truncatedData = ((IQueryable<object>)rawData).Take(RecNo).ToList();
            }
            crudBindingSource.DataSource = new BindingSource { DataSource = truncatedData };
            dgvLoadTable.DataSource = crudBindingSource;
            dgvLoadTable.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//end of crudLoadData()
        /*****************************************************************************************************************/

        //Function for "Load Table" button click
        private void btnCRUDLoadTable_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnCrudInsertSave.Visible = false;
            btnDelete.Visible = false;
            lblGreen.Visible = false;
            lblRed.Visible = false;
            lblRequired.Visible = false;
            lblEditable.Visible = false;
            lblFoundRec.Visible = false;
            //Automatically select item when nothing is selected in cboRecordPerPage
            if (cboRecordPerPage.SelectedIndex <= -1)
                cboRecordPerPage.SelectedIndex = 0;
            string RecordNo = cboRecordPerPage.Text.ToString();
            //prompt user when "Load Table" button is clicked without selecting any database or table or user 
            if (cboSelectDB.SelectedIndex <= -1 || cboSelectUser.SelectedIndex <= -1 || cboSelectTable.SelectedIndex <= -1)
            {
                MessageBox.Show("Please select database, user and table first");
            }
            else
            {
                dgvLoadTable.DataSource = null;
                dgvLoadTable.Columns.Clear();// clear any existing column
                clearSelection();
                dgvLoadTable.AllowUserToAddRows = false;
                addChkBox(); //call function to add checkboxes
                crudLoadData(RecordNo); //call function to load data in dgvLoadTable

            }//end of else statement
        }//end of btnCRUDLoadTable_Click()
        /****************************************************************************************************************************/
        //makes the combobox columns in datagridview editable
        private void HandleEditShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var cbo = e.Control as ComboBox;
            if (cbo == null)
            {
                return;
            }

            cbo.DropDownStyle = ComboBoxStyle.DropDown;
            cbo.Validating -= HandleComboBoxValidating;
            cbo.Validating += HandleComboBoxValidating;

        }//end of HandleEditShowing()


        private void HandleComboBoxValidating(object sender, CancelEventArgs e)
        {
            var combo = sender as DataGridViewComboBoxEditingControl;
            if (combo == null)
            {
                return;
            }
            if (!combo.Items.Contains(combo.Text)) //check if item is already in drop down, if not, add it to all
            {
                var comboColumn = this.dgvLoadTable.Columns[this.dgvLoadTable.CurrentCell.ColumnIndex] as DataGridViewComboBoxColumn;
                combo.Items.Add(combo.Text);
                comboColumn.Items.Add(combo.Text);
                this.dgvLoadTable.CurrentCell.Value = combo.Text;
            }
        }//end of HandleComboBoxValidating()
        /************************************************************************************************************************************************/
        //Function for making datagridview combobox column
        private void dgvCboColumn(dynamic item, string colName)
        {
            int i = dgvLoadTable.Columns[colName].Index;
            DataGridViewComboBoxColumn dgvCol = new DataGridViewComboBoxColumn();
            foreach (var rec in item)
                dgvCol.Items.Add(rec);
            dgvCol.DataPropertyName = colName;
            dgvLoadTable.Columns.Insert(i, dgvCol);
            dgvLoadTable.Columns[i].HeaderText = dgvLoadTable.Columns[i + 1].HeaderText;
            dgvLoadTable.Columns[i + 1].Visible = false;
            dgvLoadTable.Columns.RemoveAt(i + 1);
            var isRequired = from a in twrTest.CRUDRULES
                             where a.RTICOLUMN == colName &&
                             a.RTISCHEMA == cboSelectUser.Text && a.DBNAME == cboSelectDB.Text && a.ISCOMBOBOXCOLUMN == "Y"
                             select a.ISREQUIRED;
            if (isRequired.Contains("Y"))
                dgvLoadTable.Columns[i].DefaultCellStyle.BackColor = Color.YellowGreen;
            var canEdit = from a in twrTest.CRUDRULES
                          where a.RTICOLUMN == colName &&
                          a.RTISCHEMA == cboSelectUser.Text && a.DBNAME == cboSelectDB.Text &&
                          a.ISCOMBOBOXCOLUMN == "Y"
                          select a.CANEDIT;
            if (canEdit.Contains("N"))
                dgvLoadTable.Columns[i].DefaultCellStyle.BackColor = Color.LightCoral;
        }//end of  dgvCboColumn()
        /********************************************************************************************************/

        //Function for data insertion
        public void crudInsert()
        {

            //prompt user to select database,table and user 
            if (cboSelectDB.SelectedIndex <= -1 || cboSelectUser.SelectedIndex <= -1 || cboSelectTable.SelectedIndex <= -1)
            {
                MessageBox.Show("Please select database, user and table first and then click insert");
            }
            else
            {
                try
                {
                    checkCrudrules();
                    if (cboSelectTable.Text.ToString() == "CRUDRULES" && crudCount == 0)
                    { }
                    else
                    {
                        dgvLoadTable.DataSource = null;
                        dgvLoadTable.Columns.Clear();// clear any existing column
                        addChkBox();
                        crudLoadData("0"); //Calls function to load data in datagridview and recordNum is set to 0 for showing the header only in dgvLoadTable
                        dgvLoadTable.ReadOnly = false;
                        dgvLoadTable.AllowUserToAddRows = true;
                        crudLocalBinding();
                        setCboColumns();
                        setNonEditableColumnColor();
                    }
                }//end of try block
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }
            }//end of else statement
        }//end of crudInsert()
        /*******************************************************************************************************************************************/
        //Function for setting combo box columns in the gridview
        private void setCboColumns()
        {
            var context = new object();
            try
            {
                if (cboSelectUser.Text.ToString() == "CADADMIN" || cboSelectUser.Text.ToString() == "CADUSER")
                {
                    CadAdminModelContainer entityModel = new CadAdminModelContainer();
                    //CADADMIN@DEVDB01
                    if (cboSelectDB.Text.ToString() == "DEVDB01")
                        entityModel.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerTest"].ConnectionString;
                    //CADUSER@RTIDB01
                    else if (cboSelectDB.Text.ToString() == "RTIDB01")
                        entityModel.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerLive"].ConnectionString;
                    context = entityModel;
                }
                else if (cboSelectUser.Text.ToString() == "RTTRANSBROKERADMIN" || cboSelectUser.Text.ToString() == "RTTRANSBROKER")
                {
                    RtTransBrokerAdminModelContainer entityModel = new RtTransBrokerAdminModelContainer();
                    //RTTRANSBROKERADMIN@DEVDB01
                    if (cboSelectDB.Text.ToString() == "DEVDB01")
                        entityModel.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerTest"].ConnectionString;
                    //RTTRANSBROKER@RTIDB01
                    else if (cboSelectDB.Text.ToString() == "RTIDB01")
                        entityModel.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerLive"].ConnectionString;
                    context = entityModel;
                }
                else if (cboSelectUser.Text.ToString() == "TOWER" || cboSelectUser.Text.ToString() == "TOWERPROD")
                {
                    TowerModelContainer entityModel = new TowerModelContainer();
                    //TOWER@RTITSTA
                    if (cboSelectDB.Text.ToString() == "RTITSTA")
                        entityModel.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                    //TOWERPROD@TWRPROD.RTI.COM
                    else if (cboSelectDB.Text.ToString() == "TWRPROD.RTI.COM")
                        entityModel.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                    context = entityModel;
                }

                //get the table name from comboBox 'Select Table'    
                string tableName = cboSelectTable.Text.ToString();
                var comboBoxColumn = (from a in twrTest.CRUDRULES
                                      where a.ISCOMBOBOXCOLUMN == "Y" && a.RTITABLE == tableName &&
                                          a.RTISCHEMA == cboSelectUser.Text && a.DBNAME == cboSelectDB.Text
                                      select a.RTICOLUMN);
                
                foreach (var colName in comboBoxColumn)
                {
                    var getTableName = context.GetType().GetProperty(tableName).GetValue(context, null);
                    var getData = ((IQueryable<object>)getTableName).Where(colName + "!=null").Select(colName).Distinct().OrderBy("it");
                    dgvCboColumn(getData, colName);
                }
                dgvLoadTable.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
        }
        /*********************************************************************************************************************************************************************/
        //treeViewMenu click event
        private void treeViewMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                switch (e.Node.Index)
                {
                    case 0: //create
                        tabCrudTool.SelectedTab = tabCrudPage; //selects the tabCrudPage of tabCrudTool
                        lblFoundRec.Visible = false;
                        btnCrudInsertSave.Visible = true;
                        btnDelete.Visible = false;
                        btnCancel.Visible = true;
                        clearSelection();
                        crudInsert(); //calls function for insertion operation
                        break;
                    case 1: //read
                        tabCrudTool.SelectedTab = tabCrudPage; //selects the tabCrudPage of tabCrudTool
                        lblFoundRec.Visible = false;
                        lblGreen.Visible = false;
                        lblRed.Visible = false;
                        lblRequired.Visible = false;
                        lblEditable.Visible = false;
                        btnCrudInsertSave.Visible = false;
                        btnDelete.Visible = false;
                        btnCancel.Visible = false;
                        dgvLoadTable.DataSource = null;
                        dgvLoadTable.Controls.Clear();
                        dgvLoadTable.Columns.Clear();
                        dgvLoadTable.Rows.Clear();
                        break;
                    case 2: //update
                        tabCrudTool.SelectedTab = tabCrudPage; //selects the tabCrudPage of tabCrudTool
                        lblFoundRec.Visible = false;
                        btnCrudInsertSave.Visible = true;
                        btnDelete.Visible = false;
                        btnCancel.Visible = true;
                        if (dgvLoadTable.SelectedRows.Count > 0)
                            crudUpdate();
                        else
                            MessageBox.Show("Please select records to update.");
                        break;
                    case 3://delete
                        tabCrudTool.SelectedTab = tabCrudPage; //selects the tabCrudPage of tabCrudTool
                        crudLocalBinding();
                        lblFoundRec.Visible = false;
                        lblGreen.Visible = false;
                        lblRed.Visible = false;
                        lblRequired.Visible = false;
                        lblEditable.Visible = false;
                        btnCrudInsertSave.Visible = false;
                        btnDelete.Visible = true;
                        btnCancel.Visible = true;
                        break;
                    case 4: //General information about CRUDTool
                        tabCrudTool.SelectedTab = tabViewAbout;
                        break;
                    case 5://view transaction history
                        tabCrudTool.SelectedTab = tabHistory;
                       
                        break;
                    default:
                        break;
                } //end of switch 
            }//end of try block
            catch (Exception nodeEx)
            {
                MessageBox.Show("Error: " + nodeEx);
            }
        }//end of treeViewMenu click event
        /****************************************************************************************************************************/

        //Search operation on dgvLoadTable
        private void btnCrudSearch_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnCrudInsertSave.Visible = false;
            btnDelete.Visible = false;
            lblGreen.Visible = false;
            lblRed.Visible = false;
            lblRequired.Visible = false;
            lblEditable.Visible = false;
            //check if value is given for search
            if (cboCrudSearchColumn.SelectedIndex != -1 && txtCrudSearch.Text != string.Empty)
            {
                dgvLoadTable.CurrentCell = null;
                dgvLoadTable.AllowUserToAddRows = false;
                try
                {
                    addChkBox();
                    var context = new object(); ///variable for using the EDM entity
                    var TableName = cboSelectTable.Text.ToString();
                    var columnName = cboCrudSearchColumn.Text.ToString();
                    string searchValue = txtCrudSearch.Text.ToString();
                    var truncatedData = new object();
                    int n, c = 0;
                    //String dataType="N";
                    bool isNumeric = int.TryParse(searchValue, out n);
                    DateTime temp;
                    var checkColType = (from colType in twrTest.CRUDRULES
                                        where colType.RTICOLUMN == cboCrudSearchColumn.Text.ToLower() &&
                                            colType.RTISCHEMA == cboSelectUser.Text && colType.RTITABLE == cboSelectTable.Text &&
                                            colType.COLUMNTYPE != null
                                        select colType.COLUMNTYPE).FirstOrDefault();


                    if (cboSelectUser.Text.ToString() == "CADADMIN" || cboSelectUser.Text.ToString() == "CADUSER")
                    {
                        cadContext = new CadAdminModelContainer();
                        //CADADMIN@DEVDB01
                        if (cboSelectDB.Text.ToString() == "DEVDB01")
                            cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerTest"].ConnectionString;
                        //CADUSER@RTIDB01
                        else if (cboSelectDB.Text.ToString() == "RTIDB01")
                            cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerLive"].ConnectionString;
                        context = cadContext;
                    }
                    else if (cboSelectUser.Text.ToString() == "RTTRANSBROKERADMIN" || cboSelectUser.Text.ToString() == "RTTRANSBROKER")
                    {
                        rtContext = new RtTransBrokerAdminModelContainer();
                        //RTTRANSBROKERADMIN@DEVDB01
                        if (cboSelectDB.Text.ToString() == "DEVDB01")
                            rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerTest"].ConnectionString;
                        //RTTRANSBROKER@RTIDB01
                        else if (cboSelectDB.Text.ToString() == "RTIDB01")
                            rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerLive"].ConnectionString;
                        context = rtContext;
                    }
                    else if (cboSelectUser.Text.ToString() == "TOWER" || cboSelectUser.Text.ToString() == "TOWERPROD")
                    {
                        twrContext = new TowerModelContainer();
                        //TOWER@RTITSTA
                        if (cboSelectDB.Text.ToString() == "RTITSTA")
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                        //TOWERPROD@TWRPROD.RTI.COM
                        else if (cboSelectDB.Text.ToString() == "TWRPROD.RTI.COM")
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                        context = twrContext;
                    }

                    var rawData = context.GetType().GetProperty(TableName).GetValue(context, null);

                    if (isNumeric && checkColType.Contains("NUMBER"))
                    {
                        int x = Int32.Parse(txtCrudSearch.Text);
                        truncatedData = ((IQueryable<object>)rawData).Where(columnName + "=@0", x).ToList();
                    }

                    else if (DateTime.TryParse(txtCrudSearch.Text, out temp) && checkColType.Contains("DATE"))
                    {
                        var parsedDt = DateTime.Parse(txtCrudSearch.Text);
                        var nextDay = parsedDt.AddDays(1);
                        truncatedData = ((IQueryable<object>)rawData).Where(columnName + ">= @0 && " + columnName + " < @1", parsedDt, nextDay).ToList();
                    }
                    else
                    {

                        truncatedData = ((IQueryable<object>)rawData).Where(columnName + "=@0", searchValue).ToList();

                    }

                    crudBindingSource.DataSource = new BindingSource { DataSource = truncatedData };
                    dgvLoadTable.DataSource = crudBindingSource;
                    dgvLoadTable.Refresh();

                    //dgvLoadTable.DataSource = truncatedData;
                    txtCrudSearch.Text = String.Empty;
                    foreach (DataGridViewRow row in dgvLoadTable.Rows)
                    { c++; }
                    if (c > 0)
                    {
                        lblFoundRec.Visible = true;
                        lblFoundRec.Text = c + " records found";
                    }
                    else
                    {
                        lblFoundRec.Visible = false;
                        MessageBox.Show("No records are found.");
                    }
                       

                }//end of try block
                catch (Exception exc)
                {
                    // MessageBox.Show(exc.Message);
                }


            }
            else
                MessageBox.Show("Please select column name and enter search value.");

        }//end of btnCrudSearch_Click()



        /*****************************************************************************************************/
        //autocomplete combobox---> cboCrudSearchColumn textChanged event
        private void cboCrudSearchColumn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int itemsIndex = 0;
                foreach (string item in cboCrudSearchColumn.Items)
                {
                    if (item.IndexOf(cboCrudSearchColumn.Text) == 0)
                    {
                        cboCrudSearchColumn.SelectedIndex = itemsIndex;
                        cboCrudSearchColumn.Select(cboCrudSearchColumn.Text.Length - 1, 0);
                        break;
                    }
                    itemsIndex++;
                }//end of for-each
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }//end of cboCrudSearchColumn textChanged event
        /***************************************************************************************************************/

        //Save inserted data
        private void btnCrudInsertSave_Click(object sender, EventArgs e)
        {
            var context = new object();
            bool correct = true;
            string TestOrLive = null;
            //checks if any data is given as input before clicking the save button
            try
            {
                int rowNum = dgvLoadTable.CurrentCellAddress.Y;
                if (rowNum == -1)
                {
                    MessageBox.Show("No input is given!");
                    return;
                }

                //check if row is empty, simply return
                else if (IsRowEmpty(rowNum))
                {
                    MessageBox.Show("There is no data in the selected row!");
                    return;
                }

                else if (dgvLoadTable.SelectedRows.Count > 0)
                {

                    var ColumnRequired = (from col in twrTest.CRUDRULES
                                          where col.ISREQUIRED == "Y" && col.RTITABLE == cboSelectTable.Text && col.RTISCHEMA == cboSelectUser.Text
                                              && col.RTICOLUMN != null
                                          select col.RTICOLUMN);
                    //for (int i = 0; i < (dgvLoadTable.Rows.Count - 1); i++)
                    foreach (DataGridViewRow row in dgvLoadTable.SelectedRows)
                    {
                        foreach (var column in ColumnRequired)
                        {
                            if (row.Cells[column].Value == null || row.Cells[column].Value == DBNull.Value ||
                                row.Cells[column].Value.ToString() == String.Empty)
                            {
                                //MessageBox.Show("Please enter input for the all required(green) field.");
                                correct = false;
                            }
                            else
                                correct = true;
                        }
                    }
                    if (correct)
                    {
                        TransactionDetails tr = new TransactionDetails();
                        if (tr.ShowDialog() == DialogResult.OK)
                        {
                            if (cboSelectUser.Text.ToString() == "CADADMIN" || cboSelectUser.Text.ToString() == "CADUSER")
                            {
                                cadContext.Configuration.AutoDetectChangesEnabled = true;
                                //CADADMIN@DEVDB01
                                if (cboSelectDB.Text.ToString() == "DEVDB01")
                                {
                                    cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerTest"].ConnectionString;
                                    TestOrLive = "Test";
                                }
                                    //CADUSER@RTIDB01
                                else if (cboSelectDB.Text.ToString() == "RTIDB01")
                                {
                                    cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerLive"].ConnectionString;
                                    TestOrLive = "Live";
                                }
                                //TransactionDetails tr = new TransactionDetails();
                                //if (tr.ShowDialog() == DialogResult.OK)
                                //{
                                //cadContext.HookSaveChanges(FuncDelegate);
                                TransactionDetails.DetectChanges(cadContext, TestOrLive);
                                cadContext.SaveChanges();
                                //}

                            }
                            else if (cboSelectUser.Text.ToString() == "RTTRANSBROKERADMIN" || cboSelectUser.Text.ToString() == "RTTRANSBROKER")
                            {
                                //RTTRANSBROKERADMIN@DEVDB01
                                if (cboSelectDB.Text.ToString() == "DEVDB01")
                                {
                                    rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerTest"].ConnectionString;
                                    TestOrLive = "Test";
                                }
                                    //RTTRANSBROKER@RTIDB01
                                else if (cboSelectDB.Text.ToString() == "RTIDB01")
                                {
                                    rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerLive"].ConnectionString;
                                    TestOrLive = "Test";
                                }
                                //TransactionDetails tr = new TransactionDetails();
                                //if (tr.ShowDialog() == DialogResult.OK)
                                //{
                                //rtContext.HookSaveChanges(FuncDelegate);
                                TransactionDetails.DetectChanges(rtContext, TestOrLive);
                                rtContext.SaveChanges();
                                //}
                            }
                            else if (cboSelectUser.Text.ToString() == "TOWER" || cboSelectUser.Text.ToString() == "TOWERPROD")
                            {
                                //TOWER@RTITSTA
                                if (cboSelectDB.Text.ToString() == "RTITSTA")
                                {
                                    twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                                    TestOrLive = "Test";
                                }//TOWERPROD@TWRPROD.RTI.COM
                                else if (cboSelectDB.Text.ToString() == "TWRPROD.RTI.COM")
                                {
                                    twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                                    TestOrLive = "Live";
                                }
                                //TransactionDetails tr = new TransactionDetails();
                                //if (tr.ShowDialog() == DialogResult.OK)
                                //{
                                //twrContext.HookSaveChanges(FuncDelegate);
                                TransactionDetails.DetectChanges(twrContext, TestOrLive);
                                twrContext.SaveChanges();
                                //}
                            }
                            MessageBox.Show("Records saved successfully.");
                        }
                    }

                    else
                        MessageBox.Show("Please enter input for the all required(green) field.");
                }
                else
                    MessageBox.Show("Please select the rows you want to save.");
            }//end of try
            catch (Exception saveEx)
            {
                MessageBox.Show("Error: " + saveEx);
            }
        }//end of function btnCrudInsertSave_Click
        /******************************************************************************************/
        //function for adding new object in the entity model
        void crudLocalBinding()
        {
            var entity = new object();
            var tableName = cboSelectTable.Text.ToString();
            try
            {
                if (cboSelectUser.Text.ToString() == "RTTRANSBROKERADMIN" || cboSelectUser.Text.ToString() == "RTTRANSBROKER")
                {
                    //RTTRANSBROKERADMIN@DEVDB01
                    if (cboSelectDB.Text.ToString() == "DEVDB01")
                        rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerTest"].ConnectionString;
                    //RTTRANSBROKER@RTIDB01
                    else if (cboSelectDB.Text.ToString() == "RTIDB01")
                        rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerLive"].ConnectionString;
                
                    switch (cboSelectTable.Text.ToString())
                    {
                        case "RTTRANS_HL7_FTP_CONFIG":
                            crudBindingSource.DataSource = rtContext.RTTRANS_HL7_FTP_CONFIG.Local.ToBindingList();
                            break;
                        case "ETL_CONFIG":
                            crudBindingSource.DataSource = rtContext.ETL_CONFIG.Local.ToBindingList();
                            break;
                        case "CUSTOMIZESEGMENTS":
                            crudBindingSource.DataSource = rtContext.CUSTOMIZESEGMENTS.Local.ToBindingList();
                            break;
                        case "FILTERHL7":
                            crudBindingSource.DataSource = rtContext.FILTERHL7.Local.ToBindingList();
                            break;
                    }//end of switch

                }//end of if user is RTTRANSBROKERADMIN 
                if (cboSelectUser.Text.ToString() == "CADADMIN" || cboSelectUser.Text.ToString() == "CADUSER")
                {
                    //CADADMIN@DEVDB01
                    if (cboSelectDB.Text.ToString() == "DEVDB01")
                        cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerTest"].ConnectionString;
                    //CADUSER@RTIDB01
                    else if (cboSelectDB.Text.ToString() == "RTIDB01")
                        cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerLive"].ConnectionString;
                
                    switch (cboSelectTable.Text.ToString())
                    {
                        case "TOWERIMPORTs":
                            crudBindingSource.DataSource = cadContext.TOWERIMPORTs.Local.ToBindingList();
                            break;
                        case "HL7_FTP_CONFIG":
                            crudBindingSource.DataSource = cadContext.HL7_FTP_CONFIG.Local.ToBindingList();
                            break;
                        case "EMSCAN_BATCH":
                            crudBindingSource.DataSource = cadContext.EMSCAN_BATCH.Local.ToBindingList();
                            break;
                        case "TOWERIMPORTCLIENTs":
                            crudBindingSource.DataSource = cadContext.TOWERIMPORTCLIENTs.Local.ToBindingList();
                            break;
                        case "RTI_FILETYPE":
                            crudBindingSource.DataSource = cadContext.RTI_FILETYPE.Local.ToBindingList();
                            break;
                        case "RTI_ACCTSCENARIO":
                            crudBindingSource.DataSource = cadContext.RTI_ACCTSCENARIO.Local.ToBindingList();
                            break;

                    }//end of switch

                }//end of if user is CADADMIN
                if (cboSelectUser.Text.ToString() == "TOWER" || cboSelectUser.Text.ToString() == "TOWERPROD")
                {
                    //TOWER@RTITSTA
                    if (cboSelectDB.Text.ToString() == "RTITSTA")
                        twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                    //TOWERPROD@TWRPROD.RTI.COM
                    else if (cboSelectDB.Text.ToString() == "TWRPROD.RTI.COM")
                        twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                
                    switch (cboSelectTable.Text.ToString())
                    {
                        case "RTICLIENTs":
                            crudBindingSource.DataSource = twrContext.RTICLIENTs.Local.ToBindingList();
                            break;
                        case "CRUDRULES":
                            crudBindingSource.DataSource = twrContext.CRUDRULES.Local.ToBindingList();
                            break;
                        case "BPRRULES":
                            crudBindingSource.DataSource = twrContext.BPRRULES.Local.ToBindingList();
                            break;
                    }

                }
                if (tabCrudTool.SelectedTab == tabHistory)
                {
                    //TOWER@RTITSTA
                        if (rdoBtnTest.Checked == true)
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                        //TOWERPROD@TWRPROD.RTI.COM
                        else if (rdoBtnLive.Checked == true)
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;

                        switch (cboAction.SelectedIndex.ToString())
                        {
                            case "0":
                                HistoryBinding.DataSource = twrContext.CRUDTRACKINGs.Local.ToBindingList();
                                break;
                            case "1":
                                HistoryBinding.DataSource = twrContext.QUERYTRACINGs.Local.ToBindingList();
                                break;
                        }
                    
                    
                }

            }
            catch (Exception exAdd)
            { MessageBox.Show("Error" + exAdd); }//end of catch block
        }//end of function crudBindingSource_AddingNew()
        /*****************************************************************************************************/
        //method to check if a row is empty in datagridview
        private bool IsRowEmpty(int index)
        {
            return dgvLoadTable.Rows[index].Cells.OfType<DataGridViewCell>()
                                            .All(c => c.Value == null);
            
        }

        /*******************************************************************************************/
        //make the databound comboboxes editable
        private void dgvLoadTable_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                //autocomplete mode for all comboboxes in dgvLoadTable
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                    ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }//end of function dgvLoadTable_EditingControlShowing
        /*****************************************************************************************/
        //add checkbox column and select the checked rows
        private void addChkBox()
        {
            try
            {
                //check box column in the datagrid view
                dgvLoadTable.Columns.Clear();
                DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
                //set name for the check box column
                colCB.Name = "Select";
                colCB.HeaderText = "";
                colCB.Width = 20;
                //If you use header check box then use it 
                colCB.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvLoadTable.Columns.Add(colCB);
                //Select cell where checkbox to be display
                Rectangle rect = this.dgvLoadTable.GetCellDisplayRectangle(0, -1, true);            //0 Column index -1(header row) is row index 
                //Mention size of the checkbox
                chkbox.Size = new Size(15, 15);
                //set position of header checkbox where to places
                rect.Offset(3, 3);
                chkbox.Location = rect.Location;
                chkbox.CheckedChanged += chkBoxChange;
                //Add CheckBox control to datagridView
                this.dgvLoadTable.Controls.Add(chkbox);
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }
        private void chkBoxChange(object sender, EventArgs e)
        {
            try
            {
                for (int k = 0; k <= dgvLoadTable.RowCount - 1; k++)
                {
                    this.dgvLoadTable[0, k].Value = this.chkbox.Checked;

                }

                this.dgvLoadTable.EndEdit();
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        private void dgvLoadTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try{
                if (dgvLoadTable.Columns[0].Name == "Select")
                {
                    foreach (DataGridViewRow row in dgvLoadTable.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(true))
                        {
                            row.Selected = true;
                            row.DefaultCellStyle.SelectionBackColor = Color.Gray;
                        }
                        else
                            row.Selected = false;
                    }//end of foreach
                }

            }//end of if the first column of datagridview is the checkbox column
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }


        /***********************************************************************************************************************/
        //function for showing the records to be updated
        private void crudUpdate()
        {
            try
            {
                checkCrudrules();
                if (cboSelectTable.Text.ToString() == "CRUDRULES" && crudCount == 0)
                {

                }
                else
                {
                    foreach (DataGridViewRow row in dgvLoadTable.Rows)
                    {
                        if (row.Selected == true)
                        {
                            row.Visible = true;

                        }
                        else row.Visible = false;
                    }
                    setCboColumns();
                    setNonEditableColumnColor();
                    lblGreen.Visible = false;
                    lblRed.Visible = false;
                    lblRequired.Visible = false;
                    lblEditable.Visible = false;
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }//end of crudUpdate()
        /************************************************************************************/
        //function for deletion
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                checkCrudrules();
                if (cboSelectTable.Text.ToString() == "CRUDRULES" && crudCount == 0)
                {

                }
                else
                {
                    string TestOrLive = null;
                    if (dgvLoadTable.SelectedRows.Count > 0)
                    {
                        TransactionDetails tr = new TransactionDetails();
                        if (tr.ShowDialog() == DialogResult.OK)
                        {
                            foreach (DataGridViewRow row in this.dgvLoadTable.SelectedRows)
                            {
                                if (row.Cells["Select"].Value != null && row.Cells["Select"].Value.Equals(true))
                                {
                                    crudBindingSource.RemoveAt(row.Index);

                                }
                            }//end of foreach
                            if (cboSelectUser.Text.ToString() == "RTTRANSBROKERADMIN" || cboSelectUser.Text.ToString() == "RTTRANSBROKER")
                            {
                                //RTTRANSBROKERADMIN@DEVDB01
                                if (cboSelectDB.Text.ToString() == "DEVDB01")
                                {
                                    rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerTest"].ConnectionString;
                                    TestOrLive = "Test";
                                }
                                //RTTRANSBROKER@RTIDB01
                                else if (cboSelectDB.Text.ToString() == "RTIDB01")
                                {
                                    rtContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerLive"].ConnectionString;
                                    TestOrLive = "Live";
                                }
                                    //rtContext.HookSaveChanges(FuncDelegate);
                                TransactionDetails.DetectChanges(rtContext, TestOrLive);
                                    rtContext.SaveChanges();
                              }
                            else if (cboSelectUser.Text.ToString() == "CADADMIN" || cboSelectUser.Text.ToString() == "CADUSER")
                            {
                                //CADADMIN@DEVDB01
                                if (cboSelectDB.Text.ToString() == "DEVDB01")
                                {
                                    cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerTest"].ConnectionString;
                                    TestOrLive = "Test";
                                }
                                //CADUSER@RTIDB01
                                else if (cboSelectDB.Text.ToString() == "RTIDB01")
                                {
                                    cadContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerLive"].ConnectionString;
                                    TestOrLive = "Live";
                                }
                                    //cadContext.HookSaveChanges(FuncDelegate);
                                TransactionDetails.DetectChanges(cadContext, TestOrLive);
                                cadContext.SaveChanges();
                             }

                            else if (cboSelectUser.Text.ToString() == "TOWER" || cboSelectUser.Text.ToString() == "TOWERPROD")
                            {
                                //TOWER@RTITSTA
                                if (cboSelectDB.Text.ToString() == "RTITSTA")
                                {
                                    twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                                    TestOrLive = "Test";
                                }
                                    //TOWERPROD@TWRPROD.RTI.COM
                                else if (cboSelectDB.Text.ToString() == "TWRPROD.RTI.COM")
                                {
                                    twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                                    TestOrLive = "Live";
                                }
                                //twrContext.HookSaveChanges(FuncDelegate);
                                TransactionDetails.DetectChanges(twrContext, TestOrLive);
                                twrContext.SaveChanges();
                            }
                            if (dgvLoadTable.SelectedRows.Count == dgvLoadTable.Rows.Count)
                            {
                                dgvLoadTable.DataSource = null;
                                dgvLoadTable.Controls.Clear();
                            }
                            MessageBox.Show("Records Deleted!");
                        }
                    
                        else if (tr.ShowDialog() == DialogResult.Cancel)
                        {
                            clearSelection();
                        }
                    }//end of if any row is selected

                    else
                        MessageBox.Show("Please select the column you want to delete.");
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }//end of btnDelete_Click()


        /*****************************************************************************************************************/
        //function for clearing any selection
        void clearSelection()
        {
            try
            {
                DataGridViewSelectionMode oldmode = dgvLoadTable.SelectionMode;

                dgvLoadTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dgvLoadTable.ClearSelection();

                dgvLoadTable.SelectionMode = oldmode;
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }//end of clearSelection()
        /*******************************************************************************************/
        //Function for validating datagridview input

        private void dgvLoadTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(this, e.Exception.Message, "Error");
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLoadTable.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel operation for the selected records?", "To cancel select Yes", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in this.dgvLoadTable.SelectedRows)
                        {
                            if (row.Cells["Select"].Value != null && row.Cells["Select"].Value.Equals(true))
                            // dgvLoadTable.Rows.RemoveAt(row.Index);
                            {
                                clearSelection();
                                row.Cells["Select"].Value = false;

                             }
                        }//end of foreach
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to cancel all rows? If you want to cancel particular rows, then select them. To cancel all select Yes and to select particular rows select No.", "To cancel select Yes", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        dgvLoadTable.DataSource = null;
                        dgvLoadTable.Columns.Clear();// clear any existing column
                        dgvLoadTable.Rows.Clear();
                        dgvLoadTable.Controls.Clear();
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        private void dgvLoadTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dgvLoadTable.Rows)
            {
                if (row.IsNewRow)
                    row.Selected = true;
            }

            try
            {
                if (dgvLoadTable.CurrentRow.Cells[e.ColumnIndex].ReadOnly)
                {
                    MessageBox.Show("This cell can not be edited.");
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }

        }



        private void dgvLoadTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                crudLocalBinding();
                DataGridViewColumn newColumn = dgvLoadTable.Columns[e.ColumnIndex];
                DataGridViewColumn oldColumn = dgvLoadTable.SortedColumn;
                HandleDataGridViewEvents.SortDatagridview(newColumn, oldColumn, dgvLoadTable);
             }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        private void cboSelectTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboCrudSearchColumn.SelectedIndex = -1;
                cboCrudSearchColumn.Items.Clear();
                var cboSearchItem = (from item in twrTest.CRUDRULES
                                     where item.RTITABLE == cboSelectTable.Text && item.RTISCHEMA == cboSelectUser.Text && item.RTICOLUMN != null
                                     select item.RTICOLUMN);
                foreach (var searchColumnName in cboSearchItem)
                    cboCrudSearchColumn.Items.Add(searchColumnName.ToUpper());
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        private void dgvLoadTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                crudLocalBinding();
                // Put each of the columns into programmatic sort mode.
                HandleDataGridViewEvents.dgv_DataBindingComplete(dgvLoadTable);
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        private void setNonEditableColumnColor()
        {
            try
            {
                lblGreen.Visible = true;
                lblRed.Visible = true;
                lblRequired.Visible = true;
                lblEditable.Visible = true;
                //non-editable columns
                var coloredColumnEdit = (from col in twrTest.CRUDRULES
                                         where col.CANEDIT == "N" && col.RTITABLE == cboSelectTable.Text && col.RTISCHEMA == cboSelectUser.Text
                                             && col.RTICOLUMN != null && col.ISCOMBOBOXCOLUMN=="N"
                                         select col.RTICOLUMN);
                foreach (var column in coloredColumnEdit)
                {
                    dgvLoadTable.Columns[column].DefaultCellStyle.BackColor = Color.LightCoral;
                    dgvLoadTable.Columns[column].ReadOnly = true;
                    
                }
                //required columns
                var coloredColumnRequired = (from col in twrTest.CRUDRULES
                                             where col.ISREQUIRED == "Y" && col.RTITABLE == cboSelectTable.Text && col.RTISCHEMA == cboSelectUser.Text
                                                 && col.RTICOLUMN != null && col.ISCOMBOBOXCOLUMN == "N"
                                             select col.RTICOLUMN);
                foreach (var column in coloredColumnRequired)
                {
                    dgvLoadTable.Columns[column].DefaultCellStyle.BackColor = Color.YellowGreen;
                }

            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }

        }

        //dynamic: store the query generated by EF.
        //private static void FuncDelegate(DbContext dbContext, string command)
        //{
        //    TransactionDetails.StoreGeneratedQuery(command);
        //}
        

        public void checkCrudrules()
        {
            try
            {
                if (cboSelectTable.Text.ToString() == "CRUDRULES")
                {
                    if (crudCount == 0)
                    {
                        Credential frm = new Credential();
                        if (frm.ShowDialog() != DialogResult.OK)
                        {
                            // The user canceled.
                            frm.Close();
                        }
                        else
                        {
                            crudCount++;
                            lblCrudAdmin.Visible = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            { MessageBox.Show("Error: " + ex); }
            
        }

        //View History Tab
        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            try
            {
               
                //Automatically select item when nothing is selected in cboHistoryRecNo
                if (cboHistoryRecNo.SelectedIndex <= -1)
                    cboHistoryRecNo.SelectedIndex = 0;
                if (cboAction.SelectedIndex > -1) // && cboHistoryDbType.SelectedIndex > -1)
                {
                    if (rdoBtnTest.Checked == true || rdoBtnLive.Checked == true)
                    {
                        var historyContext = new object();
                        dgvHistory.DataSource = null;
                        dgvHistory.Rows.Clear();
                        clearSelection();
                        twrContext = new TowerModelContainer();
                        //TOWER@DEVB
                        if (rdoBtnTest.Checked == true)
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                        //TOWERPROD@TWRPROD.RTI.COM
                        else if (rdoBtnLive.Checked == true)
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                        historyContext = twrContext;

                        var historyTruncatedData = new object();
                        var historyRawData = historyContext.GetType().GetProperty(cboAction.Text.ToString()).GetValue(historyContext, null);
                        if (cboHistoryRecNo.Text.ToString() == "All")
                        {
                            historyTruncatedData = ((IQueryable<object>)historyRawData).ToList();
                        }
                        else
                        {
                            int RecNo = Int32.Parse(cboHistoryRecNo.Text.ToString());
                            historyTruncatedData = ((IQueryable<object>)historyRawData).Take(RecNo).ToList();
                        }
                        HistoryBinding.DataSource = new BindingSource { DataSource = historyTruncatedData };
                        dgvHistory.DataSource = HistoryBinding;
                        dgvHistory.Refresh();
                    }
                    else
                        MessageBox.Show("Please choose Test or Live");
                }
                else
                    MessageBox.Show("Please choose a table."); // and if you want to view it in Live or Test.");
            }
            catch(Exception ex)
            { MessageBox.Show("Error: " + ex); }
        }

        private void dgvHistory_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                crudLocalBinding();
                DataGridViewColumn newColumn = dgvHistory.Columns[e.ColumnIndex];
                DataGridViewColumn oldColumn = dgvHistory.SortedColumn;
                HandleDataGridViewEvents.SortDatagridview(newColumn, oldColumn, dgvHistory);
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        private void dgvHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            crudLocalBinding();
            // Put each of the columns into programmatic sort mode. 
            HandleDataGridViewEvents.dgv_DataBindingComplete(dgvHistory);
        }

        private void btnHistorySearch_Click(object sender, EventArgs e)
        {
            //check if value is given for search
            if (cboHistorySearchCol.SelectedIndex != -1 && txtHistorySearch.Text != string.Empty)
            {
                if (rdoBtnTest.Checked == true || rdoBtnLive.Checked == true)
                {
                    try
                    {
                        dgvHistory.CurrentCell = null;
                        dgvHistory.AllowUserToAddRows = false;
                        string TableName = cboAction.Text.ToString();
                        var columnName = cboHistorySearchCol.Text.ToString();
                        string searchValue = txtHistorySearch.Text.ToString();
                        var truncatedData = new object();
                        int n, c = 0;
                        bool isNumeric = int.TryParse(searchValue, out n);
                        DateTime temp;
                        var checkColType = (from colType in twrTest.CRUDRULES
                                            where colType.RTICOLUMN == cboHistorySearchCol.Text.ToLower() &&
                                            colType.RTITABLE == TableName && colType.COLUMNTYPE != null
                                            select colType.COLUMNTYPE).FirstOrDefault();
                        //TOWER@DEVB
                        twrContext = new TowerModelContainer();
                        if (rdoBtnTest.Checked == true)
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                        //TOWERPROD@TWRPROD.RTI.COM
                        else if (rdoBtnLive.Checked == true)
                            twrContext.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;

                        var rawData = twrContext.GetType().GetProperty(TableName).GetValue(twrContext, null);

                        if (isNumeric && checkColType.Contains("NUMBER"))
                        {
                            int x = Int32.Parse(txtHistorySearch.Text);
                            truncatedData = ((IQueryable<object>)rawData).Where(columnName + "=@0", x).ToList();
                        }
                        else if (DateTime.TryParse(txtHistorySearch.Text, out temp) && checkColType.Contains("DATE"))
                        {
                            var parsedDt = DateTime.Parse(txtHistorySearch.Text);
                            var nextDay = parsedDt.AddDays(1);
                            truncatedData = ((IQueryable<object>)rawData).Where(columnName + ">= @0 && " + columnName + " < @1", parsedDt, nextDay).ToList();
                        }
                        else
                        {
                            truncatedData = ((IQueryable<object>)rawData).Where(columnName + "=@0", searchValue).ToList();
                        }
                        HistoryBinding.DataSource = new BindingSource { DataSource = truncatedData };
                        dgvHistory.DataSource = HistoryBinding;
                        dgvHistory.Refresh();

                        //dgvHistory.DataSource = truncatedData;
                        txtHistorySearch.Text = String.Empty;
                        foreach (DataGridViewRow row in dgvHistory.Rows)
                        { c++; }
                        if (c > 0)
                        {
                            lblFoundHistoryRec.Visible = true;
                            lblFoundHistoryRec.Text = c + " records found";
                        }
                        else
                        {
                            lblFoundHistoryRec.Visible = false;
                            MessageBox.Show("No records are found.");
                        }


                    }//end of try block
                    catch (Exception exc)
                    {
                        // MessageBox.Show(exc.Message);
                    }
                }
                else
                    MessageBox.Show("Please select where do you want to search: Test or Live;");


            }
            else
                MessageBox.Show("Please select column name and enter search value.");
        }

        private void cboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboHistorySearchCol.SelectedIndex = -1; //clears combobox text upon new selection
                cboHistorySearchCol.Items.Clear();
                if(cboAction.SelectedIndex==0)
                {
                    var cboHistorySearchColItem=(from a in twrTest.CRUDRULES 
                                                 where a.RTITABLE=="CRUDTRACKINGs" && a.RTICOLUMN!= null
                                             select a.RTICOLUMN.ToUpper()).Distinct().OrderBy(x => x);
                    foreach (var ColumnName in cboHistorySearchColItem)
                        cboHistorySearchCol.Items.Add(ColumnName);
                }

                else if (cboAction.SelectedIndex == 1)
                {
                    var cboHistorySearchColItem = (from a in twrTest.CRUDRULES
                                                   where a.RTITABLE == "QUERYTRACINGs" && a.RTICOLUMN != null
                                                   select a.RTICOLUMN.ToUpper()).Distinct().OrderBy(x => x);
                    foreach (var ColumnName in cboHistorySearchColItem)
                        cboHistorySearchCol.Items.Add(ColumnName);
                }
                
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        private void dgvHistory_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvHistory.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void linkLblAdvSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (cboSelectTable.SelectedIndex != -1)
                {
                    Advanced_Search childForm = new Advanced_Search(this);
                    childForm.Show();
                }
                else
                    MessageBox.Show("Please Select the table you want to search in.");
            }
            catch (Exception ex)
            { MessageBox.Show("Error: " + ex); }
        }

       }
}
