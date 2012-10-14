using System;
using System.Collections.Generic;

namespace SpinnerLogicPayLoad
{
    public class ReceiptItem : ICloneable
    {
        public string Product { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Reward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public Boolean Token { get; set; }
        public int Priority { get; set; }
        public List<RewardRequirement> RewardRequirements { get; set; }
    }

    public class RewardRequirement
    {
        public int RewardId { get; set; }
        public string Product { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }

    public class PayLoad
    {
        public int Id { get; set; }
        public List<Reward> Rewards { get; set; }
        public int Total { get; set; }
        public int Priority { get; set; }

        public PayLoad()
        {
            Rewards = new List<Reward>();
        }
    }
}
