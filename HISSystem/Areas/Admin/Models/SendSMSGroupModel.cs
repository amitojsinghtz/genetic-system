using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class SendSMSGroupModel
    {
       public int ID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }

    }
}
