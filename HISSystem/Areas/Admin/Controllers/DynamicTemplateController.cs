using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.UnitOfServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DynamicTemplateController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;

        public DynamicTemplateController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }
        public IActionResult Index()
        {
            var templateList = db.DynamicTemplateService.GetAllTemplates().ToList();
            ViewBag.Templates = templateList.Select(x => x.TemplateType?.Name + ">>" + x.SubTemplateType?.Name ?? "");
            List<Template> templates = new List<Template>();
            return View(templates);
        }
        [HttpGet]
        public IActionResult AddTemplate()
        {
            AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();
            var LookupList = db.LookupService.GetLookups();
            ViewBag.TemplateType = LookupList.Where(x => x.Type == "TemplateType");
            ViewBag.SubTemplateType = LookupList.Where(x => x.Type == "SubTemplateType");
            ViewBag.TemplateField = LookupList.Where(x => x.Type == "TemplateField");
            return PartialView("_AddTemplate", dynamicTemplate);
        }

        [HttpPost]
        public IActionResult AddTemplate(AddDynamicTemplate dynamicTemplate)
        {
            Template template = new Template
            {
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = 1,
                AddedBy = 1,
                TemplateTypeID = dynamicTemplate.Template.TemplateTypeID,
                SubTemplateTypeID = dynamicTemplate.Template.SubTemplateTypeID,
                IsActive = true,
                TemplateColumns = dynamicTemplate.TemplateColumns
            };

            var result = db.DynamicTemplateService.AddTemplate(template);
            var templateId = result.ID;

            AddDynamicTemplate newdynamicTemplate = new AddDynamicTemplate();
            dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(result.ID);
            dynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);

            ViewBag.TemplateID = dynamicTemplate.Template.ID;

            ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TemplateType");
            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_GetTemplateDetail", dynamicTemplate);
        }

        [HttpGet]
        public IActionResult UpdateTemplate(int ID)
        {
            AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();

            ViewBag.TemplateType = db.LookupService.GetLookUpByTypeName("TemplateType");
            ViewBag.SubTemplateType = db.LookupService.GetLookUpByTypeName("SubTemplateType");
            ViewBag.TemplateField = db.LookupService.GetLookUpByTypeName("TemplateField");
            ViewBag.TemplateID = ID;
            dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(ID);

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
        public IActionResult UpdateTemplate(AddDynamicTemplate dynamicTemplate)
        {

            var newDynamicTemplate = new AddDynamicTemplate();
            newDynamicTemplate = dynamicTemplate;
            newDynamicTemplate.Template.ID = dynamicTemplate.Template.ID;

            db.DynamicTemplateService.DeleteTemplateColumnsByID(dynamicTemplate.Template.ID);
            db.DynamicTemplateService.InsertTemplateColumn(dynamicTemplate.TemplateColumns);

            AddDynamicTemplate passDynamicTemplate = new AddDynamicTemplate();
            passDynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(dynamicTemplate.Template.ID);
            passDynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);
            ViewBag.TemplateID = passDynamicTemplate.Template.ID;

            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_GetTemplateDetail", passDynamicTemplate);
        }


        [HttpGet]
        public IActionResult GetDynamicTemplateByID(int tempID)
        {
            AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();
            var LookupList = db.LookupService.GetLookups();

            dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(tempID);
            dynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);

            ViewBag.TemplateID = dynamicTemplate.Template.ID;

            ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TemplateType");
            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_GetTemplateDetail", dynamicTemplate);
        }

        [HttpGet]
        public IActionResult GetDynamicTemplate(string temptype, string subtemptype)
        {
            AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();
            var LookupList = db.LookupService.GetLookups();
            dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByName(temptype, subtemptype);
            dynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);

            ViewBag.TemplateID = dynamicTemplate.Template.ID;

            ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TemplateType");
            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_GetTemplateDetail", dynamicTemplate);
        }


        [HttpGet]
        public IActionResult GetDropDownDependency(int ID)
        {
            AddDynamicTemplate newdynamicTemplate = new AddDynamicTemplate();
            var result = db.DynamicTemplateService.GetTemplateByID(ID);
            newdynamicTemplate.Template = result;
            ViewBag.TempID = ID;
            ViewBag.Column = result.TemplateColumns.Where(x => x.TemplateFieldID == 222 || x.TemplateFieldID == 618).ToList();

            ViewBag.DependentColumn = result.TemplateColumns.Where(x => x.TemplateFieldID != 215 && x.TemplateFieldID != 213 && x.TemplateFieldID != 214 && x.TemplateFieldID != 222 && x.TemplateFieldID != 216).Select(x => new SelectListItem
            {
                Text = x.TemplateField.Name,
                Value = x.ID.ToString()
            }).ToList();

            if (ViewBag.Column != null && ViewBag.DependentColumn != null)
            {
                newdynamicTemplate.TempDropDownDependencyList = db.DynamicTemplateService.GetTempDropDownDependenciesByTempID(ID);
                ViewBag.TemplateName = result.TemplateType?.Name;
                ViewBag.SubTemplateName = result.SubTemplateType?.Name ?? "";

                ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

                ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

                return PartialView("_DropDownDependency", newdynamicTemplate);
            }

            return null;
        }

        [HttpPost]
        public IActionResult PostDropDownDependency(List<TempDropDownDependency> tempDependencies)
        {

            if (tempDependencies != null && tempDependencies.Count() > 0)
            {
                db.DynamicTemplateService.RemoveDropDownDependencyList(Convert.ToInt32(tempDependencies[0].TempID));
                db.DynamicTemplateService.AddDropDownDependencyList(tempDependencies);

                AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();
                var LookupList = db.LookupService.GetLookups();
                //var templateID = db.DynamicTemplateService.GetTemplateColumnsByID(Convert.ToInt32(tempDependencies[0].TempID));
                dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(Convert.ToInt32(tempDependencies[0].TempID));
                dynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);

                ViewBag.TemplateID = dynamicTemplate.Template.ID;

                ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TemplateType");
                ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
                ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
                ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
                ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

                ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

                ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

                return PartialView("_GetTemplateDetail", dynamicTemplate);
            }

            return null;

        }

        [HttpGet]
        public IActionResult AddDependency(int ID)
        {
            AddDynamicTemplate newdynamicTemplate = new AddDynamicTemplate();
            var result = db.DynamicTemplateService.GetTemplateByID(ID);
            newdynamicTemplate.Template = result;
            ViewBag.TempID = ID;

            ViewBag.CheckBoxID = result.TemplateColumns.Where(x => x.TemplateFieldID == 216 || x.TemplateFieldID == 620 || x.TemplateFieldID == 621).ToList().Select(x => new SelectListItem
            {
                Text = x.TemplateField.Name,
                Value = x.ID.ToString()
            }).ToList();

            ViewBag.ColumnList = result.TemplateColumns.Where(x => x.TemplateFieldID != 215 && x.TemplateFieldID != 213 && x.TemplateFieldID != 214 && x.TemplateFieldID != 222 && x.TemplateFieldID != 216).Select(x => new SelectListItem
            {
                Text = x.TemplateField.Name,
                Value = x.ID.ToString()
            }).ToList();

            if (ViewBag.CheckBoxID != null && ViewBag.ColumnList != null)
            {
                newdynamicTemplate.TempDependencyList = db.DynamicTemplateService.GetTempDependenciesByTempID(ID);
                ViewBag.TemplateName = result.TemplateType?.Name;
                ViewBag.SubTemplateName = result.SubTemplateType?.Name ?? "";

                ViewBag.Dependency = new List<SelectListItem>
            {
              new SelectListItem{ Text="Yes", Value = "0" },
              new SelectListItem{ Text="No", Value = "1" }
            };

                return PartialView("_AddDependency", newdynamicTemplate);
            }

            return null;
        }

        [HttpPost]
        public IActionResult AddDependency(List<TempDependency> tempDependencies)
        {

            if (tempDependencies != null && tempDependencies.Count() > 0)
            {
                db.DynamicTemplateService.RemoveTempDependencyListByTempID(Convert.ToInt32(tempDependencies[0].TempID));
                db.DynamicTemplateService.AddTempDependencyList(tempDependencies);

                AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();
                var LookupList = db.LookupService.GetLookups();
                var templateID = db.DynamicTemplateService.GetTemplateByColID(Convert.ToInt32(tempDependencies[0].ChkBoxID));
                dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(templateID);
                dynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);

                ViewBag.TemplateID = dynamicTemplate.Template.ID;

                ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TemplateType");
                ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
                ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
                ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
                ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

                ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

                ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

                return PartialView("_GetTemplateDetail", dynamicTemplate);
            }

            return null;

        }


        [HttpGet]
        public bool DeleteTemplate(int templateId)
        {
            return db.DynamicTemplateService.DeleteTemplatebyId(templateId);

        }

        [HttpGet]
        public IActionResult AddTemplateData(int templateId)
        {
            AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();
            var LookupList = db.LookupService.GetLookups();
            dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(templateId);

            ViewBag.TemplateID = dynamicTemplate.Template.ID;

            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };

            return PartialView("_AddTemplateData", dynamicTemplate);
        }

        [HttpGet]
        public bool TempValidation(int? tempId, int? subTempId)
        {
            if (tempId != null)
            {
                if (subTempId != null)
                {
                    Template template = db.DynamicTemplateService.GetTemplateByTempSubTempId(Convert.ToInt32(tempId), Convert.ToInt32(subTempId));

                    if (template != null)
                        return false;
                    else
                        return true;
                }
                else
                {
                    Template template = db.DynamicTemplateService.GetTemplateByTempId(Convert.ToInt32(tempId));

                    if (template != null)
                        return false;
                    else
                        return true;
                }
            }
            else
            {
                return false;
            }

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

        //    bool result = db.DynamicTemplateService.SaveTemplateData(templateData);

        //    return result;
        //}
        [HttpPost]
        public bool AddTemplateDetail(AddDynamicTemplate dynamicTemplate)
        {
            try
            {


                TemplateData templateData = dynamicTemplate.TemplateData;

                if (templateData.Genes != null)
                    templateData.GeneID = string.Join(',', dynamicTemplate.TemplateData.Genes);
                templateData.IsActive = true;
                templateData.AddedBy = 1;
                templateData.UpdatedBy = 1;

                var result = db.DynamicTemplateService.SaveTemplateData(templateData);

                //AddDynamicTemplate passDynamicTemplate = new AddDynamicTemplate();
                //passDynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(dynamicTemplate.TemplateData.TemplateID);
                //passDynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(passDynamicTemplate.Template.ID);
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

                //dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByName("General", "Epilepsy");
                //dynamicTemplate.TemplateDataList = db.DynamicTemplateService.GetTemplateDataID(dynamicTemplate.Template.ID);

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

        [HttpGet]
        public IActionResult UpdateTemplateData(int tempDataID)
        {
            AddDynamicTemplate dynamicTemplate = new AddDynamicTemplate();
            dynamicTemplate.TemplateData = db.DynamicTemplateService.GetTemplateDataByID(tempDataID);
            dynamicTemplate.Template = db.DynamicTemplateService.GetTemplateByID(dynamicTemplate.TemplateData.TemplateID);

            ViewBag.TemplateID = dynamicTemplate.Template.ID;

            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };
            return PartialView("_UpdateTemplateData", dynamicTemplate);
        }

        [HttpPost]
        public bool UpdateTemplateData(AddDynamicTemplate dynamicTemplate)
        {
            TemplateData templateData = dynamicTemplate.TemplateData;

            if (templateData.Genes != null)
                templateData.GeneID = string.Join(',', dynamicTemplate.TemplateData.Genes);

            templateData.IsActive = true;
            templateData.AddedBy = 1;
            templateData.UpdatedBy = 1;

            bool result = db.DynamicTemplateService.UpdateTemplateData(templateData);

            return result;
        }

        [HttpGet]
        public bool DeleteTemplateData(int id)
        {
            bool result = db.DynamicTemplateService.RemoveTemplateDataByID(id);
            return result;
        }

        [HttpGet]
        public IActionResult SearchTemplate(string searchQuery)
        {
            if (searchQuery != null)
            {
                var templateList = db.DynamicTemplateService.SearchTemplate(searchQuery).ToList();
                ViewBag.Templates = templateList.Select(x => x.TemplateType?.Name + ">>" + x.SubTemplateType.Name ?? "");
                List<Template> templates = new List<Template>();
                return View("_SearchMasterTemplate", templates);
            }
            else
            {
                var templateList = db.DynamicTemplateService.GetAllTemplates().ToList();
                ViewBag.Templates = templateList.Select(x => x.TemplateType?.Name + ">>" + x.SubTemplateType?.Name ?? "");
                List<Template> templates = new List<Template>();
                return View("_SearchMasterTemplate", templates);
            }
        }
    }
}