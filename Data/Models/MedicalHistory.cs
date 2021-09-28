using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class MedicalHistory : BaseEntity
    {
        public string UnknownDiseases { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string FamilyHistory { get; set; }
        public string PaymentType { get; set; }
        public string Summary { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        [NotMapped]
        public string[] UnknownDiseaseArray { get; set; }
        [NotMapped]
        public string[] FamilyHistoryArray { get; set; }
    }
}
