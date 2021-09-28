using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
   public class PatientOrderVitamin
    {
        public int ID { get; set; }
        public int FeederTypeID { get; set; }
        public string Recommendations { get; set; }
        public bool Effect { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup FeederType { get; set; }
        public int OrderID { get; set; }
        public virtual PatientOrder Order { get; set; }
        public virtual List<PatientOrderEffectedGeneVitamin> PatientOrderEffectedGeneVitaminList { get; set; }
        [NotMapped]
        public string[] EffectedGenes { get; set; }
    }
}
