using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class TempDropDownDependency
    {
        public int ID { get; set; }
        public int? ColID { get; set; }
        public int? DropDownId { get; set; }
        public int? DepOnOption { get; set; }
        public int? TempID { get; set; }
        [ForeignKey("ColID")]
        public virtual TemplateColumn Col { get; set; }
        [ForeignKey("DropDownId")]
        public virtual TemplateColumn DropDown { get; set; }
    }
}
