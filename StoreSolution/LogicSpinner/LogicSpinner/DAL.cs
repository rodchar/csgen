using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LogicSpinner
{
    public abstract class DAL
    {
        private const string CONNECTION_STRING = @"
            Data Source=.\sqlexpress;Initial Catalog=bpstore;
            Integrated Security=true
            ";



        public static DataTable GetDatabaseRecords(string queryString, KeyValuePair<string,string>[] args = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                
                if (args != null)
                {
                    command.Parameters.AddWithValue(args[0].Key, args[0].Value);
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);
                reader.Close();
            }
            return dt;
        }

        public static decimal GetSafeDecimal(object p)
        {
            //http://forums.asp.net/t/1383849.aspx/1
            return p == DBNull.Value ? 0M : Convert.ToDecimal(p);
        }
    }

    public class ProductDAL: DAL
    {
        public static List<Product> Products()
        {
            string queryString = string.Format(@"
            SELECT * FROM PRODUCTS
            ");

            DataTable dt = GetDatabaseRecords(queryString);

            List<Product> p = GetGenericProductList(dt);

            return p;
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
                    Cost = GetSafeDecimal(dr["Cost"])
                };

                productList.Add(p);
            }

            return productList;
        }
    }

    public class RewardDAL : DAL
    {
        public static List<Reward> Rewards()
        {
            string queryString = string.Format(@"
            SELECT * FROM REWARDS
            ");

            DataTable dt = GetDatabaseRecords(queryString);

            List<Reward> r = GetGenericRewardList(dt);

            return r;
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
    }

    public class PurchaseDAL : DAL
    {
        public static List<Purchase> Purchases()
        {
            List<Purchase> list = new List<Purchase>();
            //Todo: 

            return list;
        }

        public static Purchase Purchase(int id)
        {
            Purchase p = new Purchase();
            //Todo: 

            return p;
        }

        private static List<Purchase> GetGenericPurchaseList()
        {
            List<Purchase> purchases = new List<Purchase>();
            //Todo: 

            return purchases;
        }

        public static List<PurchaseItem> PurchaseItems(int purchaseId)
        {
            List<PurchaseItem> list = new List<PurchaseItem>();

            string queryString = string.Format(@"
            SELECT P.Id, P.Name FROM PURCHASEITEMS I
            JOIN PRODUCTS P ON I.ProductId = P.Id
            WHERE I.PURCHASEID = @PurchaseId
            ");
            KeyValuePair<string, string> parm1 = 
                new KeyValuePair<string, string>("@PurchaseId", purchaseId.ToString());
            KeyValuePair<string, string>[] sqlParms = { parm1 };
            DataTable dt = GetDatabaseRecords(queryString, sqlParms);

            list = GetGenericPurchaseItemList(dt);

            return list;
        }

        private static List<PurchaseItem> GetGenericPurchaseItemList(DataTable dt)
        {
            List<PurchaseItem> list = new List<PurchaseItem>();

            foreach (DataRow dr in dt.Rows)
            {
                PurchaseItem item = new PurchaseItem();
                item.Id = Convert.ToInt32(dr["Id"]);
                item.Name = dr["Name"].ToString();

                list.Add(item);
            }

            return list;
        }
    }
}
