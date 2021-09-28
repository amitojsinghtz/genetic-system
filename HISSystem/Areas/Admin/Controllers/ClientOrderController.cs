using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using HISSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoreLinq.Extensions;
using Service.UnitOfServices;
namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClientOrderController : Controller
    {
        public const int PageSize = 10;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;

        public ClientOrderController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }
        public IActionResult Index()
        {
            var clientOrderList = db.ClientOrderService.GetClientOrderList();
            var clientOrders = new PagedData<ClientOrder>();
            clientOrders.Data = (clientOrderList).Take(PageSize);
    
            clientOrders.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)clientOrderList.Count() / PageSize));
            return View(clientOrders);
        }

        public IActionResult GetAllOrders()
        {
            var clientOrderList = db.ClientOrderService.GetClientOrderList();
            var clientOrders = new PagedData<ClientOrder>();
            clientOrders.Data = (clientOrderList).Take(PageSize);

            clientOrders.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)clientOrderList.Count() / PageSize));
            return View("_Index",clientOrders);
        }

        public IActionResult GetPendingOrders()
        {
            var clientOrderList = db.ClientOrderService.GetClientOrderList().Where(x => x.Status?.Name == "Pending");
            var clientOrders = new PagedData<ClientOrder>();
            clientOrders.Data = (clientOrderList).Take(PageSize);

            clientOrders.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)clientOrderList.Count() / PageSize));
            return View("_Index",clientOrders);

        }

        public IActionResult getOrders(int page, int orderType)
        {
            var clientOrderList = db.ClientOrderService.GetClientOrderList();
            var clientOrders = new PagedData<ClientOrder>();
            clientOrders.Data = (clientOrderList).Skip(PageSize * (page - 1)).Take(PageSize);
            //clientOrders.Data = clientOrderList;
            clientOrders.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)clientOrderList.Count() / PageSize));
            return PartialView("_Index",clientOrders);
        }

        [HttpGet]
        public IActionResult AddOrder(int? id)
        {
            ClientOrderViewModel viewModel = new ClientOrderViewModel();
            viewModel.ClientOrder = new ClientOrder();
            int? createdByID = Convert.ToInt32(Request.Cookies["ID"]);
            if (createdByID != null)
            {
                viewModel.ClientOrder.CreatedBy = Convert.ToInt32(createdByID);
                var tempUser = db.UserService.GetAll().Where(x => x.ID == createdByID).FirstOrDefault();
                if (tempUser != null)
                    viewModel.CreatedByField = (tempUser.EnFirstName ?? "" + " " + tempUser.EnSecondName ?? "" + " " + tempUser.EnThirdName);
            }

            ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
            ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
            ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);

            ViewBag.TestTypes = db.TestTempService.GetTestTemps().Select(x => new SelectListItem
            {
                Text = (x.TestTempType?.Name + ">>" + x.SubTestTempType?.Name),
                Value = x.ID.ToString()
            }).ToList();

            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");

            if (id != null && id != 0)
            {

                viewModel.ClientOrder.User = db.UserService.GetUser(Convert.ToInt64(id));
            }

            return PartialView("AddClientOrder", viewModel);
        }


        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public IActionResult AddOrder(ClientOrderViewModel clientOrder, IFormFile file)
        {
            try
            {
                if (clientOrder.ClientOrder.FollowUpArray != null)
                    clientOrder.ClientOrder.FollowUp = string.Join(',', clientOrder.ClientOrder.FollowUpArray);

                for (int i = 0; i < clientOrder.ClientOrderData.Count(); i++)
                {
                    if (clientOrder.ClientOrderData[i].Genes != null)
                        clientOrder.ClientOrderData[i].GeneID = string.Join(',', clientOrder.ClientOrderData[i].Genes);
                }

                ClientOrder order = clientOrder.ClientOrder;
                order.ClientOrderTests = new List<ClientOrderTest>();

                if (clientOrder.ClientOrder.TestTypeArray != null)
                {
                    for (int i = 0; i < clientOrder.ClientOrder.TestTypeArray.Count(); i++)
                    {
                        ClientOrderTest orderTest = new ClientOrderTest
                        {
                            TestTemplateID = Convert.ToInt32(clientOrder.ClientOrder.TestTypeArray[i]),
                            Done = false
                        };
                        db.TestTempService.MarkTestTempInUse(orderTest.TestTemplateID);
                        order.ClientOrderTests.Add(orderTest);
                    }
                }

                order.ClientOrderData = clientOrder.ClientOrderData;
                order.OrderNo = db.ClientOrderService.GetMaxOrderNo();
                db.ClientOrderService.AddClientOrder(order);
                AttachmentModel attachmentModel = new AttachmentModel
                {
                    File = file,
                    ID = order.UserID,
                    TableName = "ClientOrder"
                };
                UploadAttachment(attachmentModel);
                var clientOrderList = db.ClientOrderService.GetClientOrderList();
                var clientOrders = new PagedData<ClientOrder>();
                clientOrders.Data = (clientOrderList).Take(PageSize);
                clientOrders.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)clientOrderList.Count() / PageSize));
                return PartialView("_Index", clientOrders);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public bool AddOrderForPatient(ClientOrderViewModel clientOrder, IFormFile file)
        {
            try
            {
                if (clientOrder.ClientOrder.FollowUpArray != null)
                    clientOrder.ClientOrder.FollowUp = string.Join(',', clientOrder.ClientOrder.FollowUpArray);

                if(clientOrder.ClientOrderData != null) { 
                for (int i = 0; i < clientOrder.ClientOrderData.Count(); i++)
                {
                    if (clientOrder.ClientOrderData[i].Genes != null)
                        clientOrder.ClientOrderData[i].GeneID = string.Join(',', clientOrder.ClientOrderData[i].Genes);
                }
                }
                ClientOrder order = clientOrder.ClientOrder;
                order.ClientOrderTests = new List<ClientOrderTest>();

                if (clientOrder.ClientOrder.TestTypeArray != null)
                {
                    for (int i = 0; i < clientOrder.ClientOrder.TestTypeArray.Count(); i++)
                    {
                        ClientOrderTest orderTest = new ClientOrderTest
                        {
                            TestTemplateID = Convert.ToInt32(clientOrder.ClientOrder.TestTypeArray[i]),
                            Done = false
                        };
                        order.ClientOrderTests.Add(orderTest);
                    }
                }

                order.ClientOrderData = clientOrder.ClientOrderData;
                order.OrderNo = db.ClientOrderService.GetMaxOrderNo();
                db.ClientOrderService.AddClientOrder(order);

                if(file != null) { 
                AttachmentModel attachmentModel = new AttachmentModel
                {
                    File = file,
                    ID = order.UserID,
                    TableName = "ClientOrder"
                };
                UploadAttachment(attachmentModel);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult OrderPDF(int orderId)
        {
            ClientOrderViewModel viewModel = new ClientOrderViewModel();
            List<ClientOrderData> clientOrderDatas = new List<ClientOrderData>();

            ClientOrder clientOrder = db.ClientOrderService.GetClientOrderByID(orderId);
            
            if(clientOrder.FollowUpArray != null)
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
            viewModel.TemplateList = db.DynamicTemplateService.GetAllTemplates().Where(x => x.TemplateTypeID == clientOrder.Template.TemplateTypeID).ToList();
            for (int i = 0; i < viewModel.TemplateList.Count(); i++)
            {
                clientOrderDatas.AddRange(db.ClientOrderService.GetClientOrderDataByTempOrderID(viewModel.TemplateList[i].ID, clientOrder.ID));
            }
            viewModel.ClientOrderData = clientOrderDatas;

            if (viewModel.ClientOrderData != null)
            {
                for (int i = 0; i < viewModel.ClientOrderData.Count; i++)
                {
                    viewModel.ClientOrderData[i].GetStringBuilder = new System.Text.StringBuilder();
                    if (viewModel.ClientOrderData[i].Genes != null)
                    {
                        for (int j = 0; j < viewModel.ClientOrderData[i].Genes.Length; j++)
                        {
                            viewModel.ClientOrderData[i].GetStringBuilder.Append((db.LookupService.GetLookUpByTypeName("Gene").Where(x => x.ID == Convert.ToInt32(viewModel.ClientOrderData[i].Genes[j])).FirstOrDefault().Name + " "));
                        }
                    }

                }
            }

            return PartialView("_OrderPDF", viewModel);
        }

        [HttpGet]
        public IActionResult UpdateOrder(int orderId)
        {
            ClientOrderViewModel viewModel = new ClientOrderViewModel();
            List<ClientOrderData> clientOrderDatas = new List<ClientOrderData>();
            
            ClientOrder clientOrder = db.ClientOrderService.GetClientOrderByID(orderId);
            //int? createdByID = Convert.ToInt32(Request.Cookies["ID"]);
            if (clientOrder.CreatedBy != 0)
            {
                var tempUser = db.UserService.GetAll().Where(x => x.ID == clientOrder.CreatedBy).FirstOrDefault();
                if (tempUser != null)
                    viewModel.CreatedByField = (tempUser.EnFirstName ?? "" + " " + tempUser.EnSecondName ?? "" + " " + tempUser.EnThirdName);
            }
            viewModel.ClientOrder = clientOrder;
            ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");
            ViewBag.TestTypes = db.TestTempService.GetTestTemps().Select(x => new SelectListItem
            {
                Text = (x.TestTempType?.Name + ">>" + x.SubTestTempType?.Name),
                Value = x.ID.ToString()
            }).ToList();
            ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
            ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");
            ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
            ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
            ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");

            viewModel.TemplateList = db.DynamicTemplateService.GetAllTemplates().Where(x => x.TemplateTypeID == clientOrder.Template.TemplateTypeID).ToList();
            
            for(int i = 0; i < viewModel.TemplateList.Count(); i++)
            {
                clientOrderDatas.AddRange(db.ClientOrderService.GetClientOrderDataByTempOrderID(viewModel.TemplateList[i].ID, clientOrder.ID));
            }

            viewModel.ClientOrderData = clientOrderDatas;

            ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
            ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
            ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);


            ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };
            ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };

            return PartialView("AddClientOrder", viewModel);
        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public IActionResult UpdateOrder(ClientOrderViewModel clientOrder, IFormFile file)
        {
            try { 
            if (clientOrder.ClientOrder.FollowUpArray != null)
                clientOrder.ClientOrder.FollowUp = string.Join(',', clientOrder.ClientOrder.FollowUpArray);

            ClientOrder order = clientOrder.ClientOrder;

            order.ClientOrderTests = new List<ClientOrderTest>();

            if (clientOrder.ClientOrder.TestTypeArray != null)
            {
                for (int i = 0; i < clientOrder.ClientOrder.TestTypeArray.Count(); i++)
                {
                    ClientOrderTest orderTest = new ClientOrderTest
                    {
                        TestTemplateID = Convert.ToInt32(clientOrder.ClientOrder.TestTypeArray[i]),
                        ClientOrderID = order.ID,
                        Done = false
                    };
                    order.ClientOrderTests.Add(orderTest);
                }
            }

            if (clientOrder.ClientOrderData != null)
            {
                for (int i = 0; i < clientOrder.ClientOrderData.Count(); i++)
                {
                    if (clientOrder.ClientOrderData[i].Genes != null)
                        clientOrder.ClientOrderData[i].GeneID = string.Join(',', clientOrder.ClientOrderData[i].Genes);
                }

                order.ClientOrderData = clientOrder.ClientOrderData;
            }

            var previousData = db.ClientOrderService.GetClientOrderDataByOrderID(order.ID);

            if(previousData != null)
            {
                db.ClientOrderService.RemoveClientOrderDataList(previousData.ToList());
            }
            
            db.ClientOrderService.RemoveClientOrderTestByOrderID(order.ID);


            db.ClientOrderService.UpdateClientOrder(order);

            var clientOrderList = db.ClientOrderService.GetClientOrderList();

            PagedData<ClientOrder> clientOrders = new PagedData<ClientOrder>
            {
                Data = (clientOrderList).Take(PageSize),
                NumberOfPages = Convert.ToInt32(Math.Ceiling((double)clientOrderList.Count() / PageSize))
            };

            return PartialView("_Index", clientOrders);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetTemplateData(int type)
        {
            if (type != 0)
            {
                ClientOrderViewModel viewModel = new ClientOrderViewModel();
                var tmpID = db.DynamicTemplateService.GetAllTemplates().Where(x => x.ID == type).FirstOrDefault();
                viewModel.TemplateList = db.DynamicTemplateService.GetAllTemplateByTempID(tmpID.TemplateTypeID).ToList() ;
                ViewBag.Doctors = db.UserService.GetByRole(3).Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
                ViewBag.Clients = db.UserService.GetPatients().Select(x => new { ID = x.ID, Name = x.EnFirstName + " " + x.EnThirdName });
                ViewBag.Templates = db.DynamicTemplateService.GetAllTemplates().Select(x => new { x.ID, x.TemplateType.Name }).DistinctBy(x => x.Name);
                ViewBag.TestTypes = db.TestTempService.GetTestTemps().Select(x => new SelectListItem
                {
                    Text = (x.TestTempType?.Name + ">>" + x.SubTestTempType?.Name),
                    Value = x.ID.ToString()
                }).ToList();
                ViewBag.EffectedGene = db.LookupService.GetLookUpByTypeName("Gene");
                ViewBag.FollowUp = db.LookupService.GetLookUpByTypeName("FollowUpType");
                ViewBag.Element = db.LookupService.GetLookUpByTypeName("Element");
                ViewBag.ConsumptionType = db.LookupService.GetLookUpByTypeName("ConsumptionType");
                ViewBag.FeederType = db.LookupService.GetLookUpByTypeName("FeederType");
                ViewBag.Result = new List<SelectListItem>
            {
              new SelectListItem{ Text="High", Value = "1" },
              new SelectListItem{ Text="Medium", Value = "2" },
              new SelectListItem{ Text="Low", Value = "3" }
            };
                ViewBag.LevelUp = new List<SelectListItem>
            {
              new SelectListItem{ Text="Increase", Value = "1" },
              new SelectListItem{ Text="Decrease", Value = "2" }
            };
                List<TemplateData> templateDatas = new List<TemplateData>();
                for(int i = 0; i < viewModel.TemplateList.Count(); i++)
                {
                    templateDatas.AddRange(db.DynamicTemplateService.GetTemplateDataID(viewModel.TemplateList[i].ID));
                }

                viewModel.TemplateDataList = templateDatas;

                return PartialView("_GetOrderTemplate", viewModel);
            }
            else
                return null;
        }

        [HttpPost]
        public IActionResult SearchOrder(Search model)
        {
            var searchList = db.ClientOrderService.SearchClientOrder(model).ToList(); 
            var pagedClients = new PagedData<ClientOrder>();

            pagedClients.Data = searchList.Take(PageSize);
            pagedClients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)searchList.Count() / PageSize));

            return PartialView("_Index", pagedClients);
        }

        [HttpGet]
        public IActionResult DeleteOrder(int orderId)
        {
            bool result = db.ClientOrderService.DeleteClientOrder(orderId);

            if(result == true)
            {
                var clientOrderList = db.ClientOrderService.GetClientOrderList();
                var clientOrders = new PagedData<ClientOrder>();
                clientOrders.Data = (clientOrderList).Take(PageSize);
                clientOrders.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)clientOrderList.Count() / PageSize));
                return PartialView("_Index",clientOrders);
            }
            else
            return null;
        }
        [HttpGet]
        public IActionResult Attachment(int userID)
        {
            var result = db.AttachmentService.GetById(userID, "ClientOrder");
            ViewBag.UserID = userID;
            ViewBag.TableName = "ClientOrder";
            return PartialView("_Attachment", result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachment(AttachmentModel attachment)
        {
            var model = new Data.Models.Attachment();
            if (attachment.File != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", attachment.File.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                   await attachment.File.CopyToAsync(stream);
                    model.AttachmentPath = "/uploaded/" + attachment.File.FileName;
                    model.AttachmentName = attachment.File.FileName;
                }
            }
            model.UserID = attachment.ID;
            model.TableName = attachment.TableName;
            db.AttachmentService.Add(model);
            return Ok(true);
        }

        public class AttachmentModel
        {
            public IFormFile File { get; set; }
            public int ID { get; set; }
            public string TableName { get; set; }
        }
    }
}