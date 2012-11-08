using System;
using System.Windows.Forms;
using DataAccessLayer;

namespace DataGridViewExercise1
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
            dgv1.Click += Dgv1Click;
            dgv1.PageNumber = 1;
            dgv1.PageSize = 3;            
            dgv1.DataSource = Class1.GetData("vwReceiptItemNamesProducts", dgv1.PageSize, dgv1.PageNumber, "ProductName", dgv1.SearchEntry, "ProductName");
        }

        void Dgv1Click(object sender, EventArgs e)
        {
            var ctrl = sender as Control;

            if ("Next|Prev|First|Last|Search".IndexOf(ctrl.Text) > -1)
                dgv1.DataSource = Class1.GetData("vwReceiptItemNamesProducts", dgv1.PageSize, dgv1.PageNumber, "ProductName", dgv1.SearchEntry, "ProductName");

        }
    }
}
