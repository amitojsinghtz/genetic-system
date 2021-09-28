using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class TestTemplateColumn
    {
        public int ID { get; set; }
        public int TestTemplateID { get; set; }
        public int TestTemplateFieldID { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        //public virtual TestTemplate TestTemplate { get; set; }
        public virtual Lookup TestTemplateField { get; set; }
    }
}
