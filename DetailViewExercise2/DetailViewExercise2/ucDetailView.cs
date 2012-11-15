using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccessLayer;

namespace DetailViewExercise2
{
    public partial class ucDetailView : UserControl
    {
        public ucDetailView()
        {
            InitializeComponent();
        }

        public List<DataTable> DataSource { get; set; }
        public List<ColumnMetaData> MetaList { get; set; }
        public List<string> ColumnNames { get; set; }

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

        public void DynamicStuff(TableLayoutPanel table)
        {
            int curCol = 1;
            DataRow detailRow = DataSource[0].Rows[0];

            for (int i = 0; i < ColumnNames.Count; i++)
            {
                Label label1 = new Label();
                label1.Text = ColumnNames[i].ToString();
                table.Controls.Add(label1, curCol, i);

                var drSpecial = MetaList.Find(x => x.FieldName == ColumnNames[i]);

                if (drSpecial != null)
                {
                    ColumnMetaData m = new ColumnMetaData();

                    if (drSpecial.ControlType.Contains("DropDownList"))
                    {
                        int resultTableIndex = 0;
                        int.TryParse(drSpecial.ControlType.Split(',')[1], out resultTableIndex);

                        ComboBox cmb = new ComboBox();

                        foreach (DataRow item in DataSource[resultTableIndex].Rows)
                        {
                            cmb.Items.Add(item["Text"].ToString());
                        }

                        cmb.SelectedText = detailRow[i].ToString();

                        table.Controls.Add(cmb, drSpecial.ColumnPosition + 1, i);
                    }
                }
                else //Just use standard textbox for input.
                {
                    TextBox textbox1 = new TextBox();
                    textbox1.Text = detailRow[i].ToString();
                    table.Controls.Add(textbox1, curCol + 1, i);
                }
            }
        }
    }
}
