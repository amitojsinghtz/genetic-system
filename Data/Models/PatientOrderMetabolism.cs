using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class PatientOrderMetabolism
    {
        public int ID { get; set; }
        public int ConsumptionTypeID { get; set; }
        public int OrderID { get; set; }
        public int Effected { get; set; }
        public string Recomendation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup ConsumptionType { get; set; }
        public virtual PatientOrder Order { get; set; }
        public virtual List<PatientOrderEffectedGeneMetabolism> PatientOrderEffectedGeneMetabolismList { get; set; }
        [NotMapped]
        public string[] EffectedGenes { get; set; }
    }
}
