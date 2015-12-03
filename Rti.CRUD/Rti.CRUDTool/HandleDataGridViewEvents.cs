using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Rti.DataModel;
using System.Drawing;

namespace Rti.CRUDTool
{
    class HandleDataGridViewEvents
    {
        //Function for sorting Datagridview
        public static void SortDatagridview(DataGridViewColumn NewColumn, DataGridViewColumn OldColumn, DataGridView dgv)
        {
            try
            {
                ListSortDirection direction;
                // If oldColumn is null, then the DataGridView is not sorted. 
                if (OldColumn != null)
                {
                    // Sort the same column again, reversing the SortOrder. 
                    if (OldColumn == NewColumn && dgv.SortOrder == SortOrder.Ascending)
                    {
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        // Sort a new column and remove the old SortGlyph.
                        direction = ListSortDirection.Ascending;
                        OldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }
                // Sort the selected column.
                dgv.Sort(NewColumn, direction);
                NewColumn.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex);}
        }

        public static void dgv_DataBindingComplete(DataGridView dgv)
        {
            // Put each of the columns into programmatic sort mode.
            try
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error:" + ex); }
        }

        

        
    }
}
