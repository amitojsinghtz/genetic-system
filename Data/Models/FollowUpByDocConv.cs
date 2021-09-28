using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class FollowUpByDocConv
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int SenderID { get; set; }
        public string Message { get; set; }
        public int? ParentID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual FollowUpByDocConv Parent { get; set; }
        public virtual ClientOrder Order { get; set; }
        public virtual User Sender { get; set; }
    }
}
