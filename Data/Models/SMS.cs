using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
   public class SMS
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string CreatedBy { get; set; } 
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
