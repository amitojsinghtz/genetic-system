using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class EmailViewModel
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string AlterEmail { get; set; }
        public string TriggerType { get; set; }
        public int TargetID { get; set; }
        public IFormFile File { get; set; }
    }
}
