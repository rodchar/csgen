using System;
using System.Collections.Generic;

namespace SpinnerLogicPayLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rew> eligibleRewards;
            List<Pur> receiptItems;
            GetRecords(out eligibleRewards, out receiptItems);

            BLL bll = new BLL(receiptItems, eligibleRewards);
            bll.Run();
        }

        private static void GetRecords(out List<Rew> eligibleRewards, out List<Pur> receiptItems)
        {
            Rew rew1 = new Rew() { Id = 1, Value = 20, Token = true, Priority = 50, RewReq = new List<Req>() };
            rew1.RewReq.Add(new Req() { RewardId = 1, Product = "Strawberry", Quantity = 1 });

            Rew rew2 = new Rew() { Id = 2, Value = 10, Token = false, Priority = 50, RewReq = new List<Req>() };
            rew2.RewReq.Add(new Req() { RewardId = 2, Product = "Strawberry", Quantity = 1 });

            Rew rew3 = new Rew() { Id = 3, Value = 60, Token = true, Priority = 49, RewReq = new List<Req>() };
            rew3.RewReq.Add(new Req() { RewardId = 3, Product = "Strawberry", Quantity = 1 });
            rew3.RewReq.Add(new Req() { RewardId = 3, Product = "Banana", Quantity = 1 });

            Rew rew4 = new Rew() { Id = 4, Value = 50, Token = true, Priority = 50, RewReq = new List<Req>() };
            rew4.RewReq.Add(new Req() { RewardId = 4, Product = "Banana", Quantity = 3 });

            eligibleRewards = new List<Rew>();
            eligibleRewards.AddRange(new[] { rew1, rew2, rew3, rew4 });

            receiptItems = new List<Pur>();
            receiptItems.Add(new Pur() { Product = "Strawberry", Quantity = 1 });
            receiptItems.Add(new Pur() { Product = "Banana", Quantity = 3 });
        }
    }
}
