using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class PatientPreAssesment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int PatientEncounterID { get; set; }
        public int PatientID { get; set; }
        public string ChiefComplaint { get; set; }
      
        public string PhysicalNote { get; set; }
        public virtual PatientEncounter PatientEncounter { get; set; }
        public virtual User Patient { get; set; }
        public virtual InitialMedicalAssessment InitialMedicalAssessment { get; set; }
    }
}
