using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rti.DataModel;
using System.Configuration;
using System.Data.Entity;

namespace Rti.CRUDTool
{
    public partial class Advanced_Search : Form
    {
        SQLCM parentForm = null;
        TowerModelContainer towerAdvSearch = new TowerModelContainer();
        CadAdminModelContainer cadAdvSearch = new CadAdminModelContainer();
        RtTransBrokerAdminModelContainer rtAdvSearch = new RtTransBrokerAdminModelContainer();
        
        public Advanced_Search(SQLCM parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            string dbName = this.parentForm.cboSelectDB.Text.ToString();
            string userName = this.parentForm.cboSelectUser.Text.ToString();
            string tableName = this.parentForm.cboSelectTable.Text.ToString();
            Initialize(dbName, userName, tableName);
        }

        public void Initialize(string DBName, string UserName, string TableName)
        {
            //Add items to AND/OR comboboxes 
            string[] advSearchLogicalOp = new string[] { "&&", "||" };
            foreach (var logicalOp in advSearchLogicalOp)
            {
                cboAdvSearchAO2.Items.Add(logicalOp);
                cboAdvSearchAO3.Items.Add(logicalOp);
                cboAdvSearchAO4.Items.Add(logicalOp);
                cboAdvSearchAO5.Items.Add(logicalOp);
            }

            //Add items to Column comboboxes 
            var cboColumnNames = (from a in towerAdvSearch.CRUDRULES
                                  where a.DBNAME == DBName && a.RTISCHEMA == UserName && a.RTITABLE == TableName
                                      && a.RTICOLUMN != null
                                  select a.RTICOLUMN.ToUpper()).Distinct();
            foreach (var colName in cboColumnNames)
            {
                cboAdvSearchCol1.Items.Add(colName);
                cboAdvSearchCol2.Items.Add(colName);
                cboAdvSearchCol3.Items.Add(colName);
                cboAdvSearchCol4.Items.Add(colName);
                cboAdvSearchCol5.Items.Add(colName);
            }

            //Add items to operators comboboxes
            string[] advSearchOp = new string[] { "==", "!=", ">", "<", ">=", "<=" };
            foreach (var op in advSearchOp)
            {
                cboAdvSearchOp1.Items.Add(op);
                cboAdvSearchOp2.Items.Add(op);
                cboAdvSearchOp3.Items.Add(op);
                cboAdvSearchOp4.Items.Add(op);
                cboAdvSearchOp5.Items.Add(op);
            }
        }

        private void btnAdvsearchCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdvsearchSearch_Click(object sender, EventArgs e)
        {
            var newContext = new object();
            var truncatedData = new object();
            var query = new StringBuilder();
            string TableName = this.parentForm.cboSelectTable.Text.ToString();
            string UserName = this.parentForm.cboSelectUser.Text.ToString();
            string DBName = this.parentForm.cboSelectDB.Text.ToString();
            //Column Names
            string col1 = cboAdvSearchCol1.Text.ToString();
            string col2 = cboAdvSearchCol2.Text.ToString();
            string col3 = cboAdvSearchCol3.Text.ToString();
            string col4 = cboAdvSearchCol4.Text.ToString();
            string col5 = cboAdvSearchCol5.Text.ToString();

            //Operators
            string op1 = cboAdvSearchOp1.Text.ToString();
            string op2 = cboAdvSearchOp2.Text.ToString();
            string op3 = cboAdvSearchOp3.Text.ToString();
            string op4 = cboAdvSearchOp4.Text.ToString();
            string op5 = cboAdvSearchOp5.Text.ToString();

            //SearchValues
            string searchVal1 = txtAdvSearchVal1.Text; //.ToString();
            string searchVal2 = txtAdvSearchVal2.Text.ToString();
            string searchVal3 = txtAdvSearchVal3.Text.ToString();
            string searchVal4 = txtAdvSearchVal4.Text.ToString();
            string searchVal5 = txtAdvSearchVal5.Text.ToString();

            //Logical operators
            string lOp2 = cboAdvSearchAO2.Text.ToString();
            string lOp3 = cboAdvSearchAO3.Text.ToString();
            string lOp4 = cboAdvSearchAO4.Text.ToString();
            string lOp5 = cboAdvSearchAO5.Text.ToString();

            StringBuilder finalQuery = new StringBuilder();
            //if (cboAdvSearchCol1.SelectedIndex > -1 && cboAdvSearchOp1.SelectedIndex > -1 && txtAdvSearchVal1.Text != string.Empty)
            //{
            //    finalQuery = QueryBuilder(col1, op1, searchVal1, TableName, UserName);
            //}
            //if (cboAdvSearchAO2.SelectedIndex > -1 && cboAdvSearchCol2.SelectedIndex > -1 && cboAdvSearchOp2.SelectedIndex > -1 && txtAdvSearchVal2.Text != string.Empty)
            //{
            //    //finalQuery.AppendFormat(" " + lOp2 + " " + QueryBuilder(col2, op2, searchVal2, TableName, UserName));
            //    finalQuery.AppendFormat("  {0} {1}", lOp2, QueryBuilder(col2, op2, searchVal2, TableName, UserName));
            //}
            //if (cboAdvSearchAO3.SelectedIndex > -1 && cboAdvSearchCol3.SelectedIndex > -1 && cboAdvSearchOp3.SelectedIndex > -1 && txtAdvSearchVal4.Text != string.Empty)
            //{
            //    //finalQuery.AppendFormat(" " + lOp3 + " " + QueryBuilder(col3, op3, searchVal3, TableName, UserName));
            //    finalQuery.AppendFormat("  {0} {1}", lOp3, QueryBuilder(col3, op3, searchVal3, TableName, UserName));
            //}
            //if (cboAdvSearchAO4.SelectedIndex > -1 && cboAdvSearchCol4.SelectedIndex > -1 && cboAdvSearchOp4.SelectedIndex > -1 && txtAdvSearchVal4.Text != string.Empty)
            //{
            //    // finalQuery.AppendFormat(" " + lOp4 + " " + QueryBuilder(col4, op4, searchVal4, TableName, UserName));
            //    finalQuery.AppendFormat("  {0} {1}", lOp4, QueryBuilder(col4, op4, searchVal4, TableName, UserName));
            //}
            //if (cboAdvSearchAO5.SelectedIndex > -1 && cboAdvSearchCol5.SelectedIndex > -1 && cboAdvSearchOp5.SelectedIndex > -1 && txtAdvSearchVal5.Text != string.Empty)
            //{
            //    //finalQuery.AppendFormat(" " + lOp5 + " " + QueryBuilder(col5, op5, searchVal5, TableName, UserName));
            //    finalQuery.AppendFormat("  {0} {1}", lOp5, QueryBuilder(col5, op5, searchVal5, TableName, UserName));
            //}

            BindingSource sqlBindingSource = new BindingSource();
            newContext = setConnection(DBName, UserName);
            var rawData = newContext.GetType().GetProperty(TableName).GetValue(newContext, null);
            truncatedData = ((IQueryable<object>)rawData).Where(col1 + ">=  Convert.ToDateTime(" + DateTime.Parse(txtAdvSearchVal1.Text) + ").Date ").ToList();
            //this.parentForm.crudBindingSource.DataSource = new BindingSource { DataSource = truncatedData };
            //this.parentForm.dgvLoadTable.DataSource = this.parentForm.crudBindingSource;
            //this.parentForm.dgvLoadTable.Refresh();
            sqlBindingSource.DataSource = new BindingSource { DataSource = truncatedData };
            dgvAdvSearch.DataSource = sqlBindingSource;
            dgvAdvSearch.Refresh();


        }

