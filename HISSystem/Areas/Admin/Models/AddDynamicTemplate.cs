using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class AddDynamicTemplate
    {
        public AddDynamicTemplate()
        {
            this.Template = new Template();
            this.TemplateColumns = new List<TemplateColumn>();
            this.TemplateData = new TemplateData();
            this.TemplateDataList = new List<TemplateData>();
        }
        public Template Template { get; set; }
        public List<TemplateColumn> TemplateColumns { get; set; }
        public TemplateData TemplateData { get; set; }
        public List<TemplateData> TemplateDataList { get; set; }
        public List<TempDependency> TempDependencyList { get; set; }
        public List<TempDropDownDependency> TempDropDownDependencyList { get; set; }
    }
}
