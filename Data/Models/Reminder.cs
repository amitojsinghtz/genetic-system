using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Reminder
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
