using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
   public class EffectedGeneMineral
    {
        public int ID { get; set; }
        public int GeneID { get; set; }
        public int MineralID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public Lookup Gene { get; set; }
        public MineralTemplate Mineral { get; set; }
    }
}
