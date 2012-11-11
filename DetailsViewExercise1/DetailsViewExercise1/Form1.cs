using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetailsViewExercise1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            detailsView_uc1.ColumnNames = new List<string>();
            detailsView_uc1.ColumnNames.Add("Id");
            detailsView_uc1.ColumnNames.Add("Name");
            detailsView_uc1.DataSource = GetRecord();
        }

        private static DataSet1.dtProductRow GetRecord()
        {
            DataSet1.dtProductDataTable dt = new DataSet1.dtProductDataTable();
            DataSet1.dtProductRow dr = dt.NewdtProductRow();
            dr.Id = 1;
            dr.Name = "WidgetA";
            dt.Rows.Add(dr);
            return dr;
        }
    }
}