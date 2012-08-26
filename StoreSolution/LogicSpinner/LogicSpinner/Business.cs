using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicSpinner
{
    public class Business
    {


        public static List<string> Rewards(Dictionary<string, int> rewards, List<string> purchased)
        {
            List<string> toReturn = new List<string>();

            foreach (string test in purchased)
            {

                var testDictionary = ToGroupDictionary(test);
                testDictionary.Keys.ToList().ForEach(k =>
                    Console.WriteLine(rewards.ContainsKey(k) &&
                        rewards[k] >= testDictionary[k]));

                // [0] := 
                // ["banana", 1] 
                // ["strawberry", 1] 
                // true, banana and strawberry exist 

                // [1] := 
                // ["strawberry", 1] 
                // true, strawberry exists 

                // [2] := 
                // ["banana", 3] 
                // false, too many bananas 

            }

            return toReturn;
        }


        private static Dictionary<string, int> ToGroupDictionary(string value)
        {
            return value.Split(',')
                .GroupBy(s => s.Trim())
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
