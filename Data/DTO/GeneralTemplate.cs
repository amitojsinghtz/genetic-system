using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class PatientOrderGeneral
    {
        public PatientOrderGeneral()
        {
            this.PatientOrderMineralList = new List<PatientOrderMineral>();
            this.PatientOrderMetabolismList = new List<PatientOrderMetabolism>();
            this.PatientOrderVitaminList = new List<PatientOrderVitamin>();
            this.PatientOrderDietList = new List<PatientOrderDiet>();
            this.PatientOrderFitnessList = new List<PatientOrderFitness>();
        }
        public List<PatientOrderMineral> PatientOrderMineralList { get; set; }
        public List<PatientOrderMetabolism> PatientOrderMetabolismList { get; set; }
        public List<PatientOrderVitamin> PatientOrderVitaminList { get; set; }
        public List<PatientOrderDiet> PatientOrderDietList { get; set; }
        public List<PatientOrderFitness> PatientOrderFitnessList { get; set; }
    }
}
