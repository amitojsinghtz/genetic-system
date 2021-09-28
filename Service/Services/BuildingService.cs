using Data.Models;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class BuildingService:IBuildingService
    {
        private IUnitOfWork db;
        public BuildingService(IUnitOfWork db)
        {
            this.db = db;
        }

        public void Add(Building model)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Building GeById(long id)
        {
            return db.Building.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Building> GetAll()
        {
            return db.Building.GetAll();
        }

        public void Update(Building model)
        {
            throw new NotImplementedException();
        }
    }
}
