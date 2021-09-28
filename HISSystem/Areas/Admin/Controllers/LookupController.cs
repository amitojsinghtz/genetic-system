using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using HISSystem.Areas.Admin.Models;
using Data.Helpers;
using Data.Models;
using Service.UnitOfServices;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LookupController : Controller
    {
        public const int PageSize = 15;
        private readonly IUnitOfService db;
        public LookupController(IUnitOfService db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            List<Lookup> lookupTry = new List<Lookup>();

            var lookupList = db.LookupService.GetLookups().Select(x => x.Type).Distinct();
            ViewBag.Lookups = lookupList;
            
            return View(lookupTry);
        }

        public IActionResult DevExpressDemo()
        {
            return View();
        }

        public IActionResult GetLookupIndex(string lookupName)
        {
            LookupIndexModel lookupIndex = new LookupIndexModel();
            lookupIndex.PagedLookups = new PagedData<Lookup>();
            lookupIndex.Lookup = new Lookup();
            ViewBag.LookupName = lookupName;

            var xray = db.LookupService.GetLookups().Where(x => x.IsActive == true).ToList();

            var result = xray.Where(x => x.Type.Equals(lookupName)).ToList();

            lookupIndex.PagedLookups.Data = result.OrderBy(x => x.ID).Take(PageSize).ToList();
            lookupIndex.PagedLookups.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));

            if (lookupName == "Floor")
            {
                foreach (var item in lookupIndex.PagedLookups.Data)
                {
                    item.Type = xray.Where(x => x.ID == Convert.ToInt32(item.ParentId)).FirstOrDefault().Name.ToString();
                }
            }
            else if (lookupName == "Room")
            {
                foreach (var item in lookupIndex.PagedLookups.Data)
                {
                    Lookup lookup = new Lookup();

                    lookup = xray.Where(x => x.ID == Convert.ToInt32(item.ParentId)).FirstOrDefault();

                    string grandParent = xray.Where(x => x.ID == Convert.ToInt32(lookup.ParentId)).FirstOrDefault().Name.ToString();

                    item.Type = grandParent + "/" + lookup.Name;
                }
            }

            return PartialView("_Index", lookupIndex);
        }
 
        [HttpGet]
        public IActionResult _GetLookupIndex(string lookupName, int page)
        {
            LookupIndexModel lookupIndex = new LookupIndexModel();
            lookupIndex.PagedLookups = new PagedData<Lookup>();
            lookupIndex.Lookup = new Lookup();
            ViewBag.LookupName = lookupName;

            var xray = db.LookupService.GetLookups().Where(x => x.IsActive == true).ToList();
            var result = xray.Where(x => x.Type.Equals(lookupName)).ToList();

            //var result = xray.Where(x => x.Type.Contains(lookupName)).ToList();

            lookupIndex.PagedLookups.Data = result.OrderBy(x => x.ID).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
            lookupIndex.PagedLookups.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));

            

            if (lookupName == "Floor")
            {
                foreach (var item in lookupIndex.PagedLookups.Data)
                {
                    item.Type = xray.Where(x => x.ID == Convert.ToInt32(item.ParentId)).FirstOrDefault().Name.ToString();
                }
            }
            else if (lookupName == "Room")
            {
                foreach (var item in lookupIndex.PagedLookups.Data)
                {
                    Lookup lookup = new Lookup();

                    lookup = xray.Where(x => x.ID == Convert.ToInt32(item.ParentId)).FirstOrDefault();

                    string grandParent = xray.Where(x => x.ID == Convert.ToInt32(lookup.ParentId)).FirstOrDefault().Name.ToString();

                    item.Type = grandParent + "/" + lookup.Name;
                }
            }


            return PartialView("_Index", lookupIndex);
        }
  
        [HttpGet]
        public IActionResult Add(string lookupName)
        {
            switch (lookupName)
            {
                case "Floor":
                    ViewBag.Buildings = db.LookupService.GetLookUpByTypeName("Building");
                    break;

                case "Room":
                    ViewBag.Buildings = db.LookupService.GetLookUpByTypeName("Building");
                    ViewBag.Floors = db.LookupService.GetLookUpByTypeName("Floor");
                    break;
            }

            int result = db.LookupService.GetLookUpByTypeName(lookupName).Max(x => x.TypeID);
            Lookup lookup = new Lookup();
            lookup.TypeID = (++result);
            lookup.Type = lookupName;
            ViewBag.lookupName = lookupName;

            return PartialView("_Add", lookup);
        }

        [HttpGet]
        public IActionResult AddMaster()
        {
            Lookup lookup = new Lookup();
            lookup.TypeID = 1;
            lookup.Type = "";
            return PartialView("_AddMaster", lookup);
        }
        [HttpPost]
        public IActionResult AddNewMaster(Lookup lookup)
        {
            try { 
            lookup.AddedDate = DateTime.UtcNow;
            lookup.IsActive = true;
            var name = lookup.Name;
            var type = lookup.Type;
            lookup.Type = type.Trim();
            lookup.Name= name.Trim();
            db.LookupService.InsertUser(lookup);
            return Json(true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        [Area("Admin")]
        public IActionResult GetFloor(string BuildingID)
        {
            List<Lookup> floorList = new List<Lookup>();
            List<Floor> Floors = new List<Floor>();
            floorList = db.LookupService.GetLookups().Where(x => x.ParentId == BuildingID).ToList();

            foreach (var item in floorList)
            {
                Floor floor = new Floor();
                floor.ID = item.ID;
                floor.Name = item.Name;
                Floors.Add(floor);
            }

            Floors.Insert(0, new Floor { ID = 0, Name = "Select" });

            return Json(new SelectList(Floors, "ID", "Name"));
        }

        [HttpPost]
        public IActionResult Add(Lookup lookup)
        {
            lookup.IsActive = true;
            var name = lookup.Name;
            lookup.Name = name.Trim();
            lookup.AddedDate = System.DateTime.Now;
            lookup.ModifiedDate = System.DateTime.Now;
            db.LookupService.InsertUser(lookup);

            return Json(true);

            //var lookupName = name;
            //LookupIndexModel lookupIndex = new LookupIndexModel();
            //lookupIndex.PagedLookups = new PagedData<Lookup>();
            //lookupIndex.Lookup = new Lookup();
            //ViewBag.LookupName = lookupName;

            //var xray = db.LookupService.GetLookups().Where(x => x.IsActive == true).ToList();

            //var result = xray.Where(x => x.Type.Equals(lookupName)).ToList();

            //lookupIndex.PagedLookups.Data = result.OrderBy(x => x.ID).Take(PageSize).ToList();
            //lookupIndex.PagedLookups.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));

            //if (lookupName == "Floor")
            //{
            //    foreach (var item in lookupIndex.PagedLookups.Data)
            //    {
            //        item.Type = xray.Where(x => x.ID == Convert.ToInt32(item.ParentId)).FirstOrDefault().Name.ToString();
            //    }
            //}
            //else if (lookupName == "Room")
            //{
            //    foreach (var item in lookupIndex.PagedLookups.Data)
            //    {
            //        Lookup lookups = new Lookup();

            //        lookups = xray.Where(x => x.ID == Convert.ToInt32(item.ParentId)).FirstOrDefault();

            //        string grandParent = xray.Where(x => x.ID == Convert.ToInt32(lookups.ParentId)).FirstOrDefault().Name.ToString();

            //        item.Type = grandParent + "/" + lookups.Name;
            //    }
            //}

            //return PartialView("_Index", lookupIndex);

        }

        public IActionResult Delete(int lookupID)
        {
            Lookup lookup = new Lookup();
            lookup = db.LookupService.GetLookup(lookupID);
            lookup.IsActive = false;
            db.LookupService.UpdateUser(lookup);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetLookup(int lookupID)
        {
            Lookup lookup = new Lookup();
            lookup = db.LookupService.GetLookup(lookupID);

            return PartialView("_GetLookup", lookup);
        }

        [HttpPost]
        public IActionResult GetLookup(Lookup lookup)
        {
            lookup.ModifiedDate = System.DateTime.Now;
            lookup.IsActive = true;
            db.LookupService.UpdateUser(lookup);

            return Json(true);
        }

        [HttpGet]
        public IActionResult SearchMasterLookup(string searchQuery)
        {
            List<Lookup> lookupTry = new List<Lookup>();

            if (searchQuery != null)
            {
                var lookupList = db.LookupService.GetLookupList().Where(x => x.Type.StartsWith(searchQuery, StringComparison.OrdinalIgnoreCase) && x.IsActive==true).Select(x => x.Type).Distinct().ToList();
                ViewBag.Lookups = lookupList;
            }
            else
            {
                var lookupList = db.LookupService.GetLookups().Where(x => x.IsActive == true).Select(x => x.Type).Distinct().ToList();
                ViewBag.Lookups = lookupList;
            }    
            

            return PartialView("search", lookupTry);
        }

        [HttpGet]
        public IActionResult SearchLookupDetails(string searchString, string lookupName)
        {
            var lookupList = db.LookupService.GetLookups().ToList();

            var searchResult = lookupList;

            if (searchString != null && lookupName != null)
            {
                searchResult = lookupList.Where(x => x.Type == lookupName && x.Name.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            } 
            else if(searchString == null && lookupName != null)
            {
                searchResult = lookupList.Where(x => x.Type == lookupName).ToList();
            }

            LookupIndexModel lookupIndex = new LookupIndexModel();
            lookupIndex.PagedLookups = new PagedData<Lookup>();
            lookupIndex.Lookup = new Lookup();
            ViewBag.LookupName = lookupName;

            lookupIndex.PagedLookups.Data = searchResult.OrderBy(x => x.ID).Take(PageSize).ToList();
            lookupIndex.PagedLookups.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)searchResult.Count() / PageSize));

            return PartialView("_SearchDetail", lookupIndex);
        }
        public IActionResult CustomValidation(string type)
        {

            return View();
        
        
        }

    }
}