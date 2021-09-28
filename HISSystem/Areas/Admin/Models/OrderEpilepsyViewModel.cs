using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace GeneticSystem.Areas.Admin.Models
{
    public class OrderEpilepsyViewModel
    {
        public IEnumerable<Lookup> Gene { get; set; }
        public List<EpilepsyTemplate> epilepsylist { get; set; }
    }
}
