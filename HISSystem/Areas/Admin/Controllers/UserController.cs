using Data.Helpers;
using Data.Models;
using HISSystem.Areas.Admin.Models;
using HISSystem.Helper;
using HISSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HISSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private IUnitOfService db;
        public UserController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }

        public IActionResult Index()
        {
            var users = new PagedData<User>();

            var context = db.UserService.GetUsers();
            users.Data = context.Take(PageSize).ToList();
            users.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Count() / PageSize));

            return View(users);
        }

        public List<User> ReturnUser()
        {
            var users = db.UserService.GetUsers().ToList();

            return users;
        }

        public ActionResult _Index(int page)
        {
            var users = new PagedData<User>();

            var context = db.UserService.GetUsers();
            users.Data = context.Skip(PageSize * (page - 1)).Take(PageSize).ToList();
            users.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)context.Count() / PageSize));

            return PartialView(users);
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            var xray = db.UserService.GetUsers().ToList();
            var searchList = new List<User>();

            if (!String.IsNullOrEmpty(search))
            {
                try
                {
                    searchList = xray.Where(x => (x.ArFirstName != null && x.ArFirstName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.Mobile != null && x.Mobile.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.Email != null && x.Email.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.PersonalInformation != null && x.PersonalInformation.Department != null && x.PersonalInformation.Department.Name != null && x.PersonalInformation.Department.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.PersonalInformation != null && x.PersonalInformation.Position != null && x.PersonalInformation.Position.Name != null && x.PersonalInformation.Position.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.Role != null && x.Role.Name != null && x.Role.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.EnFirstName != null && x.EnFirstName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.Address != null && x.Address.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.PersonalInformation != null && x.PersonalInformation.Country != null && x.PersonalInformation.Country.Name != null && x.PersonalInformation.Country.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (x.PersonalInformation != null && x.PersonalInformation.City != null && x.PersonalInformation.City.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                searchList = xray;
            }

            var pagedSearchList = new PagedData<User>();
            pagedSearchList.Data = searchList.Take(PageSize).ToList();
            pagedSearchList.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)searchList.Count() / PageSize));

            return PartialView("_Index", pagedSearchList);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var loginid = Request.Cookies["Id"];
            if(loginid != null)
            {
                var res = db.UserService.GetUser(Convert.ToInt32(loginid));
                var loginusername = res.EnFirstName + " " + res.EnSecondName + " " + res.EnThirdName + " " + res.EnFamilyName;
                ViewBag.UserName = loginusername;
            }

            ViewBag.Gender = db.LookupService.GetLookUpByTypeName("Gender");
            ViewBag.EmployeeType = db.LookupService.GetLookUpByTypeName("EmployeeType");
            ViewBag.SocialStatus = db.LookupService.GetLookUpByTypeName("SocialStatus");
            ViewBag.PlaceOfBirth = db.LookupService.GetLookUpByTypeName("PlaceOfBirth");
            ViewBag.Status = db.LookupService.GetLookUpByTypeName("Status");
            ViewBag.Religion = db.LookupService.GetLookUpByTypeName("Religion");
            ViewBag.SubDepartment = db.LookupService.GetLookUpByTypeName("SubDepartment");
            ViewBag.Department = db.LookupService.GetLookUpByTypeName("Department");
            ViewBag.ResponsibleOfficer = db.LookupService.GetLookUpByTypeName("ResponsibleOfficer");
            ViewBag.Speciality = db.LookupService.GetLookUpByTypeName("Speciality");
            ViewBag.Position = db.LookupService.GetLookUpByTypeName("Position");
            ViewBag.EmployeeClass = db.LookupService.GetLookUpByTypeName("EmployeeClass");
            ViewBag.IdentificationType = db.LookupService.GetLookUpByTypeName("IdentificationType");

            ViewBag.EducationLevel = db.EducationLevelService.GetAll().ToList();
            ViewBag.Country = db.CountryService.GetAll();
            ViewBag.City = db.CityService.GetAll();
            ViewBag.Role = db.RoleService.GetAll();
            ViewBag.UserDepartment = db.UserService.UserByDepartment(true, 1);
            ViewBag.Branch = db.BranchService.GetAll();
            var model = new User();

            model.AddedDate = DateTime.UtcNow;
            model.PersonalInformation = new PersonalInformation();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Data.Models.User model, IFormFile file, IFormFile filesign,int? SpecialTax)
        {
            if (file != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                    model.ImagePath = "/uploaded/" + file.FileName;
                }
            }

            if (filesign != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", filesign.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    filesign.CopyToAsync(stream);
                    model.SignaturePath = "/uploaded/" + filesign.FileName;
                }
            }

            if (model.RegisterationNo == null)
            {
                var tempId = db.UserService.GetAll().OrderByDescending(x => x.RegisterationNo).First();
                model.RegisterationNo = (Convert.ToInt32(tempId.RegisterationNo) + 1).ToString();

                if (model.UserName == null)
                {
                    model.UserName = model.RegisterationNo;
                }
            }

            model.AddedDate = DateTime.UtcNow;
            model.RoleID = db.RoleService.GetByName("Employee");
            model.IsActive = true;
            model.AddedBy = Convert.ToInt32(Request.Cookies["ID"]);
            model.Password = CommonMethod.Encrypt("12345");
            model.PersonalInformation.SpecialTax = SpecialTax;

            db.UserService.InsertUser(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = db.UserService.GetUser(id);
            var depid = model.PersonalInformation?.DepartmentID;
            ViewBag.Gender = db.LookupService.GetLookUpByTypeName("Gender");
            ViewBag.EmployeeType = db.LookupService.GetLookUpByTypeName("EmployeeType");
            ViewBag.SocialStatus = db.LookupService.GetLookUpByTypeName("SocialStatus");
            ViewBag.PlaceOfBirth = db.LookupService.GetLookUpByTypeName("PlaceOfBirth");
            ViewBag.Status = db.LookupService.GetLookUpByTypeName("Status");
            ViewBag.Religion = db.LookupService.GetLookUpByTypeName("Religion");
            ViewBag.ResponsibleOfficer = db.LookupService.GetLookUpByTypeName("ResponsibleOfficer");
            ViewBag.Speciality = db.LookupService.GetLookUpByTypeName("Speciality");
            ViewBag.Position = db.LookupService.GetLookUpByTypeName("Position");
            ViewBag.EmployeeClass = db.LookupService.GetLookUpByTypeName("EmployeeClass");
            ViewBag.IdentificationType = db.LookupService.GetLookUpByTypeName("IdentificationType");
            ViewBag.SubDepartment = db.LookupService.GetLookUpByTypeName("SubDepartment").Where(x => x.ParentId == ((depid).ToString()));
            ViewBag.Department = db.LookupService.GetLookUpByTypeName("Department");
            
            ViewBag.EducationLevel = db.EducationLevelService.GetAll().ToList();
            ViewBag.Country = db.CountryService.GetAll();
            ViewBag.City = db.CityService.GetAll();
            ViewBag.Role = db.RoleService.GetAll();
            ViewBag.UserDepartment = db.UserService.UserByDepartment(true, 1);
            ViewBag.Branch = db.BranchService.GetAll();
          
            var loginid = model.AddedBy;
            if (model.Password != null)
            {
                var decryptpassword = CommonMethod.Decrypt(model.Password);
                model.Password = decryptpassword;
            }
        

            if (loginid != 0 && loginid!=null)
            {
                var res=db.UserService.GetUser((int)loginid);
                var loginusername = res.EnFirstName +" "+ res.EnSecondName + " " + res.EnThirdName+ " " + res.EnFamilyName;
                ViewBag.AddedBy = loginusername;
            }
           
            model.PersonalInformation = db.PersonalInformationService.GeByUser(model.ID);
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Data.Models.User model, IFormFile file, IFormFile filesign, int? SpecialTax)
        {
            if (file != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                    model.ImagePath = "/uploaded/" + file.FileName;
                }
            }

            if (filesign != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", filesign.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    filesign.CopyToAsync(stream);
                    model.SignaturePath = "/uploaded/" + filesign.FileName;
                }
            }
            model.Password = CommonMethod.Encrypt(model.Password);
            model.PersonalInformation.SpecialTax = SpecialTax;
            model.ModifiedDate = DateTime.UtcNow;
            model.IsActive = true;
            db.UserService.UpdateUser(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(long id)
        {
            db.UserService.DeleteUser(id);
            return RedirectToAction("Index");
        }
        public IActionResult Department(bool isActive, int? departmentID)
        {
            var result = db.UserService.UserByDepartment(isActive, departmentID);
            ViewBag.UserDepartment = result;
            return View(result);
        }
        public IActionResult History(int id, string tableName)
        {
            var result = db.LogTableService.GetAllByTargetId(id, tableName);
            return PartialView(result);
        }
        public IActionResult Attachment(int id, string tableName)
        {
            var result = db.AttachmentService.GetById(id, tableName);
            ViewBag.UserID = id;
            ViewBag.TableName = tableName;
            return PartialView(result);
        }
        public IActionResult ChangePassword(long id)
        {
            ChangePassword model = new ChangePassword();
            var result = db.UserService.GetUser(id);
            model.ID = result.ID;
            model.CurrentPassword = result.Password;
            model.Email = result.Email;
            model.UserName = result.UserName;
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult ResetPassword(ChangePassword model)
        {
           var oldpassword= CommonMethod.Encrypt(model.CurrentPassword);
            var result = db.UserService.GetUsers().Where(x=>x.ID==model.ID && x.Password== oldpassword).FirstOrDefault();
            if (result != null)
            {
                result.Password = CommonMethod.Encrypt(model.NewPassword);
                db.UserService.UpdateUser(result);
                return Json(true);

            }
            
            return Json(false);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsRegisterationInUse(/*[Bind(Prefix = "PersonalInformation.RegisterationNo")]*/ string RegisterationNo)
        {
            if (RegisterationNo != null)
            {
                var xray = db.UserService.GetAll().Where(x => x.RegisterationNo == RegisterationNo).FirstOrDefault();

                if (xray == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"User {RegisterationNo} is already in use");
                }
            }
            else
            {
                return Json(true);
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUserNameInUse(/*[Bind(Prefix = "PersonalInformation.RegisterationNo")]*/ string UserName)
        {
            if (UserName != null)
            {
                var xray = db.UserService.GetAll().Where(x => x.UserName == UserName).FirstOrDefault();

                if (xray == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"User {UserName} is already in use");
                }
            }
            else
            {
                return Json(true);
            }
        }

        public IActionResult FillSubDepartmentDropDown(int DepartmentID)
        {
            List<Lookup> subdepartmentList = new List<Lookup>();

            if (DepartmentID != 0)
            {
                var result = db.LookupService.GetLookups().Where(x => Convert.ToInt32(x.ParentId) == DepartmentID && x.Type.Contains("SubDepartment")).ToList();
                foreach (var item in result)
                {
                    Lookup SubDepartment = new Lookup();

                    SubDepartment.ID = item.ID;
                    SubDepartment.Name = item.Name;
                    SubDepartment.ParentId = item.ParentId;

                    subdepartmentList.Add(SubDepartment);
                }
            }

            return Json(subdepartmentList);
        }
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
                        userList = checkList.Where(x =>x.PersonalInformation !=null && x.PersonalInformation.IdentificationTypeID == SearchString).ToList();
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
                        userList = userList.Where(x => x.PersonalInformation.IdentificationTypeID == SearchString).ToList();
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