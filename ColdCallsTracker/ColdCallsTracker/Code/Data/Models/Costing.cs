﻿namespace ColdCallsTracker.Code.Data.Models
{
    public class Costing : EntityBase
    {
        public string Name { get; set; }

        public int Unit { get; set; }
        public double Qty { get; set; }
        public double? Cost { get; set; }
        public double Multiplier { get; set; }
        public double? Total { get; set; }
        public string Remark { get; set; }

        public int CategoryId { get; set; }

        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}