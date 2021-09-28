using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace GeneticSystem.Areas.Admin.Models
{
    public class AddGroupViewModel
    {
        public int ID { get; set; }
       public string GroupName { get; set; }
        public List<GroupClientList> GroupClientLists { get; set; }
    }
}
