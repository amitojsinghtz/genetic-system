using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class OrderProstateViiewModel
    {
        public IEnumerable<Lookup> Gene { get; set; }
        public List<ProstateTemplate> prostatelist { get; set; }
    }
}
