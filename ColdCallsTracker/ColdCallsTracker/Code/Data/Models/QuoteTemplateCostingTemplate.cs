namespace ColdCallsTracker.Code.Data.Models
{
    public class QuoteTemplateCostingTemplate
    {
        public int QuoteTemplateId { get; set; }
        public QuoteTemplate QuoteTemplate { get; set; }

        public int CostingTemplateId { get; set; }
        public CostingTemplate CostingTemplate { get; set; }

        public double Multiplier { get; set; } = 1;
    }
}