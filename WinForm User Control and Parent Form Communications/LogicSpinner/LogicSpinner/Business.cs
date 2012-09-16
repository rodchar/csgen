using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LogicSpinner
{
    public class Business
    {
        //http://stackoverflow.com/a/12093574/139698
        public static List<Reward> Rewards(List<Reward> rewards, List<ReceiptItem> receiptItems)
        {
            List<Reward> toReturn = new List<Reward>();
            
            string sReceiptItems = string.Empty;

            foreach (var item in receiptItems)
            {
                sReceiptItems += string.Format("{0},", item.Name);
            }

            Dictionary<string, int> receiptItemsDictionary = ToGroupDictionary(sReceiptItems);            

            foreach (Reward reward in rewards)
            {
                Boolean isQualifiedReward = false;

                var rewardDictionary = ToGroupDictionary(reward.ProductCsv);

                rewardDictionary.Keys.ToList().ForEach(k =>
                {
                    //http://stackoverflow.com/q/12126832/139698

                    if (receiptItemsDictionary.ContainsKey(k) &&
                        receiptItemsDictionary[k] >= rewardDictionary[k])
                    {
                        isQualifiedReward = true;
                    }
                    else
                    {
                        isQualifiedReward = false;
                        return;
                    }

                });

                if (isQualifiedReward)
                {
                    toReturn.Add(reward);
                }
            }

            return toReturn;
        }

        private static Dictionary<string, int> ToGroupDictionary(string value)
        {
            return value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(s => s.Trim())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public static BpDS.ProductsDataTable Products()
        {
            return ProductDAL.GetProducts();
        }

        public static BpDS.ProductsDataTable ProductsNotMatched()
        {
            return ProductDAL.ProductsNotMatchedYet();
        }

        public static BpDS.ReceiptItemNamesDataTable ReceiptItemNamesNotMatched()
        {
            return ReceiptDAL.ReceiptItemNamesNotMatchedYet();
        }
    }
}
