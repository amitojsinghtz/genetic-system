using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class PatientPreAssesmentVital
    {
        public PatientPreAssesmentVital()
        {
            PreAssessmentVitalDetails = new List<PreAssessmentVitalDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int VitalTypeID { get; set; }
        public int PatientEncounterID { get; set; }
        public string Value { get; set; }
        public List<PreAssessmentVitalDetail> PreAssessmentVitalDetails { get; set; }
        public virtual Lookup VitalType { get; set; }
        public virtual PatientEncounter PatientEncounter { get; set; }

    }
}
