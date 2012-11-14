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

--Detail Row
SELECT * FROM {0} WHERE CustomerID=@search;
            
--Supporting Data for detail page.
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

            //How do I get column names to print in this C# program?
            //http://stackoverflow.com/a/2557943/139698
            foreach (DataColumn item in list[0].Columns)
            {
                ucDetailView1.ColumnNames.Add(item.ColumnName);
            }

            List<ColumnMetaData> meta = new List<ColumnMetaData>();

            DataTable dt1 = list[1];
            dt1.PrimaryKey = new DataColumn[] { dt1.Columns["FieldName"] };

            foreach (DataRow dr in dt1.Rows)
            {
                ColumnMetaData c = new ColumnMetaData();
                c.FieldName = dr["FieldName"].ToString();
                int colPos = 0;
                int.TryParse(dr["ColumnPosition"].ToString(), out colPos);
                c.ColumnPosition = colPos;
                int rowPos = 0;
                int.TryParse(dr["RowPosition"].ToString(), out rowPos);
                c.RowPosition = rowPos;
                c.ControlType = dr["ControlType"].ToString();
                meta.Add(c);
            }

            return list;
        }

        private static List<DataTable> GetDatabaseRecords(string queryString, string searchString)
        {
            List<DataTable> list = new List<DataTable>();

            //Possible result sets that are returned from database.
            //1. Detail row information
            //2. Meta table about special fields
            //3. DropDownList result sets

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