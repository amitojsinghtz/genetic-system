using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class AddTestDynamicTemplate
    {
        public AddTestDynamicTemplate()
        {
            this.TestTemplate = new TestTemplate();
            this.TestTemplateColumns = new List<TestTemplateColumn>();
            this.TestTemplateData = new TestTemplateData();
            this.TestTemplateDataList = new List<TestTemplateData>();
        }
        public TestTemplate TestTemplate { get; set; }
        public List<TestTemplateColumn> TestTemplateColumns { get; set; }
        public TestTemplateData TestTemplateData { get; set; }
        public List<TestTemplateData> TestTemplateDataList { get; set; }
    }
}
