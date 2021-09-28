using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace HISSystem.Areas.Admin.Models
{
    public class PatientEncounterViewModel
    {
        public PatientEncounterViewModel()
        {
            PatientPreAssesmentVitals = new List<PatientPreAssesmentVital>();
            User = new User();
            PatientEncounter = new PatientEncounter();
            PatientPreAssesment = new PatientPreAssesment();
        }
        public User User { get; set; }
        public PatientEncounter PatientEncounter { get; set; }
        public PatientPreAssesment PatientPreAssesment { get; set; }
       
        public List<PatientPreAssesmentVital> PatientPreAssesmentVitals { get; set; }
        public List<InitialMedicalAssessmentDetail> InitialMedicalAssessmentDetail { get; set; }
        public List<PreAssessmentVitalDetail> PreAssessmentVitalDetails { get; set; }
    }
}
