using Data.Models;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class PatientService: IPatientAdmissionService
    {
        private IUnitOfWork db;
        public PatientService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(PatientAdmission model)
        {
            db.PatientAdmission.Insert(model);
        }
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
        public PatientAdmission GetById(long id)
        {
            return db.PatientAdmission.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public PatientAdmission GetPatientAdmission(long id)
        {
            //db.PatientAdmission.Get().Where(x => x.UserID == id).OrderByDescending(x => x.AddedDate).FirstOrDefault();

            var patientAdmissions = db.PatientAdmission.Get().Where(x => x.UserID == id).Include(x => x.PatientType).Include(x => x.OutPatientType).ToList();

            return patientAdmissions.Where(x => /*x.PatientType.Type == "PatientType" && x.PatientType.TypeID == 1*/ x.PatientTypeID == 59).OrderByDescending(x => x.AddedDate).FirstOrDefault();
        }

        public PatientAdmission GetOutPatient(long id)
        {
            //db.PatientAdmission.Get().Where(x => x.UserID == id).OrderByDescending(x => x.AddedDate).FirstOrDefault();

            var patientAdmissions = db.PatientAdmission.Get().Where(x => x.UserID == id).ToList();

            return patientAdmissions.Where(x => x.PatientTypeID == 62).OrderByDescending(x => x.AddedDate).FirstOrDefault();
        }

        public IEnumerable<PatientAdmission> GetAll()
        {
           return db.PatientAdmission.GetAll();
        }

        public PatientEncounter GetPatientEncounter(int id)
        {
            //return db.PatientEncounter.Get().Where(x => x.PatientID == id).FirstOrDefault();
            return null;
        }

        public void Update(PatientAdmission model)
        {
            db.PatientAdmission.Update(model);
            db.PatientAdmission.SaveChanges();
        }
    }
}
