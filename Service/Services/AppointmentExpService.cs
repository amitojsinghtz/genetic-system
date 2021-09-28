using DevExtreme.NETCore.Demos.Models;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class AppointmentExpService
    {
        private IUnitOfWork db;

        public AppointmentExpService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<AppointmentExp> GetAppointments()
        {
            var result =  db.AppointmentExp.Get().Select(x => new AppointmentExp { 
             AllDay = x.AllDay,
             AppointmentId = x.AppointmentId,
             EndDate = x.EndDate,
             StartDate = x.StartDate,
             Text = x.Text,
             RecurrenceRule = x.RecurrenceRule,
             Description = x.Description,
             ClientID = x.ClientID,
             TestID = x.TestID,
             GeneID = x.GeneID
            }).ToList();

            return result;

        }

        public AppointmentExp AddAppointment(AppointmentExp appointmentExp)
        {
            db.AppointmentExp.Insert(appointmentExp);

            return appointmentExp;
        }
        public AppointmentExp GetAppointmentByID(int id)
        {
            return db.AppointmentExp.Get().Where(x => x.AppointmentId == id).FirstOrDefault();
        }
        public AppointmentExp UpdateAppointmentExp(AppointmentExp appointmentExp)
        {
            try { 
            db.AppointmentExp.Update(appointmentExp);
            db.AppointmentExp.SaveChanges();
            }catch(Exception ex)
            {
                throw ex;
            }

            return appointmentExp;
        }
        public void DeleteAppointmentExp(AppointmentExp appointmentExp)
        {
            db.AppointmentExp.Remove(appointmentExp);
            db.AppointmentExp.SaveChanges();
        }
    }
}
