using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IPatientAdmissionService
    {
        IEnumerable<PatientAdmission> GetAll();
        PatientAdmission GetById(long id);
        void Add(PatientAdmission model);
        void Update(PatientAdmission model);
        void Delete(long id);
        PatientAdmission GetPatientAdmission(long id);
        PatientEncounter GetPatientEncounter(int id);
    }
}
