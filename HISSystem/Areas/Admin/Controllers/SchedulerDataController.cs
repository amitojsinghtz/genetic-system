using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfServices;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using Data.Models;
using DevExtreme.NETCore.Demos.Models;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerDataController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private IUnitOfService db;

        public SchedulerDataController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var xray = Json(DataSourceLoader.Load(db.AppointmentExpService.GetAppointments(), loadOptions));
            return Json(DataSourceLoader.Load(db.AppointmentExpService.GetAppointments(), loadOptions));
        }

        [HttpPost]
        public IActionResult Post([FromForm]string values)
        {
            var newAppointment = new AppointmentExp();
            values = values.Replace("ClientID", "ClientID");
            values = values.Replace("Genes", "GeneId");
            values = values.Replace("Tests", "TestTempArray");
            JsonConvert.PopulateObject(values, newAppointment);

            //if(newAppointment.TestTempArray != null)
            //{
            //    newAppointment.TestID = string.Join(',', newAppointment.TestTempArray);
            //}

            if (!TryValidateModel(newAppointment))
                return BadRequest();

            db.AppointmentExpService.AddAppointment(newAppointment);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromForm]int key, [FromForm]string values)
        {
            var appointment = db.AppointmentExpService.GetAppointmentByID(key);
            values = values.Replace("ClientID", "ClientID");
            values = values.Replace("Genes", "GeneId");
            values = values.Replace("Tests", "TestTempArray");
            JsonConvert.PopulateObject(values, appointment);
     
            if (!TryValidateModel(appointment))
                return BadRequest();

            db.AppointmentExpService.UpdateAppointmentExp(appointment);

            return Ok();
        }

        [HttpDelete]
        public void Delete([FromForm]int key)
        {
            var appointment = db.AppointmentExpService.GetAppointmentByID(key);
            db.AppointmentExpService.DeleteAppointmentExp(appointment);
        }
    }
}