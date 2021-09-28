using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using HISSystem.Filters;
using HISSystem.Helper;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;

namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SystemLogController : Controller
    {
        private readonly IUnitOfService db;
        public SystemLogController(IUnitOfService db)
        {
            this.db = db;
        }

        [CustomAuth(Page.SystemLog, ActionButton.Index)]
        public IActionResult Index()
        {
            List<LogTable> result = new List<LogTable>();
            return View(result);
        }

        public IActionResult GetLogDetails(int tableID)
        {
            var data = db.LogTableService.GetDetails(tableID);
            return View(data);
        }
    }
}