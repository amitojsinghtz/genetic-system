using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class AccessPermission : BaseEntity
    {
        public int ViewControlID { get; set; }
        public int RoleID { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ViewControl ViewControl { get; set; }
        public virtual Role Role { get; set; }
    }
}
