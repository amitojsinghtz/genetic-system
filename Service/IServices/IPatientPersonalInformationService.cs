using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IPatientPersonalInformationService
    {
        List<PatientPersonalInformation> GetAll();
        PatientPersonalInformation GetById(long id);
        PatientPersonalInformation Add(PatientPersonalInformation model);
        void Update(PatientPersonalInformation model);
        void Delete(long id);
        int GetRegistrationNo();
        void UpdateStatus(int id, int status);
    }
}
