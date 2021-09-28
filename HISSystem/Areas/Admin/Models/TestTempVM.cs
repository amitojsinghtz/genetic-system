using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class TestTempVM
    {
        public TestTemp TestTemp { get; set; }
        public List<TestTempCol> TestTempCol { get; set; }
        public IEnumerable<DropDownVM> DropDown { get; set; }
        public List<TestTempData> TestTempDataList { get; set; }
        public List<TestTempData>[] expData { get; set; }
        //row no for adding/editing template data
        public int rowNo { get; set; }
    }
}
