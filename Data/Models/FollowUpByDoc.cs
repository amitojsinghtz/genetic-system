using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class FollowUpByDoc
    {
        public int ID { get; set; }
        public int? OrderID { get; set; }
        public string Summmary { get; set; }
        public int? DoctorID { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual User Doctor { get; set; }
    }
}
