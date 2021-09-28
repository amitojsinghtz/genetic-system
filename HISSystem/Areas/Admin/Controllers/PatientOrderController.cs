using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using Data.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class PatientOrderController : Controller
    {
        public const int PageSize = 50;
        private readonly IUnitOfService db;
        public PatientOrderController(IUnitOfService db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var orderList = new PagedData<PatientOrder>();

            var getPatientOrderList = db.PatientOrderService.GetPatientOrder().ToList();

            orderList.Data = getPatientOrderList;
            orderList.Data = (getPatientOrderList).Take(PageSize);
            orderList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)getPatientOrderList.Count() / PageSize));
            return View(orderList);
        }
        public IActionResult AddPatientOrder()
        {

            ViewBag.Patients = db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();
            ViewBag.Doctors = db.UserService.GetByRole(3);
            return View();

        }
        public IActionResult getpatientDetails(int Id)
        {
            var result = new User();
            if (Id != 0)
            {
                result = db.UserService.GetPatient(Id);
                return Json(result);

            }
            return Json(result);
        }
        public IActionResult GetTemplateDetails(int type, bool preFillData)
        {
            ViewBag.EffectedTypes = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };
            ViewBag.Date = DateTime.UtcNow;
            var getThis = preFillData;

            switch(preFillData)
            {
                case true:
                    if (type == 2)
                    {

                        var test = db.TemplateService.GetEpilepsyTemplate().Where(x => x.IsActive == true).ToList();
                        var lookup = db.LookupService.GetLookups().ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));

                        var result = new OrderEpilepsyViewModel();

                        result.epilepsylist = test;
                        result.Gene = genetype;
                        ViewBag.EffectedGenes = lookup.Where(x => x.Type == "Gene");
                        //return Json(result);
                        return PartialView("EpilepsyTemplate", result);
                    }
                    else if (type == 3)
                    {
                        var lookup = db.LookupService.GetLookups().ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));
                        var consumptiontype = lookup.Where(x => x.Type.Contains("ConsumptionType"));
                        var test = db.TemplateService.GetFatConsumptionTemplate().Where(x => x.IsActive == true).ToList();
                        ViewBag.EffectedGenes = genetype;
                        ViewBag.ConsumptionType = consumptiontype;

                        var result = new FatConsumptionViewModel();
                        result.ConsumptionType = consumptiontype;
                        result.fatconsumptionlist = test;
                        result.Gene = genetype;
                        return PartialView("FatConsumptionTemplate", result);
                    }
                    else if (type == 4)
                    {
                        var lookup = db.LookupService.GetLookups().ToList();
                        var test = db.TemplateService.GetMethayationTemplate().Where(x => x.IsActive == true).ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));
                        var result = new OrderMethyationViewModel();
                        result.methyationlist = test;
                        result.Gene = genetype;
                        ViewBag.EffectedGenes = lookup.Where(x => x.Type == "Gene");

                        ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };
                        //return Json(result);
                        return PartialView("MethyationTemplate", result);
                    }
                    else if (type == 5)
                    {

                        var test = db.TemplateService.GetProstateTemplateList().Where(x => x.IsActive == true).ToList();
                        var lookup = db.LookupService.GetLookups().ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));

                        var result = new OrderProstateViiewModel();
                        result.prostatelist = test;
                        result.Gene = genetype;
                        ViewBag.EffectedGenes = lookup.Where(x => x.Type == "Gene");
                        //return Json(result);
                        return PartialView("ProstateTemplate", result);
                    }
                    else if (type == 1)
                    {
                        var lookupList = db.LookupService.GetLookups().Where(x => x.IsActive == true).ToList();

                        ViewBag.ConsumptionTypes = lookupList.Where(x => x.Type == "ConsumptionType");
                        ViewBag.Elements = lookupList.Where(x => x.Type == "Element");
                        ViewBag.EffectedGenes = lookupList.Where(x => x.Type == "Gene");
                        ViewBag.FeederType = lookupList.Where(x => x.Type == "FeederType");

                        GeneralTemplateViewModel GeneralTemplateViewModel = new GeneralTemplateViewModel
                        {
                            MetabolismTemplates = db.TemplateService.GetMetabolismTemplate().Where(x => x.IsActive == true).ToList(),
                            MineralTemplates = db.TemplateService.GetMineralTemplates().Where(x => x.IsActive == true).ToList(),
                            DietTemplates = db.TemplateService.GetDietTemplate().Where(x => x.IsActive == true).ToList(),
                            FitnessTemplates = db.TemplateService.GetFitnessTemplate().Where(x => x.IsActive == true).ToList(),
                            VitaminTemplates = db.TemplateService.GetVitaminTemplate().Where(x => x.IsActive == true).ToList()
                        };

                        return PartialView("GeneralTemplate", GeneralTemplateViewModel);

                    }
                    break;

                case false:
                    if (type == 2)
                    {

                        var lookup = db.LookupService.GetLookups().ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));

                        var result = new OrderEpilepsyViewModel();

                        result.Gene = genetype;
                        ViewBag.EffectedGenes = lookup.Where(x => x.Type == "Gene");
                        //return Json(result);
                        return PartialView("EpilepsyTemplate", result);
                    }
                    else if (type == 3)
                    {
                        var lookup = db.LookupService.GetLookups().ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));
                        var consumptiontype = lookup.Where(x => x.Type.Contains("ConsumptionType"));
                        ViewBag.EffectedGenes = genetype;
                        ViewBag.ConsumptionType = consumptiontype;

                        var result = new FatConsumptionViewModel();
                        result.ConsumptionType = consumptiontype;
                        result.Gene = genetype;
                        return PartialView("FatConsumptionTemplate", result);
                    }
                    else if (type == 4)
                    {
                        var lookup = db.LookupService.GetLookups().ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));

                        var result = new OrderMethyationViewModel();
                        result.Gene = genetype;
                        ViewBag.EffectedGenes = lookup.Where(x => x.Type == "Gene");

                        ViewBag.Result = new List<SelectListItem>
                        {
                            new SelectListItem{ Text="High", Value = "1" },
                            new SelectListItem{ Text="Medium", Value = "2" },
                            new SelectListItem{ Text="Low", Value = "3" }
                        };
                        //return Json(result);
                        return PartialView("MethyationTemplate", result);
                    }
                    else if (type == 5)
                    {

                        var lookup = db.LookupService.GetLookups().ToList();
                        var genetype = lookup.Where(x => x.Type.Contains("Gene"));

                        var result = new OrderProstateViiewModel();
                        result.Gene = genetype;
                        ViewBag.EffectedGenes = lookup.Where(x => x.Type == "Gene");
                        //return Json(result);
                        return PartialView("ProstateTemplate", result);
                    }
                    else if (type == 1)
                    {
                        var lookupList = db.LookupService.GetLookups().Where(x => x.IsActive == true).ToList();

                        ViewBag.ConsumptionTypes = lookupList.Where(x => x.Type == "ConsumptionType");
                        ViewBag.Elements = lookupList.Where(x => x.Type == "Element");
                        ViewBag.EffectedGenes = lookupList.Where(x => x.Type == "Gene");
                        ViewBag.FeederType = lookupList.Where(x => x.Type == "FeederType");

                        GeneralTemplateViewModel GeneralTemplateViewModel = new GeneralTemplateViewModel();
                        return PartialView("GeneralTemplate", GeneralTemplateViewModel);

                    }
                    break;
            }


           
            var test1 = "";
            return Json(test1);
        }
        [HttpPost]
        public IActionResult AddPatientOrders(AddOrderViewModel model)
        {
            var patientorder = new PatientOrder();
            var orderno = 0;
            var dateNow = DateTime.UtcNow;
            var getLastOrderNo = db.PatientOrderService.GetPatientOrderNo();
            if (getLastOrderNo == null || getLastOrderNo.OrderNo == 0)
            {
                orderno = 7001;
            }
            else
            {
                orderno = getLastOrderNo.OrderNo + 1;
            }
            patientorder.DoctorID = model.DoctorID;
            patientorder.PatientID = model.PatientID;
            patientorder.OrderDate = model.OrderDate;
            patientorder.GeneTemplateID = model.GeneTemplateID;
            patientorder.FollowUpNeeded = model.FollowUpNeeded;
            patientorder.Summary = model.Summary;
            patientorder.CreatedOn = DateTime.UtcNow;
            patientorder.UpdatedOn = DateTime.UtcNow;
            patientorder.OrderNo = orderno;
            patientorder.IsActive = true;
            var addedorder = db.PatientOrderService.AddOrder(patientorder);
            var orderid = addedorder.ID;

            if (model.GeneTemplateID == 1)
            {
                var mineralList = model.GeneralTemplates.MineralTemplates.Select(x => new PatientOrderMineral
                {
                    OrderID = orderid,
                    CreatedOn = dateNow,
                    UpdatedOn = dateNow,
                    IsActive = true,
                    Effect = x.Effect,
                    ElementID = x.ElementID,
                    Recomendations = x.Recomendations,
                    PatientOrderEffectedGeneMineralList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneMineral
                    {
                        GeneID = int.Parse(p),
                        CreatedOn = dateNow,
                        UpdatedOn = dateNow,
                        IsActive = true
                    }).ToList()
                }).ToList();

                db.PatientOrderService.AddPatientOrderMineralList(mineralList);

                var metabolismList = model.GeneralTemplates.MetabolismTemplates.Select(x => new PatientOrderMetabolism
                {
                    OrderID = orderid,
                    CreatedOn = dateNow,
                    UpdatedOn = dateNow,
                    IsActive = true,
                    Effected = x.Effected,
                    ConsumptionTypeID = x.ConsumptionTypeID,
                    Recomendation = x.Recomendation,
                    PatientOrderEffectedGeneMetabolismList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneMetabolism
                    {
                        GeneID = int.Parse(p),
                        CreatedOn = dateNow,
                        UpdatedOn = dateNow,
                        IsActive = true
                    }).ToList()
                }).ToList();

                db.PatientOrderService.AddPatientOrderMetabolismList(metabolismList);

                var fitnessList = model.GeneralTemplates.FitnessTemplates.Select(x => new PatientOrderFitness
                {
                    OrderID = orderid,
                    CreatedOn = dateNow,
                    UpdatedOn = dateNow,
                    IsActive = true,
                    EffectedGood = x.EffectedGood,
                    EffectedBad = x.EffectedBad,
                    ConsumptionTypeID = x.ConsumptionTypeID,
                    PatientOrderEffectedGeneFitnessList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneFitness
                    {
                        GeneID = int.Parse(p),
                        CreatedOn = dateNow,
                        UpdatedOn = dateNow,
                        IsActive = true
                    }).ToList()
                }).ToList();

                db.PatientOrderService.AddPatientOrderFitnessList(fitnessList);

                var dietList = model.GeneralTemplates.DietTemplates.Select(x => new PatientOrderDiet
                {
                    OrderID = orderid,
                    CreatedOn = dateNow,
                    UpdatedOn = dateNow,
                    IsActive = true,
                    EffectedGood = x.EffectedGood,
                    EffectedBad = x.EffectedBad,
                    ConsumptionTypeID = x.ConsumptionTypeID,
                    PatientOrderEffectedGeneDietList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneDiet
                    {
                        GeneID = int.Parse(p),
                        CreatedOn = dateNow,
                        UpdatedOn = dateNow,
                        IsActive = true
                    }).ToList()
                }).ToList();

                db.PatientOrderService.AddPatientOrderDietList(dietList);

                var vitaminList = model.GeneralTemplates.VitaminTemplates.Select(x => new PatientOrderVitamin
                {
                    OrderID = orderid,
                    CreatedOn = dateNow,
                    UpdatedOn = dateNow,
                    IsActive = true,
                    FeederTypeID = x.FeederTypeID,
                    Effect = x.Effect,
                    Recommendations = x.Recommendations,
                    PatientOrderEffectedGeneVitaminList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneVitamin
                    {
                        GeneID = int.Parse(p),
                        CreatedOn = dateNow,
                        UpdatedOn = dateNow,
                        IsActive = true
                    }).ToList()
                }).ToList();

                db.PatientOrderService.AddPatientOrderVitaminList(vitaminList);

            }
            else if (model.GeneTemplateID == 2)
            {
                model.PatientOrderEpilepsyList.ForEach(x => { x.IsActive = true; x.OrderID = orderid; x.CreatedOn = dateNow; x.UpdatedOn = dateNow; });

                db.PatientOrderService.AddPatientOrderEpilepsyList(model.PatientOrderEpilepsyList);
            }
            else if (model.GeneTemplateID == 3)
            {
                foreach (var item in model.PatientOrderFatConsumptionList)
                {
                    item.OrderID = orderid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;

                }
                db.PatientOrderService.AddPatientOrderFatConsumptionList(model.PatientOrderFatConsumptionList);
            }
            else if (model.GeneTemplateID == 4)
            {
                foreach (var item in model.PatientOrderMethyationList)
                {
                    item.OrderID = orderid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;

                }
                db.PatientOrderService.AddPatientOrderMethyationList(model.PatientOrderMethyationList);
            }
            else if (model.GeneTemplateID == 5)
            {
                foreach (var item in model.PatientOrderProstateList)
                {
                    item.OrderID = orderid;
                    item.IsActive = true;
                    item.CreatedOn = DateTime.UtcNow;
                    item.UpdatedOn = DateTime.UtcNow;

                }
                db.PatientOrderService.AddPatientOrderProstateList(model.PatientOrderProstateList);
            }

            return Json(true);
        }

        public IActionResult GetOrder()
        {
            var orderList = new PagedData<PatientOrder>();

            var getPatientOrderList = db.PatientOrderService.GetPatientOrder().ToList();

            orderList.Data = getPatientOrderList;
            orderList.Data = (getPatientOrderList).Take(PageSize);
            orderList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)getPatientOrderList.Count() / PageSize));
            return View(orderList);
        }

        public IActionResult UpdateOrder(int Id)
        {
            var addorderviewmodel = new AddOrderViewModel();
            var orderlist = db.PatientOrderService.GetPatientOrderById(Id);
            addorderviewmodel.ID = orderlist.ID;
            addorderviewmodel.OrderNo = orderlist.OrderNo;
            addorderviewmodel.PatientID = orderlist.PatientID;
            addorderviewmodel.DoctorID = orderlist.DoctorID;
            addorderviewmodel.FollowUpNeeded = orderlist.FollowUpNeeded;
            addorderviewmodel.GeneTemplateID = orderlist.GeneTemplateID;
            addorderviewmodel.OrderDate = orderlist.OrderDate;
            addorderviewmodel.DueDate = orderlist.DueDate;
            addorderviewmodel.Summary = orderlist.Summary;
            addorderviewmodel.User = orderlist.Patient;


            switch(orderlist.GeneTemplateID)
            {
                case 1:
                    var tempGeneralData = db.PatientOrderService.GetGeneralOrderData(Id);
                    addorderviewmodel.PatientOrderGeneralList = new PatientOrderGeneral
                    {
                        PatientOrderMineralList = tempGeneralData.PatientOrderMineralList,
                        PatientOrderMetabolismList = tempGeneralData.PatientOrderMetabolismList,
                        PatientOrderDietList = tempGeneralData.PatientOrderDietList,
                        PatientOrderFitnessList = tempGeneralData.PatientOrderFitnessList,
                        PatientOrderVitaminList = tempGeneralData.PatientOrderVitaminList,
                    };
                    addorderviewmodel.GeneTemplateID = 1;
                    break;
                case 2:
                    var orderepilepsyList = db.PatientOrderService.GetPatientOrderEpilepsyById(Id);
                    addorderviewmodel.PatientOrderEpilepsyList = orderepilepsyList;
                    addorderviewmodel.GeneTemplateID = 2;
                    break;
                case 3:
                    var orderfatconsumptionList = db.PatientOrderService.GetPatientOrderFatConsumptionById(Id);
                    addorderviewmodel.PatientOrderFatConsumptionList = orderfatconsumptionList;
                    addorderviewmodel.GeneTemplateID = 3;
                    break;
                case 4:
                    var ordermethyationList = db.PatientOrderService.GetPatientOrderMethyationById(Id);
                    addorderviewmodel.PatientOrderMethyationList = ordermethyationList;
                    addorderviewmodel.GeneTemplateID = 4;
                    break;
                case 5:
                    var orderprostateList = db.PatientOrderService.GetPatientOrderProstateById(Id);
                    addorderviewmodel.PatientOrderProstateList = orderprostateList;
                    addorderviewmodel.GeneTemplateID = 5;
                    break;
            }

            ViewBag.Patients = db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();
            ViewBag.Doctors = db.UserService.GetByRole(3);
            var lookupList = db.LookupService.GetLookups().Where(x => x.IsActive == true).ToList();
            ViewBag.Gene = lookupList.Where(x => x.Type.Contains("Gene"));
            ViewBag.FatConsumptionType = lookupList.Where(x => x.Type.Contains("FatConsumptionType"));
            ViewBag.ConsumptionTypes = lookupList.Where(x => x.Type == "ConsumptionType");
            ViewBag.Elements = lookupList.Where(x => x.Type == "Element");
            ViewBag.EffectedGenes = lookupList.Where(x => x.Type == "Gene");
            ViewBag.FeederType = lookupList.Where(x => x.Type == "FeederType");
            ViewBag.Date = DateTime.UtcNow;
            ViewBag.EffectedTypes = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };


            return View(addorderviewmodel);
        }
        [HttpPost]
        public IActionResult UpdatePatientOrders(AddOrderViewModel model)
        {
            var dateNow = DateTime.UtcNow;
            var patientorder = db.PatientOrderService.GetPatientOrderById(model.ID);
            bool checkOrderType = patientorder.GeneTemplateID == model.GeneTemplateID;
            var getOrderID = model.ID;
            patientorder.GeneTemplateID = model.GeneTemplateID;
            
            if (checkOrderType)
            {
                switch (model.GeneTemplateID)
                {
                    case 1://General ORDER
                        List<PatientOrderFitness> patientorderFitnessTest = new List<PatientOrderFitness>();
                        foreach(var item in model.PatientOrderGeneralList.PatientOrderFitnessList)
                        {
                            var tempList = db.PatientOrderService.GetPatientOrderEffectedGeneFitnesses(item.ID).ToList();

                            int[] parsedInt = Array.ConvertAll(item.EffectedGenes, s => int.Parse(s));
                            
                            //all are still there
                            var stillExistArray = tempList.Where(x => parsedInt.Contains(x.GeneID)).ToList();
                            //all that are new
                            int[] newGeneArray = parsedInt.Where(x => !tempList.Any(y => y.GeneID == x)).ToArray();
                            //all that are removed
                            var deletedGeneArray = tempList.Except(stillExistArray).ToList();
                            deletedGeneArray.ForEach(x => x.IsActive = false);

                            var newGeneList = newGeneArray.Select((p, i) => new PatientOrderEffectedGeneFitness
                            {
                                GeneID = p,
                                OrderFitnessID = item.ID,
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList();

                            item.PatientOrderEffectedGeneFitnessList = newGeneList.Union(stillExistArray).Union(deletedGeneArray).ToList();
                            patientorderFitnessTest.Add(item);
                        }
                        db.PatientOrderService.UpdatePatientOrderFitnessList(patientorderFitnessTest);

                        List<PatientOrderMineral> patientOrderMineralTemp = new List<PatientOrderMineral>();
                        foreach (var item in model.PatientOrderGeneralList.PatientOrderMineralList)
                        {
                            var tempList = db.PatientOrderService.GetPatientOrderEffectedGeneMineral(item.ID).ToList();

                            int[] parsedInt = Array.ConvertAll(item.EffectedGenes, s => int.Parse(s));

                            //all are still there
                            var stillExistArray = tempList.Where(x => parsedInt.Contains(x.GeneID)).ToList();
                            //all that are new
                            int[] newGeneArray = parsedInt.Where(x => !tempList.Any(y => y.GeneID == x)).ToArray();
                            //all that are removed
                            var deletedGeneArray = tempList.Except(stillExistArray).ToList();
                            deletedGeneArray.ForEach(x => x.IsActive = false);

                            var newGeneList = newGeneArray.Select((p, i) => new PatientOrderEffectedGeneMineral
                            {
                                GeneID = p,
                                OrderMineralID = item.ID,
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList();

                            item.PatientOrderEffectedGeneMineralList = newGeneList.Union(stillExistArray).Union(deletedGeneArray).ToList();

                            patientOrderMineralTemp.Add(item);
                        }
                        db.PatientOrderService.UpdatePatientOrderMineralList(patientOrderMineralTemp);

                        List<PatientOrderMetabolism> patientOrderMetabolismTemp = new List<PatientOrderMetabolism>();
                        foreach (var item in model.PatientOrderGeneralList.PatientOrderMetabolismList)
                        {
                            var tempList = db.PatientOrderService.GetPatientOrderEffectedGeneMetabolism(item.ID).ToList();

                            int[] parsedInt = Array.ConvertAll(item.EffectedGenes, s => int.Parse(s));

                            //all are still there
                            var stillExistArray = tempList.Where(x => parsedInt.Contains(x.GeneID)).ToList();
                            //all that are new
                            int[] newGeneArray = parsedInt.Where(x => !tempList.Any(y => y.GeneID == x)).ToArray();
                            //all that are removed
                            var deletedGeneArray = tempList.Except(stillExistArray).ToList();
                            deletedGeneArray.ForEach(x => x.IsActive = false);

                            var newGeneList = newGeneArray.Select((p, i) => new PatientOrderEffectedGeneMetabolism
                            {
                                GeneID = p,
                                OrderMetabolismID = item.ID,
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList();

                            item.PatientOrderEffectedGeneMetabolismList = newGeneList.Union(stillExistArray).Union(deletedGeneArray).ToList();

                            patientOrderMetabolismTemp.Add(item);
                        }
                        db.PatientOrderService.UpdatePatientOrderMetabolismList(patientOrderMetabolismTemp);

                        List<PatientOrderDiet> patientOrderDietTemp = new List<PatientOrderDiet>();
                        foreach (var item in model.PatientOrderGeneralList.PatientOrderDietList)
                        {
                            var tempList = db.PatientOrderService.GetPatientOrderEffectedGeneDiet(item.ID).ToList();

                            int[] parsedInt = Array.ConvertAll(item.EffectedGenes, s => int.Parse(s));

                            //all are still there
                            var stillExistArray = tempList.Where(x => parsedInt.Contains(x.GeneID)).ToList();
                            //all that are new
                            int[] newGeneArray = parsedInt.Where(x => !tempList.Any(y => y.GeneID == x)).ToArray();
                            //all that are removed
                            var deletedGeneArray = tempList.Except(stillExistArray).ToList();
                            deletedGeneArray.ForEach(x => x.IsActive = false);

                            var newGeneList = newGeneArray.Select((p, i) => new PatientOrderEffectedGeneDiet
                            {
                                GeneID = p,
                                OrderDietID = item.ID,
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList();

                            item.PatientOrderEffectedGeneDietList = newGeneList.Union(stillExistArray).Union(deletedGeneArray).ToList();

                            patientOrderDietTemp.Add(item);
                        }
                        db.PatientOrderService.UpdatePatientOrderDietList(patientOrderDietTemp);

                        List<PatientOrderVitamin> patientOrderVitaminTemp = new List<PatientOrderVitamin>();
                        foreach (var item in model.PatientOrderGeneralList.PatientOrderVitaminList)
                        {
                            var tempList = db.PatientOrderService.GetPatientOrderEffectedGeneVitamin(item.ID).ToList();

                            int[] parsedInt = Array.ConvertAll(item.EffectedGenes, s => int.Parse(s));

                            //all are still there
                            var stillExistArray = tempList.Where(x => parsedInt.Contains(x.GeneID)).ToList();
                            //all that are new
                            int[] newGeneArray = parsedInt.Where(x => !tempList.Any(y => y.GeneID == x)).ToArray();
                            //all that are removed
                            var deletedGeneArray = tempList.Except(stillExistArray).ToList();
                            deletedGeneArray.ForEach(x => x.IsActive = false);

                            var newGeneList = newGeneArray.Select((p, i) => new PatientOrderEffectedGeneVitamin
                            {
                                GeneID = p,
                                OrderVitaminID = item.ID,
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList();

                            item.PatientOrderEffectedGeneVitaminList = newGeneList.Union(stillExistArray).Union(deletedGeneArray).ToList();

                            patientOrderVitaminTemp.Add(item);
                        }
                        db.PatientOrderService.UpdatePatientOrderVitaminList(patientOrderVitaminTemp);

                        break;
                    case 2:

                        db.PatientOrderService.UpdatePatientOrderEpilepsyList(model.PatientOrderEpilepsyList);

                        break;
                    case 3:

                        db.PatientOrderService.UpdatePatientOrderFatConsumptionList(model.PatientOrderFatConsumptionList);

                        break;
                    case 4:

                        db.PatientOrderService.UpdatePatientOrderMethyationList(model.PatientOrderMethyationList);

                        break;
                    case 5:

                        db.PatientOrderService.UpdatePatientOrderProstateList(model.PatientOrderProstateList);

                        break;
                }

            }

            if (!checkOrderType)
            {
                switch (model.GeneTemplateID)
                {
                    case 1:
                        var mineralList = model.GeneralTemplates.MineralTemplates.Select(x => new PatientOrderMineral
                        {
                            ID = 0,
                            OrderID = getOrderID,
                            CreatedOn = dateNow,
                            UpdatedOn = dateNow,
                            IsActive = true,
                            Effect = x.Effect,
                            ElementID = x.ElementID,
                            Recomendations = x.Recomendations,
                            PatientOrderEffectedGeneMineralList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneMineral
                            {
                                GeneID = int.Parse(p),
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList()
                        }).ToList();

                        db.PatientOrderService.AddPatientOrderMineralList(mineralList);

                        var metabolismList = model.GeneralTemplates.MetabolismTemplates.Select(x => new PatientOrderMetabolism
                        {
                            ID = 0,
                            OrderID = getOrderID,
                            CreatedOn = dateNow,
                            UpdatedOn = dateNow,
                            IsActive = true,
                            Effected = x.Effected,
                            ConsumptionTypeID = x.ConsumptionTypeID,
                            Recomendation = x.Recomendation,
                            PatientOrderEffectedGeneMetabolismList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneMetabolism
                            {
                                GeneID = int.Parse(p),
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList()
                        }).ToList();

                        db.PatientOrderService.AddPatientOrderMetabolismList(metabolismList);

                        var fitnessList = model.GeneralTemplates.FitnessTemplates.Select(x => new PatientOrderFitness
                        {
                            ID = 0,
                            OrderID = getOrderID,
                            CreatedOn = dateNow,
                            UpdatedOn = dateNow,
                            IsActive = true,
                            EffectedGood = x.EffectedGood,
                            EffectedBad = x.EffectedBad,
                            ConsumptionTypeID = x.ConsumptionTypeID,
                            PatientOrderEffectedGeneFitnessList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneFitness
                            {
                                GeneID = int.Parse(p),
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList()
                        }).ToList();

                        db.PatientOrderService.AddPatientOrderFitnessList(fitnessList);

                        var dietList = model.GeneralTemplates.DietTemplates.Select(x => new PatientOrderDiet
                        {
                            ID=0,
                            OrderID = getOrderID,
                            CreatedOn = dateNow,
                            UpdatedOn = dateNow,
                            IsActive = true,
                            EffectedGood = x.EffectedGood,
                            EffectedBad = x.EffectedBad,
                            ConsumptionTypeID = x.ConsumptionTypeID,
                            PatientOrderEffectedGeneDietList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneDiet
                            {
                                GeneID = int.Parse(p),
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList()
                        }).ToList();

                        db.PatientOrderService.AddPatientOrderDietList(dietList);

                        var vitaminList = model.GeneralTemplates.VitaminTemplates.Select(x => new PatientOrderVitamin
                        {
                            ID=0,
                            OrderID = getOrderID,
                            CreatedOn = dateNow,
                            UpdatedOn = dateNow,
                            IsActive = true,
                            FeederTypeID = x.FeederTypeID,
                            Effect = x.Effect,
                            Recommendations = x.Recommendations,
                            PatientOrderEffectedGeneVitaminList = x.EffectedGenes.Select((p, i) => new PatientOrderEffectedGeneVitamin
                            {
                                GeneID = int.Parse(p),
                                CreatedOn = dateNow,
                                UpdatedOn = dateNow,
                                IsActive = true
                            }).ToList()
                        }).ToList();

                        db.PatientOrderService.AddPatientOrderVitaminList(vitaminList);
                        break;

                    case 2:
                        model.PatientOrderEpilepsyList.ForEach(x => 
                        {
                            x.ID = 0; x.IsActive = true; x.OrderID = getOrderID; x.CreatedOn = dateNow; x.UpdatedOn = dateNow; 
                        });

                        db.PatientOrderService.AddPatientOrderEpilepsyList(model.PatientOrderEpilepsyList);
                        break;

                    case 3:
                        foreach (var item in model.PatientOrderFatConsumptionList)
                        {
                            item.ID = 0;
                            item.OrderID = getOrderID;
                            item.IsActive = true;
                            item.CreatedOn = DateTime.UtcNow;
                            item.UpdatedOn = DateTime.UtcNow;

                        }
                        db.PatientOrderService.AddPatientOrderFatConsumptionList(model.PatientOrderFatConsumptionList);
                        break;

                    case 4:
                        foreach (var item in model.PatientOrderMethyationList)
                        {
                            item.ID = 0;
                            item.OrderID = getOrderID;
                            item.IsActive = true;
                            item.CreatedOn = DateTime.UtcNow;
                            item.UpdatedOn = DateTime.UtcNow;

                        }
                        db.PatientOrderService.AddPatientOrderMethyationList(model.PatientOrderMethyationList);
                        break;

                    case 5:
                        foreach (var item in model.PatientOrderProstateList)
                        {
                            item.ID = 0;
                            item.OrderID = getOrderID;
                            item.IsActive = true;
                            item.CreatedOn = DateTime.UtcNow;
                            item.UpdatedOn = DateTime.UtcNow;

                        }
                        db.PatientOrderService.AddPatientOrderProstateList(model.PatientOrderProstateList);
                        break;
                }
            }
            patientorder.FollowUpNeeded = model.FollowUpNeeded;
            patientorder.Summary = model.Summary;
            db.PatientOrderService.UpdateOrder(patientorder);

            return Json(true);
        }

    }
}