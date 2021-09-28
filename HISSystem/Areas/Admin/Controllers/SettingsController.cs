using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using GeneticSystem.Helper;
using Data.Models;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly IUnitOfService service;

        public SettingsController(IUnitOfService db)
        {
            this.service = db;
        }
        public IActionResult Index()
        {
            ImagesViewModel model = new ImagesViewModel();
            ViewBag.ImagesForList = service.LookupService.GetLookUpByTypeName("ImageType");

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ImagesViewModel model = new ImagesViewModel();
            ViewBag.ImagesForList = service.LookupService.GetLookUpByTypeName("ImageType");

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ImagesViewModel model, IFormFile file)
        {
            if (file == null)
                return null;

            CustImage saveModel = new CustImage
            {
                ImageType = model.ImageType
            };

            ImageToByte fConvert = new ImageToByte();
            saveModel.ImagePath = fConvert.GetFileBits(file);
            //saveModel.CompanyId = service.CompanyProfileService.GetAll().FirstOrDefault().ID;
            service.CompanyProfileService.AddImage(saveModel);
            var logo = service.CompanyProfileService.GetImageByTypeId(181);
            string something = String.Format("data:image/gif;base64,{0}", logo);
            HttpContext.Session.SetString("logo", something);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public string GetImage(int typeId)
        {
            var Image = service.CompanyProfileService.GetImageByTypeId(typeId);
            if (Image != null)
            {
                string baseImg = String.Format("data:image/gif;base64,{0}", Image);
                return baseImg;
            }
            else
                return null;
        }
    }
}