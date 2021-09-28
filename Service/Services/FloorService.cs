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
    public class FloorService:IFloorService
    {
        private IUnitOfWork db;
        public FloorService(IUnitOfWork db)
        {
            this.db = db;
        }

        public void Add(Floor model)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Floor GeById(long id)
        {
            return db.Floor.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Floor> GetAll()
        {
            return db.Floor.GetAll();
        }

        public void Update(Floor model)
        {
            throw new NotImplementedException();
        }
    }
}
