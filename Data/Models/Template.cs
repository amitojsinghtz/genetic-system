using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("TemplateTbl")]
   public class Template
    {
        public int ID { get; set; }
        public int TemplateTypeID { get; set; }
        public int? SubTemplateTypeID { get; set; }
        public bool IsActive { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual Lookup TemplateType { get; set; }
        public virtual Lookup SubTemplateType { get; set; }
        public virtual List<TemplateColumn> TemplateColumns { get; set; }
    }
}
