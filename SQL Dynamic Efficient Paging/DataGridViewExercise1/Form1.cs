using System;
using System.Windows.Forms;
using DataAccessLayer;
using SQL_Dynamic_Efficient_Paging;

namespace DataGridViewExercise1
{
    public partial class Form1 : Form
    {
        const string SEARCH_FIELD = "ProductName";
        const string DEFAULT_SORT = "ProductName";
        const int PAGE_SIZE = 3;

        public Form1()
        {
            InitializeComponent();
            dgv1.Click += Dgv1Click;
            
            dgv1.PageNumber = 1;
            dgv1.PageSize = PAGE_SIZE;
            dgv1.SortBy = DEFAULT_SORT;

            LoadDataSource();
        }

        private void LoadDataSource()
        {
            //SortBy is required.
            if (string.IsNullOrEmpty(dgv1.SortBy)) dgv1.SortBy = DEFAULT_SORT;

            DataSet1 ds = new DataSet1();
            var dt = Class1.GetData("vwReceiptItemNamesProducts", dgv1.PageSize, dgv1.PageNumber, SEARCH_FIELD, dgv1.SearchEntry, dgv1.SortBy);
            dgv1.DataSource = dt;
        }

        void Dgv1Click(object sender, EventArgs e)
        {
            var ctrl = sender as Control;

            if ("Next|Prev|First|Last|Search".IndexOf(ctrl.Text) > -1
                || e.GetType().Name == "DataGridViewCellMouseEventArgs")
            {                
                LoadDataSource();
            }

        }
    }
}
