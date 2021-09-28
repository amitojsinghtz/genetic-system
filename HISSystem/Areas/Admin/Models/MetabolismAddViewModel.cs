using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class MetabolismAddViewModel
    {
        public int ID { get; set; }
        public int ConsumptionTypeID { get; set; }
        public int Effected { get; set; }
        public string Recomendation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup ConsumptionType { get; set; }
        public virtual List<EffectedGeneMetabolism> EffectedGeneMetabolism { get; set; }


    }
}
