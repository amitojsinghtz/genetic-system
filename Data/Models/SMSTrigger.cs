using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class SMSTrigger
    {
        public int ID { get; set; }
        public string TriggerName { get; set; }
        public int SMSID { get; set; }         
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual SMS SMS { get; set; }
    }
}
