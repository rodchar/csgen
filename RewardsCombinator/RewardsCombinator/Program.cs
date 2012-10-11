using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RewardsCombinator
{
    class Program
    {
        static List<Purchased> purchasedList;
        static List<Requirements> requirementsList;

        static void Main(string[] args)
        {
            //At this point we have the eligible rewards 
            //for a purchase. Now, just have to figure out
            //the best combination to give the highest value

            //Plan is to iterate the rewards in an outer loop
            //As soon as it finds a qualified reward it will start
            //an inner loop to discover the remaining rewards

            //Thinking about a QualifiedRewards class that has an id, 
            //the qualified rewards collection, 
            //and a total sum property

            PopulateTestData();


            IEnumerable<int> rewardIds = requirementsList.Select(x => x.RewardId).Distinct();

            foreach (int rewardId in rewardIds)
            {
                //Console.WriteLine(rewardId.ToString());

                //Do work here.
                
            }

            Console.WriteLine();

            //Example Linq
            //http://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b

            var test = from t in requirementsList
                       where t.RewardId == 3
                       select t;
            Console.WriteLine(test.ToList()[0].Product);
        }

        private static void PopulateTestData()
        {
            //Set up data
            purchasedList = new List<Purchased>();
            Purchased purchased1 = new Purchased() { Product = "Strawberry", Quantity = 1 };
            Purchased purchased2 = new Purchased() { Product = "Banana", Quantity = 3 };
            purchasedList.Add(purchased1);
            purchasedList.Add(purchased2);

            requirementsList = new List<Requirements>();
            Requirements requirement1 = new Requirements() { Product = "Strawberry", Quantity = 1, Value = 20, RewardId = 1 };
            Requirements requirement2 = new Requirements() { Product = "Strawberry", Quantity = 1, Value = 10, RewardId = 2 };
            Requirements requirement3 = new Requirements() { Product = "Strawberry", Quantity = 1, Value = 60, RewardId = 3 };
            Requirements requirement4 = new Requirements() { Product = "Banana", Quantity = 1, Value = 60, RewardId = 3 };
            Requirements requirement5 = new Requirements() { Product = "Banana", Quantity = 3, Value = 50, RewardId = 4 };
            requirementsList.AddRange(new[] { requirement1, requirement2, requirement3, requirement4, requirement5 });

            foreach (var item in purchasedList)
            {
                Console.WriteLine(string.Format("{0}, {1}", item.Product, item.Quantity));
            }
            Console.WriteLine();
            foreach (var item in requirementsList)
            {
                Console.WriteLine(string.Format("{0} {1}, {2}", item.RewardId, item.Product, item.Quantity));
            }
        }
    }

    public class Purchased
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Requirements
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }
        public int Priority { get; set; }
        public int RewardId { get; set; }
    }

    public class Combinations
    {
        public int Id { get; set; }
        public List<int> RewardIds { get; set; }
        public int TotalSum { get; set; }
    }
}
