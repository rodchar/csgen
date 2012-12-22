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

        /// <summary>
        /// This is the only required object
        /// needed to initialize it.
        /// </summary>
        public DataPayload DataPayload
        {
            set
            {
                DataPayload d = value;
                _dataSources = d.DataSources;                
                //DataTable 1 = Detail row.
                //DataTable 2 = Metadata row.
                //DataTable 3 = Supporting data for ComboBoxes
                //  DataFormats:
                //  ControlType = DropDownList,[ResultSetIndex]
                //  Resultset Index will start at 2 and higher.
                _columnNames = d.ColumnNames;
                //All column names of detail row.
                _metaList = d.MetaList;
                //SpecialTable1 - information about special control
                //types other than textbox.
            }
        }

        private List<DataTable> _dataSources;
        private List<ColumnMetaData> _metaList;
        private List<string> _columnNames;
        DataRow _detailRow;
        TableLayoutPanel _table;

        private void ucDetailView_Load(object sender, EventArgs e)
        {
            //TableLayoutPanel Sample
            //http://code.msdn.microsoft.com/windowsdesktop/TableLayoutPanel-Sample-f1504098
            _table = new TableLayoutPanel();
            _table.AutoSize = true;
            _table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            DynamicStuff();

            this.Controls.Add(_table);
        }

        public void DynamicStuff()
        {
            int curCol = 1;
            _detailRow = _dataSources[0].Rows[0];
            _detailRow.BeginEdit();

            for (int i = 0; i < _columnNames.Count; i++)
            {
                string columnName = _columnNames[i];

                Label label1 = new Label();
                label1.Text = columnName.ToString();
                _table.Controls.Add(label1, curCol, i);

                var drSpecial = _metaList.Find(x => x.FieldName == columnName);

                if (drSpecial != null)
                {
                    ColumnMetaData m = new ColumnMetaData();

                    if (drSpecial.ControlType.Contains("DropDownList"))
                    {
                        int resultTableIndex = 0;
                        int.TryParse(drSpecial.ControlType.Split(',')[1], out resultTableIndex);

                        ComboBox cmb = new ComboBox();
                        cmb.DataBindings.Add("Text", _dataSources[0], columnName);

                        foreach (DataRow item in _dataSources[resultTableIndex].Rows)
                        {
                            cmb.Items.Add(item["Text"].ToString());
                        }

                        cmb.SelectedText = _detailRow[i].ToString();

                        _table.Controls.Add(cmb, drSpecial.ColumnPosition + 1, i);
                    }
                }
                else //Just use standard textbox for input.
                {
                    TextBox textbox1 = new TextBox();

                    //Use BeginEdit and EndEdit on data table to change row state
                    //
                    //http://stackoverflow.com/q/14000592/139698
                    //http://www.pcreview.co.uk/forums/databinding-datarow-t1244411.html
                    textbox1.DataBindings.Add("Text", _dataSources[0], columnName );
                    textbox1.Text = _detailRow[i].ToString();
                    _table.Controls.Add(textbox1, curCol + 1, i);
                }
            }
        }

        public DataTable SaveData()
        {
            _detailRow.EndEdit();
            //Will return a data table so 
            //can merge with typed data table

            //_dataSources[0].AcceptChanges();

            return _dataSources[0];
        }
    }
}