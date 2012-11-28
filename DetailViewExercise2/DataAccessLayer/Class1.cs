using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Class1
    {        
        private static string ConnectionString = string.Format(
        @"Data Source=.\sqlexpress; Initial Catalog={0}; Integrated Security=true", "Northwind");        

        public static DataPayload GetRecord(string tableName, string fieldKey, string recordKey)
        {
            string queryString = string.Format(
            @"

--Detail Row
SELECT * FROM {0} WHERE {1}=@search;
            
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
            
            ", tableName, fieldKey);

            List<DataTable> list = GetDatabaseRecords(queryString, recordKey);

            List<ColumnMetaData> meta = new List<ColumnMetaData>();

            DataTable dt1 = list[1];
            //dt1.PrimaryKey = new DataColumn[] { dt1.Columns["FieldName"] };

            List<string> columnNames = new List<string>();

            //How do I get column names to print in this C# program?
            //http://stackoverflow.com/a/2557943/139698
            foreach (DataColumn item in list[0].Columns)
            {
                columnNames.Add(item.ColumnName);
            }

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

            
            DataPayload dataPayload = new DataPayload();
            dataPayload.DataSources = list;
            dataPayload.MetaList = meta;
            dataPayload.ColumnNames = columnNames;

            return dataPayload;
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
