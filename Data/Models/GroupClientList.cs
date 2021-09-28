using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class GroupClientList
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual User User { get; set; }
        public virtual SMSGroup Group { get; set; }
        public bool IsActive { get; set; }
       

    }
}
