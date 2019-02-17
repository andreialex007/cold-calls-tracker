using System;

namespace ColdCallsTracker.Code.Data.ViewModels._Common
{
    public class ViewModelBase
    {

        public DateTime DateCreate { get; set; }
        public DateTime DateModify { get; set; }
        public int Id { get; set; }
    }
}