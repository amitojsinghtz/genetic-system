using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.UnitOfServices;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;
        private readonly IConfiguration iConfiguration;
        public EmailController(IUnitOfService db, IHostingEnvironment _appEnvironment, IConfiguration iConfig)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
            this.iConfiguration = iConfig;
        }
        public IActionResult Index()
        {
            var Emails = new PagedData<Email>();
            var emailList = db.EmailService.GetEmails().Where(x => x.IsActive == true).ToList();
            Emails.Data = (emailList).Take(PageSize);

            Emails.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)emailList.Count() / PageSize));
            return View(Emails);
        }
        public IActionResult GetEmails(int page, string searchstring)
        {
            var Emails = new PagedData<Email>();
            var emailList = db.EmailService.GetEmails().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                emailList = emailList.Where(x => x.Name != null && x.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) ||
                (x.Subject != null && x.Subject.Contains(searchstring, StringComparison.OrdinalIgnoreCase)) ||
                (x.Body != null && x.Body.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            Emails.Data = (emailList).Skip(PageSize * (page - 1)).Take(PageSize);
            Emails.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)emailList.Count() / PageSize));

            return PartialView(Emails);
        }
        public IActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Add(Email model)
        {
            model.CreatedBy = Request.Cookies["ID"];
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var status = db.EmailService.Add(model);
            return RedirectToAction("index");


        }
        public IActionResult Update(int id)
        {
            var data = db.EmailService.GeById(id);
            return PartialView(data);
        }
        [HttpPost]
        public IActionResult Update(Email model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            db.EmailService.Update(model);
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            db.EmailService.Delete(id);
            return RedirectToAction("index");
        }
        public IActionResult Trigger()
        {
            var triggers = new PagedData<EmailTrigger>();

            var data = db.EmailService.GetAllEmailTriggers();
            triggers.Data = (data).Take(PageSize);
            triggers.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)data.Count() / PageSize));

            return View(triggers);
        }
        public IActionResult AddTriggerEmail()
        {
            var model = new EmailTrigger();
            ViewBag.AllEmails = db.EmailService.GetEmails().Where(x => x.IsActive == true).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult AddTriggerEmails(EmailTrigger model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var result = db.EmailService.AddTrigger(model);
            return RedirectToAction("Trigger");
        }
        public IActionResult UpdateEmailTrigger(int Id)
        {
            var model = db.EmailService.GetTriggersById(Id);
            ViewBag.AllEmails = db.EmailService.GetEmails().Where(x => x.IsActive == true).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult UpdateEmailTriggers(EmailTrigger model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            db.EmailService.UpdateTrigger(model);
            return RedirectToAction("Trigger");

        }
        public IActionResult DeleteTrigger(int id)
        {
            db.EmailService.DeleteTrigger(id);
            return RedirectToAction("Trigger");
        }
        public IActionResult GetTriggers(int page, string searchstring)
        {
            var EmailTrigger = new PagedData<EmailTrigger>();
            var emailtriggerList = db.EmailService.GetAllEmailTriggers().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                emailtriggerList = emailtriggerList.Where(x => x.Email != null && x.Email.Name != null && x.Email.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
            }
            EmailTrigger.Data = (emailtriggerList).Skip(PageSize * (page - 1)).Take(PageSize);
            EmailTrigger.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)emailtriggerList.Count() / PageSize));

            return PartialView(EmailTrigger);

        }

        public IActionResult Send(EmailSendModel emailViewmodel)
        {
            // List<IFormFile> File = null;
            if (emailViewmodel.File2!=null)
            {
                emailViewmodel.File2 = new List<IFormFile>(Request.Form.Files);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                SendEmails.SendInvoiceMail(emailViewmodel, ms, this.iConfiguration);
            }
            return Json(true);      
        }
    }
}