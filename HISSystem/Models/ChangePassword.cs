using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HISSystem.Models
{
    public class ChangePassword
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
