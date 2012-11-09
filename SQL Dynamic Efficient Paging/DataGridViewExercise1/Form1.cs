using System;
using System.Windows.Forms;
using DataAccessLayer;
using SQL_Dynamic_Efficient_Paging;

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

            LoadDataSource();
        }

        private void LoadDataSource()
        {
            DataSet1 ds = new DataSet1();
            var dt = Class1.GetData("vwReceiptItemNamesProducts", dgv1.PageSize, dgv1.PageNumber, "ProductName", dgv1.SearchEntry, "ProductName");
            ds.ReceiptItemNamesProducts.Merge(dt);
            dgv1.DataMember = ds.ReceiptItemNamesProducts.ToString();
            dgv1.DataSource = ds;
        }

        void Dgv1Click(object sender, EventArgs e)
        {
            var ctrl = sender as Control;

            if ("Next|Prev|First|Last|Search".IndexOf(ctrl.Text) > -1)
            {
                LoadDataSource();
            }

        }
    }
}
