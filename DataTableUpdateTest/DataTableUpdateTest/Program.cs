using System.Data;
using System.Data.SqlClient;

namespace DataTableUpdateTest
{
    class Program
    {
        internal const string CONNECTION_STRING = @"
            Data Source=.\sqlexpress;Initial Catalog=northwind;
            Integrated Security=true
            ";

        static void Main(string[] args)
        {
            DataTable dt = GetData();

            SqlDataAdapter da = new SqlDataAdapter("select * from customers where CustomerID = 'ALFKI'", CONNECTION_STRING);
            
            //http://support.microsoft.com/kb/310376
            var cb = new SqlCommandBuilder(da);

            da.Fill(dt);
            DataRow dr = dt.Rows[0];
            dr["City"] = "Augusta";

            da.Update(dt);
        }

        private static DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerID");
            dt.Columns.Add("City");

            return dt;
        }
    }
}