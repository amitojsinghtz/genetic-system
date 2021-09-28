using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class PatientOrderGeneral
    {
        //For Getting Order Data And Binding For Update
        public List<PatientOrderMineral> PatientOrderMineralList { get; set; }
        public List<PatientOrderMetabolism> PatientOrderMetabolismList { get; set; }
        public List<PatientOrderDiet> PatientOrderDietList { get; set; }
        public List<PatientOrderFitness> PatientOrderFitnessList { get; set; }
        public List<PatientOrderVitamin> PatientOrderVitaminList { get; set; }
    }
}
