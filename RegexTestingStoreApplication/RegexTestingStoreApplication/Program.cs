using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string item = "strawb42 bana 1 10   1.93";
            //string pattern = @"(?<str>[\w\s]*)(?<qty>\s\d*\s)(?<num>\d*\.\d+)";
            string pattern = @"([\w\s]+?)(\s\d+?)?\s*(\d*\.\d{2})";
            
            String str = string.Empty;
            String qty = string.Empty;

            String nuum = string.Empty;

            foreach (Match match in Regex.Matches(item, pattern))
            {
                //str = match.Groups["str"].Value;
                //qty = match.Groups["qty"].Value;
                //nuum = match.Groups["num"].Value;

                str = match.Groups[1].Value;
                qty = match.Groups[2].Value;
                nuum = match.Groups[3].Value;
            }

            Console.WriteLine("{0},{1},{2}", str, qty, nuum);

        }
    }
}
