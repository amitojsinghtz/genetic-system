using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class TestTempData
    {
        public int ID { get; set; }
        public int TestTempID { get; set; }
        public int TestTempColID { get; set; }
        public string StringValue { get; set; }
        public bool BoolValue { get; set; }
        public int? DropDownValueID { get; set; }
        public bool IsActive { get; set; }
        public int RowNo { get; set; }
        [NotMapped]
        public string[] multiSelect { get; set; }
        [NotMapped]
        public StringBuilder multiSelectString { get; set; }
        public virtual TestTemp TestTemp { get; set; }
        public virtual TestTempCol TestTempCol { get; set; }
        public virtual Lookup DropDownValue { get; set; }
    }
}
