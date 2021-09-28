using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.UnitOfWork;
using Service.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class ViewControlService : IViewControlService
    {
        //private ApplicationContext db;
        private IUnitOfWork db;
        public ViewControlService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(ViewControl model)
        {
            db.ViewControl.Insert(model);

            db.ViewControl.SaveChanges();
        }

        public void Delete(long id)
        {
            var result = db.ViewControl.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.ViewControl.Update(result);
                db.ViewControl.SaveChanges();
            }
        }

        public ViewControl GeById(long id)
        {
            return db.ViewControl.Get().Where(x => x.ID == id).FirstOrDefault();
            //return null;
        }

        public IEnumerable<ViewControl> GetAll()
        {
            // return db.ViewControl.ToList();
            return db.ViewControl.Get().Where(x => x.IsActive == true).Include(x => x.AccessPermission).ToList();

        }

        public List<ActionPermission> GetAllActionPermission()
        {
            // return db.ActionPermission.Include(x => x.ViewControl).Include(x => x.ActionControl).ToList();
            return null;
        }

        public List<AccessPermission> GetAllViewPermission()
        {
            // return db.AccessPermission.Include(x => x.ViewControl).ToList();
            return null;
        }

        public void Update(ViewControl model)
        {
            db.ViewControl.Update(model);
            db.ViewControl.SaveChanges();
        }
    }
}
