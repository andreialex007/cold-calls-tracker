namespace ColdCallsTracker.Code.Data.Models
{
    public class CostingTemplate : EntityBase
    {
        public string Name { get; set; }

        public int Unit { get; set; }
        public double Qty { get; set; }
        public double? Cost { get; set; }
        public double? Total { get; set; }
    }
}