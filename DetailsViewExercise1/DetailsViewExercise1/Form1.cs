using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DetailsViewExercise1
{
    public partial class Form1 : Form
    {
        const string TABLE_NAME = "Customers";
        const string RECORD_ID = "ALFKI";
        const string DATABASE_NAME = "Northwind";
        private static string ConnectionString = string.Format(
            @"Data Source=.\sqlexpress; Initial Catalog={0}; Integrated Security=true", DATABASE_NAME);

        public Form1()
        {
            InitializeComponent();

            //detailsView_uc1.ColumnNames = new List<string>();
            //detailsView_uc1.ColumnNames.Add("Id");
            //detailsView_uc1.ColumnNames.Add("Name");

            ucDetailView1.DataRow1 = GetRecord();
        }

        private DataRow GetRecord()
        {
            //DataSet1.dtProductDataTable dt = new DataSet1.dtProductDataTable();
            //DataSet1.dtProductRow dr = dt.NewdtProductRow();
            //dr.Id = 1;
            //dr.Name = "WidgetA";
            //dt.Rows.Add(dr);
            //return dr;

            string queryString = string.Format(
            @"

             SELECT * FROM {0} WHERE CustomerID=@search;
            ",TABLE_NAME);

            DataTable dt = GetDatabaseRecords(queryString, RECORD_ID);

            ucDetailView1.ColumnNames = new List<string>();

            foreach (DataColumn item in dt.Columns)
            {
                ucDetailView1.ColumnNames.Add(item.ColumnName);
            }
            
            return dt.Rows[0];
        }

        private static DataTable GetDatabaseRecords(string queryString, string searchString)
        {
            var dt = new DataTable();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(queryString, connection);

                if (!string.IsNullOrEmpty(searchString))
                    command.Parameters.AddWithValue("@search", searchString);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dt.Load(reader);

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return dt;
        }
    }
}