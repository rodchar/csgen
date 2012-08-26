using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace LogicSpinner.Test
{
    [TestFixture]
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ExecuteAssembly(@"C:\Program Files\NUnit 2.5.2\bin\net-2.0\nunit-console.exe", new string[] { Assembly.GetExecutingAssembly().Location });
        }

        [Test]
        public static void TestMethod2()
        {
            Dictionary<string, int> toReturn = new Dictionary<string, int>();

            List<Reward> rewards = Rewards();
            List<Product> purchased = Purchased();

            string sPurchased = string.Empty;

            foreach (var item in purchased)
            {
                sPurchased += string.Format("{0},", item.Name);
            }
            
            Dictionary<string, int> purchasedDictionary = ToGroupDictionary(sPurchased);

            Console.WriteLine();
            
            foreach (Reward reward in rewards)
            {
                var rewardDictionary = ToGroupDictionary(reward.ProductsCsv);
                
                rewardDictionary.Keys.ToList().ForEach(k =>
                {
                    //http://stackoverflow.com/q/12126832/139698
                    
                    if (purchasedDictionary.ContainsKey(k) &&
                        purchasedDictionary[k] >= rewardDictionary[k])
                    {
                        //todo: add to toReturn list code.
                        Console.WriteLine("Found!");
                    }

                });

            }
        }

        private static List<Product> Purchased()
        {
            List<Product> purchased = new List<Product>();

            purchased.Add(new Product() { Id = 1, Name = "banana" });
            purchased.Add(new Product() { Id = 1, Name = "banana" });
            purchased.Add(new Product() { Id = 1, Name = "kiwi" });
            purchased.Add(new Product() { Id = 1, Name = "strawberry" });

            return purchased;
        }
       
        private static List<Reward> Rewards()
        {
            List<Reward> toReturn = new List<Reward>();
            toReturn.Add(new Reward() { Id = 1, ProductsCsv = "banana,strawberry" });
            toReturn.Add(new Reward() { Id = 2, ProductsCsv = "strawberry" });
            toReturn.Add(new Reward() { Id = 2, ProductsCsv = "banana,banana,banana" });

            return toReturn;
        }

        private static Dictionary<string, int> ToGroupDictionary(string value)
        {
            return value.Split(new[]{','}, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(s => s.Trim())
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}