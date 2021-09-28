using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class VitaminTemplate
    {
        public int ID { get; set; }
        public int FeederTypeID { get; set; }
        public string Recommendations { get; set; }
        public bool Effect { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup FeederType { get; set; }
        public virtual List<EffectedGeneVitamin> EffectedGeneList { get; set; }
        [NotMapped]
        public string[] EffectedGenes { get; set; }
    }
}
