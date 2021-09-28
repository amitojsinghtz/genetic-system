using Data.Models;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class PatientPersonalInformationService : IPatientPersonalInformationService
    {
        private IUnitOfWork db;
        public PatientPersonalInformationService(IUnitOfWork db)
        {
            this.db = db;
        }

        public PatientPersonalInformation Add(PatientPersonalInformation model)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<PatientPersonalInformation> GetAll()
        {
            throw new NotImplementedException();
        }

        public PatientPersonalInformation GetById(long id)
        {
            throw new NotImplementedException();
        }

        public int GetRegistrationNo()
        {
            var regno = db.PatientPersonalInformation.Get().OrderByDescending(x => x.RegistrationNo).Select(x=>x.RegistrationNo).FirstOrDefault();
            if(regno == null)
            {
                regno = 0;
            }
            return Convert.ToInt32(regno+1);
        }

        public void Update(PatientPersonalInformation model)
        {
            db.PatientPersonalInformation.Update(model);
        }
        public void UpdateStatus(int id, int status)
        {
          var patient = db.PatientPersonalInformation.Get().Where(x=>x.ID==id).FirstOrDefault();
            patient.PatientTypeID = status;
            db.PatientPersonalInformation.Update(patient);
            db.PatientPersonalInformation.SaveChanges();
        }
    }
}
