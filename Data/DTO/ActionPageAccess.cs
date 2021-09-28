using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    class ActionPageAccess
    {
        public string ID { get; set; }
        public string ViewControlID { get; set; }
        public string RoleID { get; set; }
        public string ViewControlActionID { get; set; }
        public int ActionID { get; set; }
       
        public Nullable<bool> IsActive { get; set; }
        public virtual ViewControlDTO Menu { get; set; }
        public virtual RoleDTO Role { get; set; }
        public virtual ActionPageDTO ActionPage { get; set; }
    }
}
