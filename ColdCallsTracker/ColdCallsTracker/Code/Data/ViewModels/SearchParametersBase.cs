namespace ColdCallsTracker.Code.Data.ViewModels
{
    public class SearchParametersBase
    {
        public string OrderBy { get; set; }
        public bool IsAsc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}