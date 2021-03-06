﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SpinnerLogicPayLoad
{
    public class BLL
    {
        List<ReceiptItem> _receiptItems;
        List<Reward> _rewardListOrdered;

        public PayLoad Run()
        {
            var receiptItemsOriginal = _receiptItems.Clone() as List<ReceiptItem>;

            var payLoads = new List<PayLoad>();

            //Todo: review this
            var rewardIdList = new[] { _rewardListOrdered[0].Id, _rewardListOrdered[1].Id, _rewardListOrdered[2].Id, _rewardListOrdered[3].Id };

            foreach (var rewardId in rewardIdList)
            {
                var rewardListReOrdered = ForwardBackward(_rewardListOrdered, rewardId);
                var payLoad = new PayLoad();

                foreach (var reward in rewardListReOrdered)
                {                    
                    QualifyReward(rewardId, payLoad, reward);
                }

                if (payLoad.Rewards.Count() > 0)
                    payLoads.Add(payLoad);

                _receiptItems = receiptItemsOriginal.Clone() as List<ReceiptItem>;
            } 

            //Return the payload!
            return FindMaxValue(payLoads, x => x.Total, y => y.Priority);
        }

        private void QualifyReward(int initialRewardId, PayLoad payLoad, Reward reward)
        {
            //This is the payload.

            //This method contains the business logic
            //required to calculate the best rewards
            //from eligible rewards. 

            var rewardRequirements = reward.RewardRequirements;

            var xCategoryList =
                from r in rewardRequirements
                from p in _receiptItems
                    .Where(y => r.Category == y.Category && y.Quantity >= r.Quantity && (r.Category != null || y.Category != null))
                select new { r, p };

            var xProductList =
                from r in rewardRequirements
                from p in _receiptItems
                    .Where(x => r.Product == x.Product && x.Quantity >= r.Quantity)
                select new { r, p };

            if (xCategoryList.Count() + xProductList.Count() == rewardRequirements.Count() || (reward.Token == false)) //or if token not required?
            {
                payLoad.Id = initialRewardId;
                payLoad.Rewards.Add(reward);
                payLoad.Total += reward.Value;
                payLoad.Priority = reward.Priority;

                if (reward.Token)
                {
                    foreach (var item in rewardRequirements)
                    {
                        if (item.Product != null)
                            _receiptItems.Where(x => x.Product == item.Product).FirstOrDefault().Quantity -= item.Quantity;
                        if (item.Category != null)
                        _receiptItems.Where(x => x.Category == item.Category).FirstOrDefault().Quantity -= item.Quantity;
                    }
                }
            }
        }

        #region Constructors

        public BLL(List<ReceiptItem> receiptItems, List<Reward> eligibleRewards)
        {
            _receiptItems = receiptItems;
            _rewardListOrdered = eligibleRewards;            
        }

        #endregion Constructors

        #region General Helpers

        private static List<Reward> ForwardBackward(List<Reward> a, int start)
        {
            start -= 1;
            return a.Skip(start).Concat(a.Take(start)).ToList();
        }

        private PayLoad FindMaxValue<T>(List<T> list, Converter<T, int> projection, Converter<T, int> projection2)
        {
            PayLoad toReturn = null;
            PayLoad toReturn2 = null;

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
                    toReturn = item as PayLoad;
                }
                if (value2 < minValue)
                {
                    minValue = value2;
                    toReturn2 = item as PayLoad;
                }
            }

            if (toReturn != toReturn2)
                return toReturn2;
            else
                return toReturn;
        }

        #endregion General Helpers
    }
}