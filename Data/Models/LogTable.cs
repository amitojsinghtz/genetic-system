using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class LogTable
    {
        public LogTable()
        {
            LogData = new List<LogData>();
        }
        public int ID { get; set; }
        public int UserID { get; set; }
        public string IPAddress { get; set; }
        public string TableName { get; set; }
        public int TargetID { get; set; }
        public string Operation { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsActive { get; set; }
        public List<LogData> LogData { get; set; }
    }
}
