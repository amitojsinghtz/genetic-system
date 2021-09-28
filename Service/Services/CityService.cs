using Data.Models;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class CityService : ICityService
    {
        private IUnitOfWork db;

        public CityService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(City model)
        {
            db.City.Insert(model);
        }

        public void Delete(long id)
        {
            var result = db.City.Get().Where(x => x.ID == id).FirstOrDefault();
            db.City.Remove(result);
            db.City.SaveChanges();
        }

        public City GeById(long id)
        {
            return db.City.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<City> GetAll()
        {
            return db.City.GetAll();
        }

        public void Update(City model)
        {
            db.City.Update(model);
        }
    }
}
