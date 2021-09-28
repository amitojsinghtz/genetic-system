using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class AddMineralViewModel
    {
        public int ID { get; set; }
        public int? ElementID { get; set; }
        public string Recomendations { get; set; }
        public bool Effect { get; set; }


        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup Element { get; set; }
        public virtual List<EffectedGeneMineral> EffectedGene { get; set; }
    }
}
