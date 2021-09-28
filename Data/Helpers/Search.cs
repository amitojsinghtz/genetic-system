using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Helpers
{
    public class Search
    {
        public string RegistrationNo { get; set; }
        public int OrderNo { get; set; }
        public int ID { get; set; }
        public string PatientName { get; set; }
        public string PatientMobile { get; set; }
        public string PatientCity { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Status { get; set; }
    }
}
