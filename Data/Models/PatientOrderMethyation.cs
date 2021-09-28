using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
  public  class PatientOrderMethyation
    {
        public int ID { get; set; }
        public int? GeneID { get; set; }
        public int OrderID { get; set; }
        public bool HaveMutation { get; set; }
        public int? Result { get; set; }
        public string ResultSummary { get; set; }
        public string Recommendations { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup Gene { get; set; }
        public virtual PatientOrder Order { get; set; }
    }
}
