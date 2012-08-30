using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicSpinner
{
    public class Business
    {
        //http://stackoverflow.com/a/12093574/139698
        public static List<Reward> Rewards(List<Reward> rewards, List<PurchaseItem> purchased)
        {
            List<Reward> toReturn = new List<Reward>();
            
            string sPurchased = string.Empty;

            foreach (var item in purchased)
            {
                sPurchased += string.Format("{0},", item.Name);
            }

            Dictionary<string, int> purchasedDictionary = ToGroupDictionary(sPurchased);            

            foreach (Reward reward in rewards)
            {
                Boolean isQualifiedReward = false;

                var rewardDictionary = ToGroupDictionary(reward.ProductsCsv);

                rewardDictionary.Keys.ToList().ForEach(k =>
                {
                    //http://stackoverflow.com/q/12126832/139698

                    if (purchasedDictionary.ContainsKey(k) &&
                        purchasedDictionary[k] >= rewardDictionary[k])
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
            return value.Split(',')
                .GroupBy(s => s.Trim())
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
