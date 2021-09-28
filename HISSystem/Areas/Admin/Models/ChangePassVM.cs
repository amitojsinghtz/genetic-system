using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class ChangePassVM
    {
        public int userId { get; set; }
        public string oldPass { get; set; }
        public string newPass { get; set; }
        public string userName { get; set; }
    }
}
