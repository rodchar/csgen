using System;
using System.Collections.Generic;
using System.Linq;

namespace SpinnerLogicPayLoad
{
    public class BLL 
    {
        Rew rew;
        Col1 c1;
        List<Pur> pList;
        List<Pur> pListOriginal;
        List<Req> rList;
        int[] wList;
        List<Rew> wListOrdered;
        List<Col1> payLoads;

        public void Start()
        {            
            payLoads = new List<Col1>();

            //Outer loop starts here
            foreach (var outerItem in wList)
            {
                var wListReOrdered = ForwardBackward(wListOrdered, outerItem);
                c1 = new Col1();

                foreach (var innerItem in wListReOrdered)
                {
                    rew = innerItem;
                    QualifyReward(outerItem);
                }

                if (c1.Rewards.Count() > 0)
                    payLoads.Add(c1);

                //Reset purchased list here
                pList = pListOriginal.Clone() as List<Pur>;

            } //Outer loop ends here

            var payLoad = FindMaxValue(payLoads, x => x.Total);
        }

        private void QualifyReward(int initialRewardId)
        {
            var rListFilter = rList.Where(x => x.RewardId == rew.Id).ToList();

            var xList = from r in rListFilter
                        join p in pList
                            on r.Product equals p.Product
                        where r.Quantity <= p.Quantity
                        select new { r, p };

            if ((xList.Any() && rListFilter.Count == xList.Count()) || (rew.Token == false)) //or if token not required?
            {
                c1.Id = initialRewardId;
                c1.Rewards.Add(rew.Id);
                c1.Total += rew.Value;

                if (rew.Token)
                {
                    foreach (var item in rListFilter)
                    {
                        pList.Where(x => x.Product == item.Product).FirstOrDefault().Quantity -= item.Quantity;
                    }
                }
            }
        }

        public BLL(List<Pur> a, List<Req> b)
        {
            Rew rew1 = new Rew() { Id = 1, Value = 20, Token = true };
            Rew rew2 = new Rew() { Id = 2, Value = 10, Token = false };
            Rew rew3 = new Rew() { Id = 3, Value = 60, Token = true };
            Rew rew4 = new Rew() { Id = 4, Value = 50, Token = true };

            //For inner loop
            wListOrdered = new List<Rew>();
            wListOrdered.AddRange(new[] { rew1, rew2, rew3, rew4 });

            //For outer loop
            wList = new[] { rew1.Id, rew2.Id, rew3.Id, rew4.Id };

            pList = new List<Pur>();
            pList.Add(new Pur() { Product = "Strawberry", Quantity = 1 });
            pList.Add(new Pur() { Product = "Banana", Quantity = 3 });

            pListOriginal = new List<Pur>();
            pListOriginal.Add(new Pur() { Product = "Strawberry", Quantity = 1 });
            pListOriginal.Add(new Pur() { Product = "Banana", Quantity = 3 });

            rList = new List<Req>();
            rList.Add(new Req() { RewardId = 1, Product = "Strawberry", Quantity = 1 });
            rList.Add(new Req() { RewardId = 2, Product = "Strawberry", Quantity = 1 });
            rList.Add(new Req() { RewardId = 3, Product = "Strawberry", Quantity = 1 });
            rList.Add(new Req() { RewardId = 3, Product = "Banana", Quantity = 1 });
            rList.Add(new Req() { RewardId = 4, Product = "Banana", Quantity = 3 });
        }

        private static List<Rew> ForwardBackward(List<Rew> a, int start)
        {
            start -= 1;
            return a.Skip(start).Concat(a.Take(start)).ToList();
        }

        private Col1 FindMaxValue<T>(List<T> list, Converter<T, int> projection)
        {
            Col1 toReturn = null;

            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int maxValue = int.MinValue;
            foreach (T item in list)
            {
                int value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                    toReturn = item as Col1;
                }
            }
            return toReturn;
        }
    }    
}