using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class FollowUpTestTempData
    {
        public int ID { get; set; }
        public int TestTempID { get; set; }
        public int TestTempColID { get; set; }
        public string StringValue { get; set; }
        public bool BoolValue { get; set; }
        public int RowNo { get; set; }
        public int OrderID { get; set; }
        [NotMapped]
        public string[] multiSelect { get; set; }
        [NotMapped]
        public StringBuilder multiSelectString { get; set; }
        public virtual TestTemp TestTemp { get; set; }
        public virtual TestTempCol TestTempCol { get; set; }
        public virtual ClientOrder Order { get; set; }
    }
}
