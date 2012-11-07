using System;
using System.Data.SqlClient;
using System.Data;

namespace SQL_Dynamic_Efficient_Paging
{
    internal class Program
    {
        private const string ConnectionString =
            @"Data Source=.\sqlexpress; Initial Catalog=bpstore; Integrated Security=true";

        private static void Main()
        {
            var dt = GetData("vwReceiptItemNamesProducts", 3, 2, "ProductName", "", "ProductName");

            var dtReceiptItemNamesProducts = new DataSet1.ReceiptItemNamesProductsDataTable();

            dtReceiptItemNamesProducts.Merge(dt);

            foreach (DataSet1.ReceiptItemNamesProductsRow row in dtReceiptItemNamesProducts.Rows)
            {
                Console.WriteLine("{0},{1}", row.ProductName, row.ReceiptItemName);
            }
        }

        private static DataTable GetData(string tableName, int pageSize, int pageNumber, string sortString)
        {
            var dt = GetData(tableName, pageSize, pageNumber, string.Empty, string.Empty, sortString);
            return dt;
        }

        //Main SQL Paging, Sorting, Searching 
        private static DataTable GetData(string tableName, int pageSize, int pageNumber, string searchField, string searchString, string sortString)
        {

            var searchLine = (!string.IsNullOrEmpty(searchField) && !string.IsNullOrEmpty(searchString))
                                 ? string.Format("WHERE {0} like '%'+@search+'%' OR @search is null", searchField)
                                 : string.Empty;

            string queryString = string.Format(
                @"

DECLARE	@page_size INT = {1};
DECLARE @page_nbr INT = {2};
IF object_id('tempdb..#Payload') IS NOT NULL DROP TABLE #Payload;

SELECT 0 as 'TR', 0 as 'TP',  
ROW_NUMBER() OVER ( ORDER BY {3} ) AS 'Seq'	
, *
INTO #Payload
FROM {0}
{4}

DECLARE @total_recs INT;
DECLARE @total_pages INT;
SELECT @total_recs = COUNT(*) FROM #Payload;
SELECT @total_pages = @total_recs / @page_size;

UPDATE #Payload
SET TR = @total_recs,
TP = @total_pages;

SELECT *
FROM #Payload
WHERE seq > (@page_nbr - 1) * @page_size
  AND seq <= @page_nbr * @page_size;

                "
                , tableName, pageSize, pageNumber, sortString, searchLine);

            return GetDatabaseRecords(queryString, searchString);
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