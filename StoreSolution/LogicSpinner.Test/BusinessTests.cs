﻿using System;
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
            List<Product> purchased = Purchased();

            //Action
            rewards = Business.Rewards(rewards, purchased);

            //Assert
            Assert.AreEqual(rewards.Count, 2);

            //Output
            Console.WriteLine();
            Console.WriteLine("SpinnerLogic_Result_Has_Count_Of_2");
            rewards.ForEach(r => Console.WriteLine(r.ProductsCsv));
        }

        private static List<Product> Purchased()
        {
            List<Product> purchased = new List<Product>();

            purchased.Add(new Product() { Id = 1, Name = "banana" });
            purchased.Add(new Product() { Id = 2, Name = "banana" });
            purchased.Add(new Product() { Id = 3, Name = "kiwi" });
            purchased.Add(new Product() { Id = 4, Name = "strawberry" });

            return purchased;
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