using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;

namespace HISSystem.Areas.Admin.Models
{
    public class LookupIndexModel
    {
        public PagedData<Lookup> PagedLookups { get; set; }
        public Lookup Lookup { get; set; }
    }
}
