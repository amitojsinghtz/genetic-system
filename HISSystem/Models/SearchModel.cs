using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HISSystem.Models
{
    public class SearchModel
    {
        public int InPatient { get; set; }
        public int OutPatient { get; set; }
        public int appointmentToday { get; set; }
        public string RegistrationNo { get; set; }
        public int ID { get; set; }
        public string PatientName { get; set; }
        public int PatientMobile { get; set; }
        public string PatientCity { get; set; }
        public string BirthDate { get; set; }
        public int Status { get; set; }
    }
}
