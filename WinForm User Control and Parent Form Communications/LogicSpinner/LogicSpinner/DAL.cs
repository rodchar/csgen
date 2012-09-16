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

        public static string GetScalarValue(string queryString, KeyValuePair<string, string>[] args = null)
        {
            string toReturn = string.Empty;

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                if (args != null)
                {
                    command.Parameters.AddWithValue(args[0].Key, args[0].Value);
                }

                connection.Open();
                toReturn = command.ExecuteScalar().ToString();
            }
            return toReturn;
        }

        public static decimal GetSafeDecimal(object p)
        {
            //http://forums.asp.net/t/1383849.aspx/1
            return p == DBNull.Value ? 0M : Convert.ToDecimal(p);
        }
    }

    public class ProductDAL: DAL
    {
        static BpDSTableAdapters.ProductsTableAdapter daProducts =
            new BpDSTableAdapters.ProductsTableAdapter();

        public static void UpdateProduct(Product p)
        {
            if (p.Id == 0)
            {
                daProducts.Insert(p.Name, p.Cost);    
            }
            else
            {
                daProducts.Update(p.Name, p.Cost, p.Id);
            }
        }        

        public static void FillProducts(BpDS.ProductsDataTable dt)
        {
            daProducts.Fill(dt);
        }

        public static BpDS.ProductsDataTable GetProducts()
        {
            return daProducts.GetData();
        }

        public static BpDS.ProductsDataTable ProductsNotMatchedYet()
        {
            return daProducts.GetProductsNotMatchedYet();
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

        public static List<Reward> Rewards(List<ReceiptItem> products)
        {
            string sProducts = string.Empty;

            foreach (var item in products)
            {
                sProducts += string.Format("ProductCsv like '%{0}%' or ", item.Name);
            }

            sProducts = sProducts.TrimEnd(' ', 'o', 'r');

            string queryString = string.Format(@"
             Select * from Rewards where {0}
            ", sProducts);

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
                    ProductCsv = dr["ProductCsv"].ToString()

                };

                list.Add(r);
            }

            return list;
        }
    }

    public class ReceiptDAL : DAL
    {
        static BpDSTableAdapters.ReceiptsTableAdapter daReceipts =
            new BpDSTableAdapters.ReceiptsTableAdapter();
        static BpDSTableAdapters.ReceiptItemsTableAdapter daReceiptItems =
            new BpDSTableAdapters.ReceiptItemsTableAdapter();
        static BpDSTableAdapters.ReceiptItemNamesTableAdapter daReceiptItemNames =
            new BpDSTableAdapters.ReceiptItemNamesTableAdapter();

        static BpDS.ReceiptsDataTable dtReceipts;
        //static BpDS.ReceiptItemsDataTable dtReceiptItems;
        //static BpDS.ReceiptItemNamesDataTable dtReceiptItemNames;

        public static List<Receipt> ReceiptItems()
        {
            List<Receipt> list = new List<Receipt>();
            //Todo: 

            return list;
        }

        public static Receipt Receipt(int id)
        {
            Receipt p = new Receipt();
            //Todo: 

            return p;
        }

        private static List<Receipt> GetGenericReceiptList()
        {
            List<Receipt> receiptItems = new List<Receipt>();
            //Todo: 

            return receiptItems;
        }

        public static List<ReceiptItem> ReceiptItems(int receiptId)
        {
            List<ReceiptItem> list = new List<ReceiptItem>();

            string queryString = string.Format(@"
            SELECT P.Id, P.Name FROM ReceiptItems I
            JOIN PRODUCTS P ON I.ProductId = P.Id
            WHERE I.ReceiptId = @ReceiptId
            ");
            KeyValuePair<string, string> parm1 = 
                new KeyValuePair<string, string>("@ReceiptId", receiptId.ToString());
            KeyValuePair<string, string>[] sqlParms = { parm1 };
            DataTable dt = GetDatabaseRecords(queryString, sqlParms);

            list = GetGenericReceiptItemList(dt);

            return list;
        }

        

        public static void UpdateReceiptItemName(ReceiptItemName r)
        {
            if (r.Id == 0)
            {
                daReceiptItemNames.Insert(r.Name);
            }
            else
            {
                daReceiptItemNames.Update(r.Name,r.Id);
            }

        }

        //Insert new receipt 
        public static int UpdateReceipt(Receipt receipt)
        {       
            int newReceiptId = 0;
            //New Receipt
            if (receipt.Id == 0)
            {
                newReceiptId = daReceipts.Insert(receipt.Date, null);
            }

            return newReceiptId;
        }

        //Save receipt item 
        public static string UpdateReceiptItem(int receiptId, ReceiptItem item)
        {
            string productName = string.Empty;
            if (receiptId > 0)
            {
                productName = daReceiptItems.InsertQuery(receiptId, item.Name).ToString();
            }
            return productName;
        }
        
        public static Receipt GetReceipt(int id)
        {
            dtReceipts = daReceipts.GetDataBy(id);
            Receipt receipt = new Receipt();
            receipt.Id = dtReceipts.FirstOrDefault().Id;
            receipt.Date = dtReceipts.FirstOrDefault().Date;
            return receipt;
        }

        public static BpDS.ReceiptItemNamesDataTable ReceiptItemNamesNotMatchedYet()
        {
            return daReceiptItemNames.GetReceiptItemNamesNotMatchedYet();
        }
        
        private static List<ReceiptItem> GetGenericReceiptItemList(DataTable dt)
        {
            List<ReceiptItem> list = new List<ReceiptItem>();

            foreach (DataRow dr in dt.Rows)
            {
                ReceiptItem item = new ReceiptItem();
                item.Id = Convert.ToInt32(dr["Id"]);
                item.Name = dr["Name"].ToString();

                list.Add(item);
            }

            return list;
        }



    }
}