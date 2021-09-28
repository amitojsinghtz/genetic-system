using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using HISSystem.Filters;
using HISSystem.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.UnitOfServices;
using System.Collections.Generic;
using System.Linq;

namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private IUnitOfService db;

        public DashboardController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }

        [CustomAuth(Page.Dashboard, ActionButton.Index)]
        public IActionResult Index()
        {
            DashboardVM dashboardVM = new DashboardVM
            {
                PendingOrders = db.ClientOrderService.GetPendingOrderForDashboard(),
                AllOrders = db.ClientOrderService.GetAllOrderForDashboard(),
                TestOrders = db.ClientOrderService.GetTestOrderForDashboard()
            };

            return View(dashboardVM);
        }

        //public IActionResult _Index()
        //{
        //    DashboardVM dashboardVM = new DashboardVM
        //    {
        //        PendingOrders = db.ClientOrderService.GetPendingOrderForDashboard(),
        //        AllOrders = db.ClientOrderService.GetAllOrderForDashboard(),
        //        TestOrders = db.ClientOrderService.GetTestOrderForDashboard()
        //    };

        //    return PartialView("_Index", dashboardVM);
        //}

        public JsonResult GetEvents()
        {
            var reminderList = db.ReminderService.GetAllReminders();

            JsonResult Data = new JsonResult(new { Data = reminderList });

            return Json(new { Data = reminderList });

        }

        [HttpPost]
        public JsonResult SaveEvent(Reminder reminder)
        {
            bool status = false;

            if (reminder.ID > 0)
            {
                var oldReminder = db.ReminderService.GetReminderByID(reminder.ID);

                if (oldReminder != null)
                {
                    oldReminder.Subject = reminder.Subject;
                    oldReminder.Start = reminder.Start;
                    oldReminder.End = reminder.End;
                    oldReminder.Description = reminder.Description;
                    oldReminder.IsFullDay = reminder.IsFullDay;
                    oldReminder.ThemeColor = reminder.ThemeColor;

                    db.ReminderService.Update(oldReminder);
                }
            }
            else
                db.ReminderService.Add(reminder);

            status = true;
            JsonResult Data = new JsonResult(new { status });

            return Data;
        }

        [HttpPost]
        public JsonResult DeleteEvent (int eventID)
        {
            bool status = false;

            var evnt = db.ReminderService.GetReminderByID(eventID);
            
            if(evnt != null)
            {
                db.ReminderService.Remove(evnt);
                status = true;
            }

            JsonResult Data = new JsonResult(new { status });

            return Data;
        }

        public IActionResult SchedulerExp()
        {
            SchedulerVM schedulerVM = new SchedulerVM();

            schedulerVM.AppointmentExps = db.AppointmentExpService.GetAppointments();

            schedulerVM.Users = db.UserService.GetPatients().Select(x => new CustDropDown
            {
                ID = x.ID,
                Name = (x.EnFirstName ?? "" + " " + x.EnSecondName ?? "" + " " + x.EnThirdName ?? "")
            }).ToList();

            schedulerVM.Genes = db.DynamicTemplateService.GetAllTemplates().ToList().Select(x => new CustDropDown
            {
                ID = x.ID,
                Name = (x.TemplateType?.Name + ">>" + x.SubTemplateType?.Name)
            }).ToList();

            schedulerVM.TestTemps = db.TestTempService.GetTestTemps().Select(x => new CustDropDown
            {
                ID = x.ID,
                Name = (x.TestTempType?.Name + ">>" + x.SubTestTempType?.Name)
            }).ToList();

            return View(schedulerVM);
        }

        [HttpGet]
        public IActionResult SearchOrders(int orderType, string searchKey)
        {
            if (orderType == 397 || orderType == 398)
            {
                DashboardVM dashboardVM = new DashboardVM
                {
                    AllOrders = db.ClientOrderService.SearchClientOrderByType(orderType, searchKey).ToList()

                };

                return PartialView ("_AllPenOrder", dashboardVM);
            }
            else
            {
                DashboardVM dashboardVM = new DashboardVM
                {
                    TestOrders = db.ClientOrderService.SearchClientOrderByType(orderType, searchKey).ToList()

                };

                return PartialView("_TestOrder", dashboardVM);
            }
            }
        }
    }
