using Data.Helpers;
using Data.Models;
using HISSystem.Areas.Admin.Models;
using HISSystem.Filters;
using HISSystem.Helper;
using HISSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.UnitOfServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Nancy.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using GeneticSystem.Areas.Admin.Models;
using Newtonsoft.Json;

namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class PatientController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;
        public PatientController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;

        }

        [CustomAuth(Page.Patient, ActionButton.Index)]
        public IActionResult Index()
        {
            var patients = new PagedData<User>();
            var patientList = db.UserService.GetPatients().OrderByDescending(x=>x.AddedDate).ToList();

            //inPatientList = patientList.Where(x => x.PatientPersonalInformation.PatientTypeID == 59).ToList();

            //outPatientList = patientList.Where(x => x.PatientPersonalInformation.PatientTypeID == 62).ToList();

            //var forAppointmentList = db.AppointmentService.GetAll().Where(x => x.StartDate.Value.Date == System.DateTime.Today.Date).ToList();

            //foreach (var item in forAppointmentList)
            //{
            //    appointmentTodayList.Add(item.User);
            //}

            //var result = inPatientList.Union(outPatientList).Union(appointmentTodayList);

            patients.Data = (patientList).Take(PageSize);

            patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)patientList.Count() / PageSize));

            ViewBag.Status = db.LookupService.GetByType("Status");


            //foreach (User user in patients.Data)
            //{
            //    var tryResult = patientadmissionList.Where(x => x.UserID == user.ID && x.PatientTypeID == 59).OrderBy(x => x.AddedBy).FirstOrDefault();

            //    if (user.PatientPersonalInformation.PatientTypeID == 59)
            //    {
            //        user.PatientAdmission = tryResult;
            //    }
            //    else
            //    {
            //        user.PatientAdmission = null;
            //    }

            //}

            return View(patients);
        }
        public IActionResult GetPatients(int page)
        {
            var patients = new PagedData<User>();
            var result = db.UserService.GetPatients().OrderByDescending(x=>x.AddedDate).ToList();
            //   .OrderByDescending(x => x.AddedDate).ToList();

            //var forCompare = db.AppointmentService.GetAll().Where(x => x.StartDate.Value.Date == System.DateTime.Today.Date).ToList();

            //List<User> users = new List<User>();

            //List<Lookup> lookup = db.LookupService.GetLookups().ToList();

            //foreach (var item in forCompare)
            //{
            //    users.Add(item.User);
            //}
            //var result1 = result.Union(users);
            patients.Data = (result).Skip(PageSize * (page - 1)).Take(PageSize);

            patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));

            ViewBag.Status = db.LookupService.GetByType("Status");

            //foreach (var item in patients.Data)
            //{
            //    var tryResult = db.PatientService.GetAll().Where(x => x.UserID == item.ID).FirstOrDefault();
            //    if (tryResult != null)
            //    {
            //        item.PatientAdmission.AdmissionType = lookup.Where(x => x.ID == tryResult.AdmissionTypeID).FirstOrDefault();
            //    }

            //}
            return PartialView(patients);
        }

        [HttpPost]
        public IActionResult SearchPatient(SearchModel model)
        {
            var InPatientList = new List<User>();
            var OutPatientList = new List<User>();
            var AppointmentTodayList = new List<User>();
            var searchList = new List<User>();

            var xray = db.UserService.GetPatients().ToList();

            var result = xray.OrderByDescending(x => x.AddedDate).ToList();

            try
            {
                searchList = result.Where(x => (String.IsNullOrEmpty(model.BirthDate) || ((x.PatientPersonalInformation.DateOfBirth != null) && x.PatientPersonalInformation.DateOfBirth.Value.Date.Equals(Convert.ToDateTime(model.BirthDate).Date))) &&
                   (String.IsNullOrEmpty(model.RegistrationNo) || (x.RegisterationNo != null) && (x.RegisterationNo.Contains(model.RegistrationNo, StringComparison.OrdinalIgnoreCase))) &&
                   //(model.ID == 0 || x.ID.ToString().Contains(model.ID.ToString(), StringComparison.OrdinalIgnoreCase)) &&
                   (String.IsNullOrEmpty(model.PatientName) || ((x.EnFirstName !=null) && (x.EnFirstName.Contains(model.PatientName)) 
                   || (x.ArFirstName !=null) && (x.ArFirstName.Contains(model.PatientName)))) &&
                   (model.Status == 0 || x.StatusID.ToString().Contains(model.Status.ToString(), StringComparison.OrdinalIgnoreCase)) &&
                   (model.PatientMobile == 0 || (x.Mobile != null && x.Mobile.ToString().Contains(model.PatientMobile.ToString(), StringComparison.OrdinalIgnoreCase))) &&
                   (String.IsNullOrEmpty(model.PatientCity) || ((x.PatientPersonalInformation != null) && (x.PatientPersonalInformation.City != null) && (x.PatientPersonalInformation.City.Name != null) && (x.PatientPersonalInformation.City.Name.Contains(model.PatientCity.ToString(), StringComparison.OrdinalIgnoreCase))))
                    ).ToList();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            var patients = new PagedData<User>();

            patients.Data = searchList.OrderByDescending(x => x.AddedDate).Take(PageSize).ToList();
            patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)searchList.Count() / PageSize));

            return PartialView("GetPatients", patients);
        }


        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> AutoCompleteReg()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var list = db.UserService.GetPatients();

                var names = (from N in list
                             where N.PatientPersonalInformation.RegistrationNo.ToString().Contains(term)
                             select new { value = N.RegisterationNo });
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> AutoCompleteName()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var list = db.UserService.GetPatients();

                var names = (from N in list
                             where (N.EnFirstName != null && N.EnFirstName.Contains(term, StringComparison.OrdinalIgnoreCase))
                             select new { value = N.EnFirstName ?? null + N.EnSecondName ?? null + N.EnThirdName ?? null });


                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> AutoCompleteNameWithID()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();

                if (term == null)
                {
                    term = "";
                }
                var list = db.UserService.GetPatients();

                var names = (from N in list
                             where (N.EnFirstName != null && N.EnFirstName.Contains(term, StringComparison.OrdinalIgnoreCase))
                             select new
                             {
                                 label = N.EnFirstName ?? "" + N.EnSecondName ?? "" + N.EnThirdName ?? "",
                                 value = N.EnFirstName ?? "" + N.EnSecondName ?? "" + N.EnThirdName ?? "",
                                 id = N.ID
                             });

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> AutoCompleteID()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var list = db.UserService.GetPatients();

                var names = (from N in list
                             where (N.ID.ToString().Contains(term, StringComparison.OrdinalIgnoreCase))
                             select new { value = N.ID });

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        public JsonResult GetCities(int countryId)
        {
            var result = db.CityService.GetAll().Where(x => x.CountryID == countryId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            return Json(result);
        }

        //AddPatient=
        #region Add and Update delete
        public IActionResult Add()
        {
            ViewBag.UserName = Request.Cookies["UserName"];
            ViewBag.Country = db.CountryService.GetAll();
            //ViewBag.City = db.CityService.GetAll();
            //ViewBag.City = new City
            //{
            //    ID = 0,
            //    Name = "SELECT"
            //};

            ViewBag.Gender = db.LookupService.GetLookUpByTypeName("Gender");
            ViewBag.EmployeeType = db.LookupService.GetLookUpByTypeName("EmployeeType");
            ViewBag.InsuranceCompany = db.LookupService.GetLookUpByTypeName("InsuranceCompany");
            ViewBag.BloodGroup = db.LookupService.GetLookUpByTypeName("BloodGroup");
            ViewBag.SirName = db.LookupService.GetLookUpByTypeName("Title");
            ViewBag.SocialStatus = db.LookupService.GetLookUpByTypeName("SocialStatus");
            ViewBag.PlaceOfBirth = db.LookupService.GetLookUpByTypeName("PlaceOfBirth");
            ViewBag.SecurityGroup = db.LookupService.GetLookUpByTypeName("SecurityGroup");
            ViewBag.IdentificationType = db.LookupService.GetLookUpByTypeName("IdentificationType");
            ViewBag.Status = db.LookupService.GetLookUpByTypeName("Status");
            ViewBag.Religion = db.LookupService.GetLookUpByTypeName("Religion");

            var model = new User();
            model.RegisterationNo = "";
            model.UserName = "";
            model.AddedDate = DateTime.UtcNow;
            ViewBag.AppointmentStatus = "Pending";
            return View(model);
        }
        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public IActionResult Add(PatientViewModel model)
        {
            if (model.User.PatientPersonalInformation.HaveInsurance == false)
            {
                model.User.PatientPersonalInformation.EmployeeTypeID = null;
                model.User.PatientPersonalInformation.OccupationLevelID = null;
                model.User.PatientPersonalInformation.InsuranceCompanyID = null;
                model.User.PatientPersonalInformation.CompanyName = null;
            }
            User usermodel = new User();
            var result = new User();
            int LoginID = Convert.ToInt32(Request.Cookies["ID"]);
            usermodel = model.User;
            usermodel.AddedBy = LoginID;
            if (model.File != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", model.File.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.File.CopyToAsync(stream);
                    usermodel.ImagePath = "/uploaded/" + model.File.FileName;
                }
            }

            if (model.User.RegisterationNo == null)
            {
                var tempId = db.UserService.GetAll().OrderByDescending(x => x.RegisterationNo).First();
                usermodel.RegisterationNo = (Convert.ToInt32(tempId.RegisterationNo) + 1).ToString();

                if (model.User.UserName == null)
                {
                    usermodel.UserName = model.User.RegisterationNo;
                }
            }

            if (model.User.ID == 0)
            {
                usermodel.AddedDate = DateTime.UtcNow;
                usermodel.RoleID = db.RoleService.GetByName("Patient");
                usermodel.IsActive = true;
                usermodel.PatientPersonalInformation.RegistrationNo = db.PatientPersonalInformationService.GetRegistrationNo();
                usermodel.Password = CommonMethod.Encrypt("12345");/*.Substring(0, 6);*/

                if(model.MedicalHistory != null && model.MedicalHistory.Count() > 0)
                {
                    foreach(var item in model.MedicalHistory)
                    {
                        if(item.UnknownDiseaseArray != null)
                            item.UnknownDiseases = string.Join(",", item.UnknownDiseaseArray);
                        if (item.FamilyHistoryArray != null)
                            item.FamilyHistory = string.Join(",", item.FamilyHistoryArray);
                    }

                    usermodel.MedicalHistory = model.MedicalHistory;
                }
                result = db.UserService.Add(usermodel);
                result.RegisterationNo = usermodel.RegisterationNo;
                result.UserName = usermodel.UserName;
                usermodel.PatientRelative = AddList(model.PatientRelative, result.ID);
                db.UserService.AddPatientRelative(usermodel.PatientRelative);
            }
            else
            {
                usermodel.ModifiedDate = DateTime.UtcNow;
                usermodel.IsActive = true;
                usermodel.RoleID = db.RoleService.GetByName("Patient");
                db.UserService.UpdateUser(usermodel);
                usermodel.PatientRelative = AddList(model.PatientRelative, result.ID);
                db.UserService.UpdatePatientRelative(usermodel.PatientRelative);
            }

            ViewBag.Country = db.CountryService.GetAll();

            if (usermodel.PatientPersonalInformation.PlaceOfBirthID != null)
            {
                ViewBag.City = db.CityService.GetAll().Where(x => x.CountryID == usermodel.PatientPersonalInformation.PlaceOfBirthID);
            }
            else
            {
                ViewBag.City = Enumerable.Empty<SelectListItem>();
            }
            ViewBag.AddedBy = LoginID;


            ViewBag.Gender = db.LookupService.GetLookUpByTypeName("Gender");
            ViewBag.EmployeeType = db.LookupService.GetLookUpByTypeName("EmployeeType");
            ViewBag.InsuranceCompany = db.LookupService.GetLookUpByTypeName("InsuranceCompany");
            ViewBag.BloodGroup = db.LookupService.GetLookUpByTypeName("BloodGroup");
            ViewBag.SirName = db.LookupService.GetLookUpByTypeName("Title");
            ViewBag.SocialStatus = db.LookupService.GetLookUpByTypeName("SocialStatus");
            ViewBag.PlaceOfBirth = db.LookupService.GetLookUpByTypeName("PlaceOfBirth");
            ViewBag.SecurityGroup = db.LookupService.GetLookUpByTypeName("SecurityGroup");
            ViewBag.IdentificationType = db.LookupService.GetLookUpByTypeName("IdentificationType");
            ViewBag.Status = db.LookupService.GetLookUpByTypeName("Status");
            ViewBag.Religion = db.LookupService.GetLookUpByTypeName("Religion");

            return Json(result);
        }

        public IActionResult Update(long id)
        {
            var LoginID = Request.Cookies["ID"];
            ViewBag.UserName = Request.Cookies["UserName"];


            ViewBag.Gender = db.LookupService.GetLookUpByTypeName("Gender");
            ViewBag.EmployeeType = db.LookupService.GetLookUpByTypeName("EmployeeType");
            ViewBag.InsuranceCompany = db.LookupService.GetLookUpByTypeName("InsuranceCompany");
            ViewBag.BloodGroup = db.LookupService.GetLookUpByTypeName("BloodGroup");
            ViewBag.SirName = db.LookupService.GetLookUpByTypeName("Title");
            ViewBag.SocialStatus = db.LookupService.GetLookUpByTypeName("SocialStatus");
            ViewBag.PlaceOfBirth = db.LookupService.GetLookUpByTypeName("PlaceOfBirth");
            ViewBag.SecurityGroup = db.LookupService.GetLookUpByTypeName("SecurityGroup");
            ViewBag.IdentificationType = db.LookupService.GetLookUpByTypeName("IdentificationType");
            ViewBag.Status = db.LookupService.GetLookUpByTypeName("Status");
            ViewBag.Religion = db.LookupService.GetLookUpByTypeName("Religion");

       
          
            var result = db.UserService.GetUser(id);
            result.Password = "12345";

            ViewBag.Country = db.CountryService.GetAll();

            if (result.PatientPersonalInformation.PlaceOfBirthID != null)
            {
                ViewBag.City = db.CityService.GetAll().Where(x => x.CountryID == result.PatientPersonalInformation.PlaceOfBirthID);
            }
            else
            {

                ViewBag.City = Enumerable.Empty<SelectListItem>();
            }
                
           

            if (result.AddedBy.HasValue && result.AddedBy != 0)
            {
                var addedBy = db.UserService.GetAll().Where(x => x.ID == result.AddedBy).FirstOrDefault().UserName.ToString();
                ViewBag.addedBy = addedBy;
            }
            else
            {
                ViewBag.addedBy = "Admin";
            }

            return View(result);
        }

        [HttpPost]
        public IActionResult Update(PatientViewModel model)
        {
            if (model.User.PatientPersonalInformation.HaveInsurance == false)
            {
                model.User.PatientPersonalInformation.EmployeeTypeID = null;
                model.User.PatientPersonalInformation.OccupationLevelID = null;
                model.User.PatientPersonalInformation.InsuranceCompanyID = null;
                model.User.PatientPersonalInformation.CompanyName = null;
            }
            User usermodel = new User();
            usermodel.ModifiedBy = Convert.ToInt32(Request.Cookies["ID"]);
            usermodel = model.User;

            if (model.File != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", model.File.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.File.CopyToAsync(stream);
                    usermodel.ImagePath = "/uploaded/" + model.File.FileName;
                }
            }
            usermodel.ModifiedDate = DateTime.UtcNow;
            usermodel.RoleID = db.RoleService.GetByName("Patient");
            usermodel.Password = CommonMethod.Encrypt(model.User.Password);
            db.UserService.UpdateUser(usermodel);

            if (model.MedicalHistory != null && model.MedicalHistory.Count() > 0)
            {
                foreach (var item in model.MedicalHistory)
                {
                    if (item.UnknownDiseaseArray != null)
                        item.UnknownDiseases = string.Join(",", item.UnknownDiseaseArray);
                    if (item.FamilyHistoryArray != null)
                        item.FamilyHistory = string.Join(",", item.FamilyHistoryArray);
                }

                usermodel.MedicalHistory = model.MedicalHistory;

                db.UserService.UpdateMedicalHistoryList(usermodel.MedicalHistory);
            }

            foreach (var item in model.PatientRelative)
            {
                usermodel.PatientRelative.Add(new PatientRelative
                {
                    ID = item.ID,
                    SameAddress = item.SameAddress,
                    AddedDate = DateTime.UtcNow,
                    Address = item.Address,
                    Comment = item.Comment,
                    IsActive = true,
                    RelationID = item.RelationID,
                    StatusID = item.StatusID,
                    RelativeName = item.RelativeName,
                    Telephone = item.Telephone,
                    UserID = item.UserID
                });
            }
            db.UserService.UpdatePatientRelative(usermodel.PatientRelative);
            return Json(model);
        }

        [HttpGet]
        public IActionResult NearestRelative(int? id)
        {
            var lookup = db.LookupService.GetLookups().ToList();
            ViewBag.Relations = lookup.Where(x => x.Type.Contains("Relation")).ToList();
            ViewBag.Status = lookup.Where(x => x.Type.Contains("Status")).ToList();
            User model = new User();
            if (id != null && id > 0)
            {
                model = db.UserService.GetUser(Convert.ToInt32(id));
            }
            return PartialView("_NearestRelative", model);
        }

        [HttpGet]
        public IActionResult MedicalHistory(int? id)
        {
            ViewBag.FamilyHistory = db.LookupService.GetLookUpByTypeName("FamilyHistory").ToList();
            ViewBag.UnknownDisease = db.LookupService.GetLookUpByTypeName("UnknownDisease").ToList();
            User model = new User();
            if (id != null && id > 0)
            {
                model = db.UserService.GetUser(Convert.ToInt32(id));

                if (model.MedicalHistory != null && model.MedicalHistory.Count() > 0)
                {
                    foreach (var item in model.MedicalHistory)
                    {
                        if (item.FamilyHistory != null)
                            item.FamilyHistoryArray = item.FamilyHistory.Split(",");
                        if (item.UnknownDiseases != null)
                            item.UnknownDiseaseArray = item.UnknownDiseases.Split(",");
                    }
                }
            }
            return PartialView("_MedicalHistory", model);
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

        #endregion Add and Update delete

        #region Addmission
        public IActionResult Admission(int id)
        {
            PatientAdmission model = new PatientAdmission();
            var lookup = db.LookupService.GetLookups();
            ViewBag.Bed = db.BedService.GetAll();
            ViewBag.Appointment = db.AppointmentService.GetAll().Where(x => x.UserID == id).ToList();
            model = db.PatientService.GetPatientAdmission(id);

            if (model == null)
            {
                model = new PatientAdmission();
                model.UserID = id;
                var user = db.UserService.GetAll().Where(x => x.ID == id).FirstOrDefault();
                ViewBag.Patient = user.EnFirstName + user.EnSecondName + user.EnThirdName;
                model.AddedDate = DateTime.UtcNow.Date;
                var CreatedByID = Convert.ToInt32(HttpContext.Request.Cookies["ID"]);
                if (CreatedByID == null || CreatedByID == 0)
                {
                    return Redirect("/account/login");
                }
                var createdBy = db.UserService.GetAll().Where(x => x.ID == CreatedByID).FirstOrDefault();
                ViewBag.CreatedBy = createdBy.EnFirstName + " " + createdBy.EnSecondName + " " + createdBy.EnThirdName;
            }
            else
            {
                var createdBy = db.UserService.GetAll().Where(x => x.ID == model.AddedBy).FirstOrDefault();
                var user = db.UserService.GetAll().Where(x => model.UserID == id).FirstOrDefault();
                ViewBag.Patient = user.EnFirstName + user.EnSecondName + user.EnThirdName;

                if (createdBy != null)
                {
                    ViewBag.CreatedBy = createdBy.EnFirstName + " " + createdBy.EnSecondName + " " + createdBy.EnThirdName;
                }

            }

            ViewBag.Admission = lookup.Where(x => x.Type == "AdmissionType").ToList();
            ViewBag.Department = lookup.Where(x => x.Type.Contains("Department")).ToList();
            ViewBag.Supplier = lookup.Where(x => x.Type.Contains("Supplier")).ToList();
            ViewBag.Building = lookup.Where(x => x.Type == "Building").ToList();
            ViewBag.Floor = lookup.Where(x => x.Type == "Floor").ToList();
            ViewBag.Room = lookup.Where(x => x.Type == "Room").ToList();
            ViewBag.Elective = lookup.Where(x => x.Type.Equals("Elective") || x.Type.Equals("Emergancy")).ToList();



            return PartialView(model);
        }
        public IActionResult FillFloorDropDown(int BuildingID)
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
                    Floor.BuildingID = Convert.ToInt32(item.ParentId);
                    floorList.Add(Floor);
                }
            }

            return Json(floorList);
        }
        public IActionResult FillRoomDropDown(int FloorID)
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
                    Room.FloorID = Convert.ToInt32(item.ParentId);

                    roomList.Add(Room);
                }
            }

            return Json(roomList);
        }
        public IActionResult FillBedDropDown(int RoomID)
        {
            var bed = new List<Bed>();


            var bedList = db.BedService.GetAll().Where(x => x.RoomID == RoomID).ToList();

            foreach (var item in bedList)
            {
                Bed tempBed = new Bed();

                tempBed.ID = item.ID;
                tempBed.Name = item.Name;
                tempBed.RoomID = item.RoomID;

                bed.Add(tempBed);

            }
            return Json(bed);
        }
        public IActionResult FillTypeDropDown(int id)
        {
            var lookup = db.LookupService.GetLookups();

            if (id == 70)
            {
                lookup = lookup.Where(x => x.Type == "Elective");
            }
            else
            {
                lookup = lookup.Where(x => x.Type == "Emergancy");
            }
            ViewBag.Building = lookup.Where(x => x.Type == "Building").ToList();
            ViewBag.Floor = lookup.Where(x => x.Type == "Floor").ToList();
            ViewBag.Room = lookup.Where(x => x.Type == "Room").ToList();
            ViewBag.Bed = db.BedService.GetAll();

            return Json(lookup);
        }
        [HttpPost]
        public IActionResult Admission(PatientAdmission model)
        {
            var patientAdmission = new PatientAdmission();
            var lookupList = db.LookupService.GetLookups().ToList();
            var bed = db.BedService.GetAll().Where(x => x.ID == model.BedID).FirstOrDefault();
            var PatientType = db.LookupService.GetLookups().Where(x => x.Type == "PatientType" && x.TypeID == 1).FirstOrDefault();

            if (model.ID > 0)
            {
                patientAdmission = model;
                patientAdmission.IsActive = true;
                patientAdmission.ModifiedDate = DateTime.UtcNow;
                patientAdmission.PatientTypeID = PatientType.ID;
                db.PatientService.Update(patientAdmission);
            }
            else
            {
                model.AddedBy = Convert.ToInt32(HttpContext.Request.Cookies["ID"]);
                model.IsActive = true;
                model.AddedDate = DateTime.UtcNow;
                model.PatientTypeID = PatientType.ID;
                db.PatientService.Add(model);
            }

            if (model.AppointmentID != null && model.AppointmentID != 0)
            {
                var appointment = db.AppointmentService.GetAll().Where(x => x.ID == model.AppointmentID).FirstOrDefault();
                var tempID = db.LookupService.GetLookups().Where(x => x.Type == "AppointmentStatus" && x.TypeID == 2).FirstOrDefault();
                appointment.AppointmentStatusID = tempID.ID;
                db.AppointmentService.Update(appointment);
            }

            bed.StatusID = lookupList.Where(x => x.Type == "BedStatus" && x.TypeID == 1).FirstOrDefault().ID;
            db.BedService.Update(bed);

            User user = db.UserService.GetAll().Where(x => x.ID == model.UserID).FirstOrDefault();
            user.PatientPersonalInformation.PatientTypeID = lookupList.Where(x => x.Type == "PatientType" && x.TypeID == 1).FirstOrDefault().ID;
            db.UserService.UpdateUser(user);

            return RedirectToAction("Index");
        }
        #endregion end Addmission

        public IActionResult Action(int id)

        {
            var user = db.UserService.GetAll();
            AppointmentViewModel actionViewModel = new AppointmentViewModel();
            List<Dropdown> doctors = new List<Dropdown>();
            var lookup = db.LookupService.GetLookups();

            int role = db.RoleService.GetByName("Doctor");
            int patientRole = db.RoleService.GetByName("Patient");

            actionViewModel.PatientAdmission = db.PatientService.GetOutPatient(id);

            var patient = user.Where(x => x.RoleID == patientRole).ToList();
            var doctor = user.Where(x => x.RoleID == 3).ToList();

            User model = user.Where(x => x.ID == (Convert.ToInt32(id))).FirstOrDefault();

            if (model != null && model.PatientPersonalInformation != null)
            {
                model.PatientPersonalInformation.PatientTypeID = lookup.Where(x => x.Type == "PatientType" && x.Name == "Out-Patient").Select(x => x.ID).FirstOrDefault();
            }

            ViewBag.Patient = model.EnFirstName + model.EnSecondName + model.EnThirdName;

            ViewBag.OutPatientType = lookup.Where(x => x.Type == "OutPatientType").ToList();
            ViewBag.Department = lookup.Where(x => x.Type.Contains("Department")).ToList();
            ViewBag.Speciality = lookup.Where(x => x.Type.Contains("Speciality")).ToList();
            ViewBag.Room = lookup.Where(x => x.Type.Contains("Room")).ToList();
            ViewBag.Purpose = lookup.Where(x => x.Type.Contains("Purpose")).ToList();
            ViewBag.Appoitment = ViewBag.Appointment = db.AppointmentService.GetAll().Where(x => x.UserID == id).ToList();
            ViewBag.ReservedBy = lookup.Where(x => x.Type.Contains("Department")).ToList();

            foreach (var item in doctor)
            {
                doctors.Add(new Dropdown
                {
                    Name = item.EnFirstName + " " + item.EnSecondName + " " + item.EnThirdName,
                    ID = item.ID
                });
            }
            ViewBag.AddedBy = "";
            ViewBag.AddedDate = DateTime.UtcNow.Date;
            ViewBag.Doctor = doctors;
            var addedByUser = new User();
            if (actionViewModel.PatientAdmission == null)
            {
                addedByUser = user.Where(x => x.ID == Convert.ToInt32(Request.Cookies["ID"])).FirstOrDefault();
                ViewBag.AddedBy = addedByUser.EnFirstName + " " + addedByUser.EnSecondName + " " + addedByUser.EnThirdName;
                ViewBag.AddedDate = DateTime.UtcNow.Date;
            }
            else
            {
                addedByUser = user.Where(x => x.ID == actionViewModel.PatientAdmission.AddedBy).FirstOrDefault();

                if (addedByUser != null)
                {
                    ViewBag.AddedBy = addedByUser.EnFirstName + " " + addedByUser.EnSecondName + " " + addedByUser.EnThirdName;
                }

            }

            actionViewModel.User = model;

            return PartialView(actionViewModel);
        }



        [HttpPost]
        public IActionResult Action(AppointmentViewModel model)
        {
            PatientAdmission patientAdmission = model.PatientAdmission;
            var PatientType = db.LookupService.GetLookups().Where(x => x.Type == "PatientType" && x.TypeID == 2).FirstOrDefault();

            if (patientAdmission.ID > 0)
            {
                model.PatientAdmission.UserID = model.User.ID;
                model.PatientAdmission.IsActive = true;
                model.PatientAdmission.ModifiedDate = DateTime.UtcNow;
                model.PatientAdmission.PatientTypeID = PatientType.ID;
                db.PatientService.Update(model.PatientAdmission);
            }
            else
            {
                model.PatientAdmission.IsActive = true;
                model.PatientAdmission.UserID = model.User.ID;
                var createdBy = Convert.ToInt32(HttpContext.Request.Cookies["ID"]);
                model.PatientAdmission.AddedDate = DateTime.UtcNow;
                model.PatientAdmission.PatientTypeID = PatientType.ID;
                db.PatientService.Add(model.PatientAdmission);
            }

            var user = db.UserService.GetUser(model.User.ID);
            var lookup = db.LookupService.GetLookups().ToList();

            if (model.PatientAdmission.AppointmentID != null && model.PatientAdmission.AppointmentID != 0)
            {
                var appointment = db.AppointmentService.GetAll().Where(x => x.ID == model.PatientAdmission.AppointmentID).FirstOrDefault();
                var tempID = db.LookupService.GetLookups().Where(x => x.Type == "AppointmentStatus" && x.TypeID == 2).FirstOrDefault();
                appointment.AppointmentStatusID = tempID.ID;
                db.AppointmentService.Update(appointment);
            }

            model.User.PatientPersonalInformation.PatientTypeID = lookup.Where(x => x.Name == "Out-Patient").Select(x => x.ID).FirstOrDefault();

            db.PatientPersonalInformationService.UpdateStatus(model.User.PatientPersonalInformation.ID, Convert.ToInt32(model.User.PatientPersonalInformation.PatientTypeID));

            return RedirectToAction("Index");
        }

        #region Appointment  
        public IActionResult Appointment()
        {
            ViewBag.Doctor = db.UserService.GetByRole(db.RoleService.GetByName("Doctor"));
            ViewBag.Department = db.LookupService.GetByType("Department");

            ViewBag.AppointmentStatus = db.LookupService.GetByType("AppointmentStatus");
            List<Dropdown> dropdown = new List<Dropdown>();
            ViewBag.Speciality = db.LookupService.GetByType("Speciality");
            ViewBag.Room = db.LookupService.GetLookups().Where(x => x.Type == "Room").ToList();
            ViewBag.Purpose = db.LookupService.GetByType("Purpose");
            ViewBag.date = System.DateTime.UtcNow;
            ViewBag.AppointmentStatus = "Pending";
            var patient = db.UserService.GetPatients();

            foreach (var item in patient)
            {
                dropdown.Add(new Dropdown
                {
                    Name = item.EnFirstName + " " + item.EnSecondName + " " + item.EnThirdName,
                    ID = item.ID
                });
            }
            ViewBag.Patient = dropdown;

            List<JsonEvent> model = new List<JsonEvent>();
            var result = db.AppointmentService.GetAll();

            foreach (var item in result)
            {
                model.Add(new JsonEvent
                {
                    name = item.Doctor.EnFirstName + " " + item.Doctor.EnSecondName + " " + item.Doctor.EnThirdName,
                    id = item.ID,
                    start = Convert.ToDateTime(item.Date),
                    //   End = Convert.ToDateTime(item.Date),
                });
            }
            ViewBag.Colums = model;
            return View();
        }

        public IActionResult CompletedAppointments()
        {
            ViewBag.Doctor = db.UserService.GetByRole(db.RoleService.GetByName("Doctor"));
            ViewBag.Department = db.LookupService.GetByType("Department");

            ViewBag.AppointmentStatus = db.LookupService.GetByType("AppointmentStatus");
            List<Dropdown> dropdown = new List<Dropdown>();
            List<Dropdown> appointmentDropdown = new List<Dropdown>();
            ViewBag.Speciality = db.LookupService.GetByType("Speciality");
            ViewBag.Room = db.LookupService.GetLookups().Where(x => x.Type == "Room").ToList();
            ViewBag.Purpose = db.LookupService.GetByType("Purpose");
            ViewBag.date = System.DateTime.UtcNow;
            ViewBag.AppointmentStatus = "Pending";
            var patient = db.UserService.GetPatients();
            var result = db.AppointmentService.GetAll();
            var appointmentList = result.Where(x => x.IsActive == true).Select(x => x.Doctor.EnFirstName).ToList();
            //foreach (var item in result)
            //{
            //    appointmentDropdown.Add(new Dropdown
            //    {
            //        Name = item.Doctor.EnFirstName ?? null + "" + item.Doctor.EnSecondName ?? null + "" + item.Doctor.EnThirdName ?? null,
            //        ID = item.ID
            //    });
            //}

            ViewBag.AppointmentList = appointmentList;

            foreach (var item in patient)
            {
                dropdown.Add(new Dropdown
                {
                    Name = item.EnFirstName + " " + item.EnSecondName + " " + item.EnThirdName,
                    ID = item.ID
                });
            }
            ViewBag.Patient = dropdown;

            List<JsonEvent> model = new List<JsonEvent>();


            foreach (var item in result)
            {
                model.Add(new JsonEvent
                {
                    name = item.Doctor.EnFirstName + " " + item.Doctor.EnSecondName + " " + item.Doctor.EnThirdName,
                    id = item.ID,
                    start = Convert.ToDateTime(item.Date),
                    //   End = Convert.ToDateTime(item.Date),
                });
            }
            ViewBag.Colums = model;
            return View();
        }

        [HttpGet]
        public IActionResult GetDoctorColumn(int? doctorID, int? departmentID)
        {
            List<JsonEvent> model = new List<JsonEvent>();
            var doctors = db.UserService.GetByRole(db.RoleService.GetByName("Doctor"));
            if (doctorID != null || departmentID != null)
            {
                doctors = doctors.Where(x => x.ID == doctorID || (x.PersonalInformation.DepartmentID != null && x.PersonalInformation.DepartmentID == departmentID)).ToList();
            }

            foreach (var item in doctors)
            {
                model.Add(new JsonEvent
                {
                    name = item.EnFirstName + " " + item.EnFirstName + " " + item.EnFirstName,
                    id = item.ID,
                    start = DateTime.UtcNow.Date,
                    end = DateTime.UtcNow.Date,
                });
            }

            return Json(new { data = model });
        }
        [HttpGet]
        public IActionResult GetDoctorAppointments()
        {
            List<JsonEvent> model = new List<JsonEvent>();
            var result = db.AppointmentService.GetAll();

            foreach (var item in result)
            {
                model.Add(new JsonEvent
                {
                    id = item.Doctor.ID,
                    start = Convert.ToDateTime(item.StartDate),
                    end = Convert.ToDateTime(item.EndDate),
                    text = item.Doctor.EnFirstName + " " + item.Doctor.EnSecondName + " " + item.Doctor.EnThirdName,
                    resource = item.Doctor.ID,
                    Doctor = item.Doctor?.EnFirstName ?? "" +  " " + item.Doctor?.EnSecondName ?? "" + " " + item.Doctor?.EnThirdName ?? "",
                    Patient = item.User?.EnFirstName ?? "" + " " + item.User?.EnSecondName ?? "" + " " + item.User.EnThirdName ?? "",
                    Department = item.Department?.Name ?? "",
                    Room = item.Room?.Name ?? "",
                    Mobile = item.User.Mobile,
                    Note = item.Note

                });
            }          
            return Json(new { data = model });
        }

        [HttpGet]
        public IActionResult GetDoctorAppointmentsByDate(DateTime? startDate)
        {
            List<JsonEvent> model = new List<JsonEvent>();
            var result = db.AppointmentService.GetAll().Where(x => x.StartDate.Value.Date == (Convert.ToDateTime(startDate.Value.Date))).ToList() ;

            foreach (var item in result)
            {
                model.Add(new JsonEvent
                {
                    id = item.Doctor.ID,
                    start = Convert.ToDateTime(item.StartDate),
                    end = Convert.ToDateTime(item.EndDate),
                    text = item.Doctor.EnFirstName + " " + item.Doctor.EnSecondName + " " + item.Doctor.EnThirdName,
                    resource = item.Doctor.ID,
                });
            }

            return Json(new { data = model });
        }

        [HttpGet]
        public IEnumerable<JsonEvent> GetEvents()
        {
            List<JsonEvent> model = new List<JsonEvent>();
            var result = db.AppointmentService.GetAll();

            foreach (var item in result)
            {
                model.Add(new JsonEvent
                {
                    id = item.Doctor.ID,
                    start = Convert.ToDateTime(item.StartDate),
                    end = Convert.ToDateTime(item.EndDate),
                    text = item.Doctor.EnFirstName + " " + item.Doctor.EnSecondName + " " + item.Doctor.EnThirdName,
                    resource = item.Doctor.ID,
                });
            }

            return model;
        }



        [HttpPost]
        public IActionResult AddAppointment(Appointment model)
        {
            JsonEvent jsonData = new JsonEvent();
            model.AddedDate = DateTime.UtcNow;
            model.IsActive = true;
            model.CreatedBy = Convert.ToInt32(HttpContext.Request.Cookies["ID"]);
            model.AppointmentStatusID = db.LookupService.GetLookups().Where(x => x.Type == "AppointmentStatus" && x.TypeID == 1).FirstOrDefault().ID;
            var result = db.AppointmentService.Add(model);
            var Data = db.AppointmentService.GetById(result.ID);
            jsonData.id = Data.ID;
            if (Data.User != null && Data.User.PersonalInformation != null)
            {
                jsonData.name = Data.User.EnFirstName + " " + Data.User.EnSecondName + " " + Data.User.EnThirdName;
            }
            jsonData.start = Convert.ToDateTime(Data.StartDate);
            jsonData.end = Convert.ToDateTime(Data.EndDate);
            return new JsonResult(new { Data = new Dictionary<string, object> { { "Status", 1 }, { "Message", "Add Success fully" }, { "Data", jsonData } } });
        }
        #endregion Appointment

        #region Send SMS and Email
        public IActionResult SendToMobile()
        {
            return Json(new { Status = true });
        }
        public IActionResult SendToEmail()
        {
            return Json(new { Status = true });
        }
        #endregion Send SMS and Email

        public IActionResult AllocatedBed(int id, int? StatusID, bool? Isolated)
        {
            var lookup = db.LookupService.GetLookups();
            var bedList = db.BedService.GetAll().ToList();
            var result = db.BedService.GetByDepartment(id, StatusID, Isolated).ToList();

            ViewBag.Department = new SelectList(lookup.Where(x => x.Type.Contains("Department")).ToList(), "ID", "Name", id);

            ViewBag.Status = new SelectList(lookup.Where(x => x.Type.Contains("BedStatus")).ToList(), "ID", "Name");

            return PartialView(result);
        }
        public IActionResult addSearch(string searchstring, int searchtype, int page)
        {
            var patients = new PagedData<User>();
            var context = db.UserService.GetPatients();
            if (string.IsNullOrEmpty(searchstring))
            {
                patients.Data = context.OrderByDescending(x => x.AddedDate).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Count() / PageSize));
            }
            else
            {
                if (searchtype == 1)
                {
                    patients.Data = context.Where(x => x.PatientPersonalInformation.RegistrationNo.ToString().Contains(searchstring)).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                    patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Where(x => x.PatientPersonalInformation.RegistrationNo.ToString().Contains(searchstring)).Count() / PageSize));
                }
                else if (searchtype == 2)
                {
                    patients.Data = context.Where(x => x.EnFirstName.Contains(searchstring, StringComparison.OrdinalIgnoreCase)).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                    patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Where(x => x.EnFirstName.Contains(searchstring, StringComparison.OrdinalIgnoreCase)).Count() / PageSize));

                }
                else if (searchtype == 3)
                {
                    patients.Data = context.Where(x => x.Mobile.Contains(searchstring)).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                    patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Where(x => x.Mobile.Contains(searchstring)).Count() / PageSize));
                }

            }

            return PartialView("_searchadd", patients);
        }

        public IActionResult appointmentSearchList(string searchstring, int searchtype, int page)
        {
            var patients = new PagedData<User>();
            var pagedAppointments = new PagedData<Appointment>();
            var context = db.UserService.GetPatients();
            var appointmentList = db.AppointmentService.GetAll().ToList();
            if (string.IsNullOrEmpty(searchstring))
            {
                pagedAppointments.Data = appointmentList.OrderByDescending(x => x.AddedDate).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                pagedAppointments.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)appointmentList.Count() / PageSize));
            }
            else
            {
                if (searchtype == 1)
                {
                    patients.Data = context.Where(x => x.PatientPersonalInformation.RegistrationNo.ToString().Contains(searchstring)).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                    patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Where(x => x.PatientPersonalInformation.RegistrationNo.ToString().Contains(searchstring)).Count() / PageSize));
                }
                else if (searchtype == 2)
                {
                    patients.Data = context.Where(x => x.EnFirstName.Contains(searchstring, StringComparison.OrdinalIgnoreCase)).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                    patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Where(x => x.EnFirstName.Contains(searchstring, StringComparison.OrdinalIgnoreCase)).Count() / PageSize));

                }
                else if (searchtype == 3)
                {
                    patients.Data = context.Where(x => x.Mobile.Contains(searchstring)).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                    patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Where(x => x.Mobile.Contains(searchstring)).Count() / PageSize));
                }

            }

            return PartialView(pagedAppointments);
        }



        public List<PatientRelative> AddList(List<PatientRelative> model, int UserID)
        {
            List<PatientRelative> result = new List<PatientRelative>();

            foreach (var item in model)
            {
                result.Add(new PatientRelative
                {
                    SameAddress = item.SameAddress,
                    AddedDate = DateTime.UtcNow,
                    Address = item.Address,
                    Comment = item.Comment,
                    IsActive = true,
                    RelationID = item.RelationID,
                    StatusID = item.StatusID,
                    RelativeName = item.RelativeName,
                    Telephone = item.Telephone,
                    UserID = UserID
                });
            }
            return result;
        }

        public IActionResult PatientEncounter(int id)
        {
            PatientEncounterViewModel model = new PatientEncounterViewModel();
            model.User = db.UserService.GetPatient(id);
            var getpatientencounter = db.PatientEncounterService.GetByPatientId(id);
            var lookupList = db.LookupService.GetLookups();
            var tryLookup = lookupList.Where(x => x.Type == "VitalType").ToList();

            var initialMedicalDetailList = db.LookupService.GetIMDetailLookups().ToList();
            var inmedLookupList = initialMedicalDetailList.ToList();
            var generalLookupList = initialMedicalDetailList.Where(x => x.Parent == "ExaminationDetail").ToList();
            if (getpatientencounter !=null)
            {
                model.PatientEncounter = getpatientencounter;
                var Id=getpatientencounter.ID;
                var paitentpreassesment = db.PatientEncounterService.GetPatientPreAssesmentDetail(Id);
                if (paitentpreassesment != null)
                {
                    var patientpreassesmentid = paitentpreassesment.ID;
                    model.PatientPreAssesment = paitentpreassesment;
                    var patientpreassesmentvital = db.PatientEncounterService.GetPatientPreAssesmentVital(Id).ToList();
                    model.PatientPreAssesmentVitals = patientpreassesmentvital;
                    model.PreAssessmentVitalDetails = new List<PreAssessmentVitalDetail>();
                    foreach (var item in patientpreassesmentvital)
                    {
                        var xray = db.PatientEncounterService.GetPreAssessmentVitalDetail(item.ID).ToList();
                        if (xray.Count > 0)
                        {

                            model.PreAssessmentVitalDetails.AddRange(xray);
                        }

                    }
                   

                    if (patientpreassesmentvital.Count == 0)
                    {
                        var lookupList1 = db.LookupService.GetLookups();
                        var tryLookup1 = lookupList1.Where(x => x.Type == "VitalType").ToList();
                        model.PatientPreAssesmentVitals = tryLookup1.Select(x => new PatientPreAssesmentVital
                        {
                            VitalTypeID = x.ID,
                            VitalType = x,
                            PreAssessmentVitalDetails = lookupList1.Where(y => Convert.ToInt32(y.ParentId) == x.ID).Select(z => new PreAssessmentVitalDetail
                            {
                                VitalDetail = z,
                                VitalDetailID = z.ID
                            }).ToList()
                        }).ToList();

                    }

                var intialmedicalassesment = db.PatientEncounterService.GetInitialMedicalAssessment(patientpreassesmentid);
                    if (intialmedicalassesment != null)
                    {
                        model.PatientPreAssesment.InitialMedicalAssessment = intialmedicalassesment;
                        var intialmedicalassesmentId = intialmedicalassesment.ID;
                        var intialmedicalassesmentdetail = db.PatientEncounterService.GetInitialMedicalAssessmentDetails(intialmedicalassesmentId);
                        if (intialmedicalassesmentdetail.Count> 0)
                        {
                            model.InitialMedicalAssessmentDetail = intialmedicalassesmentdetail;
                        }
                        else
                        {
                            model.PatientPreAssesment.InitialMedicalAssessment.InitialMedicalAssesmentDetails = inmedLookupList.Select(x => new InitialMedicalAssessmentDetail
                            {
                                InitialMedicalDetailTypeId = x.ID,
                                InitialMedicalDetailType = x
                            }).ToList();

                        }
                       
                    }
                    else
                    {
                        model.PatientPreAssesment.InitialMedicalAssessment = new InitialMedicalAssessment();
                        model.PatientPreAssesment.InitialMedicalAssessment.InitialMedicalAssesmentDetails = inmedLookupList.Select(x => new InitialMedicalAssessmentDetail
                        {
                            InitialMedicalDetailTypeId = x.ID,
                            InitialMedicalDetailType = x
                        }).ToList();
                    }
                
                    

                }
                else
                {

                    model.PatientPreAssesment.InitialMedicalAssessment = new InitialMedicalAssessment();
                    model.PatientPreAssesment.InitialMedicalAssessment.InitialMedicalAssesmentDetails = new List<InitialMedicalAssessmentDetail>();

                    model.PatientPreAssesmentVitals = tryLookup.Select(x => new PatientPreAssesmentVital
                    {
                        VitalTypeID = x.ID,
                        VitalType = x,
                        PreAssessmentVitalDetails = lookupList.Where(y => Convert.ToInt32(y.ParentId) == x.ID).Select(z => new PreAssessmentVitalDetail
                        {
                            VitalDetail = z,
                            VitalDetailID = z.ID
                        }).ToList()
                    }).ToList();

                    model.PatientPreAssesment.InitialMedicalAssessment.InitialMedicalAssesmentDetails = inmedLookupList.Select(x => new InitialMedicalAssessmentDetail
                    {
                        InitialMedicalDetailTypeId = x.ID,
                        InitialMedicalDetailType = x
                    }).ToList();


                }

            }  
            
            //model.PatientEncounter = db.PatientService.GetPatientEncounter(id);
            else
            {
                model.PatientEncounter = new PatientEncounter();
                var encounter = db.PatientEncounterService.GetRegisterationNo();
                //var encounterid = encounter.EncounterID + 1;

                //model.PatientEncounter.EncounterID = encounterid;
                model.PatientPreAssesment.InitialMedicalAssessment = new InitialMedicalAssessment();
                model.PatientPreAssesment.InitialMedicalAssessment.InitialMedicalAssesmentDetails = new List<InitialMedicalAssessmentDetail>();

                model.PatientPreAssesmentVitals = tryLookup.Select(x => new PatientPreAssesmentVital
                {
                    VitalTypeID = x.ID,
                    VitalType = x,
                    PreAssessmentVitalDetails = lookupList.Where(y => Convert.ToInt32(y.ParentId) == x.ID).Select(z => new PreAssessmentVitalDetail
                    {
                        VitalDetail = z,
                        VitalDetailID = z.ID
                    }).ToList()
                }).ToList();

                model.PatientPreAssesment.InitialMedicalAssessment.InitialMedicalAssesmentDetails = inmedLookupList.Select(x => new InitialMedicalAssessmentDetail
                {
                    InitialMedicalDetailTypeId = x.ID,
                    InitialMedicalDetailType = x
                }).ToList();

               //model.PatientPreAssesment.InitialMedicalAssessment.ExaminationDetails = generalLookupList.Select(x => new ExaminationDetail
               // {
               //     ExaminationDetailTypeId = x.ID,
               //     ExaminationDetailType = x
               // }).ToList();
            }

            model.PatientEncounter.EncounterDate = DateTime.Today;
            model.PatientEncounter.PatientID = id;

            return View(model);
        }

        [HttpPost]
        public IActionResult EncounterAdd(PatientEncounterViewModel model)
        {
           
            var result = new User();
           
            var encounter = db.PatientEncounterService.GetRegisterationNo();
            var encounterid1 = encounter.EncounterID + 1;
            model.PatientEncounter.EncounterID = encounterid1;
            var encounterdetail= db.PatientEncounterService.InsertPatientEncounter(model.PatientEncounter);
            var encounterid = encounterdetail.ID;
            model.PatientPreAssesment.PatientEncounterID = encounterid;
            model.PatientPreAssesment.PatientID = encounterdetail.PatientID;
            var patientpreassesment = db.PatientEncounterService.InsertPreAssesment(model.PatientPreAssesment);
            var patientpreassesmentid = patientpreassesment.ID;
            model.PatientPreAssesmentVitals.Select(c => { c.PatientEncounterID = encounterid; return c; }).ToList();
            var patientassesmentvital = db.PatientEncounterService.InsertPatientPreAssesmentVital(model.PatientPreAssesmentVitals);
            foreach (var item in model.PreAssessmentVitalDetails)
            {
                item.PatientPreAssesmentVitalID = patientassesmentvital.Where(x => x.VitalTypeID == Convert.ToInt32(item.VitalDetail.ParentId)).FirstOrDefault().ID;
                item.VitalDetail = null;
            }
            var preassesmentvitaldetail = db.PatientEncounterService.InsertPreAssesmentVitalDetail(model.PreAssessmentVitalDetails);
            model.PatientPreAssesment.InitialMedicalAssessment.PatientPreAssesmentID = patientpreassesmentid;
            // var initialMedicalAssessment = db.PatientEncounterService.InsertInitialMedicalAssessment(model.PatientPreAssesment.InitialMedicalAssessment);
            foreach (var item1 in model.InitialMedicalAssessmentDetail)
            {
                item1.InitialMedicalAssessmentId = patientpreassesment.InitialMedicalAssessment.ID;
            }
            var intialmedicaldetail = db.PatientEncounterService.InsertInitialMedicalAssessmentDetail(model.InitialMedicalAssessmentDetail);


            return Json(result);
        }


        public IActionResult SearchById(string Id)
        {
            var patients = new PagedData<User>();
            var context = db.UserService.GetPatients();

            if (!string.IsNullOrEmpty(Id))
            {
                var result = context.Where(x => x.ID.ToString().Contains(Id.ToString(), StringComparison.OrdinalIgnoreCase) ||
                (x.EnFirstName != null && x.EnFirstName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.EnSecondName != null && x.EnSecondName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.RegisterationNo != null && x.RegisterationNo.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.Mobile != null && x.Mobile.ToString().Contains(Id, StringComparison.OrdinalIgnoreCase)))
                .OrderByDescending(x => x.AddedDate).Take(PageSize).ToList();
                patients.Data = result;
                patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));
            }
            else
            {
                patients.Data = null;
            }
            return PartialView("_searchadd", patients);

        }

        public IActionResult SearchForOrder(string Id)
        {
            var patients = new PagedData<User>();
            var context = db.UserService.GetPatients();

            if (!string.IsNullOrEmpty(Id))
            {
                var result = context.Where(x => x.ID.ToString().Contains(Id.ToString(), StringComparison.OrdinalIgnoreCase) ||
                (x.EnFirstName != null && x.EnFirstName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.ArFirstName != null && x.ArFirstName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.ArSecondName != null && x.ArSecondName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.ArThirdName != null && x.ArThirdName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.EnSecondName != null && x.EnSecondName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.EnThirdName != null && x.EnThirdName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.RegisterationNo != null && x.RegisterationNo.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.Mobile != null && x.Mobile.ToString().Contains(Id, StringComparison.OrdinalIgnoreCase)))
                .OrderByDescending(x => x.AddedDate).Take(PageSize).ToList();
                patients.Data = result;
                patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));
            }
            else
            {
                patients.Data = null;
            }
            return PartialView("_SearchForOrder", patients);

        }


        public class JsonEvent
        {
            public int id { get; set; }
            public string name { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public string text { get; set; }
            public int resource { get; set; }
            public string Doctor { get; set; }
            public string Patient { get; set; }
            public string Department { get; set; }
            public string Room { get; set; }
            public string Mobile { get; set; }
            public string Note { get; set; }

        }
        public IActionResult getpaitentsinAppointment()
        {
            var patients = new PagedData<User>();
            var result = db.UserService.GetPatients().OrderBy(x => x.AddedDate).ToList();

            List<Lookup> lookup = db.LookupService.GetLookups().ToList();

            patients.Data = (result).Take(PageSize);

            patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)result.Count() / PageSize));

            ViewBag.Status = db.LookupService.GetByType("Status");

            foreach (var item in patients.Data)
            {
                var tryResult = db.PatientService.GetAll().Where(x => x.UserID == item.ID).FirstOrDefault();
                if (tryResult != null)
                {
                    item.PatientAdmission.AdmissionType = lookup.Where(x => x.ID == tryResult.AdmissionTypeID).FirstOrDefault();
                }

            }
            return PartialView("_SearchAdd", patients);
        }
        public IActionResult getDoctorDetails(int Id)
        {
            var context = db.UserService.GetUser(Id);
            return Json(context);


        }
        public IActionResult SearchInAppointmentList(string Id, int page)
        {
            var patients = new PagedData<User>();
            var result = db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();

            var searchList = result;

            if (!string.IsNullOrEmpty(Id))
            {

                searchList = result.Where(x => x.RegisterationNo.Contains(Id.ToString(), StringComparison.OrdinalIgnoreCase) ||
                (x.EnFirstName != null && x.EnFirstName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.EnSecondName != null && x.EnSecondName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.ID.ToString().Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.ArFirstName != null && x.ArFirstName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.Mobile != null && x.Mobile.Contains(Id, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            patients.Data = searchList.OrderByDescending(x => x.AddedDate).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
            patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)searchList.Count() / PageSize));

            return PartialView("_SearchAdd", patients);
        }

        public IActionResult GetAllPatients()
        {
            var patients = new PagedData<User>();

            var context = db.UserService.GetPatients().ToList();
            patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Count() / PageSize));
            //   patients.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Count() / PageSize));

            return PartialView("_SearchAdd", patients);
        }

        public IActionResult GetAppointmentID(int AppointmentID)
        {
            var appointment = db.AppointmentService.GetById(AppointmentID);

            return Json(appointment);
        }

        public IActionResult GetAppointmentReservedBy(int AppointmentID)
        {
            var appointment = db.AppointmentService.GetById(AppointmentID);
            var user = db.UserService.GetAll().Where(x => x.ID == appointment.CreatedBy).FirstOrDefault();
            string createdByName = "";
            if (user != null)
            {
                createdByName = user.EnFirstName + " " + user.EnSecondName + " " + user.EnThirdName;
            }



            return Json(createdByName);
        }

        // shubham 
        public IActionResult SearchInCompletedAppointmentList(string Id, DateTime? StartDate, DateTime? EndDate, int page, int AppointmentStatusID)
        {
            ViewBag.StatusId = AppointmentStatusID;
            var patients = new PagedData<User>();
            var pagedAppointments = new PagedData<Appointment>();
            var context = db.UserService.GetPatients();
            var getLookup = db.LookupService.GetLookups().Where(x => x.Type == "AppointmentStatus" && x.TypeID == AppointmentStatusID).FirstOrDefault();
            var getLookupId = getLookup.ID;
            var searchList = db.AppointmentService.GetAll().Where(x => x.AppointmentStatusID == getLookupId).ToList();
            if (!string.IsNullOrEmpty(Id))
            {
                searchList = searchList.Where(x => x.User != null && x.User.RegisterationNo.Contains(Id.ToString(), StringComparison.OrdinalIgnoreCase) ||
                (x.Doctor != null && x.Doctor.EnFirstName != null && x.Doctor.EnFirstName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.Doctor != null && x.Doctor.EnSecondName != null && x.Doctor.EnSecondName.Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.ID.ToString().Contains(Id, StringComparison.OrdinalIgnoreCase)) ||
                (x.User != null && x.User.EnFirstName != null && x.User.EnFirstName.Contains(Id, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            if (StartDate != null && EndDate != null)
            {
                searchList = searchList.Where(x => x.StartDate >= StartDate && x.StartDate <= EndDate).ToList();
            }
            else if (StartDate != null && EndDate == null)
            {
                searchList = searchList.Where(x => x.StartDate >= StartDate).ToList();
            }
            else if (StartDate == null && EndDate != null)
            {
                searchList = searchList.Where(x => x.StartDate <= EndDate).ToList();
            }
            pagedAppointments.Data = searchList.OrderByDescending(x => x.AddedDate).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
            pagedAppointments.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)searchList.Count() / PageSize));


            return PartialView("appointmentSearchList", pagedAppointments);

        }

        public IActionResult AppointmentListOfStatus(int AppointmentStatus, int page)
        {
            var patients = new PagedData<User>();
            var pagedAppointments = new PagedData<Appointment>();
            var getLookup = db.LookupService.GetLookups().Where(x => x.Type == "AppointmentStatus" && x.TypeID == AppointmentStatus).FirstOrDefault();
            var getLookupId = getLookup.ID;
            ViewBag.StatusId = AppointmentStatus;
            var appointmentList = db.AppointmentService.GetAll().ToList();
            var appointmentstatuslist = appointmentList.Where(x => x.AppointmentStatusID == getLookupId).ToList();
            pagedAppointments.Data = appointmentstatuslist.OrderByDescending(x => x.AddedDate).Skip(PageSize * (page - 1)).Take(PageSize).ToList();
            pagedAppointments.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)appointmentstatuslist.Count() / PageSize));

            return PartialView("appointmentSearchList", pagedAppointments);
        }
        public IActionResult ExportViaEmail(int Id)
        {

            var result = db.UserService.GetUser(Id);
            var company = db.CompanyProfileService.GetAll().FirstOrDefault();
            EmailViewModel email = new EmailViewModel();
            var template = db.EmailService.getTriggerByType(Convert.ToInt32(EmailTriggerType.Client).ToString());
            if (template != null)
            {
                if(template.Email.Body.Contains("##Name"))
                {
                    template.Email.Body= template.Email.Body.Replace("##Name",result.EnFirstName);

                }
                if (template.Email.Body.Contains("##CompanyName"))
                {
                    template.Email.Body= template.Email.Body.Replace("##CompanyName", company.CompanyEnName);

                }
                email.Subject = template.Email.Subject + " " + "(" + result.RegisterationNo + ")";
                email.Body = template.Email.Body;
            }
            email.TargetID = result.ID;
            email.FromEmail = company.Email;
            email.ToEmail = result.Email;
            email.TriggerType = EmailTriggerType.Client.ToString();
            return PartialView(email);
        }
        public IActionResult ExportViaSMS(int Id)
        
        {
            var result = db.UserService.GetUser(Id);
            var company = db.CompanyProfileService.GetAll().FirstOrDefault();
            SMSViewModel sms = new SMSViewModel();
            var template = db.SMSService.getTriggerByType(Convert.ToInt32(EmailTriggerType.Client).ToString());
            if (template != null)
            {
                if (template.SMS.Body.Contains("##Name"))
                {
                    template.SMS.Body = template.SMS.Body.Replace("##Name", result.EnFirstName);

                }
                if (template.SMS.Body.Contains("##CompanyName"))
                {
                    template.SMS.Body = template.SMS.Body.Replace("##CompanyName", company.CompanyEnName);

                }
               
                sms.Body = template.SMS.Body;
            }
            sms.TargetID = result.ID;
            sms.FromMobile = company.Telephone1;
            sms.ToMobile = result.Mobile;
            sms.TriggerType = SMSTriggerType.Client.ToString();
            return PartialView(sms);
        }

        public IActionResult CustExportSMS(int Id)

        {
            var result = db.UserService.GetUser(Id);
            var company = db.CompanyProfileService.GetAll().FirstOrDefault();
            SMSViewModel sms = new SMSViewModel();
            var template = db.SMSService.getTriggerByType(Convert.ToInt32(EmailTriggerType.Client).ToString());
            if (template != null)
            {
                if (template.SMS.Body.Contains("##Name"))
                {
                    template.SMS.Body = template.SMS.Body.Replace("##Name", result.EnFirstName);

                }
                if (template.SMS.Body.Contains("##CompanyName"))
                {
                    template.SMS.Body = template.SMS.Body.Replace("##CompanyName", company.CompanyEnName);

                }

                sms.Body = template.SMS.Body;
            }
            sms.TargetID = result.ID;
            sms.FromMobile = company.Telephone1;
            sms.ToMobile = result.Mobile;
            sms.TriggerType = SMSTriggerType.Client.ToString();
            return PartialView(sms);
        }

        //public IActionResult CustExportViaSMS(int Id, string message)

        //{
        //    var result = db.UserService.GetUser(Id);
        //    var company = db.CompanyProfileService.GetAll().FirstOrDefault();
        //    SMSViewModel sms = new SMSViewModel();
        //    var template = db.SMSService.getTriggerByType(Convert.ToInt32(EmailTriggerType.Client).ToString());
        //    if (template != null)
        //    {
        //        if (template.SMS.Body.Contains("##Name"))
        //        {
        //            template.SMS.Body = template.SMS.Body.Replace("##Name", result.EnFirstName);

        //        }
        //        if (template.SMS.Body.Contains("##CompanyName"))
        //        {
        //            template.SMS.Body = template.SMS.Body.Replace("##CompanyName", company.CompanyEnName);

        //        }

        //        sms.Body = "";
        //    }
        //    sms.TargetID = result.ID;
        //    sms.FromMobile = company.Telephone1;
        //    sms.ToMobile = result.Mobile;
        //    sms.TriggerType = SMSTriggerType.Client.ToString();
        //    return PartialView(sms);
        //}

        public IActionResult CustRemoteValidation(string Type, string SearchString, int ID)
        {
            var checkList = db.UserService.GetAll();
            var userList = new List<User>();
            if (ID == 0)
            {
                switch (Type)
                {
                    case ("regn"):
                        userList = checkList.Where(x => x.RegisterationNo == SearchString).ToList();
                        break;
                    case ("typeid"):
                        userList = checkList.Where(x => x.PatientPersonalInformation != null && x.PatientPersonalInformation.IdentificationTypeID != null &&
                        (x.PatientPersonalInformation.IdentificationTypeID.Equals(SearchString, StringComparison.OrdinalIgnoreCase))).ToList();
                        break;
                    case ("username"):
                        userList = checkList.Where(x => x.UserName == SearchString).ToList();
                        break;
                }
                if (userList.Count > 0)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }

            }
            else
            {
                userList = checkList.Where(x => x.ID != ID).ToList();
                switch (Type)
                {
                    case ("regn"):
                        userList = userList.Where(x => x.RegisterationNo == SearchString).ToList();
                        break;
                    case ("typeid"):
                        userList = userList.Where(x => x.PatientPersonalInformation != null && (x.PatientPersonalInformation.IdentificationTypeID.Equals(SearchString, StringComparison.OrdinalIgnoreCase))).ToList();
                        break;
                    case ("username"):
                        userList = userList.Where(x => x.UserName == SearchString).ToList();
                        break;
                }
                if (userList.Count > 0)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }
            }
        }
    }
}
