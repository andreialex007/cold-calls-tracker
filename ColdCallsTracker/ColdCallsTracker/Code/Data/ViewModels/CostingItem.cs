using ColdCallsTracker.Code.Data.ViewModels._Common;

namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class CostingItem : ViewModelBase
    {
        public string Name { get; set; }

        public int Unit { get; set; }
        public double Qty { get; set; }
        public double Cost { get; set; }
        public double Total { get; set; }

        public int CategoryId { get; set; }

        public int QuoteId { get; set; }
        public QuoteItem Quote { get; set; }
    }
}
