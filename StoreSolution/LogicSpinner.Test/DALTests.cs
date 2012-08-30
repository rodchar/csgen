using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LogicSpinner.Test
{
    [TestFixture]
    public class DALTests
    {

        [SetUp]
        protected void SetUp()
        {

        }

        [TearDown]
        protected void TearDown()
        {

        }

        [Test]
        public void GetProducts()
        {
            List<Product> p = DAL.Products();

            Assert.Greater(p.Count, 0);
        }


        [Test]
        public void GetRewards()
        {
            List<Reward> r = DAL.Rewards();

            Assert.Greater(r.Count, 0);
        }
    }
}
