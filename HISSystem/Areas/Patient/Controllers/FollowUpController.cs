using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using GeneticSystem.Areas.Admin.Models.FollowUp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;

namespace GeneticSystem.Areas.Patient.Controllers
{
    public class FollowUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}