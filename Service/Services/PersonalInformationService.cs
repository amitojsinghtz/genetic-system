using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class PersonalInformationService : IPersonalInformation
    {
        private IUnitOfWork db;
        public PersonalInformationService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(PersonalInformation model)
        {
            db.PersonalInformation.Insert(model);
        }

        public void Delete(long id)
        {
            var result = db.PersonalInformation.Get().Where(x => x.ID == id).FirstOrDefault();
            db.PersonalInformation.Remove(result);
            db.PersonalInformation.SaveChanges();
        }

        public PersonalInformation GeById(long id)
        {
            return db.PersonalInformation.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public PersonalInformation GeByUser(long id)
        {
            return db.PersonalInformation.Get().Where(x=>x.UserID == id).Include(x=>x.Country).Include(x=>x.City).FirstOrDefault();
        }

        public IEnumerable<PersonalInformation> GetAll()
        {
            return db.PersonalInformation.GetAll();
        }

        public void Update(PersonalInformation model)
        {
            db.PersonalInformation.Update(model);
        }
    }
}
