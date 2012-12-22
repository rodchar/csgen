using System;
using System.Windows.Forms;
using DataAccessLayer;
using SQL_Dynamic_Efficient_Paging;

namespace DataGridViewExercise1
{
    public partial class Form1 : Form
    {
        //Use a view for best results
        const string TABLE_NAME = "vwProductsAndCategories";
        const string SEARCH_FIELD = "Name";
        const string DEFAULT_SORT = "Name";
        const int PAGE_SIZE = 5;

        public Form1()
        {
            InitializeComponent();
            dgv1.UserControlClicked += new EventHandler(Dgv1Click);
                        
            dgv1.PageSize = PAGE_SIZE;
            dgv1.SortBy = DEFAULT_SORT;

            LoadDataSource();
        }

        private void LoadDataSource()
        {
            //SortBy is required.
            if (string.IsNullOrEmpty(dgv1.SortBy)) dgv1.SortBy = DEFAULT_SORT;

            DataSet1 ds = new DataSet1();
            var dt = Class1.GetData(TABLE_NAME, dgv1.PageSize, dgv1.PageNumber, SEARCH_FIELD, dgv1.SearchEntry, dgv1.SortBy);
            dgv1.DataSource = dt;
        }

        void Dgv1Click(object sender, EventArgs e)
        {
            var ctrl = sender as Control;

            if (ctrl.Text != string.Empty && "Next|Prev|First|Last|Search".IndexOf(ctrl.Text) > -1
                || e.GetType().Name == "DataGridViewCellMouseEventArgs")
            {
                LoadDataSource();
            }

            if (e.GetType().Name == "DataGridViewCellEventArgs")
            {
                //This event means a row was selected by double-click
                const int RECORD_ID_OFFSET = 3;
                MessageBox.Show(dgv1.DataRowViewSelected[RECORD_ID_OFFSET].ToString());
            }
        }
    }
}