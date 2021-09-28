using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;

namespace GeneticSystem.Areas.Admin.Controllers
{
       [Area("Admin")]
    public class CompanyProfileController : Controller
    {
        private readonly IUnitOfService db;
        private readonly IHostingEnvironment _appEnvironment;
        public CompanyProfileController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }
        public IActionResult Index()
        {
           CompanyProfile companyProfile = new CompanyProfile();
           var Companyprofile = db.CompanyProfileService.GetAll().FirstOrDefault();
            if (Companyprofile != null)
            {
                companyProfile = Companyprofile;
            }
            

            return View(companyProfile);
        }
        [HttpPost]
        public IActionResult Add(CompanyProfile companyProfile)
        {
            if (companyProfile.ID > 0)
            {
                db.CompanyProfileService.Update(companyProfile);
            }
            else
            {
                db.CompanyProfileService.Add(companyProfile);
            }
            return RedirectToAction("Index");
        }
    }
}