namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class QuoteSearchParameters : SearchParametersBase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
    }
}