using System;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer;

namespace SQL_Dynamic_Efficient_Paging
{
    internal class Program
    {
        private const string ConnectionString =
            @"Data Source=.\sqlexpress; Initial Catalog=bpstore; Integrated Security=true";

        private static void Main()
        {
            var dt = Class1.GetData("vwReceiptItemNamesProducts", 3, 2, "ProductName", "", "ProductName");

            var dtReceiptItemNamesProducts = new DataSet1.ReceiptItemNamesProductsDataTable();

            dtReceiptItemNamesProducts.Merge(dt);

            foreach (DataSet1.ReceiptItemNamesProductsRow row in dtReceiptItemNamesProducts.Rows)
            {
                Console.WriteLine("{0},{1}", row.ProductName, row.ReceiptItemName);
            }
        }
    }
}