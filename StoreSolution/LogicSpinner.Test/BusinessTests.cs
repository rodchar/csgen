using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LogicSpinner.Test
{
    [TestFixture]
    public class BusinessTests
    {
        [SetUp]
        protected void Setup() { }

        [TearDown]
        protected void TearDown() { }

        [Test]
        public static void SpinnerLogic_Result_Has_Count_Of_2()
        {
            //Arrange
            List<Reward> rewards = Rewards();
            List<PurchaseItem> purchased = Purchased();

            //Action
            rewards = Business.Rewards(rewards, purchased);

            //Assert
            Assert.AreEqual(rewards.Count, 2);

            //Output
            Console.WriteLine();
            Console.WriteLine("SpinnerLogic_Result_Has_Count_Of_2");
            rewards.ForEach(r => Console.WriteLine(r.ProductsCsv));
        }

        [Test]
        public void Slow_SpinnerLogic_with_database()
        {
            //Get Purchase Id
            int purchaseId = 1;
            //Get Purchase Items by Purchase Id
            List<PurchaseItem> purchases = PurchaseDAL.PurchaseItems(purchaseId);

            //Get Rewards that have product name in
            // table Rewards column ProductCsv. 

            //Run these results through SpinnerLogic
            // using Business.Rewards(purchased,rewards)
        }

        private static List<PurchaseItem> Purchased()
        {
            List<PurchaseItem> purchases = new List<PurchaseItem>();

            purchases.Add(new PurchaseItem() { Id = 1, Name = "banana" });
            purchases.Add(new PurchaseItem() { Id = 2, Name = "banana" });
            purchases.Add(new PurchaseItem() { Id = 3, Name = "kiwi" });
            purchases.Add(new PurchaseItem() { Id = 4, Name = "strawberry" });

            return purchases;
        }

        private static List<Reward> Rewards()
        {
            List<Reward> toReturn = new List<Reward>();
            toReturn.Add(new Reward() { Id = 1, ProductsCsv = "banana,strawberry" });
            toReturn.Add(new Reward() { Id = 2, ProductsCsv = "strawberry" });
            toReturn.Add(new Reward() { Id = 3, ProductsCsv = "banana,banana,banana" });
            toReturn.Add(new Reward() { Id = 4, ProductsCsv = "banana,banana,orange" });

            return toReturn;
        }
    }
}
