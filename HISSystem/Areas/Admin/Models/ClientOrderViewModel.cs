using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class ClientOrderViewModel
    {
        public ClientOrder ClientOrder { get; set; }
        public List<ClientOrderData> ClientOrderData { get; set; }
        public List<TemplateData> TemplateDataList { get; set; }
        public List<Template> TemplateList { get; set; }
        public List<ClientOrder> ClientOrderList { get; set; }
        public List<TestTempData> TestTempDataList { get; set; }
        public List<List<TestTempData>[]> expData { get; set; }
        public List<TestTempDataListVM> tempDataListVM { get; set; }
        public List<FollowUpTestTempDataListVM> followUpTempDataListVM { get; set; }
        public string CreatedByField { get; set; }
    }

    public class TestTempDataListVM
    {
        public List<TestTempData>[] expDataList { get; set; }
        public string TestName { get; set; }
        public List<TestTempCol> testTempCols { get; set; }
    }

    public class FollowUpTestTempDataListVM
    {
        public List<FollowUpTestTempData>[] expDataList { get; set; }
        public string TestName { get; set; }
        public List<TestTempCol> testTempCols { get; set; }
    }

}
