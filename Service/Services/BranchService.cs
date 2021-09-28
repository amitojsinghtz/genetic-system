using Data.Models;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class BranchService : IBranchService
    {
       // private IRepository<Branch> branch;
        private IUnitOfWork db;
        public BranchService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(Branch model)
        {
             db.Branch.Insert(model);
        }

        public void Delete(long id)
        {
            var result = db.Branch.Get().Where(x => x.ID == id).FirstOrDefault();
            db.Branch.Remove(result);
            db.Branch.SaveChanges();
        }

        public Branch GeById(long id)
        {
            return db.Branch.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Branch> GetAll()
        {
            return db.Branch.GetAll();
        }

        public void Update(Branch model)
        {
            db.Branch.Update(model);
        }
    }
}
