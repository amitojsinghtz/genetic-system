using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Text;

using Data.DTO;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq;


namespace Service.Services
{
    public class MedicalDictionaryService : IMedicalDictionaryService
    {
        private IUnitOfWork db;
        public MedicalDictionaryService(IUnitOfWork db)
        {
            this.db = db;
        }

        public List<MedicalDictionary> GetMedicalDictionary()
        {
            return db.MedicalDictionary.Get().Where(x => x.IsActive == true).Include(x => x.MedicalDictionarySpecialty).Include(x =>x.Type).ToList();
        }

        //public PatientOrder AddOrder(PatientOrder model)
        //{

        //    db.PatientOrder.Insert(model);
        //    return model;
        //}

        public MedicalDictionary AddMedicalDictionary(MedicalDictionary medicalDictionary)
        {
            db.MedicalDictionary.Insert(medicalDictionary);
            return medicalDictionary;
        }

        public void DeleteMedicalDictionary(long id)
        {
            var result = db.MedicalDictionary.Get().Where(x => x.ID == id).FirstOrDefault();
                result.IsActive = false;
                db.MedicalDictionary.Update(result);
                db.MedicalDictionary.SaveChanges();
        }

        public MedicalDictionary GetMedicalDictionaryById(int Id)
        {
            var result = db.MedicalDictionary.Get().Where(x => x.IsActive == true && x.ID ==Id).Include(x => x.MedicalDictionarySpecialty).FirstOrDefault();
            
                result.Specialty = result.MedicalDictionarySpecialty.Where(x=>x.IsActive==true).Select(x => x.SpecialtyID.ToString()).ToArray();
            return result;
        }

        public List<MedicalDictionarySpecialty> GetMedicalDictionarySpecialtyById(int Id)
        {
            return db.MedicalDictionarySpecialty.Get().Where(x => x.ID == Id && x.IsActive == true).ToList();
        }

        public MedicalDictionary UpdatMedicalDictionary(MedicalDictionary model)
        {

            db.MedicalDictionary.Update(model);
            db.MedicalDictionary.SaveChanges();
            return model;
        }

        public List<MedicalDictionarySpecialty> AddMedicalDictionaryService(List<MedicalDictionarySpecialty> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.MedicalDictionarySpecialty.AddRange(model);
            context.SaveChanges();
            return model;
        }

        public void DeleteMedicalDictionarySpecialty(long Id)
        {
            var result = db.MedicalDictionarySpecialty.Get().Where(x => x.MedicalDictionaryID == Id).ToList();
            foreach (var item in result)
            {
                item.IsActive = false;
                db.MedicalDictionarySpecialty.Delete(item);
                db.MedicalDictionarySpecialty.SaveChanges();
            }
        }
       
    }
}
