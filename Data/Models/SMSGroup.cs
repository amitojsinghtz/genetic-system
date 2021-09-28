using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
   public class SMSGroup
    {
        public int ID{ get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
