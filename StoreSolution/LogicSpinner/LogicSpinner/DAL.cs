using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LogicSpinner
{
    public class DAL
    {
        private const string CONNECTION_STRING = @"
            Data Source=.\sqlexpress;Initial Catalog=bpstore;
            Integrated Security=true
            ";

        public static List<Product> Products()
        {
            string queryString = string.Format(@"
            SELECT * FROM PRODUCTS
            ");

            DataTable dt = GetDatabaseRecords(queryString);

            List<Product> p = GetGenericProductList(dt);

            return p;
        }

        public static List<Reward> Rewards()
        {
            string queryString = string.Format(@"
            SELECT * FROM REWARDS
            ");

            DataTable dt = GetDatabaseRecords(queryString);

            List<Reward> r = GetGenericRewardList(dt);

            return r;
        }

        private static DataTable GetDatabaseRecords(string queryString)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);
                reader.Close();
            }
            return dt;
        }

        private static List<Product> GetGenericProductList(DataTable dt)
        {
            List<Product> productList = new List<Product>();

            foreach (DataRow dr in dt.Rows)
            {
                Product p = new Product()
                {
                    Id = Convert.ToInt32(dr["Id"])
                    ,
                    Name = dr["Name"].ToString()
                    ,
                    Cost =  GetSafeDecimal(dr["Cost"])
                };

                productList.Add(p);
            }

            return productList;
        }

        private static List<Reward> GetGenericRewardList(DataTable dt)
        {
            List<Reward> list = new List<Reward>();

            foreach (DataRow dr in dt.Rows)
            {
                Reward r = new Reward()
                {
                    Id = Convert.ToInt32(dr["Id"])
                    ,
                    ProductsCsv = dr["ProductCsv"].ToString()

                };

                list.Add(r);
            }

            return list;
        }

        private static decimal GetSafeDecimal(object p)
        {
            return p == DBNull.Value ? 0M : Convert.ToDecimal(p);
        }
    }
}
