using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class EmailTrigger
    {
        public int ID { get; set; }
        public string TriggerName { get; set; }
        public int EmailID { get; set; }
        public int? EmployeeID { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Email Email{get;set;}
    }
}
