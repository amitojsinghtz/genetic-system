using Data.Models;
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
    public class BedAllocationService : IBedAllocationService
    {
        private IUnitOfWork db;
        public BedAllocationService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(BedAllocation model)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public BedAllocation GeById(long id)
        {
            return db.BedAllocation.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<BedAllocation> GetAll()
        {
            return db.BedAllocation.GetAll();
        }

        public void Update(BedAllocation model)
        {
            throw new NotImplementedException();
        }
    }
}
