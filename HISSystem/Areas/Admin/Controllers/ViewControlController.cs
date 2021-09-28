using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using Data.DTO;
using System.Linq;
using System;
using System.Collections.Generic;

namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ViewControlController : Controller
    {
        private readonly IUnitOfService _service;
        public ViewControlController(IUnitOfService _service)
        {
            this._service = _service;
        }

        public IActionResult Index()
        {
            MenuAcessModel data = new MenuAcessModel();
            data.AllRoles = _service.RoleService.GetAll();
            // var result = _service.AccessPermissionService.GetAll();
            data.AllMenu = _service.ViewControlService.GetAll();

            return View(data);
        }

        public IActionResult AccessPermission()
        {
            ViewBag.Role = _service.RoleService.GetAll();
            var result = _service.AccessPermissionService.GetAll();
            return PartialView(result);
        }

        public IActionResult Add()
        {
            return PartialView();
        }
        ///[Produces("application/json")]
        [HttpGet]
        public IActionResult Viewaccesscontroldata()
        {
            var AllData = _service.AccessPermissionService.GetAll().Select(c => new AccessPermission
            {
                AddedDate=c.AddedDate,
                ID=c.ID,
                IsActive=c.IsActive,
                IsEnabled=c.IsEnabled,
                ModifiedDate=c.ModifiedDate,
                RoleID=c.RoleID,
                ViewControlID=c.ViewControlID,
            }).ToList();
            return Json(AllData);

        }
        [HttpPost]
        public IActionResult Add(ViewControl model)
        {
            _service.ViewControlService.Add(model);
            return RedirectToAction("Menu");
        }

        public IActionResult Update(int id)
        {
            var result = _service.ViewControlService.GeById(id);
            return PartialView(result);
        }

        [HttpPost]
        public IActionResult Update(ViewControl model)
        {
            model.ModifiedDate = DateTime.UtcNow;
            _service.ViewControlService.Update(model);
            return RedirectToAction("Menu");
        }

        [HttpPost]
        public IActionResult UpdateAccessPermission(int id, bool status)
        {
            var model = _service.AccessPermissionService.GetById(id);
            model.IsEnabled = status;
            _service.AccessPermissionService.Update(model);
            return Json(true);
        }
        [HttpPost]
        public IActionResult SaveViewControlAccess(AccessPermission MenuAccessData)
        {

            MenuAccessData.IsActive = true;
            MenuAccessData.IsEnabled = true;
            MenuAccessData.AddedDate= DateTime.UtcNow;
            var status = _service.AccessPermissionService.Add(MenuAccessData);
            return Json(true);
        }
        public IActionResult RemoveViewControlAccess(AccessPermission MenuAccessData)
        {
            _service.AccessPermissionService.Delete(MenuAccessData);
            return Json(true);
        }
        public IActionResult Menu()
        {
            IEnumerable<ViewControl> data = new List<ViewControl>();
          data=  _service.ViewControlService.GetAll();
            return View(data);
        }
        public IActionResult Delete(long id)
        {
            _service.ViewControlService.Delete(id);
            return RedirectToAction("Menu");
        }
    }
}