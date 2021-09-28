using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
     public  class PatientOrderEpilepsy
    {
        public int ID { get; set; }
        public int GeneID { get; set; }
        public int OrderID { get; set; }
        public bool HaveMutation { get; set; }
        public string GeneticMutation { get; set; }
        public string PersonalRiskFactor { get; set; }
        public string Hereditary { get; set; }
        public string Recommendations { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup Gene { get; set; }
        public virtual PatientOrder Order { get; set; }
    }
}
