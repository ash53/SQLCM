using Rti.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rti.CRUDTool
{
    public partial class TransactionDetails : Form
    {
        static string ticketNo, description;
        public TransactionDetails()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTicket.Text))
            {
                ticketNo = txtTicket.Text.ToString();
                description = richTxtDescription.Text.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Please enter ticket number.");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public static void DetectChanges(dynamic context, string dbConn)
        {
            ((IObjectContextAdapter)context).ObjectContext.DetectChanges();
            ObjectStateManager objectStateManager = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager;

            IEnumerable<ObjectStateEntry> addedEntries = objectStateManager.GetObjectStateEntries(EntityState.Added);
            foreach (ObjectStateEntry entry in addedEntries)
            {
                LogAddedEntries(entry, ticketNo, description, dbConn);
            }

            IEnumerable<ObjectStateEntry> modifiedEntries = objectStateManager.GetObjectStateEntries(EntityState.Modified);
            foreach (ObjectStateEntry entry in modifiedEntries)
            {
                LogModifiedEntries(entry, ticketNo, description, dbConn);
            }

            IEnumerable<ObjectStateEntry> deletedEntries = objectStateManager.GetObjectStateEntries(EntityState.Deleted);
            foreach (ObjectStateEntry entry in deletedEntries)
            {
                LogDeletedEntries(entry, ticketNo, description, dbConn);
            }
        }

        private static void LogAddedEntries(ObjectStateEntry entry, string Ticket, string Desc, string dbType)
        {
            TowerModelContainer twr = new TowerModelContainer();
            if (dbType == "Test")
                twr.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
                                 //TOWERPROD@TWRPROD.RTI.COM
            else if (dbType == "Live")
                twr.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            var query = new StringBuilder();
            var fieldValue = new StringBuilder();
            var fieldName = new StringBuilder();
            query.AppendFormat("INSERT INTO {0} (", entry.EntitySet.Name);
            var currentValues = entry.CurrentValues;
            for (var i = 0; i < currentValues.FieldCount; i++)
            {
                string fName = currentValues.DataRecordInfo.FieldMetadata[i].FieldType.Name;
                var fCurrVal = currentValues[i];
                if (!string.IsNullOrEmpty(fCurrVal.ToString()))
                {
                    sb.AppendFormat("{0}={1} || ", fName, fCurrVal);
                    var primaryCol = (from col in twr.CRUDRULES
                                      where col.RTICOLUMN.ToUpper() == fName && col.RTICOLUMN.ToUpper().Contains("ID")
                                      && col.RTITABLE == entry.EntitySet.Name select col.ISPRIMARYKEY).FirstOrDefault();
                    if (primaryCol == "Y") { }
                    else
                    {
                        fieldName.AppendFormat("{0},", fName);
                        fieldValue.AppendFormat("'{0}',", fCurrVal);
                    }
                }
            }
            query.AppendFormat("{0}) VALUES ( {1} );", fieldName.Remove(fieldName.Length - 1, 1), fieldValue.Remove(fieldValue.Length - 1, 1));
            query.Append(Environment.NewLine);
            query.AppendLine("COMMIT;");
            sb.Remove(sb.Length - 3, 3);
            CRUDTRACKING track = new CRUDTRACKING
            {
                TABLENAME = entry.EntitySet.Name,
                TICKETNUMBER = Ticket,
                TRANSACTIONTYPE = "Insert",
                DESCRIPTION = Desc,
                USERNAME = CommonFunctions.GetUserName(),
                RUNTIMESTAMP = DateTime.Now,
                NEWVALUE = Convert.ToString(sb)
            };
            QUERYTRACING generatedQuery = new QUERYTRACING()
            {
                TICKETNUMBER = Ticket,
                TRANSACTIONTYPE = "Insert",
                TRANSACTIONQUERY = Convert.ToString(query),
                RUNTIMESTAMP = DateTime.Now,
                USERNAME = CommonFunctions.GetUserName()

            };
            twr.CRUDTRACKINGs.Add(track);
            twr.QUERYTRACINGs.Add(generatedQuery);
            twr.SaveChanges();
        }

        private static void LogModifiedEntries(ObjectStateEntry entry, string Ticket, string Desc, string dbType)
        {
            StringBuilder old = new StringBuilder();
            StringBuilder current = new StringBuilder();
            TowerModelContainer twr = new TowerModelContainer();
            if (dbType == "Test")
                twr.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
            //TOWERPROD@TWRPROD.RTI.COM
            else if (dbType == "Live")
                twr.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                
            var query = new StringBuilder();
            var oldValue = new StringBuilder();
            query.AppendFormat("UPDATE {0} SET ", entry.EntitySet.Name);
            var properties = entry.GetModifiedProperties();
            var originalValues = entry.OriginalValues;
            for (var i = 0; i < originalValues.FieldCount; i++)
            {
                var fCurrVal = entry.OriginalValues.GetValue(i);
                var fName = originalValues.GetName(i);
                var primaryCol = (from col in twr.CRUDRULES where col.RTICOLUMN.ToUpper() == fName && col.RTITABLE == entry.EntitySet.Name
                                  select col.ISPRIMARYKEY).FirstOrDefault();
                if(primaryCol=="Y")
                oldValue.AppendFormat("{0}='{1}' AND ", fName, fCurrVal);
            }
            for (int i = 0; i < properties.Count(); i++)
            {
                string propertyName = properties.ToArray()[i];
                string OriginalValue = entry.OriginalValues.GetValue(entry.OriginalValues.GetOrdinal(propertyName)).ToString();
                string CurrentValue = entry.CurrentValues.GetValue(entry.CurrentValues.GetOrdinal(propertyName)).ToString();

                old.AppendFormat("{0}={1} || ", propertyName, OriginalValue);
                current.AppendFormat("{0}={1} || ", propertyName, CurrentValue);
                query.AppendFormat("{0}='{1}',", propertyName, CurrentValue);

            }
            query.Remove(query.Length - 1, 1);
            oldValue.Remove(oldValue.Length - 4, 4);
            old.Remove(old.Length - 3, 3);
            current.Remove(current.Length - 3, 3);
            query.AppendFormat(" WHERE {0};", oldValue);
            query.Append(Environment.NewLine);
            query.AppendLine("COMMIT;");

            CRUDTRACKING track = new CRUDTRACKING
            {
                TABLENAME = entry.EntitySet.Name,
                TICKETNUMBER = Ticket,
                TRANSACTIONTYPE = "Update",
                DESCRIPTION = Desc,
                USERNAME = CommonFunctions.GetUserName(),
                RUNTIMESTAMP = DateTime.Now,
                NEWVALUE = Convert.ToString(current),
                OLDVALUE = Convert.ToString(old)
            };

            QUERYTRACING generatedQuery = new QUERYTRACING()
            {
                TICKETNUMBER = Ticket,
                TRANSACTIONTYPE = "Update",
                TRANSACTIONQUERY = Convert.ToString(query),
                RUNTIMESTAMP = DateTime.Now,
                USERNAME = CommonFunctions.GetUserName()

            };
            twr.CRUDTRACKINGs.Add(track);
            twr.QUERYTRACINGs.Add(generatedQuery);
            twr.SaveChanges();
        }

        private static void LogDeletedEntries(ObjectStateEntry entry, string Ticket, string Desc, string dbType)
        {
            StringBuilder sb = new StringBuilder();
            TowerModelContainer twr = new TowerModelContainer();
            if (dbType == "Test")
                twr.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerTest"].ConnectionString;
            //TOWERPROD@TWRPROD.RTI.COM
            else if (dbType == "Live")
                twr.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["TowerModelContainerLive"].ConnectionString;
                
            var query = new StringBuilder();
            var oldValue = new StringBuilder();
            query.AppendFormat("DELETE FROM {0} WHERE ", entry.EntitySet.Name);
            var originalValues = entry.OriginalValues;
            for (var i = 0; i < originalValues.FieldCount; i++)
            {
                var fCurrVal = entry.OriginalValues.GetValue(i);
                var fName = originalValues.GetName(i);
                sb.AppendFormat("{0} = {1} ||", fName, fCurrVal);
                var primaryCol = (from col in twr.CRUDRULES
                                  where col.RTICOLUMN.ToUpper() == fName && col.RTITABLE == entry.EntitySet.Name
                                  select col.ISPRIMARYKEY).FirstOrDefault();
                if (primaryCol == "Y")
                    oldValue.AppendFormat("{0}='{1}' AND ", fName, fCurrVal);
             }
            sb.Remove(sb.Length - 2, 2);
            oldValue.Remove(oldValue.Length - 4, 4);
            query.AppendFormat("{0};", oldValue);
            query.Append(Environment.NewLine);
            query.AppendLine("COMMIT;");

            CRUDTRACKING track = new CRUDTRACKING
            {
                TABLENAME = entry.EntitySet.Name,
                TICKETNUMBER = Ticket,
                TRANSACTIONTYPE = "Delete",
                DESCRIPTION = Desc,
                USERNAME = CommonFunctions.GetUserName(),
                RUNTIMESTAMP = DateTime.Now,
                OLDVALUE = Convert.ToString(sb)
            };

            QUERYTRACING generatedQuery = new QUERYTRACING()
            {
                TICKETNUMBER = Ticket,
                TRANSACTIONTYPE = "Delete",
                TRANSACTIONQUERY = Convert.ToString(query),
                RUNTIMESTAMP = DateTime.Now,
                USERNAME = CommonFunctions.GetUserName()

            };
            twr.CRUDTRACKINGs.Add(track);
            twr.QUERYTRACINGs.Add(generatedQuery);
            twr.SaveChanges();
            
        }

        //public static void StoreGeneratedQuery(string generatedQuery)
        //{
        //    TowerModelContainer twr= new TowerModelContainer();
        //    string transactionType=null;
        //    if(generatedQuery.Contains("insert"))
        //        transactionType = "Insert";
        //    else if (generatedQuery.Contains("update"))
        //        transactionType = "Update";
        //    else if (generatedQuery.Contains("delete"))
        //        transactionType = "Delete";
        //    QUERYTRACING store = new QUERYTRACING 
        //    {
        //        TICKETNUMBER=ticketNo,
        //        TRANSACTIONTYPE = transactionType,
        //        TRANSACTIONQUERY=generatedQuery,
        //        USERNAME = CommonFunctions.GetUserName(),
        //        RUNTIMESTAMP = DateTime.Now
        //    };
        //    twr.QUERYTRACINGs.Add(store);
        //    twr.SaveChanges();
        //}
    }
}
