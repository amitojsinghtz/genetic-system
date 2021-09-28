using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Data.Models
{
   public class Email
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Subject { get; set; }
        
        public string Body { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public bool IsActive { get; set; }


    }
}
