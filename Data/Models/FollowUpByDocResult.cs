using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class FollowUpByDocResult
    {
        public int ID { get; set; }
        public int? FollowUpByDocID { get; set; }
        public bool Status { get; set; }
        public string Test { get; set; }
        public string Result { get; set; }
        public int OrderID { get; set; }
        [NotMapped]
        public string[] TestArray { get; set; }
        public virtual FollowUpByDoc FollowUpByDoc { get;set;}
        public virtual ClientOrder Order { get; set; }
    }
}
