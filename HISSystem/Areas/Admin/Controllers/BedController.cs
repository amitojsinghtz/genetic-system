using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.IServices;
using Data.Models;
using X.PagedList;
using Data.Helpers;
using HISSystem.Areas.Admin.Models;
using HISSystem.Filters;
using Service.UnitOfServices;
//using System.Web.Mvc;

namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [ClaimRequirementAttribute("Admin", "1")]
    public class BedController : Controller
    {
        public const int PageSize = 3;
        private readonly IUnitOfService db;
        public BedController(IUnitOfService db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            BedIndexModel bedIndexModel = new BedIndexModel();
            bedIndexModel.PagedBed = new PagedData<Bed>();
            Bed bedlist = new Bed();
            bedIndexModel.bed = new Bed();

            var result = db.BedService.GetAll().Where(x => x.IsActive == true).ToList();
            var lookups = db.LookupService.GetLookups().ToList();
            var floors = db.FloorService.GetAll().ToList();
            var rooms = db.RoomService.GetAll().ToList();

            ViewBag.Building = db.LookupService.GetLookups().Where(x => x.Type == "Building").ToList();

            //ViewBag.Department = lookups.Where(x => x.Type.Contains("Department")).ToList();
            ViewBag.Status = lookups.Where(x => x.Type.Contains("Status")).ToList();
            bedIndexModel.PagedBed.Data = result.OrderByDescending(x => x.AddedDate).Take(PageSize).ToList();
            bedIndexModel.PagedBed.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));
            return View(bedIndexModel);
        }

        [Area("Admin")]
        public IActionResult GetFloor(int BuildingID)
        {
            List<Floor> floorList = new List<Floor>();

            if (BuildingID != 0)
            {
                var result = db.LookupService.GetLookups().Where(x => Convert.ToInt32(x.ParentId) == BuildingID).ToList();

                foreach (var item in result)
                {
                    Floor Floor = new Floor();
                    Floor.ID = item.ID;
                    Floor.Name = item.Name;
                    floorList.Add(Floor);
                }
            }


            floorList.Insert(0, new Floor { ID = 0, Name = "Select" });

            return Json(new SelectList(floorList, "ID", "Name"));
        }

        [Area("Admin")]
        public IActionResult GetRoom(int FloorID)
        {
            List<Room> roomList = new List<Room>();

            if (FloorID != 0)
            {
                var result = db.LookupService.GetLookups().Where(x => Convert.ToInt32(x.ParentId) == FloorID).ToList();
                foreach (var item in result)
                {
                    Room Room = new Room();

                    Room.ID = item.ID;
                    Room.Name = item.Name;

                    roomList.Add(Room);
                }
            }

            roomList.Insert(0, new Room { ID = 0, Name = "Select" });

            return Json(new SelectList(roomList, "ID", "Name"));
        }

        [Area("Admin")]
        public IActionResult DynamicSearch(DropDownSearchModel dropDownSearchModel)
        {
            var bed = db.BedService.GetAll().ToList();

            var searchList = bed.Where(x =>
            (dropDownSearchModel.BuildingID == 0 || (x.BuildingID.Equals(dropDownSearchModel.BuildingID)))
            && (dropDownSearchModel.FloorID == 0 || (x.FloorID.Equals(dropDownSearchModel.FloorID)))
            && (dropDownSearchModel.RoomID == 0 || (x.RoomID.Equals(dropDownSearchModel.RoomID)))).ToList();

            BedIndexModel bedIndexModel = new BedIndexModel();
            bedIndexModel.PagedBed = new PagedData<Bed>();
            Bed bedlist = new Bed();
            bedIndexModel.bed = new Bed();

            var result = bed.Where(x => x.IsActive == true).ToList();
            //var lookups = db.LookupService.GetLookups().ToList();
            //var floors = db.FloorService.GetAll().ToList();
            //var buildings = db.FloorService.GetAll().ToList();
            //var rooms = db.RoomService.GetAll().ToList();

            //ViewBag.Department = lookups.Where(x => x.Type.Contains("Department")).ToList();
            //ViewBag.Floor = floors.ToList();
            //ViewBag.Room = rooms.ToList();
            //ViewBag.Status = lookups.Where(x => x.Type.Contains("Status")).ToList();
            //ViewBag.Building = buildings.ToList();

            bedIndexModel.PagedBed.Data = searchList.OrderByDescending(x => x.AddedDate).Take(PageSize).ToList();
            bedIndexModel.PagedBed.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)searchList.Count() / PageSize));

            if (dropDownSearchModel.BuildingID == null)
            {
                ViewBag.Floor = null;
                result = db.BedService.GetAll().Where(x => x.IsActive == true).ToList();
                bedIndexModel.PagedBed.Data = result.OrderByDescending(x => x.AddedDate).Take(PageSize).ToList();
                bedIndexModel.PagedBed.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));
            }



            return PartialView("_Index", bedIndexModel);
        }

        [Area("Admin")]
        public ActionResult _Index(int page)
        {
            BedIndexModel bedIndexModel = new BedIndexModel();
            bedIndexModel.PagedBed = new PagedData<Bed>();
            Bed bedlist = new Bed();
            bedIndexModel.bed = new Bed();
            var pagedBed = new PagedData<Bed>();

            var result = db.BedService.GetAll().Where(x => x.IsActive == true).ToList();
            var lookups = db.LookupService.GetLookups().ToList();
            var floors = db.FloorService.GetAll().ToList();
            var buildings = db.FloorService.GetAll().ToList();
            var rooms = db.RoomService.GetAll().ToList();
            //ViewBag.Department = lookups.Where(x => x.Type.Contains("Department")).ToList();
            ViewBag.Floor = floors.ToList();
            ViewBag.Room = rooms.ToList();
            ViewBag.Status = lookups.Where(x => x.Type.Contains("Status")).ToList();
            ViewBag.Building = buildings.ToList();
            bedIndexModel.PagedBed.Data = result.OrderByDescending(x => x.AddedDate).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
            bedIndexModel.PagedBed.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));
            return PartialView(bedIndexModel);
        }


        [Area("Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            var result = db.BedService.GetAll().Where(x => x.IsActive == true).ToList();
            var lookups = db.LookupService.GetLookups().ToList();
            ViewBag.Department = lookups.Where(x => x.Type.Contains("Department")).ToList();
            ViewBag.Floor = lookups.Where(x => x.Type.Contains("Floor")).ToList();
            ViewBag.Room = lookups.Where(x => x.Type.Contains("Room")).ToList();
            ViewBag.Status = lookups.Where(x => x.Type.Contains("Status")).ToList();
            ViewBag.Floor = lookups.Where(x => x.Type.Contains("Floor")).ToList();

            List<Lookup> buildings1 = new List<Lookup>();
            buildings1 = db.LookupService.GetLookups().Where(x => x.Type == "Building").ToList();

            buildings1.Insert(0, new Lookup { ID = 0, Name = "Select" });
            ViewBag.Building = buildings1.ToList();

            Bed bed = new Bed();
            return View(bed);
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Add(Bed bed)
        {
            bed.IsActive = true;
            bed.AddedDate = DateTime.UtcNow;
            db.BedService.Add(bed);
            Bed thisbed = new Bed();
            return RedirectToAction("Index");
        }

        [Area("Admin")]

        public IActionResult Delete(int id)
        {
            db.BedService.Delete(id);
            return RedirectToAction("Index");
        }

        [Area("Admin")]
        [HttpGet]
        public IActionResult GetBed(int id)
        {
            var lookups = db.LookupService.GetLookups().ToList();
            ViewBag.Department = lookups.Where(x => x.Type.Contains("Department")).ToList();
            ViewBag.Room = lookups.Where(x => x.Type.Contains("Room")).ToList();
            ViewBag.Building = lookups.Where(x => x.Type.Contains("Building")).ToList();
            ViewBag.Floor = lookups.Where(x => x.Type.Contains("Floor")).ToList();
            Bed bed = db.BedService.GeById(id);

            return View(bed);
        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult GetBed(Bed bed)
        {
            bed.IsActive = true;
            bed.ModifiedDate = DateTime.UtcNow;
            db.BedService.Update(bed);
            return RedirectToAction("Index");
        }
    }
}