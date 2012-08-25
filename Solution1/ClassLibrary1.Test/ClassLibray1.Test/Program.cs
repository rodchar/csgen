using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ClassLibrary1;
using System.Reflection;

namespace ClassLibray1.Test
{
    [TestFixture]
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ExecuteAssembly(@"C:\Program Files\NUnit 2.5.2\bin\net-2.0\nunit-console.exe", new string[] { Assembly.GetExecutingAssembly().Location });
        }

        [Test]
        public static void TestMethod()
        {
            decimal expected = 3;
            decimal actual = Class1.Add(1, 2);
            Assert.AreEqual(expected, actual);
        }
    }
}
