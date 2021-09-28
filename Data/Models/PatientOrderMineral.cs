using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
  public  class PatientOrderMineral
    {
        public int ID { get; set; }
        public int? ElementID { get; set; }
        public int OrderID { get; set; }
        public bool Effect { get; set; }
        public string Recomendations { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup Element { get; set; }
        public virtual PatientOrder Order { get; set; }
        public virtual List<PatientOrderEffectedGeneMineral> PatientOrderEffectedGeneMineralList { get; set; }
        [NotMapped]
        public string[] EffectedGenes { get; set; }
    }
}
