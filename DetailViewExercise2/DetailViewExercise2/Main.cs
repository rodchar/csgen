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
            ucDetailView1.DataPayload = d;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Comment out DynamicStuff method in user control
            //to see Main design view, if needed.
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ucDetailView1.SaveData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