        private StringBuilder QueryBuilder(string colName, string op, string searchVal, string tableName, string userName)
        {
            var checkColType = (from colType in towerAdvSearch.CRUDRULES
                                where colType.RTICOLUMN == colName.ToLower() &&
                                    colType.RTISCHEMA == userName && colType.RTITABLE == tableName &&
                                    colType.COLUMNTYPE != null
                                select colType.COLUMNTYPE).FirstOrDefault();

            int n;
            bool isNumeric = int.TryParse(searchVal, out n);
            DateTime temp;
            StringBuilder query = new StringBuilder();

            if (isNumeric && checkColType.Contains("NUMBER"))
            {
                int x = Int32.Parse(searchVal);
                query.AppendFormat(colName + " " + op + " " + searchVal);
                
            }

            else if (DateTime.TryParse(searchVal, out temp) && checkColType.Contains("DATE"))
            {
                //var parsedDt = DateTime.Parse(searchVal);
                //var nextDay = parsedDt.AddDays(1);
                //if (op == "==")
                query.AppendFormat(colName + " " + op +  "DateTime.Parse("+ Convert.ToDateTime(txtAdvSearchVal1.Text) + ")");
                //TestDate == DateTime.Parse(\"" + Convert.ToDateTime(txtDate.Text).Date +"\")"
                //else
                //    query.AppendFormat(colName + " "+ op + " " + parsedDt);
                //else if(op== ">")
                //    query.AppendFormat(colName + "> @0 ", parsedDt);
                //else if (op == ">=")
                //    query.AppendFormat(colName + ">= @0 ", parsedDt);
                //else if (op == "<")
                //    query.AppendFormat(colName + "< @0 ", parsedDt);
                //else if (op == "<=")
                //    query.AppendFormat(colName + "<= @0 ", parsedDt);
            }
            else
            {

                query.AppendFormat(colName + op+ "\"" + searchVal + "\"");

            }

            return query;
        }

        private object setConnection(string dbName, string userName)
        {
            var context = new object();
            if (userName == "CADADMIN" || userName == "CADUSER")
            {
                cadAdvSearch = new CadAdminModelContainer();
                //CADADMIN@DEVDB01
                if (dbName == "DEVDB01")
                    cadAdvSearch.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerTest"].ConnectionString;
                //CADUSER@RTIDB01
                else if (dbName == "RTIDB01")
                    cadAdvSearch.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CadAdminModelContainerLive"].ConnectionString;
                context = cadAdvSearch;
            }
            else if (userName == "RTTRANSBROKERADMIN" || userName == "RTTRANSBROKER")
            {
                rtAdvSearch = new RtTransBrokerAdminModelContainer();
                //RTTRANSBROKERADMIN@DEVDB01
                if (dbName == "DEVDB01")
                    rtAdvSearch.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerTest"].ConnectionString;
                //RTTRANSBROKER@RTIDB01
                else if (dbName == "RTIDB01")
                    rtAdvSearch.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["RtTransBrokerAdminModelContainerLive"].ConnectionString;
                context = rtAdvSearch;
            }
            else if (userName == "TOWER" || userName == "TOWERPROD")
            {
                towerAdvSearch = new TowerModelContainer();
                //TOWER@RTITSTA
                if (dbName == "RTITSTA")
                    towerAdvSearch.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                //TOWERPROD@TWRPROD.RTI.COM
                else if (dbName == "TWRPROD.RTI.COM")
                    towerAdvSearch.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                context = towerAdvSearch;
            }
            return context;
        }
    }
}
