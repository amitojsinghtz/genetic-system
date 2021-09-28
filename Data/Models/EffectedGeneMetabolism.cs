using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
   public  class EffectedGeneMetabolism
    {
        public int ID { get; set; }
        public int GeneID { get; set; }
        public int MetabolismID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup Gene { get; set; }
        public virtual MetabolismTemplate Metabolism { get; set; }
    }
}
