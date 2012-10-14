using System;
using System.Collections.Generic;

namespace SpinnerLogicPayLoad
{
    class Program 
    {
        static void Main(string[] args)
        {
            /* 
             * Qualify Reward method in class BLL is the payload 
             */
            List<Pur> receiptItems;
            List<Rew> eligibleRewards;

            Console.WriteLine();
            Console.WriteLine("------Test Records 3--------");
            TestRecords3(out receiptItems, out eligibleRewards);
            Start(receiptItems, eligibleRewards);

            Console.WriteLine();
            Console.WriteLine("------Test Records 2--------");
            TestRecords2(out receiptItems, out eligibleRewards);
            Start(receiptItems, eligibleRewards);

            Console.WriteLine();
            Console.WriteLine("------Test Records 1--------");
            TestRecords1(out receiptItems, out eligibleRewards);
            Start(receiptItems, eligibleRewards);
        }

        public static void Start(List<Pur> receiptItems, List<Rew> eligibleRewards)
        {
            PrintInput(receiptItems, eligibleRewards);

            BLL bll = new BLL(receiptItems, eligibleRewards);
            PayLoad bestRewards = bll.Run();

            PrintOutput(bestRewards);
        }

        private static void PrintOutput(PayLoad bestRewards)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rewards Won: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            bestRewards.Rewards.ForEach(x => Console.WriteLine(x.Name.ToString()));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Total Value: {0}", bestRewards.Total);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        private static void PrintInput(List<Pur> receiptItems, List<Rew> eligibleRewards)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Items Purchased: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            receiptItems.ForEach(x => Console.WriteLine("Product: {0}, Qty: {1}", x.Product, x.Quantity));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Eligible Rewards: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            eligibleRewards.ForEach(x => Console.WriteLine(x.Name));
            Console.WriteLine();
        } 

        private static void TestRecords1(out List<Pur> receiptItems, out List<Rew> eligibleRewards)
        {
            Rew rew1 = new Rew() { Id = 1, Name = "Straberry x 1 Reward_20", Value = 20, Token = true, Priority = 50, RewReq = new List<Req>() };
            Rew rew2 = new Rew() { Id = 2, Name = "Straberry x 1 Reward_10 Free Token", Value = 10, Token = false, Priority = 50, RewReq = new List<Req>() };
            Rew rew3 = new Rew() { Id = 3, Name = "Straberry 1 Banana 1  Reward_60", Value = 60, Token = true, Priority = 50, RewReq = new List<Req>() };
            Rew rew4 = new Rew() { Id = 4, Name = "Banana x 3 Reward_50", Value = 50, Token = true, Priority = 50, RewReq = new List<Req>() };
            rew1.RewReq.Add(new Req() { RewardId = 1, Category = null, Product = "Strawberry", Quantity = 1 });
            rew2.RewReq.Add(new Req() { RewardId = 2, Category = "Fruit", Product = "Strawberry", Quantity = 1 });
            rew3.RewReq.Add(new Req() { RewardId = 3, Category = null, Product = "Strawberry", Quantity = 1 });
            rew3.RewReq.Add(new Req() { RewardId = 3, Category = null, Product = "Banana", Quantity = 1 });
            rew4.RewReq.Add(new Req() { RewardId = 4, Category = null, Product = "Banana", Quantity = 3 });

            eligibleRewards = new List<Rew>();
            eligibleRewards.AddRange(new[] { rew1, rew2, rew3, rew4 });

            receiptItems = new List<Pur>();
            receiptItems.Add(new Pur() { Category = "Fruit", Product = "Strawberry", Quantity = 1 });
            receiptItems.Add(new Pur() { Category = null, Product = "Banana", Quantity = 3 });
        }

        private static void TestRecords2(out List<Pur> receiptItems, out List<Rew> eligibleRewards)
        {
            /* This test set demonstrates priority and categories */
            Rew rew1 = new Rew() { Id = 1, Name = "Straberry x 1 Reward_20", Value = 20, Token = true, Priority = 50, RewReq = new List<Req>() };
            Rew rew2 = new Rew() { Id = 2, Name = "Fruit Category x 1 Reward_10 Free Token", Value = 10, Token = false, Priority = 50, RewReq = new List<Req>() };
            Rew rew3 = new Rew() { Id = 3, Name = "Straberry 1 Banana 1  Reward_60 Priority_49", Value = 60, Token = true, Priority = 49, RewReq = new List<Req>() };
            Rew rew4 = new Rew() { Id = 4, Name = "Banana x 3 Reward_50", Value = 50, Token = true, Priority = 50, RewReq = new List<Req>() };
            rew1.RewReq.Add(new Req() { RewardId = 1, Category = null, Product = "Strawberry", Quantity = 1 });
            rew2.RewReq.Add(new Req() { RewardId = 2, Category = "Fruit", Product = null, Quantity = 1 });
            rew3.RewReq.Add(new Req() { RewardId = 3, Category = null, Product = "Strawberry", Quantity = 1 });
            rew3.RewReq.Add(new Req() { RewardId = 3, Category = null, Product = "Banana", Quantity = 1 });
            rew4.RewReq.Add(new Req() { RewardId = 4, Category = null, Product = "Banana", Quantity = 3 });

            eligibleRewards = new List<Rew>();
            eligibleRewards.AddRange(new[] { rew1, rew2, rew3, rew4 });

            receiptItems = new List<Pur>();
            receiptItems.Add(new Pur() { Category = "Fruit", Product = "Strawberry", Quantity = 1 });
            receiptItems.Add(new Pur() { Category = null, Product = "Banana", Quantity = 3 });
        }

        private static void TestRecords3(out List<Pur> receiptItems, out List<Rew> eligibleRewards)
        {
            /* This test set demonstrates priority and categories */
            Rew rew1 = new Rew() { Id = 1, Name = "Fruit x 1 Reward_20", Value = 20, Token = true, Priority = 50, RewReq = new List<Req>() };
            Rew rew2 = new Rew() { Id = 2, Name = "Strawberry x 1 Reward_10 Free Token", Value = 10, Token = false, Priority = 50, RewReq = new List<Req>() };
            Rew rew3 = new Rew() { Id = 3, Name = "Strawberry 1 Banana 1  Reward_60 Priority_49", Value = 60, Token = true, Priority = 49, RewReq = new List<Req>() };
            Rew rew4 = new Rew() { Id = 4, Name = "Banana x 3 Reward_50", Value = 50, Token = true, Priority = 50, RewReq = new List<Req>() };
            rew1.RewReq.Add(new Req() { RewardId = 1, Category = "Fruit", Product = null, Quantity = 1 });
            rew2.RewReq.Add(new Req() { RewardId = 2, Category = null, Product = "Strawberry", Quantity = 1 });
            rew3.RewReq.Add(new Req() { RewardId = 3, Category = null, Product = "Strawberry", Quantity = 1 });
            rew3.RewReq.Add(new Req() { RewardId = 3, Category = null, Product = "Banana", Quantity = 1 });
            rew4.RewReq.Add(new Req() { RewardId = 4, Category = null, Product = "Banana", Quantity = 3 });

            eligibleRewards = new List<Rew>();
            eligibleRewards.AddRange(new[] { rew1, rew2, rew3, rew4 });

            receiptItems = new List<Pur>();
            receiptItems.Add(new Pur() { Category = "Fruit", Product = "Strawberry", Quantity = 1 });
            receiptItems.Add(new Pur() { Category = null, Product = "Banana", Quantity = 3 });
        }

    }
}