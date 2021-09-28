using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using GeneticSystem.Helper;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicalDictionaryController : Controller
    {
        public const int PageSize = 5;
        private readonly IUnitOfService db;
        private readonly IHostingEnvironment _appEnvironment;
        public MedicalDictionaryController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var medicalDictionaryList = new PagedData<MedicalDictionary>();

            var getmedicalDictionaryList = db.MedicalDictionaryService.GetMedicalDictionary().OrderByDescending(x=>x.CreatedOn).ToList();

            ViewBag.City = db.CityService.GetAll().Where(x=>x.ID!=null).ToList();
            var getLookupsList = db.LookupService.GetLookups().ToList();
            ViewBag.Types = getLookupsList.Where(x => x.Type == "Type").ToList();

            //medicalDictionaryList.Data = getmedicalDictionaryList;
            medicalDictionaryList.Data = (getmedicalDictionaryList).Take(PageSize);
            medicalDictionaryList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)getmedicalDictionaryList.Count() / PageSize));
            return View(medicalDictionaryList);
        }

        [HttpGet]
        public IActionResult GetMedicalDictionary()
        {
            var medicalDictionaryList = new PagedData<MedicalDictionary>();

            var getmedicalDictionaryList = db.MedicalDictionaryService.GetMedicalDictionary().OrderByDescending(x=>x.CreatedOn).ToList();
            
            var getLookupsList = db.LookupService.GetLookups().ToList();
            ViewBag.Types = getLookupsList.Where(x => x.Type == "Type").ToList();

            medicalDictionaryList.Data = getmedicalDictionaryList;
            medicalDictionaryList.Data = (getmedicalDictionaryList).Take(PageSize);
            medicalDictionaryList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)getmedicalDictionaryList.Count() / PageSize));
            return View(medicalDictionaryList);
        }

        [HttpGet]
        public IActionResult AddMedicalDictionary()
        {
            var getLookupsList = db.LookupService.GetLookups().ToList();
            ViewBag.Types = getLookupsList.Where(x => x.Type == "Type").ToList();
            ViewBag.Specialty = getLookupsList.Where(x => x.Type == "Specialty").ToList();
            ViewBag.Class = getLookupsList.Where(x => x.Type == "Class").ToList();
            ViewBag.City = db.CityService.GetAll().ToList();

            return View();
        }
        [HttpPost]
        public IActionResult AddMedicalDictionary(MedicalDictionaryViewModel medicalDictionaryViewModel, IFormFile file)
        {
            var addmedicalDictionary = new MedicalDictionary();
            var currentDate = DateTime.UtcNow;

            addmedicalDictionary.ID = 0;
            addmedicalDictionary.EnName = medicalDictionaryViewModel.EnName;
            addmedicalDictionary.ArName = medicalDictionaryViewModel.ArName;
            addmedicalDictionary.Email = medicalDictionaryViewModel.Email;
            addmedicalDictionary.Address = medicalDictionaryViewModel.Address;
            addmedicalDictionary.CityID = medicalDictionaryViewModel.CityID;
            addmedicalDictionary.CreatedOn = currentDate;
            addmedicalDictionary.UpdatedOn = currentDate;
            addmedicalDictionary.Mobile = medicalDictionaryViewModel.Mobile;
            addmedicalDictionary.Mobile2 = medicalDictionaryViewModel.Mobile2;
            addmedicalDictionary.Telephone= medicalDictionaryViewModel.Telephone;
            addmedicalDictionary.Telephone2 = medicalDictionaryViewModel.Telephone2;
            addmedicalDictionary.TypeID = medicalDictionaryViewModel.TypeID;
            addmedicalDictionary.IsActive = true;
            addmedicalDictionary.ClassID = medicalDictionaryViewModel.ClassID;
            addmedicalDictionary.Latitude= medicalDictionaryViewModel.Latitude;
            addmedicalDictionary.Longitude= medicalDictionaryViewModel.Longitude;

            //if (file != null)
            //{
            //    var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", file.FileName);

            //    using (var stream = new FileStream(path, FileMode.Create))
            //    {
            //        file.CopyToAsync(stream);
            //        addmedicalDictionary.ImagePath = "/uploaded/" + file.FileName;
            //    }
            //}


            if (file!=null)
            {

                ImageToByte fConvert = new ImageToByte();
                byte[] bytedata = fConvert.GetFileBits(file);
                addmedicalDictionary.ImagePath = bytedata;
                //saveModel.CompanyId = service.CompanyProfileService.GetAll().FirstOrDefault().ID;
                //service.CompanyProfileService.AddImage(saveModel);
                //var logo = service.CompanyProfileService.GetImageByTypeId(181);
                //string something = String.Format("data:image/gif;base64,{0}", logo);
            }

            if(medicalDictionaryViewModel.Specialty != null)
            {

            
            var medicalDictionarySpecialityList = medicalDictionaryViewModel.Specialty.Select((p, i) => new MedicalDictionarySpecialty
            {
                ID = 0,
                IsActive = true,
                UpdatedOn = currentDate,
                CreatedOn = currentDate,
                MedicalDictionaryID = 0,
                SpecialtyID = int.Parse(p)
            }).ToList();
                
                addmedicalDictionary.MedicalDictionarySpecialty = medicalDictionarySpecialityList;
            }

            var addmedicalDictionaryData = db.MedicalDictionaryService.AddMedicalDictionary(addmedicalDictionary);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteMedicalDictionary(int id)
        {
            db.MedicalDictionaryService.DeleteMedicalDictionary(id);
            return Json(true);
        }

        public IActionResult UpdateMedicalDictionary(int id)
        {
            var getLookupsList = db.LookupService.GetLookups().ToList();

            ViewBag.Types = getLookupsList.Where(x => x.Type == "Type").ToList();
            ViewBag.Specialty = getLookupsList.Where(x => x.Type == "Specialty").ToList();
            ViewBag.Class = getLookupsList.Where(x => x.Type == "Class").ToList();
            ViewBag.City = db.CityService.GetAll().ToList();
            var currentDate = DateTime.UtcNow;

            var model = db.MedicalDictionaryService.GetMedicalDictionaryById(id);
            model.MedicalDictionarySpecialty.Where(x => x.IsActive == true);
            if (model.ImagePath!=null)
            {
                var getImageString = Convert.ToBase64String(model.ImagePath);
            //    var logo = service.CompanyProfileService.GetImageByTypeId(181);
            //string something = String.Format("data:image/gif;base64,{0}", logo);
            //HttpContext.Session.SetString("logo", something);
                model.ImageString = string.Format("data:image/gif;base64,{0}",getImageString);
            }
            else
            {
                model.ImageString = null;
            }
            

            //model.Specialty = new string[model.MedicalDictionarySpecialty.Count()];

            //model.Specialty = new string[1];
            //model.Specialty[0] = "0";

            //for (var i = 0; i < model.MedicalDictionarySpecialty.Count(); i++)
            //{
            //    model.Specialty[i] = model.MedicalDictionarySpecialty[i].SpecialtyID.ToString();
            //}

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateMedicalDictionary(MedicalDictionaryViewModel medicalDictionaryViewModel, IFormFile file)
        {
            var addmedicalDictionary = new MedicalDictionary();
            var currentDate = DateTime.UtcNow;

            addmedicalDictionary.ID = medicalDictionaryViewModel.ID;
            addmedicalDictionary.EnName = medicalDictionaryViewModel.EnName;
            addmedicalDictionary.ArName = medicalDictionaryViewModel.ArName;
            addmedicalDictionary.Email = medicalDictionaryViewModel.Email;
            addmedicalDictionary.Address = medicalDictionaryViewModel.Address;
            addmedicalDictionary.CityID = medicalDictionaryViewModel.CityID;
            addmedicalDictionary.CreatedOn = currentDate;
            addmedicalDictionary.UpdatedOn = currentDate;
            addmedicalDictionary.Mobile = medicalDictionaryViewModel.Mobile;
            addmedicalDictionary.Mobile2 = medicalDictionaryViewModel.Mobile2;
            addmedicalDictionary.Telephone = medicalDictionaryViewModel.Telephone;
            addmedicalDictionary.Telephone2 = medicalDictionaryViewModel.Telephone2;
            addmedicalDictionary.TypeID = medicalDictionaryViewModel.TypeID;
            addmedicalDictionary.IsActive = true;
            addmedicalDictionary.ClassID = medicalDictionaryViewModel.ClassID;
            addmedicalDictionary.Latitude = medicalDictionaryViewModel.Latitude;
            addmedicalDictionary.Longitude = medicalDictionaryViewModel.Longitude;

            if (file!=null)
            {
                ImageToByte fConvert = new ImageToByte();
                byte[] bytedata = fConvert.GetFileBits(file);
                addmedicalDictionary.ImagePath = bytedata;
            }
            else
            {
                addmedicalDictionary.ImagePath = medicalDictionaryViewModel.ImagePath;
            }

            if(medicalDictionaryViewModel.Specialty != null)
            {
                db.MedicalDictionaryService.DeleteMedicalDictionarySpecialty(addmedicalDictionary.ID);

                var medicalDictionarySpecialityList = medicalDictionaryViewModel.Specialty.Select((p, i) => new MedicalDictionarySpecialty
                {
                    ID = 0,
                    IsActive = true,
                    UpdatedOn = currentDate,
                    CreatedOn = currentDate,
                    MedicalDictionaryID = 0,
                    SpecialtyID = int.Parse(p)
                }).ToList();

                addmedicalDictionary.MedicalDictionarySpecialty = medicalDictionarySpecialityList;

            }
            var addmedicalDictionaryData = db.MedicalDictionaryService.UpdatMedicalDictionary(addmedicalDictionary);


            return RedirectToAction("Index");
        }

        //Added this 228
        [HttpGet]
        public IActionResult SearchEnName(int page, string searchstringen, string searchstringar, int cityID, int typeID)
        {
            var searchList = new PagedData<MedicalDictionary>();
            var medicalDictionaryList = db.MedicalDictionaryService.GetMedicalDictionary().Where(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(searchstringen))
            {
                medicalDictionaryList = medicalDictionaryList.Where(x => x.EnName != null && x.EnName.Contains(searchstringen.ToString(), StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(searchstringar))
            {
                medicalDictionaryList = medicalDictionaryList.Where(x => x.ArName != null && x.ArName.Contains(searchstringar, StringComparison.OrdinalIgnoreCase));
            }
            if (cityID != 0)
            {
                medicalDictionaryList = medicalDictionaryList.Where(x => x.CityID != null && x.CityID == cityID);
            }
            if (typeID != 0)
            {
                medicalDictionaryList = medicalDictionaryList.Where(x => x.TypeID != 0 && x.TypeID == typeID); ;
            }


            searchList.Data = (medicalDictionaryList).Skip(PageSize * (page - 1)).Take(PageSize);
            searchList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)medicalDictionaryList.Count() / PageSize));

            ViewBag.City = db.CityService.GetAll().ToList();

            return PartialView("GetMedicalDictionary", searchList);
        }
    }
}