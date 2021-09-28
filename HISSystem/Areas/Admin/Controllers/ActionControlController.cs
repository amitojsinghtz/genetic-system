using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using Data.DTO;


namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActionControlController : Controller
    {
        private readonly IUnitOfService _service;
        public ActionControlController(IUnitOfService _service)
        {
            this._service = _service;
        }

        public IActionResult Index()
        {
            ViewBag.Role = _service.RoleService.GetAll();
            var result = _service.ActionPermissionService.GetAll();
            return View(result);
        }

        public IActionResult Add()
        {
            return PartialView();
        }
        public IActionResult Delete(long id)
        {
            _service.ActionControlService.Delete(id);
            return RedirectToAction("ActionList");
        }
        [HttpPost]
        public IActionResult Add(ActionControl model)
        {
            _service.ActionControlService.Add(model);
            return RedirectToAction("ActionList");
        }

        public IActionResult Update(int id)
        {
            var result = _service.ActionControlService.GetById(id);
            return PartialView(result);
        }

        [HttpPost]
        public IActionResult Update(ActionControl model)
        {
            _service.ActionControlService.Update(model);
            return RedirectToAction("ActionList");
        }
        public ActionResult CheckActionAccess(int id)

        {
            MenuAcessModel data = new MenuAcessModel();
            data.AllRoles = _service.RoleService.GetAll();
            // var result = _service.AccessPermissionService.GetAll();
            data.AllMenu = _service.ViewControlService.GetAll();
            var allActionPage = _service.ActionControlService.GetAll();
            var AllMenuAccess = _service.ActionPermissionService.GetByRoleID(id);
            // MenuAcessModel Data = new MenuAcessModel();

            data.AllActionPage = allActionPage;
            data.AllActionPermission = AllMenuAccess;
            return PartialView(data);
        }
        public IActionResult ActionPermission()
        {
            ViewBag.Role = _service.RoleService.GetAll();
            ViewBag.View = _service.ViewControlService.GetAll();
            var result = _service.ActionPermissionService.GetAll();
            return View(result);
        }

        [HttpPost]
        public IActionResult UpdateActionPermission(int id, bool status)
        {
            var model = _service.ActionPermissionService.GetById(id);
            model.IsEnabled = status;
            _service.ActionPermissionService.Update(model);
            return Json(true);
        }

        public IActionResult AddPermission()
        {
            ViewBag.Page = _service.ViewControlService.GetAll();
            ViewBag.Role = _service.RoleService.GetAll();
            ViewBag.Action = _service.ActionControlService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult AddPermission(ActionPermission model)
        {
            model.IsActive = true;
            model.AddedDate = DateTime.UtcNow;
            model.IsEnabled = true;
            _service.ActionPermissionService.Add(model);
            return RedirectToAction("ActionPermission");
        }
        public IActionResult AllActionAccessData(int id)
        {
            var AllData = _service.ActionPermissionService.GetActionAccessByRoleID(id);
            return Json(AllData);
        }
        public IActionResult SaveActionAccess(ActionPermission ActionAccessData)
        {
            ActionAccessData.IsActive = true;
            ActionAccessData.IsEnabled = true;
            var status = _service.ActionPermissionService.Add(ActionAccessData);
            return Json(true);
        }
        public IActionResult RemoveActionAccess(ActionPermission ActionAccessData)
        {
            _service.ActionPermissionService.Delete(ActionAccessData);
            return Json(true);
        }
        public IActionResult ActionList()
        {
            var result = _service.ActionControlService.GetAll().Where(x=>x.IsActive==true);
            return View(result);
        }
      
      
    }
}