using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
  public  class PatientOrderEffectedGeneMineral
    {
        public int ID { get; set; }
        public int GeneID { get; set; }
        public int OrderMineralID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public Lookup Gene { get; set; }
        public  virtual PatientOrderMineral OrderMineral { get; set; }
    }
}
