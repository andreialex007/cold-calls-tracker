﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColdCallsTracker.Code.Data.Models
{
    public class EntityBase : IpkidEntity
    {
        public DateTime DateCreate { get; set; }
        public DateTime DateModify { get; set; }
        public int Id { get; set; }
    }
}
