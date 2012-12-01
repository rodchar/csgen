using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewBindingSource
{
    public partial class Form1 : Form
    {
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
        }

        private static DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Price");
            dt.Columns.Add("Quantity");

            DataRow dr1 = dt.NewRow();
            dr1["Id"] = 1;
            dr1["Name"] = "Product 1";
            dr1["Price"] = 1000;
            dr1["Quantity"] = 1;
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["Id"] = 2;
            dr2["Name"] = "Product 2";
            dr2["Price"] = 2000;
            dr2["Quantity"] = 1;
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["Id"] = 3;
            dr3["Name"] = "Product 3";
            dr3["Price"] = 3000;
            dr3["Quantity"] = 2;
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["Id"] = 4;
            dr4["Name"] = "Product 4";
            dr4["Price"] = 4000;
            dr4["Quantity"] = 1;
            dt.Rows.Add(dr4);

            return dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt = GetData();

            BindingSource bs = new BindingSource();

            bs.DataSource = dt;

            dataGridView1.DataSource = bs;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
