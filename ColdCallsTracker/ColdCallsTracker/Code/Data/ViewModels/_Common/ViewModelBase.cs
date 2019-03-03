using System;
using System.ComponentModel.DataAnnotations;
using ColdCallsTracker.Code.Utils;
using Newtonsoft.Json;

namespace ColdCallsTracker.Code.Data.ViewModels._Common
{
    public class ViewModelBase
    {

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime DateModify { get; set; } = DateTime.Now;

        public int Id { get; set; }
    }
}