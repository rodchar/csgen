using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace LogicSpinner.Test
{    
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ExecuteAssembly(@"C:\Program Files\NUnit 2.5.2\bin\net-2.0\nunit-console.exe", new string[] { Assembly.GetExecutingAssembly().Location });
        }
    }
}