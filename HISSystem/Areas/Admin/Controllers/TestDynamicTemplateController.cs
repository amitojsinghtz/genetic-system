using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.UnitOfServices;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestDynamicTemplateController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;

        public TestDynamicTemplateController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }
        public IActionResult Index()
        {
            var templateList = db.TestDynamicTemplateService.GetAllTemplates().ToList();
            ViewBag.Templates = templateList.Select(x => x.TestTemplateType?.Name + ">>" + x.SubTestTemplateType.Name ?? "");
            List<TestTemplate> templates = new List<TestTemplate>();
            return View(templates);
        }


        [HttpGet]
        public IActionResult AddTemplate()
        {
            AddTestDynamicTemplate dynamicTemplate = new AddTestDynamicTemplate();
            var LookupList = db.LookupService.GetLookups();
            ViewBag.TemplateType = LookupList.Where(x => x.Type == "TemplateType");
            ViewBag.SubTemplateType = LookupList.Where(x => x.Type == "SubTemplateType");
            ViewBag.TemplateField = LookupList.Where(x => x.Type == "TemplateField");
            return PartialView("_AddTemplate", dynamicTemplate);
        }
        [HttpPost]
        public bool AddTemplate(AddTestDynamicTemplate dynamicTemplate)
        {
            try
            {
            TestTemplate template = new TestTemplate
            {
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = 1,
                AddedBy = 1,
                TestTemplateTypeID = dynamicTemplate.TestTemplate.TestTemplateTypeID,
                SubTestTemplateTypeID = dynamicTemplate.TestTemplate.SubTestTemplateTypeID,
                IsActive = true,
                TestTemplateColumns = dynamicTemplate.TestTemplateColumns,
            };

            var result = db.TestDynamicTemplateService.AddTemplate(template);

            return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpGet]
        public IActionResult UpdateTemplate(int ID)
        {
            AddTestDynamicTemplate dynamicTemplate = new AddTestDynamicTemplate();

            ViewBag.TemplateType = db.LookupService.GetLookUpByTypeName("TemplateType");
            ViewBag.SubTemplateType = db.LookupService.GetLookUpByTypeName("SubTemplateType");
            ViewBag.TemplateField = db.LookupService.GetLookUpByTypeName("TemplateField");
            ViewBag.TemplateID = ID;
            dynamicTemplate.TestTemplate = db.TestDynamicTemplateService.GetTemplateByID(ID);

            //dynamicTemplate.Template.ID = getTemplate.ID;
            //dynamicTemplate.Template.IsActive = getTemplate.IsActive;
            //dynamicTemplate.Template.SubTemplateType = getTemplate.SubTemplateType;
            //dynamicTemplate.Template.SubTemplateTypeID = getTemplate.SubTemplateTypeID;
            //dynamicTemplate.Template.TemplateColumns = getTemplate.TemplateColumns;
            //dynamicTemplate.Template.TemplateType = getTemplate.TemplateType;
            //dynamicTemplate.Template.TemplateTypeID = getTemplate.TemplateTypeID;
            //dynamicTemplate.Template.UpdatedBy = getTemplate.UpdatedBy;

            return PartialView("_UpdateTemplate", dynamicTemplate);
        }
        [HttpPost]
        public IActionResult UpdateTemplate(AddTestDynamicTemplate dynamicTemplate)
        {

            var newDynamicTemplate = new AddTestDynamicTemplate();
            newDynamicTemplate = dynamicTemplate;
            newDynamicTemplate.TestTemplate.ID = dynamicTemplate.TestTemplate.ID;

            db.TestDynamicTemplateService.DeleteTemplateColumnsByID(dynamicTemplate.TestTemplate.ID);
            db.TestDynamicTemplateService.InsertTemplateColumn(dynamicTemplate.TestTemplateColumns);

            AddTestDynamicTemplate passDynamicTemplate = new AddTestDynamicTemplate();
            passDynamicTemplate.TestTemplate = db.TestDynamicTemplateService.GetTemplateByID(dynamicTemplate.TestTemplate.ID);
            passDynamicTemplate.TestTemplateDataList = db.TestDynamicTemplateService.GetTemplateDataID(dynamicTemplate.TestTemplate.ID);
            ViewBag.TemplateID = passDynamicTemplate.TestTemplate.ID;

            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_GetTemplateDetail", passDynamicTemplate);
        }



        [HttpGet]
        public IActionResult GetDynamicTemplate(string temptype, string subtemptype)
        {
            AddTestDynamicTemplate dynamicTemplate = new AddTestDynamicTemplate();

            dynamicTemplate.TestTemplate = db.TestDynamicTemplateService.GetTemplateByName(temptype, subtemptype).Result;
            dynamicTemplate.TestTemplateDataList = db.TestDynamicTemplateService.GetTemplateDataID(dynamicTemplate.TestTemplate.ID);

            ViewBag.TemplateID = dynamicTemplate.TestTemplate.ID;

            ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TemplateType");
            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_GetTemplateDetail", dynamicTemplate);
        }


        [HttpGet]
        public IActionResult AddTemplateData(int templateId)
        {
            AddTestDynamicTemplate dynamicTemplate = new AddTestDynamicTemplate();
            var LookupList = db.LookupService.GetLookups();
            dynamicTemplate.TestTemplate = db.TestDynamicTemplateService.GetTemplateByID(templateId);

            ViewBag.TemplateID = dynamicTemplate.TestTemplate.ID;

            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_AddTemplateData", dynamicTemplate);
        }

        [HttpGet]
        public bool TempValidation(int tempId, int subTempId)
        {
            TestTemplate template = db.TestDynamicTemplateService.GetTemplateByTempSubTempId(tempId, subTempId);

            if (template != null)
                return false;
            else
                return true;
        }

        //[HttpPost]
        //public bool AddTemplateDetail(AddDynamicTemplate dynamicTemplate)
        //{
        //    TemplateData templateData = dynamicTemplate.TemplateData;

        //    if (templateData.Genes != null)
        //    templateData.GeneID = string.Join(',', dynamicTemplate.TemplateData.Genes);
        //    templateData.IsActive = true;
        //    templateData.AddedBy = 1;
        //    templateData.UpdatedBy = 1;

        //    bool result = db.TestDynamicTemplateService.SaveTemplateData(templateData);

        //    return result;
        //}
        [HttpPost]
        public bool AddTemplateDetail(AddTestDynamicTemplate dynamicTemplate)
        {
            try
            {


                TestTemplateData templateData = dynamicTemplate.TestTemplateData;

                if (templateData.Genes != null)
                    templateData.GeneID = string.Join(',', dynamicTemplate.TestTemplateData.Genes);
                templateData.IsActive = true;
                templateData.AddedBy = 1;
                templateData.UpdatedBy = 1;

                var result = db.TestDynamicTemplateService.SaveTemplateData(templateData);

                //AddDynamicTemplate passDynamicTemplate = new AddDynamicTemplate();
                //passDynamicTemplate.Template = db.TestDynamicTemplateService.GetTemplateByID(dynamicTemplate.TemplateData.TemplateID);
                //passDynamicTemplate.TemplateDataList = db.TestDynamicTemplateService.GetTemplateDataID(passDynamicTemplate.Template.ID);
                //ViewBag.TemplateID = passDynamicTemplate.Template.ID;

                //ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
                //ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
                //ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
                //ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

                //ViewBag.Result = new List<SelectListItem>
                //{
                //  new SelectListItem{ Text="High", Value = "1" },
                //  new SelectListItem{ Text="Medium", Value = "2" },
                //  new SelectListItem{ Text="Low", Value = "3" }
                //};

                //dynamicTemplate.Template = db.TestDynamicTemplateService.GetTemplateByName("General", "Epilepsy");
                //dynamicTemplate.TemplateDataList = db.TestDynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);

                //ViewBag.TemplateID = dynamicTemplate.Template.ID;

                //ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TemplateType");
                //ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
                //ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
                //ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
                //ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

                //ViewBag.Result = new List<SelectListItem>
                //{
                //  new SelectListItem{ Text="High", Value = "1" },
                //  new SelectListItem{ Text="Medium", Value = "2" },
                //  new SelectListItem{ Text="Low", Value = "3" }
                //};

                //return PartialView("_GetTemplateDetail", dynamicTemplate);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool UpdateTemplateData(AddTestDynamicTemplate dynamicTemplate)
        {
            TestTemplateData templateData = dynamicTemplate.TestTemplateData;

            if (templateData.Genes != null)
                templateData.GeneID = string.Join(',', dynamicTemplate.TestTemplateData.Genes);

            templateData.IsActive = true;
            templateData.AddedBy = 1;
            templateData.UpdatedBy = 1;

            bool result = db.TestDynamicTemplateService.UpdateTemplateData(templateData);

            return result;
        }

        [HttpGet]
        public bool DeleteTemplateData(int id)
        {
            bool result = db.TestDynamicTemplateService.RemoveTemplateDataByID(id);
            return result;
        }

        [HttpGet]
        public IActionResult SearchTemplate(string searchQuery)
        {
            if (searchQuery != null)
            {
                var templateList = db.TestDynamicTemplateService.SearchTemplate(searchQuery).ToList();
                ViewBag.Templates = templateList.Select(x => x.TestTemplateType?.Name + ">>" + x.SubTestTemplateType.Name ?? "");
                List<TestTemplate> templates = new List<TestTemplate>();
                return View("_SearchMasterTemplate", templates);
            }
            else
            {
                var templateList = db.TestDynamicTemplateService.GetAllTemplates().ToList();
                ViewBag.Templates = templateList.Select(x => x.TestTemplateType?.Name + ">>" + x.SubTestTemplateType.Name ?? "");
                List<TestTemplate> templates = new List<TestTemplate>();
                return View("_SearchMasterTemplate", templates);
            }
        }
    }
}