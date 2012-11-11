using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailsViewExercise1
{
    public partial class DetailsView_uc : UserControl
    {
        public DataRow DataSource { get; set; }

        public List<string> ColumnNames { get; set; }

        public CustomInstructions SpecialInstuctions { get; set; }

        public DetailsView_uc()
        {
            InitializeComponent();
        }

        private void DetailsView_uc_Load(object sender, EventArgs e)
        {
            //TableLayoutPanel Sample
            //http://code.msdn.microsoft.com/windowsdesktop/TableLayoutPanel-Sample-f1504098
            TableLayoutPanel table = new TableLayoutPanel();
            table.AutoSize = true;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            DynamicStuff(table);

            this.Controls.Add(table);
        }

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
            Label label1 = new Label();
            //How do I get column names to print in this C# program?
            //http://stackoverflow.com/a/2557943/139698
            label1.Text = ColumnNames[0];
            TextBox tbId = new TextBox();
            tbId.Text = DataSource[0].ToString();

            //case check:
            //case dropdownlist:
            //case date calendar:
            //End - Can loop this to get most labels and entry.

            Label label2 = new Label();
            label2.Text = ColumnNames[1];
            TextBox tbName = new TextBox();
            tbName.Text = DataSource[1].ToString();
            tbName.Width = 200;

            table.Controls.Add(label1, 1, 1);
            table.Controls.Add(label2, 1, 2);
            table.Controls.Add(tbId, 2, 1);
            table.Controls.Add(tbName, 2, 2);
        }
    }

    public class CustomInstructions
    {
        //Not being used. Still considering.
        public string FieldName { get; set; }
        public string Type { get; set; }
    }
}
