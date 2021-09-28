using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class TemplateColumn
    {
        public int ID { get; set; }
        public int TemplateID { get; set; }
        public int TemplateFieldID { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public virtual Template Template { get; set; }
        public virtual Lookup TemplateField { get; set; }
        public virtual TempDependency ColIDTempDependencies { get; set; }
        public virtual ICollection<TempDependency> ChkBoxTempDependencies { get; set; }
        public virtual TempDropDownDependency ColIDDropDownDependency { get; set; }
        public virtual ICollection<TempDropDownDependency> DropDownDependencies { get; set; }

    }

}
