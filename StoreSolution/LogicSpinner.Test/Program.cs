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
            List<Reward> rewards = Rewards();
            List<Product> purchased = Purchased();

            rewards = Business.Rewards(rewards, purchased);

            Console.WriteLine();
            rewards.ForEach(r=>Console.WriteLine(r.ProductsCsv));
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
    }
}