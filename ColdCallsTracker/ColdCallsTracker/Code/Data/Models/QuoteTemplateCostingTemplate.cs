namespace ColdCallsTracker.Code.Data.Models
{
    public class QuoteTemplateCostingTemplate
    {
        public int CostingTemplateId { get; set; }
        public CostingTemplate CostingTemplate { get; set; }

        public int QuoteTemplateId { get; set; }
        public QuoteTemplate QuoteTemplate { get; set; }
    }
}