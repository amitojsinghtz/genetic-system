using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;

namespace HISSystem.Areas.Admin.Controllers
{
    public class AttachmentController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;
        public AttachmentController(IHostingEnvironment _appEnvironment, IUnitOfService db)
        {
            this._appEnvironment = _appEnvironment;
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Attachment(int id, string tableName)
        {
            var result = db.AttachmentService.GetById(id, tableName);
            return PartialView(result);
        }

        public IActionResult Delete(int id)
        {
            db.AttachmentService.Delete(id);
            return Json(true);
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public IActionResult Upload(AttachmentModel attachment)
        {
            var model = new Data.Models.Attachment();
            if (attachment.File != null)
            {
                var path = Path.Combine(_appEnvironment.WebRootPath, "Uploaded", attachment.File.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    attachment.File.CopyToAsync(stream);
                    model.AttachmentPath = "/uploaded/" + attachment.File.FileName;
                    model.AttachmentName = attachment.File.FileName;
                }
            }
            model.UserID = attachment.ID;
            model.TableName = attachment.TableName;
            db.AttachmentService.Add(model);
            return Json(true);
        }
        public IActionResult RefreshAttach(int id, string tableName)
        {
            var result = db.AttachmentService.GetById(id, tableName);
            return Json(result);
        }
    }

    public class AttachmentModel
    {
        public IFormFile File { get; set; }
        public int ID { get; set; }
        public string TableName { get; set; }
    }
}