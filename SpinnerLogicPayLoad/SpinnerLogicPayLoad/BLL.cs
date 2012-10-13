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
        
        int[] wList;
        List<Rew> wListOrdered;
        List<Col1> payLoads;

        public Col1 Run()
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

            //Return the payload!
            return FindMaxValue(payLoads, x => x.Total, y => y.Priority);
        }

        private void QualifyReward(int initialRewardId)
        {
            var rListFilter = rew.RewReq;

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
                c1.Priority = rew.Priority;

                if (rew.Token)
                {
                    foreach (var item in rListFilter)
                    {
                        pList.Where(x => x.Product == item.Product).FirstOrDefault().Quantity -= item.Quantity;
                    }
                }
            }
        }

        public BLL(List<Pur> receiptItems, List<Rew> eligibleRewards)
        {
            //For inner loop
            wListOrdered = eligibleRewards;

            //For outer loop
            wList = new[] { wListOrdered[0].Id, wListOrdered[1].Id, wListOrdered[2].Id, wListOrdered[3].Id };
            
            //Working list of receipt items
            pList = receiptItems;
            
            //Preserves the original receipt items
            pListOriginal = pList.Clone() as List<Pur>;
        }

        private static List<Rew> ForwardBackward(List<Rew> a, int start)
        {
            start -= 1;
            return a.Skip(start).Concat(a.Take(start)).ToList();
        }

        private Col1 FindMaxValue<T>(List<T> list, Converter<T, int> projection, Converter<T, int> projection2)
        {
            Col1 toReturn = null;
            Col1 toReturn2 = null;

            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;
            foreach (T item in list)
            {
                int value = projection(item);
                int value2 = projection2(item);
                if (value > maxValue)
                {
                    maxValue = value;
                    toReturn = item as Col1;
                }
                if (value2 < minValue)
                {
                    minValue = value2;
                    toReturn2 = item as Col1;
                }
            }

            if (toReturn != toReturn2)
                return toReturn2;
            else
                return toReturn;
        }
    }    
}