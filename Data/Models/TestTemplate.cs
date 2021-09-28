using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("TestTemplateTbl")]
    public class TestTemplate
    {
        public int ID { get; set; }
        public int TestTemplateTypeID { get; set; }
        public int SubTestTemplateTypeID { get; set; }
        public bool IsActive { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual Lookup TestTemplateType { get; set; }
        public virtual Lookup SubTestTemplateType { get; set; }
        public virtual List<TestTemplateColumn> TestTemplateColumns { get; set; }
    }
}
