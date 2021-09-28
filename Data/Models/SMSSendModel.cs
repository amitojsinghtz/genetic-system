using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class SMSSendModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Body1 { get; set; }
        public string Body2 { get; set; }
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
