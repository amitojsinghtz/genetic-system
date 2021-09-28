using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class PatientOrderDiet
    {
        public int ID { get; set; }
        public int? ConsumptionTypeID { get; set; }
        public string EffectedGood { get; set; }
        public string EffectedBad { get; set; }
        public int OrderID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public Lookup ConsumptionType { get; set; }
        public virtual PatientOrder Order { get; set; }
        public virtual List<PatientOrderEffectedGeneDiet> PatientOrderEffectedGeneDietList { get; set; }
        [NotMapped]
        public string[] EffectedGenes { get; set; }
    }
}
