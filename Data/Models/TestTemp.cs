using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class TestTemp
    {
        public int ID { get; set; }
        public int TestTempTypeID { get; set; }
        public int? SubTestTempTypeID { get; set; }
        public bool IsActive { get; set; }
        public bool InUse { get; set; }
        public virtual Lookup TestTempType { get; set; }
        public virtual Lookup SubTestTempType { get; set; }
        public virtual ICollection<TestTempCol> TestTempCols { get;set;}

    }
}
