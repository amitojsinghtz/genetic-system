using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace GeneticSystem.Areas.Admin.Models
{
    public class AddOrderViewModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int GeneTemplateID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Summary { get; set; }
        public virtual User User{get;set;}
        public string FollowUpNeeded { get; set; }

        public List<PatientOrderMethyation> PatientOrderMethyationList { get; set; }
        public List<PatientOrderProstate> PatientOrderProstateList { get; set; }
        public List<PatientOrderFatConsumption> PatientOrderFatConsumptionList { get; set; }
        public List<PatientOrderEpilepsy> PatientOrderEpilepsyList { get; set; }
        public GeneralTemplateViewModel GeneralTemplates { get; set; }

        public PatientOrderGeneral PatientOrderGeneralList { get; set; }
    }

   
}
