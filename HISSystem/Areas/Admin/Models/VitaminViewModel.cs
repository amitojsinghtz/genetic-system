using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class VitaminViewModel
    {
        public int ID { get; set; }
        public int FeederTypeID { get; set; }
        public string Recommendations { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public bool Effect { get; set; }
        public virtual Lookup FeederType { get; set; }
        public List<EffectedGeneVitamin> EffectedGeneVitamins { get; set; }

    }
}
