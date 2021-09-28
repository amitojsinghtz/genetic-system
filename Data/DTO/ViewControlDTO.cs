using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
   public class ViewControlDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
