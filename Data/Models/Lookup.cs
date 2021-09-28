using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Lookup:BaseEntity
    {
        public string Type { get; set; }
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int? ParentFKId { get; set; }
        public virtual Lookup ParentFK { get; set; }
    }
}
