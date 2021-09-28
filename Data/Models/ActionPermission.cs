using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ActionPermission: BaseEntity
    {
        public int ActionID { get; set; }
        public int ViewControlID { get; set; }
        public int RoleID { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ActionControl ActionControl { get; set; }
        public virtual ViewControl ViewControl { get; set; }
        public virtual Role Role { get; set; }
    }
}
