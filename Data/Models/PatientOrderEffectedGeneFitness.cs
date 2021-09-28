using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
     public class PatientOrderEffectedGeneFitness
    {
        public int ID { get; set; }
        public int GeneID { get; set; }
        public int OrderFitnessID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup Gene { get; set; }
        public virtual PatientOrderFitness OrderFitness { get; set; }
        
    }
}
