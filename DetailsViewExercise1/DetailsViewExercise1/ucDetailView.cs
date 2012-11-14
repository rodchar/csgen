using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DetailsViewExercise1
{
    public partial class ucDetailView : UserControl
    {
        public ucDetailView()
        {
            InitializeComponent();
        }

        //First row of first table is detail row.
        public List<DataTable> DataSource { get; set; }

        public List<string> ColumnNames { get; set; }

        private void DynamicStuff(TableLayoutPanel table)
        {
            //I'm thinking about adding a database table
            //linked to records that will handle special
            //cases like dates that need calendars, validation,
            //dropdownlists, checkboxes, radio buttons, etc.
            //Maybe for dropdownlists have recordsets prepared ahead
            //to return as part of payload.

            //
            //Need to practice addding title to top of screen while 
            //retaining docked table.

            //Start - Can loop this to get most labels and entry.

            //Switch on type
            //case most:

            int curCol = 1;                 
            DataRow detailRow = DataSource[0].Rows[0];

            for (int i = 0; i < ColumnNames.Count; i++)
            {
                Label label1 = new Label();
                label1.Text = ColumnNames[i];
                table.Controls.Add(label1, curCol, i);

                DataRow drSpecial = DataSource[1].Rows.Find(ColumnNames[i]);
                
                if (drSpecial != null)
                {
                    ColumnMetaData m = new ColumnMetaData();
                    
                    string controlType = drSpecial["ControlType"].ToString();

                    if (controlType.Contains("DropDownList"))
                    {
                        int resultTableIndex = 0;
                        //This gets table index from Control Type comma delimited 
                        //if a ComboBox. Start index at 2 because 1=Detail row and
                        //2=Meta data table.
                        int.TryParse(controlType.Split(',')[1], out resultTableIndex);

                        ComboBox cmb = new ComboBox();
                        
                        foreach (DataRow item in DataSource[resultTableIndex].Rows)
                        {
                            cmb.Items.Add(item["Text"].ToString());
                        }

                        cmb.SelectedText = detailRow[i].ToString();

                        table.Controls.Add(cmb, ColumnMetaData + 1, i);
                    }
                }
                else //Just use standard textbox for input.
                {
                    TextBox textbox1 = new TextBox();
                    textbox1.Text = detailRow[i].ToString();
                    table.Controls.Add(textbox1, curCol + 1, i);
                }

            }

            //case check:
            //case dropdownlist:
            //case date calendar:
            //End - Can loop this to get most labels and entry.

            //Label label2 = new Label();
            //label2.Text = ColumnNames[1];
            //TextBox tbName = new TextBox();
            //tbName.Text = DataRow1[1].ToString();
            //tbName.Width = 200;

            //table.Controls.Add(label1, 1, 1);
            //table.Controls.Add(label2, 1, 2);
            //table.Controls.Add(tbId, 2, 1);
            //table.Controls.Add(tbName, 2, 2);
        }

        private void ucDetailView_Load(object sender, EventArgs e)
        {
            //TableLayoutPanel Sample
            //http://code.msdn.microsoft.com/windowsdesktop/TableLayoutPanel-Sample-f1504098
            TableLayoutPanel table = new TableLayoutPanel();
            table.AutoSize = true;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            DynamicStuff(table);

            this.Controls.Add(table);
        }
    }

    public class ColumnMetaData
    {
        public string FieldName { get; set; }
        public string HeaderName { get; set; }
        public int ColumnPosition { get; set; }
        public int RowPosition { get; set; }
        public int ResultSetIndex { get; set; }
        public string ControlType { get; set; }
    }
}