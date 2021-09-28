using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
  public  class PatientOrderFatConsumption
    {
        public int ID { get; set; }
        public int? ConsumptionTypeID { get; set; }
        public int? EffectedGeneID { get; set; }
        public int OrderID { get; set; }
        public string Reccomendations { get; set; }
        public bool Effected { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup ConsumptionType { get; set; }
        public virtual Lookup EffectedGene { get; set; }
        public PatientOrder Order { get; set; }
    }
}
