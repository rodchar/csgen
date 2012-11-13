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
            ucDetailView1.DataSource = GetRecord();
        }

        private List<DataTable> GetRecord()
        {
            string queryString = string.Format(
            @"

SELECT * FROM {0} WHERE CustomerID=@search;
            
-- Pull in Layout table here based on Table and Field
DECLARE @tableName NVARCHAR(50) = 'Customers'
DECLARE @stmt NVARCHAR(MAX);

SELECT * FROM SpecialTable1 WHERE TableName = @tableName;

DECLARE @SqlStatementCursor CURSOR
SET @SqlStatementCursor = CURSOR FAST_FORWARD
FOR
SELECT SqlStatements
FROM SpecialTable1 WHERE TableName = @tableName;
OPEN @SqlStatementCursor
    FETCH NEXT FROM @SqlStatementCursor
    INTO @stmt
    WHILE @@FETCH_STATUS = 0
    BEGIN

        EXEC (@stmt)

        FETCH NEXT FROM @SqlStatementCursor
        INTO @stmt
    END
CLOSE @SqlStatementCursor
DEALLOCATE @SqlStatementCursor
            
            ", TABLE_NAME);

            List<DataTable> list = GetDatabaseRecords(queryString, RECORD_ID);

            ucDetailView1.ColumnNames = new List<string>();

            foreach (DataColumn item in list[0].Columns)
            {
                ucDetailView1.ColumnNames.Add(item.ColumnName);
            }

            return list;
        }

        private static List<DataTable> GetDatabaseRecords(string queryString, string searchString)
        {
            List<DataTable> list = new List<DataTable>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(queryString, connection);

                if (!string.IsNullOrEmpty(searchString))
                    command.Parameters.AddWithValue("@search", searchString);

                try
                {
                    connection.Open();

                    //DataTable.Load automatically advances the reader to the next result. 
                    //http://stackoverflow.com/a/11362847/139698
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (!reader.IsClosed)
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            list.Add(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return list;
        }
    }
}