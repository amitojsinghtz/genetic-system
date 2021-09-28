using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class DietViewModel
    {     
            public int ID { get; set; }
            public int? ConsumptionTypeID { get; set; }
            public string EffectedGood { get; set; }
            public string EffectedBad { get; set; }
            public DateTime? CreatedOn { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public bool IsActive { get; set; }
            public virtual Lookup ConsumptionType { get; set; }
            public virtual List<EffectedGeneDiet> Gene { get; set; }  
    }
}
