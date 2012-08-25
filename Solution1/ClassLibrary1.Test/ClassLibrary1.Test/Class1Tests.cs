using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ClassLibrary1.Test
{
    [TestFixture]
    public class Class1Tests
    {
        [SetUp]
        protected void Setup()
        {
        }

        [Test]
        public void TestMethod()
        {
            //arrange
            decimal x = 1;
            decimal y = 2;

            //action
            decimal expected = 3;
            decimal actual = Class1.Add(x,y);

            //assert
            Assert.AreEqual(expected, actual);

        }

        [TearDown]
        protected void Teardown(){
        }
    }
}
