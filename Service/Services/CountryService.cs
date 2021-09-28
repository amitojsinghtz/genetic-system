using Data.Models;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class CountryService : ICountryService
    {
        private IUnitOfWork db;

        public CountryService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(Country model)
        {
            db.Country.Insert(model);
        }

        public void Delete(long id)
        {
            var result = db.Country.Get().Where(x => x.ID == id).FirstOrDefault();
            db.Country.Remove(result);
            db.Country.SaveChanges();
        }

        public Country GeById(long id)
        {
            return db.Country.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Country> GetAll()
        {
            return db.Country.GetAll();
        }

        public void Update(Country model)
        {
            db.Country.Update(model);
        }
    }
}
