using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccessLayer;

namespace DetailViewExercise2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            DataPayload d = new DataPayload();
            d = Class1.GetRecord("Customers", "CustomerID", "ALFKI", "Northwind");
            ucDetailView1.ColumnNames = d.ColumnNames;
            ucDetailView1.MetaList = d.MetaList;
            ucDetailView1.DataSource = d.DataSource;
        }
    }
}
