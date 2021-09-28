using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using GeneticSystem.Areas.Admin.Models.FollowUp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoreLinq;
using Service.UnitOfServices;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FollowUpController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;
        public FollowUpController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;

        }

        public IActionResult Index(int? id)
        {
            FollowUpVM followUpVM = new FollowUpVM();

            var roleChk = Convert.ToInt32(HttpContext.Request.Cookies["RoleID"]);

            if (roleChk == 5)
            {
                var userId = Convert.ToInt32(HttpContext.Request.Cookies["ID"]);
                followUpVM.User = db.UserService.GetPatient(Convert.ToInt32(userId));
                var result = db.ClientOrderService.GetClientOrdersByUserID(Convert.ToInt32(userId)).ToList();
                followUpVM.ClientOrderList = new Data.Helpers.PagedData<ClientOrder>();
                followUpVM.ClientOrderList.Data = (result).Take(PageSize);
                followUpVM.ClientOrderList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));
                followUpVM.RoleChk = roleChk;
            }
            else
            {
                followUpVM = new FollowUpVM
                {
                    patientList = db.UserService.GetPatients().Select(x => new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = ((x.EnFirstName ?? "") + " " + (x.EnSecondName ?? "") + " " + (x.EnThirdName ?? ""))
                    }).ToList()
                };

                if (id != null)
                {
                    followUpVM.User = db.UserService.GetPatient(Convert.ToInt32(id));
                    var result = db.ClientOrderService.GetClientOrdersByUserID(Convert.ToInt32(id)).ToList();
                    followUpVM.ClientOrderList = new Data.Helpers.PagedData<ClientOrder>();
                    followUpVM.ClientOrderList.Data = (result).Take(PageSize);
                    followUpVM.ClientOrderList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));
                }
            }

            return View(followUpVM);
        }

        public IActionResult GetClientOrders(int userID)
        {
            FollowUpVM followUpVM = new FollowUpVM();
            var roleChk = Convert.ToInt32(HttpContext.Request.Cookies["RoleID"]);
            followUpVM.RoleChk = roleChk;
            var result = db.ClientOrderService.GetClientOrdersByUserID(Convert.ToInt32(userID)).ToList(); ;

            followUpVM.ClientOrderList = new Data.Helpers.PagedData<Data.Models.ClientOrder>();
            followUpVM.ClientOrderList.Data = (result).Take(PageSize);
            followUpVM.ClientOrderList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));

            return PartialView("_GetOrder", followUpVM);
        }

        //[HttpGet]
        //public IActionResult GetOrderData(int orderId)
        //{
        //    FollowUpVM followUpVM = new FollowUpVM();
        //    ClientOrderViewModel viewModel = new ClientOrderViewModel();
        //    List<ClientOrderData> clientOrderDatas = new List<ClientOrderData>();

        //    ClientOrder clientOrder = db.ClientOrderService.GetClientOrderByID(orderId);
        //    viewModel.ClientOrder = clientOrder;
        //    ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");
        //    ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TestTempType");
        //    ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
        //    ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");
        //    ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
        //    ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
        //    ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

        //    viewModel.TemplateList = db.DynamicTemplateService.GetAllTemplates().Where(x => x.TemplateTypeID == clientOrder.Template.TemplateTypeID).ToList();

        //    for (int i = 0; i < viewModel.TemplateList.Count(); i++)
        //    {
        //        clientOrderDatas.AddRange(db.ClientOrderService.GetClientOrderDataByTempOrderID(viewModel.TemplateList[i].ID, clientOrder.ID));
        //    }

        //    viewModel.ClientOrderData = clientOrderDatas;

        //    if (viewModel.ClientOrder.ClientOrderTests != null)
        //    {
        //        if (viewModel.ClientOrder.FollowUpTestAdded == false)
        //        {
        //            TestTempVM tempVM = new TestTempVM();
        //            List<List<TestTempData>[]> expData = new List<List<TestTempData>[]>();
        //            List<TestTempDataListVM> dataListVM = new List<TestTempDataListVM>();

        //            foreach (var testType in viewModel.ClientOrder.ClientOrderTests.Where(x => x.Done == true))
        //            {
        //                var testTemp = db.TestTempService.GetTestTempById(testType.TestTemplateID);


        //                if (testTemp != null)
        //                {
        //                    tempVM.TestTempCol = db.TestTempService.GetTestTempColsByTempId(testTemp.ID);
        //                    var RowNo = (db.TestTempService.GetRowID(testTemp.ID) - 1);
        //                    var xTempData = db.TestTempService.GetTempDataByTempId(testTemp.ID);

        //                    List<TestTempData>[] tempDataList = new List<TestTempData>[RowNo];



        //                    for (int i = 0; i < RowNo; i++)
        //                    {
        //                        List<TestTempData> tempList = new List<TestTempData>();
        //                        //tempDataList[i] = xTempData.Where(x => x.RowNo == (i + 1)).ToList();

        //                        for (int j = 0; j < tempVM.TestTempCol.Count(); j++)
        //                        {
        //                            TestTempData tempData = xTempData.Where(x => x.TestTempColID == tempVM.TestTempCol[j].ID && x.RowNo == (i + 1)).FirstOrDefault();


        //                            if (tempData == null)
        //                            {
        //                                tempData = new TestTempData();
        //                                tempData.TestTempColID = tempVM.TestTempCol[j].ID;
        //                                tempData.TestTempCol = tempVM.TestTempCol[j];
        //                                tempData.TestTempID = testTemp.ID;
        //                                tempData.RowNo = RowNo;
        //                                tempData.ID = 0;
        //                            }

        //                            if (tempData.TestTempCol.TempColTypeID == 2)
        //                            {
        //                                if (tempData.StringValue != null)
        //                                {
        //                                    tempData.multiSelect = tempData.StringValue.Split(",");
        //                                }
        //                            }

        //                            tempList.Add(tempData);
        //                        }

        //                        tempDataList[i] = tempList;
        //                    }
        //                    var tempDataListVM = new TestTempDataListVM
        //                    {
        //                        expDataList = tempDataList,
        //                        testTempCols = tempVM.TestTempCol,
        //                        TestName = testTemp.TestTempType.Name
        //                    };
        //                    dataListVM.Add(tempDataListVM);
        //                    expData.Add(tempDataList);
        //                }

        //            }

        //            viewModel.tempDataListVM = dataListVM;
        //            ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
        //            ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
        //            ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);
        //            ViewBag.Result = new List<SelectListItem>
        //             {
        //             new SelectListItem{ Text="High", Value = "1" },
        //             new SelectListItem{ Text="Medium", Value = "2" },
        //             new SelectListItem{ Text="Low", Value = "3" }
        //             };

        //            followUpVM.ClientOrderViewModel = viewModel;
        //            followUpVM.FollowUpByDocConvList = db.FollowUpService.GetByDocConvs(viewModel.ClientOrder.ID);
        //            followUpVM.FollowUpByDocResultList = db.FollowUpService.GetDocResults(viewModel.ClientOrder.ID);
        //            return PartialView("_GetTempOrder", followUpVM);
        //        }
        //        else if(viewModel.ClientOrder.FollowUpTestAdded == true)
        //        {
        //            TestTempVM tempVM = new TestTempVM();
        //            viewModel.ClientOrder.TestTypeArray = viewModel.ClientOrder.TestType.Split(",");
        //            List<List<FollowUpTestTempData>[]> expData = new List<List<FollowUpTestTempData>[]>();
        //            List<FollowUpTestTempDataListVM> dataListVM = new List<FollowUpTestTempDataListVM>();
        //            foreach (var testType in viewModel.ClientOrder.TestTypeArray)
        //            {
        //                var testTemp = db.TestTempService.GetTestTempByTypeId(Convert.ToInt32(testType));


        //                if (testTemp != null)
        //                {
        //                    tempVM.TestTempCol = db.TestTempService.GetTestTempColsByTempId(testTemp.ID);
        //                    var RowNo = (db.TestTempService.GetRowID(testTemp.ID) - 1);
        //                    var xTempData = db.FollowUpService.GetFollowUpTempDataByTempId(testTemp.ID);

        //                    List<FollowUpTestTempData>[] tempDataList = new List<FollowUpTestTempData>[RowNo];



        //                    for (int i = 0; i < RowNo; i++)
        //                    {
        //                        List<FollowUpTestTempData> tempList = new List<FollowUpTestTempData>();

        //                        for (int j = 0; j < tempVM.TestTempCol.Count(); j++)
        //                        {
        //                            FollowUpTestTempData tempData = xTempData.Where(x => x.TestTempColID == tempVM.TestTempCol[j].ID && x.RowNo == (i + 1)).FirstOrDefault();


        //                            if (tempData == null)
        //                            {
        //                                tempData = new FollowUpTestTempData();
        //                                tempData.TestTempColID = tempVM.TestTempCol[j].ID;
        //                                tempData.TestTempCol = tempVM.TestTempCol[j];
        //                                tempData.TestTempID = testTemp.ID;
        //                                tempData.RowNo = RowNo;
        //                                tempData.ID = 0;
        //                            }

        //                            if (tempData.TestTempCol.TempColTypeID == 2)
        //                            {
        //                                if (tempData.StringValue != null)
        //                                {
        //                                    tempData.multiSelect = tempData.StringValue.Split(",");
        //                                }
        //                            }

        //                            tempList.Add(tempData);
        //                        }

        //                        tempDataList[i] = tempList;
        //                    }
        //                    var tempDataListVM = new FollowUpTestTempDataListVM
        //                    {
        //                        expDataList = tempDataList,
        //                        testTempCols = tempVM.TestTempCol,
        //                        TestName = testTemp.TestTempType.Name
        //                    };
        //                    dataListVM.Add(tempDataListVM);
        //                    expData.Add(tempDataList);
        //                }

        //            }

        //            viewModel.followUpTempDataListVM = dataListVM;
        //            ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
        //            ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
        //            ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);
        //            ViewBag.Result = new List<SelectListItem>
        //             {
        //             new SelectListItem{ Text="High", Value = "1" },
        //             new SelectListItem{ Text="Medium", Value = "2" },
        //             new SelectListItem{ Text="Low", Value = "3" }
        //             };

        //            followUpVM.ClientOrderViewModel = viewModel;
        //            followUpVM.FollowUpByDocConvList = db.FollowUpService.GetByDocConvs(viewModel.ClientOrder.ID);
        //            followUpVM.FollowUpByDocResultList = db.FollowUpService.GetDocResults(viewModel.ClientOrder.ID);
        //            return PartialView("_GetUpdatedTestTemp", followUpVM);
        //        }
        //    }

        //    ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
        //    ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
        //    ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);
        //    ViewBag.Result = new List<SelectListItem>
        //             {
        //             new SelectListItem{ Text="High", Value = "1" },
        //             new SelectListItem{ Text="Medium", Value = "2" },
        //             new SelectListItem{ Text="Low", Value = "3" }
        //             };

        //    followUpVM.ClientOrderViewModel = viewModel;
        //    followUpVM.FollowUpByDocConvList = db.FollowUpService.GetByDocConvs(viewModel.ClientOrder.ID);
        //    followUpVM.FollowUpByDocResultList = db.FollowUpService.GetDocResults(viewModel.ClientOrder.ID);

        //    return PartialView("_GetTempOrder", followUpVM);
        //}

        [HttpGet]
        public IActionResult GetOrderData(int orderId)
        {
            FollowUpVM followUpVM = new FollowUpVM();
            var roleChk = Convert.ToInt32(HttpContext.Request.Cookies["RoleID"]);
            followUpVM.RoleChk = roleChk;
            ClientOrderViewModel viewModel = new ClientOrderViewModel();
            List<ClientOrderData> clientOrderDatas = new List<ClientOrderData>();

            ClientOrder clientOrder = db.ClientOrderService.GetClientOrderByID(orderId);
            if (clientOrder.FollowUpArray != null)
            {
                clientOrder.FollowUpStrings = new System.Text.StringBuilder();


                for (int i = 0; i < clientOrder.FollowUpArray.Length; i++)
                {
                    clientOrder.FollowUpStrings.Append((db.LookupService.GetLookUpNameByID(Convert.ToInt32(clientOrder.FollowUpArray[i])) + " "));
                }

            }

            if (clientOrder.ClientOrderTests != null)
            {
                clientOrder.TestStrings = new System.Text.StringBuilder();

                for (int i = 0; i < clientOrder.ClientOrderTests.Count; i++)
                {
                    clientOrder.TestStrings.Append((clientOrder.ClientOrderTests[i].TestTemplate.TestTempType.Name ?? "" + ">>" + clientOrder.ClientOrderTests[i].TestTemplate.SubTestTempType.Name));
                }

            }
            viewModel.ClientOrder = clientOrder;
            ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");
            ViewBag.TestTypes = db.LookupService.GetLookUpByTypeName("TestTempType");
            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");
            ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
            ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
            ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);
            ViewBag.Result = new List<SelectListItem>
                     {
                     new SelectListItem{ Text="High", Value = "1" },
                     new SelectListItem{ Text="Medium", Value = "2" },
                     new SelectListItem{ Text="Low", Value = "3" }
                     };

            viewModel.TemplateList = db.DynamicTemplateService.GetAllTemplates().Where(x => x.TemplateTypeID == clientOrder.Template.TemplateTypeID).ToList();

            for (int i = 0; i < viewModel.TemplateList.Count(); i++)
            {
                clientOrderDatas.AddRange(db.ClientOrderService.GetClientOrderDataByTempOrderID(viewModel.TemplateList[i].ID, clientOrder.ID));
            }

            viewModel.ClientOrderData = clientOrderDatas;

            if (viewModel.ClientOrder.ClientOrderTests != null)
            {
                TestTempVM tempVM = new TestTempVM();
                //viewModel.ClientOrder.TestTypeArray = viewModel.ClientOrder.TestType.Split(",");
                List<List<FollowUpTestTempData>[]> expData = new List<List<FollowUpTestTempData>[]>();
                List<FollowUpTestTempDataListVM> dataListVM = new List<FollowUpTestTempDataListVM>();
                foreach (var testType in viewModel.ClientOrder.ClientOrderTests.Where(x => x.Done == true))
                {
                    var testTemp = db.TestTempService.GetTestTempById(testType.TestTemplateID);


                    if (testTemp != null)
                    {
                        tempVM.TestTempCol = db.TestTempService.GetTestTempColsByTempId(testTemp.ID);
                        var RowNo = (db.TestTempService.GetRowID(testTemp.ID) - 1);
                        var xTempData = db.FollowUpService.GetFollowUpTempDataByTempId(testTemp.ID, viewModel.ClientOrder.ID);

                        List<FollowUpTestTempData>[] tempDataList = new List<FollowUpTestTempData>[RowNo];

                        for (int i = 0; i < RowNo; i++)
                        {
                            List<FollowUpTestTempData> tempList = new List<FollowUpTestTempData>();

                            for (int j = 0; j < tempVM.TestTempCol.Count(); j++)
                            {
                                FollowUpTestTempData tempData = xTempData.Where(x => x.TestTempColID == tempVM.TestTempCol[j].ID && x.RowNo == (i + 1)).FirstOrDefault();


                                if (tempData == null)
                                {
                                    tempData = new FollowUpTestTempData();
                                    tempData.TestTempColID = tempVM.TestTempCol[j].ID;
                                    tempData.TestTempCol = tempVM.TestTempCol[j];
                                    tempData.TestTempID = testTemp.ID;
                                    tempData.RowNo = RowNo;
                                    tempData.ID = 0;
                                }

                                if (tempData.TestTempCol.TempColTypeID == 2)
                                {
                                    if (tempData.StringValue != null)
                                    {
                                        tempData.multiSelect = tempData.StringValue.Split(",");
                                    }
                                }

                                tempList.Add(tempData);
                            }

                            tempDataList[i] = tempList;
                        }
                        var tempDataListVM = new FollowUpTestTempDataListVM
                        {
                            expDataList = tempDataList,
                            testTempCols = tempVM.TestTempCol,
                            TestName = testTemp.TestTempType.Name
                        };
                        dataListVM.Add(tempDataListVM);
                        expData.Add(tempDataList);
                    }

                }

                viewModel.followUpTempDataListVM = dataListVM;

                followUpVM.ClientOrderViewModel = viewModel;
                followUpVM.FollowUpByDocConvList = db.FollowUpService.GetByDocConvs(viewModel.ClientOrder.ID);
                //followUpVM.FollowUpByDocResultList = db.FollowUpService.GetDocResults(viewModel.ClientOrder.ID);
                return PartialView("_GetUpdatedTestTemp", followUpVM);

            }


            followUpVM.ClientOrderViewModel = viewModel;
            followUpVM.FollowUpByDocConvList = db.FollowUpService.GetByDocConvs(viewModel.ClientOrder.ID);
            followUpVM.FollowUpByDocResultList = db.FollowUpService.GetDocResults(viewModel.ClientOrder.ID);

            return PartialView("_GetTempOrder", followUpVM);
        }

        [HttpGet]
        public bool CloseOrder(int orderId)
        {
            db.ClientOrderService.CloseClientOrderByID(orderId);
            return true;
        }

        [HttpPost]
        public IActionResult AddFollowUpSummary(FollowUpByDocConv followUpByDoc)
        {
            FollowUpVM followUpVM = new FollowUpVM();
            followUpVM.ClientOrderViewModel = new ClientOrderViewModel();
            followUpVM.ClientOrderViewModel.ClientOrder = new ClientOrder();
            followUpVM.ClientOrderViewModel.ClientOrder = db.ClientOrderService.GetClientOrderByID(followUpByDoc.OrderID);
            int? senderID = Convert.ToInt32(HttpContext.Request.Cookies["ID"]);
            followUpByDoc.SenderID = senderID != null ? Convert.ToInt32(senderID) : 1;
            followUpVM.FollowUpByDocConvList = db.FollowUpService.AddMessage(followUpByDoc);
            return PartialView("_FollowUpByDocMessage", followUpVM);
        }

        [HttpGet]
        public IActionResult AddResult(int orderID)
        {
            FollowUpVM followUpVM = new FollowUpVM();
            followUpVM.orderTest = db.FollowUpService.GetPendingTests(orderID).Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.TestTemplate.TestTempType.Name + ">>" + x.TestTemplate.SubTestTempType.Name
            }).ToList();

            return PartialView("_AddResult", followUpVM);
        }

        [HttpGet]
        public IActionResult AddTest(int orderID)
        {
            FollowUpVM followUpVM = new FollowUpVM();

            var completedList = db.FollowUpService.GetCompletedTests(orderID).Select(x => x.TestTemplate).ToList();

            var allTestList = db.TestTempService.GetTestTemps().ToList();

            followUpVM.completedOrderTests = string.Join(", ", (completedList.Select(x => (x.TestTempType?.Name ?? "" + ">>" + x.SubTestTempType?.Name ?? ""))));

            followUpVM.orderTest = allTestList.Except(completedList, new TestTempEqComparer()).Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.TestTempType.Name + ">>" + x.SubTestTempType.Name
            }).ToList();

            followUpVM.PendingTestArray = db.FollowUpService.GetPendingTests(orderID).Select(x => x.TestTemplate.ID.ToString()).ToArray();

            return PartialView("_AddTest", followUpVM);
        }

        [HttpPost]
        public bool AddTest(FollowUpVM followUpVM)
        {
            try { 
            db.FollowUpService.RemovePendingOrders(followUpVM.ClientOrderViewModel.ClientOrder.ID);

            List<ClientOrderTest> newPendingTests = followUpVM.PendingTestArray.Select(x => new ClientOrderTest
            {
                ID = 0,
                Done = false,
                TestTemplateID = Convert.ToInt32(x),
                ClientOrderID = followUpVM.ClientOrderViewModel.ClientOrder.ID
            }).ToList();

            db.FollowUpService.AddOrderTests(newPendingTests);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }


        }

        [HttpPost]
        public bool AddTestData(FollowUpVM followUpVM)
        {
            if (followUpVM.followUpTestTempDataList != null)
            {
                var clientOrder = db.ClientOrderService.GetClientOrderByID(followUpVM.ClientOrderViewModel.ClientOrder.ID);

                foreach (var item in followUpVM.followUpTestTempDataList)
                {
                    if (item != null)
                    {
                        foreach (var row in item)
                        {
                            if (row != null)
                            {
                                db.FollowUpService.AddFollowUpTestTempDataList(row);
                            }
                        }
                    }
                }
                clientOrder.FollowUpTestAdded = true;
                db.ClientOrderService.UpdateClientOrder(clientOrder);

                var orderTest = db.ClientOrderService.MarkOrderTestDone(followUpVM.clientOrderTest.TestTemplateID, followUpVM.ClientOrderViewModel.ClientOrder.ID);
                //orderTest.Done = true;
                //db.ClientOrderService.UpdateClientOrderTest(orderTest);


            }
            return true;
        }

        [HttpGet]
        public IActionResult GetTestTemp(int orderId, int selectedVal)
        {
            if (orderId == 0 || selectedVal == 0)
                return null;

            FollowUpVM followUpVM = new FollowUpVM();
            ClientOrderViewModel viewModel = new ClientOrderViewModel();
            List<ClientOrderData> clientOrderDatas = new List<ClientOrderData>();

            ClientOrder clientOrder = db.ClientOrderService.GetClientOrderByID(orderId);
            viewModel.ClientOrder = clientOrder;
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


                if (viewModel.ClientOrder.ClientOrderTests != null)
                {
                        TestTempVM tempVM = new TestTempVM();

                        List<List<TestTempData>[]> expData = new List<List<TestTempData>[]>();
                        List<TestTempDataListVM> dataListVM = new List<TestTempDataListVM>();

                        foreach (var testType in viewModel.ClientOrder.ClientOrderTests.Where(x => x.Done == false && x.ID == selectedVal))
                        {
                            var testTemp = db.TestTempService.GetTestTempById(testType.TestTemplateID);

                            if (testTemp != null)
                            {
                                tempVM.TestTempCol = db.TestTempService.GetTestTempColsByTempId(testTemp.ID);
                                var RowNo = (db.TestTempService.GetRowID(testTemp.ID) - 1);
                                var xTempData = db.TestTempService.GetTempDataByTempId(testTemp.ID);

                                List<TestTempData>[] tempDataList = new List<TestTempData>[RowNo];

                                for (int i = 0; i < RowNo; i++)
                                {
                                    List<TestTempData> tempList = new List<TestTempData>();

                                    for (int j = 0; j < tempVM.TestTempCol.Count(); j++)
                                    {
                                        TestTempData tempData = xTempData.Where(x => x.TestTempColID == tempVM.TestTempCol[j].ID && x.RowNo == (i + 1)).FirstOrDefault();


                                        if (tempData == null)
                                        {
                                            tempData = new TestTempData();
                                            tempData.TestTempColID = tempVM.TestTempCol[j].ID;
                                            tempData.TestTempCol = tempVM.TestTempCol[j];
                                            tempData.TestTempID = testTemp.ID;
                                            tempData.RowNo = (i + 1);
                                            tempData.ID = 0;
                                        }

                                        if (tempData.TestTempCol.TempColTypeID == 2)
                                        {
                                            if (tempData.StringValue != null)
                                            {
                                                tempData.multiSelect = tempData.StringValue.Split(",");
                                            }
                                        }

                                        tempList.Add(tempData);
                                    }

                                    tempDataList[i] = tempList;
                                }
                                var tempDataListVM = new TestTempDataListVM
                                {
                                    expDataList = tempDataList,
                                    testTempCols = tempVM.TestTempCol,
                                    TestName = testTemp.TestTempType.Name
                                };
                                dataListVM.Add(tempDataListVM);
                                expData.Add(tempDataList);
                            }

                        }

                        viewModel.tempDataListVM = dataListVM;
                        ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
                        ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
                        ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);
                        ViewBag.Result = new List<SelectListItem>
                                 {
                                 new SelectListItem{ Text="High", Value = "1" },
                                 new SelectListItem{ Text="Medium", Value = "2" },
                                 new SelectListItem{ Text="Low", Value = "3" }
                                 };

                        followUpVM.ClientOrderViewModel = viewModel;
                        followUpVM.FollowUpByDocConvList = db.FollowUpService.GetByDocConvs(viewModel.ClientOrder.ID);
                        followUpVM.FollowUpByDocResultList = db.FollowUpService.GetDocResults(viewModel.ClientOrder.ID);
                        return PartialView("_SelectedTest", followUpVM);
                }
                else
                    return null;
        }

    }
}
