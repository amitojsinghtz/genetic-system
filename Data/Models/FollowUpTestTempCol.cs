using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class FollowUpTestTempCol
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int TestTempID { get; set; }
        public int TempColTypeID { get; set; }
        public int DataSourceID { get; set; }
        public bool IsActive { get; set; }
        public string TestColName { get; set; }
        public int PositionNo { get; set; }
        public virtual ClientOrder Order { get; set; }
        public virtual TestTemp TestTemp { get; set; }
        public virtual MasterTempCol TempColType { get; set; }
        public virtual Lookup DataSource { get; set; }
    }
}
