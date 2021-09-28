using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using System.Linq;
using Service.IServices;
using Repository;

namespace Service.Services
{
    public class PatientEncounterService: IPatientEncounterService
    {
        private IUnitOfWork db;
        public PatientEncounterService(IUnitOfWork db)
        {
            this.db = db;
        }
        public PatientEncounter InsertPatientEncounter(PatientEncounter model)
        {
            model.IsActive = true;
          db.PatientEncounter.Insert(model);
            return model;
            //return null;
        }
        public PatientPreAssesment InsertPreAssesment(PatientPreAssesment model)
        {
           
            db.PatientPreAssesment.Insert(model);   
           return model;
            //return null;
        }
        public List<PatientPreAssesmentVital> InsertPatientPreAssesmentVital(List<PatientPreAssesmentVital> model)
        {
            //db.PatientPreAssesmentVital.AddRange(model);
            ApplicationContext context = new ApplicationContext();
            context.PatientPreAssesmentVital.AddRange(model);
            context.SaveChanges();

            return model;
        }
        
        public List<PreAssessmentVitalDetail> InsertPreAssesmentVitalDetail(List<PreAssessmentVitalDetail> model)
        {
            
            ApplicationContext context = new ApplicationContext();
            context.PreAssessmentVitalDetail.AddRange(model);
            context.SaveChanges();

            return model;
        }
        public List<InitialMedicalAssessmentDetail> InsertInitialMedicalAssessmentDetail(List<InitialMedicalAssessmentDetail> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.InitialMedicalAssessmentDetail.AddRange(model);
            context.SaveChanges();

            return model;
        }
        public PatientEncounter GetByPatientId(long id)
        {
            return db.PatientEncounter.Get().Where(x => x.PatientID == id).FirstOrDefault();
        }
        public PatientPreAssesment GetPatientPreAssesmentDetail(long Id)
        {
            return db.PatientPreAssesment.Get().Where(x => x.PatientEncounterID== Id).FirstOrDefault();
        }
        public List<PatientPreAssesmentVital> GetPatientPreAssesmentVital(long Id)
        {
            return db.PatientPreAssesmentVital.Get().Where(x => x.PatientEncounterID == Id).ToList();
        }
        public List<PreAssessmentVitalDetail> GetPreAssessmentVitalDetail(long Id)
        {
            return db.PreAssessmentVitalDetail.Get().Where(x => x.PatientPreAssesmentVitalID == Id).ToList();
        }
        public InitialMedicalAssessment GetInitialMedicalAssessment(long Id)
        {
            return db.InitialMedicalAssessment.Get().Where(x => x.PatientPreAssesmentID == Id).FirstOrDefault();
        }
        public List<InitialMedicalAssessmentDetail> GetInitialMedicalAssessmentDetails(long Id)
        {
            return db.InitialMedicalAssessmentDetail.Get().Where(x => x.InitialMedicalAssessmentId == Id).ToList();
        }
        public PatientEncounter GetRegisterationNo()
        {
            return db.PatientEncounter.Get().OrderByDescending(x => x.EncounterID).FirstOrDefault();
        }
    }
}
