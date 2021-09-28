using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace GeneticSystem.Areas.Admin.Models
{
    public class OrderMethyationViewModel
    {
        public List<MethyationTemplate> methyationlist { get; set; }
        public IEnumerable<Lookup> Gene { get; set; }
    }
}
