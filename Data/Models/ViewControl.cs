using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ViewControl : BaseEntity
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public List<AccessPermission> AccessPermission { get; set; }
    }
}
