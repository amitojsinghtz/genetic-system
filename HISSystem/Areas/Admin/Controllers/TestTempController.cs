using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.UnitOfServices;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestTempController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;

        public TestTempController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }

        public IActionResult Index()
        {
            TestTempVM testTemp = new TestTempVM();
            testTemp.DropDown = db.TestTempService.GetTestTemps().Select(x => new DropDownVM { ID = x.ID, Name = x.TestTempType.Name + ">>" + x.SubTestTempType.Name });
            return View(testTemp);
        }

        [HttpGet]
        public IActionResult AddTemplate()
        {
            TestTempVM testTempVM = new TestTempVM();

            ViewBag.TemplateType = db.LookupService.GetLookUpByTypeName("TestTempType");
            ViewBag.SubTemplateType = db.LookupService.GetLookUpByTypeName("SubTestTempType");
            ViewBag.DropDownType = db.LookupService.GetLookUpByTypeName("DropDownType");

            ViewBag.TemplateField = new List<SelectListItem>
            {
              new SelectListItem{ Text="Checkbox", Value = "1" },
              new SelectListItem{ Text="Dropdown", Value = "2" },
              new SelectListItem{ Text="Textbox", Value = "3" }
            };

            return PartialView("_AddMasterTemp", testTempVM);
        }
        [HttpPost]
        public IActionResult AddTemplate(TestTempVM testTempVM)
        {
            Response response = new Response();
            try
            {
                TestTemp addtemp = testTempVM.TestTemp;
                var result = db.TestTempService.AddTestTemp(addtemp);


                response.Code = result.ID;
                response.Status = "Success";
               

                return Json(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                response.Code = 0;
                response.Status = ex.InnerException.StackTrace;

                return Json(response);
            }
        }

        [HttpGet]
        public IActionResult GetDynamicTemplate(int tempID)
        {
            TestTempVM testTemp = new TestTempVM
            {
                TestTemp = db.TestTempService.GetTemplateById(tempID)
            };

            int tmpClCnt = 0;

            testTemp.TestTempDataList = db.TestTempService.GetTempDataByTempId(testTemp.TestTemp.ID).ToList();

            if(testTemp.TestTempDataList != null && testTemp.TestTempDataList.Count() > 0)
            {
                tmpClCnt = testTemp.TestTempDataList.Max(x => x.RowNo);
            }
            

            testTemp.expData = new List<TestTempData>[tmpClCnt];

            if(tmpClCnt > 0) { 
            for (int i = 0; i < tmpClCnt; i++)
            {
                testTemp.expData[i] = testTemp.TestTempDataList.Where(x => x.RowNo == (i + 1)).ToList();

                for (int j = 0; j < testTemp.expData[i].Count(); j++)
                {
                    if (testTemp.expData[i][j].multiSelect != null)
                    {
                        testTemp.expData[i][j].multiSelectString = new StringBuilder();

                        for (int k = 0; k < testTemp.expData[i][j].multiSelect.Count(); k++)
                        {
                            testTemp.expData[i][j].multiSelectString.Append(((db.LookupService.GetLookUpNameByID(Convert.ToInt32(Convert.ToInt32(testTemp.expData[i][j].multiSelect[k])))) + " "));
                        }
                    }
                }
            }
            }

            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };
            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            return PartialView("_GetTempDetail", testTemp);
        }

        [HttpGet]
        public IActionResult AddTemplateData(int templateId)
        {
            TestTempVM testTemp = new TestTempVM();

            testTemp.TestTemp = db.TestTempService.GetTemplateById(templateId);
            testTemp.rowNo = db.TestTempService.GetRowID(testTemp.TestTemp.ID);
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

            return PartialView("_AddTempData", testTemp);
        }

        [HttpPost]
        public IActionResult AddTemplateDetail(TestTempVM testTempVM)
        {
            var newData = testTempVM.TestTempDataList.Where(x => x.ID == 0);

            if(newData != null)
            {
                var mltiList = newData.Where(x => x.multiSelect != null);
                if(mltiList != null)
                {
                    var mltiSl = mltiList.ToList();
                    for (int i = 0; i < mltiSl.Count(); i++)
                        mltiSl[i].StringValue = string.Join(',', mltiSl[i].multiSelect);
                }
                    
                db.TestTempService.AddTestTempDataList(newData.ToList());
            }

            var olderData = testTempVM.TestTempDataList.Where(x => x.ID != 0);

            if(olderData != null)
            {
                var mltiList = olderData.Where(x => x.multiSelect != null);
                if (mltiList != null)
                {
                    var mltiSl = mltiList.ToList();
                    for (int i = 0; i < mltiSl.Count(); i++)
                        mltiSl[i].StringValue = string.Join(',', mltiSl[i].multiSelect);
                }
                db.TestTempService.UpdateTemplateDataList(olderData.ToList());
            }

            var response = new Response
            {
                Code = testTempVM.TestTempDataList[0].TestTempID,
                Status = "Success!"
            };

            return Json(response);
        }

        [HttpPost]
        public bool UpdateTemplateData(TestTempVM testTempVM)
        {
            List<TestTempData> testTempData = testTempVM.TestTempDataList;

            db.TestTempService.UpdateTemplateDataList(testTempVM.TestTempDataList);

            return true;
        }

        [HttpPost]
        public bool DeleteTemplateData(TestTempVM testTempVM)
        {
            List<TestTempData> testTempData = testTempVM.TestTempDataList;
            db.TestTempService.DeleteTemplateDataList(testTempVM.TestTempDataList);
            return true;
        }

        [HttpGet]
        public IActionResult SearchTemplate(string searchQuery)
        {
            TestTempVM testTemp = new TestTempVM();

            if (searchQuery != null)
            {
                testTemp.DropDown = db.TestTempService.SearchTemplate(searchQuery).Select(x => new DropDownVM { ID = x.ID, Name = x.TestTempType.Name + ">>" + x.SubTestTempType.Name });
                return View("_SearchMasterTemp", testTemp);
            }
            else
            {
                testTemp.DropDown = db.TestTempService.GetTestTemps().Select(x => new DropDownVM { ID = x.ID, Name = x.TestTempType.Name + ">>" + x.SubTestTempType.Name });
                return View("_SearchMasterTemp", testTemp);
            }
        }
        [HttpGet]
        public IActionResult UpdateTemplate(int ID)
        {
            TestTempVM testTempVM = new TestTempVM();

            ViewBag.TemplateType = db.LookupService.GetLookUpByTypeName("TestTempType");
            ViewBag.SubTemplateType = db.LookupService.GetLookUpByTypeName("SubTestTempType");
            ViewBag.DropDownType = db.LookupService.GetLookUpByTypeName("DropDownType");

            ViewBag.TemplateField = new List<SelectListItem>
            {
              new SelectListItem{ Text="Checkbox", Value = "1" },
              new SelectListItem{ Text="Dropdown", Value = "2" },
              new SelectListItem{ Text="Textbox", Value = "3" }
            };
            ViewBag.TemplateID = ID;

            testTempVM.TestTemp = db.TestTempService.GetTemplateById(ID);

            return PartialView("_UpdateTemp", testTempVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTemplate(TestTempVM testTempVM)
        {
            var newCol = testTempVM.TestTemp.TestTempCols.Where(x => x.ID == 0);
            var orgCol = db.TestTempService.GetTestTempColsByTempId(testTempVM.TestTemp.ID);
            var deletedList = orgCol.Except(testTempVM.TestTemp.TestTempCols.Where(x => x.ID != 0), new TempColEqComparer()).ToList();
            var prevCol = testTempVM.TestTemp.TestTempCols.Where(x => x.ID != 0).ToList();


            if (deletedList != null)
            {
                 bool result = await DeleteTestTempCols(deletedList);
            }

            if (prevCol != null)
            {
                db.TestTempService.UpdateTemplateCol(prevCol);
            }
            if (newCol != null)
            {
                db.TestTempService.AddTestTempCol(newCol.ToList());
            }

            return null;
        }

        public async Task<bool> DeleteTestTempCols(List<TestTempCol> testTempCols)
        {
            if (testTempCols != null)
            {
                for (int i = 0; i < testTempCols.Count(); i++)
                {
                    try { 
                    await db.TestTempService.DeleteTempDataByColID(testTempCols[i].ID);
                    await db.TestTempService.DeleteTempColByID(testTempCols[i].ID);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return true;
        }
        [HttpGet]
        public IActionResult UpdateTempData(int rowNo, int tempId)
        {
            if (rowNo == 0 || tempId == 0)
                return null;


            TestTempVM testTempVM = new TestTempVM();

            testTempVM.TestTemp = db.TestTempService.GetTemplateById(tempId);
            testTempVM.TestTempCol = db.TestTempService.GetTestTempColsByTempId(tempId);
            testTempVM.TestTempDataList = db.TestTempService.GetTestTempDataByRowNoAndTempID(rowNo, tempId);

            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            testTempVM.expData = new List<TestTempData>[1];
            List<TestTempData> tempList = new List<TestTempData>();

            if(testTempVM.TestTempCol != null)
            {
                for(int i = 0; i < testTempVM.TestTempCol.Count(); i++)
                {
                    TestTempData tempData = testTempVM.TestTempDataList.Where(x => x.TestTempColID == testTempVM.TestTempCol[i].ID && x.RowNo == rowNo).FirstOrDefault();

                    if(tempData == null)
                    {
                        tempData = new TestTempData();
                        tempData.TestTempColID = testTempVM.TestTempCol[i].ID;
                        tempData.TestTempCol = testTempVM.TestTempCol[i];
                        tempData.TestTempID = tempId;
                        tempData.RowNo = rowNo;
                        tempData.ID = 0;
                    }

                    tempList.Add(tempData);
                }

                testTempVM.expData[0] = tempList;
            }

            return PartialView("_UpdateTempData", testTempVM);
        }
    }
}