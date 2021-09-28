using Data.Models;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class RoleService : IRoleService
    {
       // private IRepository<Role> role;
        private IUnitOfWork db;

        public RoleService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(Role model)
        {
            db.Role.Insert(model);
        }

        public void Delete(long id)
        {
            var result = db.Role.Get().Where(x => x.ID == id).FirstOrDefault();
            db.Role.Remove(result);
            db.Role.SaveChanges();
        }

        public Role GeById(long id)
        {
            return db.Role.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Role.GetAll();
        }

        public void Update(Role model)
        {
            db.Role.Update(model);
        }

        public int GetByName(string name)
        {
            return db.Role.Get().Where(x=>x.Name.Contains(name)).FirstOrDefault().ID;
        }
    }
}
