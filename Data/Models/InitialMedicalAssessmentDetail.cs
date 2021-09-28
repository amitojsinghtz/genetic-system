using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class InitialMedicalAssessmentDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int InitialMedicalAssessmentId { get; set; }
        public int InitialMedicalDetailTypeId { get; set; }
        public bool InitialMedicalDetail { get; set; }
        public string AdditionalFieldValue1 { get; set; }
        public string AdditionalFieldValue2 { get; set; }
        //public string Type { get; set; }
        public virtual IMDetailLookup InitialMedicalDetailType { get; set; }
        public virtual InitialMedicalAssessment InitialMedicalAssessment { get; set; }
    }
}
