using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class LogData
    {
        public int ID { get; set; }
        public int LogTableID { get; set; }
        public string ColumnName { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public DateTime? AddedDate { get; set; }
    }
}
