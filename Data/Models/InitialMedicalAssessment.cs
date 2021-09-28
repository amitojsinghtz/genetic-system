using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class InitialMedicalAssessment
    {
        public InitialMedicalAssessment()
        {
            InitialMedicalAssesmentDetails = new List<InitialMedicalAssessmentDetail>();
            //ExaminationDetails = new List<InitialMedicalAssessmentDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int PatientPreAssesmentID { get; set; }
        public string InitialMedicalAssesmentNote { get; set; }
        public string InitialMedicalExaminationNote { get; set; }
        public bool Detail { get; set; }
        public string BonyPyramid { get; set; }
        public string CartilagePyramid { get; set; }
        public string Lobule { get; set; }
        public string Skin { get; set; }
        public string Nostrils { get; set; }
        public string Spetum { get; set; }
        public string Valves { get; set; }
        public string Turbinates { get; set; }
        public string Muscosa { get; set; }
        public string Secretions { get; set; }
        public string TeethGum { get; set; }
        public string Tonsils { get; set; }
        public string Pharynx { get; set; }
        public string RightEar { get; set; }
        public string LeftEar { get; set; }
        public string Larynx { get; set; }
        public string Neck { get; set; }
        public bool EyesAcuityChange { get; set; }
        public bool EyesDischarge { get; set; }
        public bool EyesPain { get; set; }
        public bool EyesDiplopia { get; set; }
        public bool EarsHearingLoss { get; set; }
        public bool EarsPain { get; set; }
        public bool EarsTinnitus { get; set; }
        public bool EarsVertigo { get; set; }
        public bool NoseDischarge { get; set; }
        public bool NoseEpistaxis { get; set; }
        public bool NoseRhinorrhea { get; set; }
        public bool MouthSoreThroat { get; set; }
        public bool MouthSores { get; set; }
        public bool MouthBleedingGum { get; set; }
        public bool NeckPain {get;set;}
        public bool NeckDecreasedMobility { get; set; }
        public bool BreastMasses { get; set; }
        public bool BreastPain { get; set; }
        public bool BreastDischarge { get; set; }
        public bool BreastLastMamm { get; set; }
        public bool CVChestPain { get; set; }
        public bool CVDiaphoresis { get; set; }
        public bool CVDoe { get; set; }
        public bool CVPnd { get; set; }
        public bool CVMurmur { get; set; }
        public bool CVOthopnea { get; set; }
        public bool CVPalpitations { get; set; }
        public bool CVDepEdema { get; set; }
        public bool RespSob { get; set; }
        public bool RespCough { get; set; }
        public bool RespSputum { get; set; }
        public bool RespWheeze { get; set; }
        public bool RespHemoptysis { get; set; }
        public bool RespTBExposure { get; set; }
        public bool RespSkinTestResults { get; set; }
        public bool GINvdc { get; set; }
        public bool GIPain { get; set; }
        public bool GIBrbpr { get; set; }
        public bool GIHematemesis { get; set; }
        public bool GIMelena { get; set; }
        public bool GIDysphagia { get; set; }
        public bool GIChangeinStoolColorConsist { get; set; }
        public bool GUDysuria { get; set; }
        public bool GUHematuria { get; set; }
        public bool GUUrgency { get; set; }
        public bool GUFrequency { get; set; }
        public bool GUDifficultyInitiatingStream { get; set; }
        public bool GUBackGroinPain { get; set; }
        public bool OBGYNBleeding { get; set; }
        public bool OBGYNDyspareunia { get; set; }
        public bool OBGYNMenopause { get; set; }
        public bool OBGYNLmp { get; set; }
        public bool MSArthraligia { get; set; }
        public bool MSMyalgia { get; set; }
        public bool MSCramps { get; set; }
        public bool MSArthritis { get; set; }
        public bool HemeAnemia { get; set; }
        public bool HemeEcchymosis { get; set; }
        public bool HemeBleeding { get; set; }
        public bool HemeTransfusion { get; set; }
        public bool EndoHeatColdIntol { get; set; }
        public bool EndoPoluria { get; set; }
        public bool EndoPolydipsia { get; set; }
        public bool NeuroSeizure { get; set; }
        public bool NeuroSyncope { get; set; }
        public bool NeuroParalysis { get; set; }
        public bool NeuroParesthesias { get; set; }
        public bool NeuroTremor { get; set; }
        public bool NeuroMemoryProblems { get; set; }
        public bool PsychAnxiety { get; set; }
        public bool PsychDepression { get; set; }
        public bool PsychAnhedonia { get; set; }
        public bool PsychSubstanceAbuse { get; set; }
        public bool PsychMoodSwings { get; set; }
        public bool PsychSuicideAttempts { get; set; }
        public List<InitialMedicalAssessmentDetail> InitialMedicalAssesmentDetails { get; set; }
        //public List <InitialMedicalAssessmentDetail> ExaminationDetails { get; set; }
        public virtual PatientPreAssesment PatientPreAssesment { get; set; }

    }
}
