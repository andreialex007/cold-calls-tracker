using System;

namespace ColdCallsTracker.Code.Data.ViewModels._Common
{
    public class ViewModelBase
    {

        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime DateModify { get; set; } = DateTime.Now;
        public int Id { get; set; }
    }
}