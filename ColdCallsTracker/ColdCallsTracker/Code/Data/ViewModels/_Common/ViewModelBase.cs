using System;
using System.ComponentModel.DataAnnotations;

namespace ColdCallsTracker.Code.Data.ViewModels._Common
{
    public class ViewModelBase
    {

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateModify { get; set; } = DateTime.Now;

        public int Id { get; set; }
    }
}