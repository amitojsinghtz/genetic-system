using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using Data.Models;
using Data.Helpers;
using GeneticSystem.Areas.Admin.Models;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TemplateController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;
        public TemplateController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }
        public IActionResult Index()
        {
            var epilepsy = new PagedData<EpilepsyTemplate>();
            var epilepsyList = db.TemplateService.GetEpilepsyTemplate().Where(x => x.IsActive == true).ToList();
            epilepsy.Data = (epilepsyList).Take(PageSize);
            epilepsy.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)epilepsyList.Count() / PageSize));
            
            return View(epilepsy);
        }
        public IActionResult AddEpilepsyTemplate()
        {
            var lookup = db.LookupService.GetLookups().ToList();
            var epilseytemplatelist= db.TemplateService.GetEpilepsyTemplate().Where(x => x.IsActive == true).ToList();
            foreach (var item in epilseytemplatelist)
            {
              var xray=  lookup.Where(x => x.ID != item.GeneID).ToList();
                lookup = xray;
            }
            ViewBag.Gene= lookup.Where(x => x.Type.Contains("Gene"));
          
            var model = new EpilepsyTemplate();
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult AddEpilepsyTemplates(EpilepsyTemplate model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var status = db.TemplateService.Add(model);
            return Json(true);
        
        }
        public IActionResult Delete(int id)
        {
            db.TemplateService.Delete(id);
            return Json(true);
        }
        public IActionResult UpdateEpilepsyTemplate(int Id)
        {
            var lookup = db.LookupService.GetLookups().ToList();
            var epilseytemplatelist = db.TemplateService.GetEpilepsyTemplate().Where(x => x.IsActive == true).ToList();
            foreach (var item in epilseytemplatelist)
            {
                if (item.ID != Id)
                {
                    var xray = lookup.Where(x => x.ID != item.GeneID).ToList();
                    lookup = xray;
                }
               
            }
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            var data = db.TemplateService.GetById(Id);
            return PartialView(data);
        }
        public IActionResult UpdateEpilepsyTemplates(EpilepsyTemplate model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            db.TemplateService.Update(model);
            return Json(true);
        }
        public IActionResult Getepilepsy(int page, string searchstring)
        {
            var epilepsytemplate = new PagedData<EpilepsyTemplate>();
            var epilepsytemplateList = db.TemplateService.GetEpilepsyTemplate().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                epilepsytemplateList = epilepsytemplateList.Where(x => x.Gene != null && x.Gene.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || (x.GeneticMutation != null && x.GeneticMutation.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) ||(x.Hereditary !=null && x.Hereditary.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) || (x.PersonalRiskFactor!=null && x.PersonalRiskFactor.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) || (x.Recommendations !=null  && x.Recommendations.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            epilepsytemplate.Data = (epilepsytemplateList).Skip(PageSize * (page - 1)).Take(PageSize);
            epilepsytemplate.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)epilepsytemplateList.Count() / PageSize));

            return PartialView(epilepsytemplate);
            
        }
        public IActionResult FitnessTemplate()
        {
            var fitness = new PagedData<FitnessTemplateViewModel>();
            var fitnessviewmodel = new  List<FitnessTemplateViewModel>();
            var fitnessList = db.TemplateService.GetFitnessTemplate().Where(x => x.IsActive == true).ToList();
            foreach (var item in fitnessList)
            {
                var xray = db.TemplateService.GetEffectedGeneFitness(item.ID).Where(x=>x.IsActive==true).ToList();
                var fitnessview = new FitnessTemplateViewModel()
                {
                    ID = item.ID,
                  ConsumptionTypeID=item.ConsumptionTypeID,
                  ConsumptionType=item.ConsumptionType,
                    EffectedGood=item.EffectedGood,
                    EffectedBad=item.EffectedBad,
                    Gene=xray
                };
                fitnessviewmodel.Add(fitnessview);
            }
            fitness.Data = (fitnessviewmodel).Take(PageSize);
             fitness.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)fitnessviewmodel.Count() / PageSize));

            return View(fitness);

        }
        public IActionResult AddFitnessTemplate()
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("ConsumptionType"));
            return View();

        }
        public IActionResult AddFitnessTemplates(AddFitnessViewModel model)
        {
            FitnessTemplate fitnesstemplatemodel = new FitnessTemplate();
            fitnesstemplatemodel.ConsumptionTypeID = model.ConsumptionTypeID;
            fitnesstemplatemodel.EffectedBad = model.EffectedBad;
            fitnesstemplatemodel.EffectedGood = model.EffectedGood;
            fitnesstemplatemodel.IsActive = true;
            fitnesstemplatemodel.CreatedOn = DateTime.UtcNow;
            fitnesstemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewfitnesstemplate = db.TemplateService.AddFitnessTemplate(fitnesstemplatemodel);
            var addnewfitnesstemplateid = addnewfitnesstemplate.ID;
            if (model.EffectedGeneFitness != null)
            {
                foreach (var item in model.EffectedGeneFitness)
                {
                    item.FitnessID = addnewfitnesstemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneFitnessList(model.EffectedGeneFitness);
            }
           
            return Json(true);
        }
        public IActionResult DeleteFitnessTemplate(int Id)
        {
            db.TemplateService.DeleteFitnessTemplate(Id);
            db.TemplateService.DeleteEffectedGeneFitness(Id);
            return Json(true);
        }
        public IActionResult UpdateFitnessTemplate(int Id)
        {
            var addfitnessviewmodel = new AddFitnessViewModel();
            var lookup = db.LookupService.GetLookups().ToList();
            var fitnesstemplate=   db.TemplateService.GetFitnessTemplateById(Id);
            var effectedgenelist = db.TemplateService.GetEffectedGeneFitnessListById(Id).Where(x => x.IsActive == true).ToList(); ;
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("ConsumptionType"));
            addfitnessviewmodel.ConsumptionTypeID = fitnesstemplate.ConsumptionTypeID;
            addfitnessviewmodel.EffectedBad = fitnesstemplate.EffectedBad;
            addfitnessviewmodel.EffectedGood = fitnesstemplate.EffectedGood;
            addfitnessviewmodel.EffectedGeneFitness = effectedgenelist;
            addfitnessviewmodel.ID = fitnesstemplate.ID;
            return View(addfitnessviewmodel);
        
        }
        [HttpPost]
        public IActionResult UpdateFitnessTemplates(AddFitnessViewModel model)
        {
            FitnessTemplate fitnesstemplatemodel = new FitnessTemplate();
            fitnesstemplatemodel.ID = model.ID;
            fitnesstemplatemodel.ConsumptionTypeID = model.ConsumptionTypeID;
            fitnesstemplatemodel.EffectedBad = model.EffectedBad;
            fitnesstemplatemodel.EffectedGood = model.EffectedGood;
            fitnesstemplatemodel.IsActive = true;
            fitnesstemplatemodel.CreatedOn = DateTime.UtcNow;
            fitnesstemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewfitnesstemplate = db.TemplateService.UpdateFitnessTemplate(fitnesstemplatemodel);
            db.TemplateService.DeleteEffectedGeneFitness(model.ID);
            var addnewfitnesstemplateid = addnewfitnesstemplate.ID;
            if (model.EffectedGeneFitness != null)
            {
                foreach (var item in model.EffectedGeneFitness)
                {
                    item.FitnessID = addnewfitnesstemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneFitnessList(model.EffectedGeneFitness);
            }
            return Json(true);
        }
        public IActionResult Getfitness(int page, string searchstring)
        {
            var fitness = new PagedData<FitnessTemplateViewModel>();
            var fitnessviewmodel = new List<FitnessTemplateViewModel>();
            var fitnessList = db.TemplateService.GetFitnessTemplate().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                fitnessList = fitnessList.Where(x => x.ConsumptionType != null && x.ConsumptionType.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || (x.EffectedBad != null && x.EffectedBad.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) || (x.EffectedGood != null && x.EffectedGood.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }

               foreach (var item in fitnessList)
              {
                var xray = db.TemplateService.GetEffectedGeneFitness(item.ID).Where(x => x.IsActive == true).ToList();
                var fitnessview = new FitnessTemplateViewModel()
                {
                    ID = item.ID,
                    ConsumptionTypeID = item.ConsumptionTypeID,
                    ConsumptionType = item.ConsumptionType,
                    EffectedGood = item.EffectedGood,
                    EffectedBad = item.EffectedBad,
                    Gene = xray
                };
                fitnessviewmodel.Add(fitnessview);
            }
            fitness.Data = (fitnessviewmodel).Skip(PageSize * (page - 1)).Take(PageSize);
            fitness.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)fitnessviewmodel.Count() / PageSize));
            return PartialView(fitness);

        }
        public IActionResult Fatconsumptiontemplate()
        {
            var fatconsumption = new PagedData<FatConsumptionTemplate>();
            var fatconsumptionList = db.TemplateService.GetFatConsumptionTemplate().Where(x => x.IsActive == true).ToList();
            fatconsumption.Data = (fatconsumptionList).Take(PageSize);
            fatconsumption.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)fatconsumptionList.Count() / PageSize));

            return View(fatconsumption);
        }
        public IActionResult AddFatConsumptionTemplate()
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("ConsumptionType"));

            var model = new FatConsumptionTemplate();
            return PartialView(model);

        }
        public IActionResult AddFatConsumptionTemplates(FatConsumptionTemplate model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var status = db.TemplateService.AddFatConsumption(model);
            return Json(true);
        }
        public IActionResult UpdateFatConsumptionTemplate(int Id)
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("ConsumptionType"));

            var data = db.TemplateService.GetFatConsumptionById(Id);
            return PartialView(data);
        }
        [HttpPost]
        public IActionResult UpdateFatConsumptionTemplates(FatConsumptionTemplate model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var status = db.TemplateService.UpdateFatConsumption(model);
            return Json(true);
        }
        public IActionResult DeleteFatConsumption(int Id)
        {
            db.TemplateService.DeleteFatConsumptionTemplate(Id);
            return Json(true);
        }
        public IActionResult GetFatConsumption(int page,string searchstring)
        {
            var fatconsumptiontemplate = new PagedData<FatConsumptionTemplate>();
            var fatconsumptiontemplateList = db.TemplateService.GetFatConsumptionTemplate().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                fatconsumptiontemplateList = fatconsumptiontemplateList.Where(x => x.EffectedGene != null && x.EffectedGene.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || (x.ConsumptionType != null && x.ConsumptionType.Name.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) || (x.Reccomendations != null && x.Reccomendations
                .Contains(searchstring, StringComparison.OrdinalIgnoreCase)) ).ToList();
            }
            fatconsumptiontemplate.Data = (fatconsumptiontemplateList).Skip(PageSize * (page - 1)).Take(PageSize);
            fatconsumptiontemplate.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)fatconsumptiontemplateList.Count() / PageSize));
            return PartialView(fatconsumptiontemplate);
        }
        public IActionResult Mineraltemplate()
        {
            var mineral = new PagedData<AddMineralViewModel>();
            var mineralviewmodel = new List<AddMineralViewModel>();
            var fitnessList = db.TemplateService.GetMineralTemplates().Where(x => x.IsActive == true).ToList();
            foreach (var item in fitnessList)
            {
                var xray = db.TemplateService.GetEffectedGeneMineralByMineralId(item.ID).Where(x => x.IsActive == true).ToList();
                var mineralview = new AddMineralViewModel()
                {
                    ID = item.ID,
                    ElementID = item.ElementID,
                    Element = item.Element,
                    Effect = item.Effect,
                    Recomendations = item.Recomendations,
                    EffectedGene = xray
                };
                mineralviewmodel.Add(mineralview);
            }
            mineral.Data = (mineralviewmodel).Take(PageSize);
            mineral.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)mineralviewmodel.Count() / PageSize));

            return View(mineral);
        }
        public IActionResult AddMineralTemplate()
        {
            var addmineralviewmodel = new AddMineralViewModel();
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.Element = lookup.Where(x => x.Type.Contains("Element"));
            return View(addmineralviewmodel);
        }
        [HttpPost]
        public IActionResult AddMineralTemplates(AddMineralViewModel model)
        {
            MineralTemplate mineraltemplatemodel = new MineralTemplate();
            mineraltemplatemodel.ElementID = model.ElementID;
            mineraltemplatemodel.Effect = model.Effect;
            mineraltemplatemodel.Recomendations = model.Recomendations;
            mineraltemplatemodel.IsActive = true;
            mineraltemplatemodel.CreatedOn = DateTime.UtcNow;
            mineraltemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewmineraltemplate = db.TemplateService.AddMineralTemplate(mineraltemplatemodel);
            var addnewmineraltemplateid = addnewmineraltemplate.ID;
            if (model.EffectedGene != null)
            {
                foreach (var item in model.EffectedGene)
                {
                    item.MineralID = addnewmineraltemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneMineralList(model.EffectedGene);
            }
            return Json(true);

        }
        public IActionResult deleteMineralTemplate(int Id)
        {
            db.TemplateService.DeleteMineralTemplate(Id);
            db.TemplateService.DeleteEffectedGeneMineral(Id);
            return Json(true);

        }
        public IActionResult UpdateMineralTemplate(int Id)
        {
            var addmineralviewmodel = new AddMineralViewModel();
            var lookup = db.LookupService.GetLookups().ToList();
            var mineraltemplate = db.TemplateService.GetMineralTemplateById(Id);
            var effectedgenelist = db.TemplateService.GetEffectedGeneMineralByMineralId(Id).Where(x => x.IsActive == true).ToList(); 

            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.Element = lookup.Where(x => x.Type.Contains("Element"));
            addmineralviewmodel.ElementID = mineraltemplate.ElementID;
            addmineralviewmodel.Recomendations = mineraltemplate.Recomendations;
            addmineralviewmodel.Effect= mineraltemplate.Effect;
            addmineralviewmodel.EffectedGene = effectedgenelist;
            addmineralviewmodel.ID = mineraltemplate.ID;
            return View(addmineralviewmodel);

        }
        [HttpPost]
        public IActionResult UpdateMineralTemplates(AddMineralViewModel model)
        {
            MineralTemplate mineraltemplatemodel = new MineralTemplate();
            mineraltemplatemodel.ID = model.ID;
            mineraltemplatemodel.ElementID= model.ElementID;
            mineraltemplatemodel.Recomendations = model.Recomendations;
            mineraltemplatemodel.Effect = model.Effect;
            mineraltemplatemodel.IsActive = true;
            mineraltemplatemodel.CreatedOn = DateTime.UtcNow;
            mineraltemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewmineraltemplate = db.TemplateService.UpdateMineralTemplate(mineraltemplatemodel);
            db.TemplateService.DeleteEffectedGeneMineral(model.ID);
            var addnewmineraltemplateid = addnewmineraltemplate.ID;
            if (model.EffectedGene !=null)
            {
                foreach (var item in model.EffectedGene)
                {
                    item.MineralID = addnewmineraltemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneMineralList(model.EffectedGene);
            }
            return Json(true);
        }
        public IActionResult GetMineral(int page,string searchstring)
        {
            var mineral = new PagedData<AddMineralViewModel>();
            var mineralviewmodel = new List<AddMineralViewModel>();
            var mineralList = db.TemplateService.GetMineralTemplates().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                mineralList = mineralList.Where(x => x.Element != null && x.Element.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || (x.Recomendations != null && x.Recomendations.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            foreach (var item in mineralList)
            {
                var xray = db.TemplateService.GetEffectedGeneMineralByMineralId(item.ID).Where(x => x.IsActive == true).ToList();
               
                var mineralview = new AddMineralViewModel()
                {
                    ID = item.ID,
                    ElementID = item.ElementID,
                    Element = item.Element,
                    Effect = item.Effect,
                    Recomendations = item.Recomendations,
                    EffectedGene = xray
                };
                mineralviewmodel.Add(mineralview);
            }
            mineral.Data = (mineralviewmodel).Skip(PageSize*(page-1)).Take(PageSize);
            mineral.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)mineralviewmodel.Count() / PageSize));

            return View(mineral);

        }
        public IActionResult Methyationtemplate()
        {
            var methayation = new PagedData<MethyationTemplate>();
            var methayationList = db.TemplateService.GetMethayationTemplate().Where(x => x.IsActive == true).ToList();
            methayation.Data = (methayationList).Take(PageSize);
            methayation.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)methayationList.Count() / PageSize));

            return View(methayation);
        }
        public IActionResult AddMethyationTemplate()
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            var model = new MethyationTemplate();
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult AddMethyationTemplates(MethyationTemplate model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var status = db.TemplateService.AddMethyationTemplate(model);
            return Json(true);
        }
        public IActionResult UpdateMethyationTemplate(int Id)
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            var model = db.TemplateService.GetMethyationTemplateById(Id);
            return PartialView(model);

        }
        [HttpPost]
        public IActionResult UpdateMethyationTemplates(MethyationTemplate model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            db.TemplateService.UpdateMethyationTemplate(model);
            return Json(true);
        }
        public IActionResult DeleteMethyationTemplate(int Id)
        {
            db.TemplateService.DeleteMetyationTemplate(Id);
            return Json(true);
        }
        public IActionResult GetMethyation(int Page,string searchstring)
        {
            var methayation = new PagedData<MethyationTemplate>();
            var methayationList = db.TemplateService.GetMethayationTemplate().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                methayationList = methayationList.Where(x => x.Gene != null && x.Gene.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || (x.Recommendations != null && x.Recommendations.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) || (x.ResultSummary != null && x.ResultSummary.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            methayation.Data = (methayationList).Skip(PageSize*(Page-1)).Take(PageSize);
            methayation.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)methayationList.Count() / PageSize));

            return View(methayation);
        }
        public IActionResult Diettemplate()
        {
            var diet = new PagedData<DietViewModel>();
            var dietviewmodel = new List<DietViewModel>();
            var dietList = db.TemplateService.GetDietTemplate().Where(x => x.IsActive == true).ToList();
            foreach (var item in dietList)
            {
                var xray = db.TemplateService.GetEffectedGeneDiet(item.ID).Where(x => x.IsActive == true).ToList();
                var dietview = new DietViewModel()
                {
                    ID = item.ID,
                    ConsumptionTypeID = item.ConsumptionTypeID,
                    ConsumptionType = item.ConsumptionType,
                    EffectedGood = item.EffectedGood,
                    EffectedBad = item.EffectedBad,
                    Gene = xray
                };
                dietviewmodel.Add(dietview);
            }
            diet.Data = (dietviewmodel).Take(PageSize);
            diet.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)dietviewmodel.Count() / PageSize));

            return View(diet);
        }
        public IActionResult AddDietTemplate()
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("ConsumptionType"));
            return View();
        }
        [HttpPost]
        public IActionResult AddDietTemplates(DietViewModel model)
        {
            DietTemplate diettemplatemodel = new DietTemplate();
            diettemplatemodel.ConsumptionTypeID = model.ConsumptionTypeID;
            diettemplatemodel.EffectedBad = model.EffectedBad;
            diettemplatemodel.EffectedGood = model.EffectedGood;
            diettemplatemodel.IsActive = true;
            diettemplatemodel.CreatedOn = DateTime.UtcNow;
            diettemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewdiettemplate = db.TemplateService.AddDietTemplate(diettemplatemodel);
            var addnewdiettemplateid = addnewdiettemplate.ID;
            if (model.Gene != null)
            {
                foreach (var item in model.Gene)
                {
                    item.DietID = addnewdiettemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneDietList(model.Gene);
            }
            return Json(true);
        }
        public IActionResult UpdateDietTemplate( int Id)
        {
            var adddietviewmodel = new DietViewModel();
            var lookup = db.LookupService.GetLookups().ToList();
            var diettemplate = db.TemplateService.GetDietTemplateById(Id);
            var effectedgenelist = db.TemplateService.GetEffectedGeneDietListById(Id).Where(x => x.IsActive == true).ToList(); ;
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("ConsumptionType"));
            adddietviewmodel.ConsumptionTypeID = diettemplate.ConsumptionTypeID;
            adddietviewmodel.EffectedBad = diettemplate.EffectedBad;
            adddietviewmodel.EffectedGood = diettemplate.EffectedGood;
            adddietviewmodel.Gene = effectedgenelist;
            adddietviewmodel.ID = diettemplate.ID;
            return View(adddietviewmodel);

        }
        [HttpPost]
        public IActionResult UpdateDietTemplates(DietViewModel model)
        {
            DietTemplate diettemplatemodel = new DietTemplate();
            diettemplatemodel.ID = model.ID;
            diettemplatemodel.ConsumptionTypeID = model.ConsumptionTypeID;
            diettemplatemodel.EffectedBad = model.EffectedBad;
            diettemplatemodel.EffectedGood = model.EffectedGood;
            diettemplatemodel.IsActive = true;
            diettemplatemodel.CreatedOn = DateTime.UtcNow;
            diettemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewdiettemplatemodel = db.TemplateService.UpdateDietTemplate(diettemplatemodel);
            db.TemplateService.DeleteEffectedGeneDiet(model.ID);
            var addnewdiettemplateid = addnewdiettemplatemodel.ID;
            if (model.Gene != null)
            {
                foreach (var item in model.Gene)
                {
                    item.DietID = addnewdiettemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneDietList(model.Gene);
            }
            return Json(true);
        }
        public IActionResult DeleteDietTemplate(int Id)
        {
            db.TemplateService.DeleteDietTemplate(Id);
            db.TemplateService.DeleteEffectedGeneDiet(Id);
            return Json(true);
        }
        public IActionResult GetDiet(int page, string searchstring)
        {
            var diet = new PagedData<DietViewModel>();
            var dietviewmodel = new List<DietViewModel>();
            var dietList = db.TemplateService.GetDietTemplate().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                dietList = dietList.Where(x => x.ConsumptionType != null && x.ConsumptionType.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || (x.EffectedBad != null && x.EffectedBad.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) || (x.EffectedGood != null && x.EffectedGood.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            foreach (var item in dietList)
            {
                var xray = db.TemplateService.GetEffectedGeneDiet(item.ID).Where(x => x.IsActive == true).ToList();
                var dietview = new DietViewModel()
                {
                    ID = item.ID,
                    ConsumptionTypeID = item.ConsumptionTypeID,
                    ConsumptionType = item.ConsumptionType,
                    EffectedGood = item.EffectedGood,
                    EffectedBad = item.EffectedBad,
                    Gene = xray
                };
                dietviewmodel.Add(dietview);
            }
            diet.Data = (dietviewmodel).Skip(PageSize * (page - 1)).Take(PageSize);
            diet.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)dietviewmodel.Count() / PageSize));
            return PartialView(diet);



        }


        ////Listing
        public IActionResult Metabolismtemplate()
        {
            var metabolism = new PagedData<MetabolismAddViewModel>();
            List<MetabolismAddViewModel> addMetabolismGeneViewModels = new List<MetabolismAddViewModel>();
            var metabolismtemplate = db.TemplateService.GetMetabolismTemplate().Where(x => x.IsActive == true).ToList();

            foreach (var item in metabolismtemplate)
            {
                var xray = db.TemplateService.GetEffectedGeneMetabolismById(item.ID).Where(x => x.IsActive == true).ToList(); ;
                var test = new MetabolismAddViewModel()

                {
                    ConsumptionTypeID = item.ConsumptionTypeID,
                    ConsumptionType = item.ConsumptionType,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn,
                    Effected = item.Effected,
                    Recomendation = item.Recomendation,
                    ID = item.ID,
                    EffectedGeneMetabolism = xray
                };

                addMetabolismGeneViewModels.Add(test);
            }

            metabolism.Data = (addMetabolismGeneViewModels).Take(PageSize);
            metabolism.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)addMetabolismGeneViewModels.Count() / PageSize));
            return View(metabolism);
        }
        //public IActionResult GetMetabolismList()
        //{

        //    var metabolism = new PagedData<MetabolismAddViewModel>();
        //    var metabolismList = db.TemplateService.GetMetabolismTemplate().Where(x => x.IsActive == true).ToList();
        //    metabolism.Data = (metabolismList).Take(PageSize);
        //    metabolism.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)metabolismList.Count() / PageSize));
        //    //var metabolismEffectiveGeneList = db.TemplateService.

        //    return View(metabolism);
        //}


        /////For Add

        public IActionResult AddMetabolismTemplate()
        {
            var metabolismtemplate = db.TemplateService.GetMetabolismTemplate().Where(x => x.IsActive == true).ToList();
            var lookups = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookups.Where(x => x.Type.Contains("Gene")).ToList();
            ViewBag.ConsumptionType = lookups.Where(x => x.Type.Contains("ConsumptionType")).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddMetabolismTemplate(MetabolismAddViewModel metabolismAddViewModel)
        {
            MetabolismTemplate metabolism = new MetabolismTemplate();
            metabolism.IsActive = true;
            metabolism.Effected = metabolismAddViewModel.Effected;
            metabolism.ConsumptionTypeID = metabolismAddViewModel.ConsumptionTypeID;
            metabolism.ConsumptionType = metabolismAddViewModel.ConsumptionType;
            metabolism.CreatedOn = DateTime.UtcNow;
            metabolism.UpdatedOn = DateTime.UtcNow;
            metabolism.Recomendation = metabolismAddViewModel.Recomendation;
            var metabolismTemplateData = db.TemplateService.Add(metabolism);

            if (metabolismAddViewModel.EffectedGeneMetabolism != null)
            {
                List<EffectedGeneMetabolism> effectedGeneMetabolisms = metabolismAddViewModel.EffectedGeneMetabolism.Select(x => new EffectedGeneMetabolism
                {
                    ID = 0,
                    GeneID = x.GeneID,
                    MetabolismID = metabolismTemplateData.ID,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    IsActive = true
                }).ToList();

                db.TemplateService.InsertEffectedGeneMetabolism(effectedGeneMetabolisms);

            }
            return Json(true);
        }


        ////// For Delete
        public IActionResult DeleteMetabolismTemplate(long id)
        {
            db.TemplateService.DeleteMetabolismTemplate(id);
            db.TemplateService.DeleteEffectedGeneMetabolism(id);
            return Json(true);
        }



        ////// For Edit

        public IActionResult EditMetabolismTemplate(long id)
        {
            var metabolismModel = new MetabolismAddViewModel();
            var lookupList = db.LookupService.GetLookups().ToList();
            var getMetabolismData = db.TemplateService.GetByIdMetabolismTemplate(id);
            var getEffectedGeneMetabolismData = db.TemplateService.GetEffectedGeneMetabolismById(id).Where(x => x.IsActive == true).ToList();
            ViewBag.Gene = lookupList.Where(x => x.Type.Contains("Gene")).ToList();
            ViewBag.ConsumptionType = lookupList.Where(x => x.Type.Contains("ConsumptionType")).ToList();
            metabolismModel.ConsumptionTypeID = getMetabolismData.ConsumptionTypeID;
            metabolismModel.Effected = getMetabolismData.Effected;
            metabolismModel.Recomendation = getMetabolismData.Recomendation;
            metabolismModel.CreatedOn = getMetabolismData.CreatedOn;
            metabolismModel.UpdatedOn = getMetabolismData.UpdatedOn;
            metabolismModel.EffectedGeneMetabolism = getEffectedGeneMetabolismData;


            return View(metabolismModel);
        }



        [HttpPost]
        public IActionResult EditMetabolismTemplate(MetabolismAddViewModel metabolismAddViewModel)
        {
            MetabolismTemplate metabolismTemplate = new MetabolismTemplate();
            metabolismTemplate.ConsumptionTypeID = metabolismAddViewModel.ConsumptionTypeID;
            metabolismTemplate.IsActive = true;
            metabolismTemplate.CreatedOn = DateTime.UtcNow;
            metabolismTemplate.UpdatedOn = DateTime.UtcNow;
            metabolismTemplate.Effected = metabolismAddViewModel.Effected;
            metabolismTemplate.Recomendation = metabolismAddViewModel.Recomendation;
            metabolismTemplate.ID = metabolismAddViewModel.ID;
            var metabolismTemplateData = db.TemplateService.UpdateMetabolismTemplate(metabolismTemplate);
            var metabolismTemplateDataID = metabolismTemplateData.ID;
            db.TemplateService.DeleteEffectedGeneMetabolism(metabolismAddViewModel.ID);
            if (metabolismAddViewModel.EffectedGeneMetabolism != null)
            {

                foreach (var item in metabolismAddViewModel.EffectedGeneMetabolism)
                {
                    item.MetabolismID = metabolismTemplateDataID;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;

                }
                db.TemplateService.InsertEffectedGeneMetabolism(metabolismAddViewModel.EffectedGeneMetabolism);

            }

            return Json(true);
        }


        public IActionResult GetMetabolismList(string searchstring, int page)
        {

            var metabolism = new PagedData<MetabolismAddViewModel>();
            List<MetabolismAddViewModel> addMetabolismGeneViewModels = new List<MetabolismAddViewModel>();
            var metabolismtemplate = db.TemplateService.GetMetabolismTemplate().Where(x => x.IsActive == true).ToList();

            if (!string.IsNullOrEmpty(searchstring))
            {
                metabolismtemplate = metabolismtemplate.Where(x => x.ConsumptionType != null && x.ConsumptionType.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || x.Recomendation != null && x.Recomendation.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || x.ConsumptionType != null && x.ConsumptionType.Type.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
            }

            foreach (var item in metabolismtemplate)
            {
                var xray = db.TemplateService.GetEffectedGeneMetabolismById(item.ID).Where(x => x.IsActive == true).ToList(); ;
                var test = new MetabolismAddViewModel()

                {
                    ConsumptionTypeID = item.ConsumptionTypeID,
                    ConsumptionType = item.ConsumptionType,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn,
                    Effected = item.Effected,
                    Recomendation = item.Recomendation,
                    ID = item.ID,
                    EffectedGeneMetabolism = xray
                };

                addMetabolismGeneViewModels.Add(test);
            }

            metabolism.Data = (addMetabolismGeneViewModels).Skip(PageSize * (page - 1)).Take(PageSize);
            metabolism.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)addMetabolismGeneViewModels.Count() / PageSize));
            return PartialView(metabolism);
        }


        /////////// For Prostate Template
        [HttpGet]
        public IActionResult ProstateTemplate()
        {
            var prostate = new PagedData<ProstateTemplate>();
            var prostateList = db.TemplateService.GetProstateTemplateList().Where(x => x.IsActive == true).ToList();
            prostate.Data = (prostateList).Take(PageSize);
            prostate.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)prostateList.Count() / PageSize));

            return View(prostate);
        }

        //For Searching
        [HttpGet]
        public IActionResult GetProstateTemplate(int page, string searchstring)
        {
            var prostateTemplate = new PagedData<ProstateTemplate>();
            var prostateTemplateList = db.TemplateService.GetProstateTemplateList().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                prostateTemplateList = prostateTemplateList.Where(x => x.Gene != null && x.Gene.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || x.GeneticMutation != null && x.GeneticMutation.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || x.PersonalRiskFactor != null && x.PersonalRiskFactor.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || x.Hereditary != null && x.Hereditary.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || x.Recommendations != null && x.Recommendations.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
            }
            prostateTemplate.Data = (prostateTemplateList).Skip(PageSize * (page - 1)).Take(PageSize);
            prostateTemplate.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)prostateTemplateList.Count() / PageSize));

            return PartialView(prostateTemplate);

        }
        //For Add
        [HttpGet]
        public IActionResult AddProstateTemplate()
        {
            var lookup = db.LookupService.GetLookups().ToList();
            var addProstateTemplate = db.TemplateService.GetProstateTemplateList().Where(x => x.IsActive == true).ToList();
            foreach (var item in addProstateTemplate)
            {
                var xray = lookup.Where(x => x.ID != item.GeneID).ToList();
                lookup = xray;
            }
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            var model = new ProstateTemplate();
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult AddProstateTemplate(ProstateTemplate prostateTemplate)
        {
            prostateTemplate.IsActive = true;
            prostateTemplate.CreatedOn = DateTime.UtcNow;
            prostateTemplate.UpdatedOn = DateTime.UtcNow;
            var test = db.TemplateService.AddProstateTemplate(prostateTemplate);
            return Json(true);
        }

        //For Edit
        [HttpGet]
        public IActionResult EditProstateTemplate(long id)
        {
            var data = db.TemplateService.GetProstateTemplateById(id);
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));

            return PartialView(data);
        }
        [HttpPost]
        public IActionResult EditProstateTemplate(ProstateTemplate prostateTemplate)
        {
            prostateTemplate.IsActive = true;
            prostateTemplate.CreatedOn = DateTime.UtcNow;
            prostateTemplate.UpdatedOn = DateTime.UtcNow;
            db.TemplateService.UpdateProstateTemplate(prostateTemplate);
            return Json(true);
        }

        ///For Delete
        public IActionResult DeleteProstateTemplate(int id)
        {
            db.TemplateService.DeleteProstateTemplate(id);
            return Json(true);
        }

        public IActionResult GeneTemplateLookup()
        {

            return View();
        }
        public IActionResult Vitamintemplate()
        {

            var vitamin = new PagedData<VitaminViewModel>();
            var vitaminviewmodel = new List<VitaminViewModel>();
            var fitnessList = db.TemplateService.GetVitaminTemplate().Where(x => x.IsActive == true).ToList();
            foreach (var item in fitnessList)
            {
                var xray = db.TemplateService.GetEffectedGeneVitamin(item.ID).Where(x => x.IsActive == true).ToList();
                var vitaminview = new VitaminViewModel()
                {
                    ID = item.ID,
                    FeederTypeID = item.FeederTypeID,
                    FeederType = item.FeederType,
                    Recommendations = item.Recommendations,
                   Effect  = item.Effect,
                   EffectedGeneVitamins = xray
                };
                vitaminviewmodel.Add(vitaminview);
            }
            vitamin.Data = (vitaminviewmodel).Take(PageSize);
            vitamin.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)vitaminviewmodel.Count() / PageSize));

            return View(vitamin);

        }
        public IActionResult AddVitaminTemplate()
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("FeederType"));
            return View();
        }
        [HttpPost]
        public IActionResult AddVitaminTemplates(VitaminViewModel model)
        {
            VitaminTemplate vitamintemplatemodel = new VitaminTemplate();
            vitamintemplatemodel.FeederTypeID = model.FeederTypeID;
            vitamintemplatemodel.Effect = model.Effect;
            vitamintemplatemodel.Recommendations = model.Recommendations;
            vitamintemplatemodel.IsActive = true;
            vitamintemplatemodel.CreatedOn = DateTime.UtcNow;
            vitamintemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewvitaminemplate = db.TemplateService.AddVitaminTemplate(vitamintemplatemodel);
            var addnewvitamintemplateid = addnewvitaminemplate.ID;
            if (model.EffectedGeneVitamins != null)
            {
                foreach (var item in model.EffectedGeneVitamins)
                {
                    item.VitaminID = addnewvitamintemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneVitaminList(model.EffectedGeneVitamins);
            }

            return Json(true);
        }
        public IActionResult UpdateVitaminTemplate(int Id)
        {
            var vitaminviewmodel = new VitaminViewModel();
            var lookup = db.LookupService.GetLookups().ToList();
            var fitnesstemplate = db.TemplateService.GetVitaminTemplateById(Id);
            var effectedgenelist = db.TemplateService.GetEffectedGeneVitamin(Id).Where(x => x.IsActive == true).ToList(); ;
            ViewBag.Gene = lookup.Where(x => x.Type.Contains("Gene"));
            ViewBag.ConsumptionType = lookup.Where(x => x.Type.Contains("FeederType"));
            vitaminviewmodel.FeederTypeID = fitnesstemplate.FeederTypeID;
            vitaminviewmodel.Effect = fitnesstemplate.Effect;
            vitaminviewmodel.Recommendations = fitnesstemplate.Recommendations
                ;
            vitaminviewmodel.EffectedGeneVitamins = effectedgenelist;
            vitaminviewmodel.ID = fitnesstemplate.ID;
            return View(vitaminviewmodel);
        }
        [HttpPost]
        public IActionResult UpdateVitaminTemplates(VitaminViewModel model)
        {
            var vitamintemplatemodel = new VitaminTemplate();
            vitamintemplatemodel.ID = model.ID;
            vitamintemplatemodel.FeederTypeID = model.FeederTypeID;
            vitamintemplatemodel.Effect = model.Effect;
            vitamintemplatemodel.Recommendations = model.Recommendations;
            vitamintemplatemodel.IsActive = true;
            vitamintemplatemodel.CreatedOn = DateTime.UtcNow;
            vitamintemplatemodel.UpdatedOn = DateTime.UtcNow;
            var addnewvitamintemplate = db.TemplateService.UpdateVitaminTemplate(vitamintemplatemodel);
            db.TemplateService.DeleteEffectedGeneVitamin(model.ID);
            var addnewfitnesstemplateid = addnewvitamintemplate.ID;
            if (model.EffectedGeneVitamins != null)
            {
                foreach (var item in model.EffectedGeneVitamins)
                {
                    item.VitaminID = addnewfitnesstemplateid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;
                }
                db.TemplateService.InsertEffectedGeneVitaminList(model.EffectedGeneVitamins);
            }
            return Json(true);
        }
        public IActionResult DeleteVitaminTemplate(int Id)
        {
            db.TemplateService.DeleteVitaminTemplate(Id);
            db.TemplateService.DeleteEffectedGeneVitamin(Id);
            return Json(true);
        }
        public IActionResult GetVitamin(int page, string searchstring)
        {
            var vitamin = new PagedData<VitaminViewModel>();
            var vitaminviewmodel = new List<VitaminViewModel>();
            var fitnessList = db.TemplateService.GetVitaminTemplate().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                fitnessList = fitnessList.Where(x => x.FeederType != null && x.FeederType.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) || (x.Recommendations != null && x.Recommendations.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            foreach (var item in fitnessList)
            {
                var xray = db.TemplateService.GetEffectedGeneVitamin(item.ID).Where(x => x.IsActive == true).ToList();

                var vitaminview = new VitaminViewModel()
                {
                    ID = item.ID,
                    FeederTypeID = item.FeederTypeID,
                    FeederType = item.FeederType,
                    Recommendations = item.Recommendations,
                    Effect = item.Effect,
                    EffectedGeneVitamins = xray
                };
                vitaminviewmodel.Add(vitaminview);
            }
            vitamin.Data = (vitaminviewmodel).Skip(PageSize*(page-1)).Take(PageSize);
            vitamin.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)vitaminviewmodel.Count() / PageSize));
            return PartialView(vitamin);

        }


    }
}