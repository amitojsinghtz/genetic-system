using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class FatConsumptionViewModel
    {
        public IEnumerable<Lookup> Gene { get; set; }
        public IEnumerable<Lookup> ConsumptionType { get; set; }
        public List<FatConsumptionTemplate> fatconsumptionlist { get; set; }
    }
}
