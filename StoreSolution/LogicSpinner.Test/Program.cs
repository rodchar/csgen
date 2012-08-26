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
        public static void TestMethod() //Initial testing template from StackOverflow
        {
            //http://stackoverflow.com/q/12093086/139698

            //decimal expected = 3;
            Dictionary<string, int> selectedDictionary = ToGroupDictionary("banana,banana,cherry,kiwi,strawberry");
            List<string> lookup = Rewards();

            Console.WriteLine();
            foreach (string test in lookup)
            {
                var testDictionary = ToGroupDictionary(test);
                testDictionary.Keys.ToList().ForEach(k =>
                    Console.WriteLine(selectedDictionary.ContainsKey(k) &&
                        selectedDictionary[k] >= testDictionary[k]));
            }
            //Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void TestMethod2()
        {
            Dictionary<string, int> toReturn = new Dictionary<string, int>();
            Dictionary<string, int> purchasedDictionary = ToGroupDictionary("banana,banana,cherry,kiwi,strawberry");
            List<string> rewards = Rewards();

            Console.WriteLine();
            foreach (string reward in rewards)
            {
                var rewardDictionary = ToGroupDictionary(reward);

                rewardDictionary.Keys.ToList().ForEach(k =>
                {
                    //http://stackoverflow.com/q/12126832/139698

                    if (purchasedDictionary.ContainsKey(k) &&
                        purchasedDictionary[k] >= rewardDictionary[k])
                    {
                        //todo: add to toReturn list code.
                    }
                });
            }
        }

        private static Dictionary<string, int> ToGroupDictionary(string value)
        {
            return value.Split(',')
                .GroupBy(s => s.Trim())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        private static Dictionary<string, int> Purchased()
        {
            //banana,banana,cherry,kiwi,strawberry
            Dictionary<string, int> purchased = new Dictionary<string, int>();
            purchased.Add("banana", 2);
            purchased.Add("cherry", 1);
            purchased.Add("kiwi", 1);
            purchased.Add("strawberry", 1);

            return purchased;
        }

        private static List<string> Rewards()
        {
            List<string> toReturn = new List<string>(){
                "banana,strawberry",
                "strawberry",
                "banana,banana,banana"
            };

            return toReturn;
        }
    }
}