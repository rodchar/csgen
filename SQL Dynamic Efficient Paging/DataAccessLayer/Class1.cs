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
        private const string ConnectionString =
            @"Data Source=.\sqlexpress; Initial Catalog=bpstore; Integrated Security=true";

        public static DataTable GetData(string tableName, int pageSize, int pageNumber, string sortString)
        {
            var dt = GetData(tableName, pageSize, pageNumber, string.Empty, string.Empty, sortString);
            return dt;
        }

        //Main SQL Paging, Sorting, Searching 
        public static DataTable GetData(string tableName, int pageSize, int pageNumber, string searchField, string searchString, string sortString)
        {

            var searchLine = (!string.IsNullOrEmpty(searchField) && !string.IsNullOrEmpty(searchString))
                                 ? string.Format("WHERE {0} like '%'+@search+'%' OR @search is null", searchField)
                                 : string.Empty;

            string queryString = string.Format(
                @"

DECLARE	@page_size INT = {1};
DECLARE @page_nbr INT = {2};
IF object_id('tempdb..#Payload') IS NOT NULL DROP TABLE #Payload;
IF (@page_nbr = 0) SELECT @page_nbr = 1;

SELECT 0 as 'TR', 0 as 'TP',  
-- Todo: Figure out sort default
-- http://stackoverflow.com/a/13595691/139698
ROW_NUMBER() OVER ( ORDER BY {3} ) AS 'Seq'	
, *
INTO #Payload
FROM {0}
{4}

DECLARE @total_recs INT;
DECLARE @total_pages INT;
DECLARE @total_pages2 DECIMAL(18,2);
SELECT @total_recs = COUNT(*) FROM #Payload;
SELECT @total_pages = @total_recs / @page_size;
SELECT @total_pages2 = @total_recs / (@page_size * 1.00)

IF (@total_pages < 1) select @total_pages = 1;
IF (@total_pages < @total_pages2) select @total_pages = @total_pages + 1;

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