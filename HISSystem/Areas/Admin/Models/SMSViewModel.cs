using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class SMSViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string FromMobile { get; set; }
        public string ToMobile { get; set; }  
        public string TriggerType { get; set; }
        public int TargetID { get; set; }

    }
}
