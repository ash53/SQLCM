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
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Runtime.Serialization;
using EFTracingProvider;
using System.Configuration;



namespace Rti.CRUDTool
{

    public partial class test : Form
    {
        RtTransBrokerAdminModelContainer rtContext = new RtTransBrokerAdminModelContainer();
        TowerModelContainer twr = new TowerModelContainer();
        BindingSource bi = new BindingSource();

        public test()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            //bi.DataSource = rtContext.FILTERHL7.ToList();
            bi.DataSource = twr.CRUDTRACKINGs.ToList();
            dataGridView1.DataSource = bi;
            dataGridView1.Refresh();

            var cboSearchItem = (from item in twr.CRUDRULES
                                 where item.RTITABLE == "CRUDTRACKINGs" && item.RTICOLUMN != null
                                 select item.RTICOLUMN).Distinct();
            foreach (var searchColumnName in cboSearchItem)
                cboAdvSearchCol1.Items.Add(searchColumnName.ToUpper());

           
            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            bi.DataSource = rtContext.FILTERHL7.Take(0).ToList();
            dataGridView1.DataSource = bi;
            bi.DataSource = rtContext.FILTERHL7.Local.ToBindingList();
            
            
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            rtContext.SaveChanges();
           
         }
        
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                bi.RemoveAt(row.Index);
                
            }
            rtContext.SaveChanges();
        }

        ///Advance search
        private void btnAdvSearch_Click(object sender, EventArgs e)
        {
            var truncatedData = new object();
            string TableName = "CRUDTRACKINGs";
            string ColumnName = cboAdvSearchCol1.Text.ToString();
            string SearchValue = txtAdvSearchVal1.Text.ToString();
            var date = DateTime.Parse(SearchValue);
            var nextDate = date.AddDays(1);
                //DateTime.Parse(SearchValue).AddDays(1);
            var rawData = twr.GetType().GetProperty(TableName).GetValue(twr, null);
            //string query = ColumnName + "=\"" + SearchValue + "\""; //string-- w
            //string query = ColumnName + "=" + SearchValue; //numeric--w
            //string query = ColumnName + ">= DateTime.Parse(" + txtAdvSearchVal1.Text +") && " + ColumnName + "< DateTime.Parse("
            //+ txtAdvSearchVal1.Text +").AddDays(1)"; 
            var query = ColumnName + @" >= DateTime.Parse(""" + date + @""") && " + ColumnName + @" < DateTime.Parse(""" + nextDate + @""")";
            //var d = DateTime.Parse(txtAdvSearchVal1.Text.ToString());
           
            truncatedData = ((IQueryable<object>)rawData).Where(query).ToList();
            
            bi.DataSource = new BindingSource { DataSource = truncatedData };
            dataGridView1.DataSource = bi;
            dataGridView1.Refresh();


        }

        private void linkLblAdvSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Advanced_Search advSearchForm = new Advanced_Search(this);
            //advSearchForm.Show();
           
        }

       
    }

   }
