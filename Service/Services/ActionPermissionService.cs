using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class ActionPermissionService : IActionPermission
    {
        private IUnitOfWork db;
        public ActionPermissionService(IUnitOfWork db)
        {
            this.db = db;
        }

        public ActionPermission Add(ActionPermission model)
        {
            db.ActionPermission.Insert(model);
            return model;
        }

        public void Delete(ActionPermission model)
        {
            var data = db.ActionPermission.Get().Where(c => c.RoleID == model.RoleID && c.ViewControlID == model.ViewControlID && c.IsActive == true && c.ActionID == model.ActionID).FirstOrDefault();
            data.ModifiedDate = System.DateTime.UtcNow;
            data.IsActive = false;
            db.ActionPermission.Update(data);
            db.ActionPermission.SaveChanges();
        }

        public List<ActionPermission> GetAll()
        {
            var context = new ApplicationContext();
            List<ActionPermission> result = new List<ActionPermission>();
            try
            {
                result = context.ActionPermission.Where(x => x.IsActive == true).Include(x => x.ActionControl).Include(x => x.ViewControl).Include(x => x.Role).ToList();
                //result = db.ActionPermission.Get().Where(x => x.IsActive == true).Include(x => x.ActionControl).Include(x => x.ViewControl).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public ActionPermission GetById(long id)
        {
            return db.ActionPermission.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Update(ActionPermission model)
        {
            db.ActionPermission.Update(model);
        }

        public IEnumerable<AccessPermission> GetByRoleID(int Id)
        {
            var result = db.AccessPermission.Get().Where(x => x.IsActive == true && x.RoleID == Id).Include(x => x.ViewControl).Include(x => x.Role).Select(v =>
            new AccessPermission
            {
                ID = v.ID,
                RoleID = v.RoleID,
                IsEnabled = v.IsEnabled,
                IsActive = v.IsActive,
                ViewControlID = v.ViewControlID,
                ViewControl = new ViewControl
                {
                    ID = v.ViewControl.ID,
                    Name = v.ViewControl.Name
                }
            }).ToList();
            return result;
        }
        public IEnumerable<ActionPermission> GetActionAccessByRoleID(int Id)
        {
            var result = db.ActionPermission.Get().Where(x => x.IsActive == true && x.RoleID == Id).Select(v =>
            new ActionPermission
            {
                ID = v.ID,
                RoleID = v.RoleID,
                ViewControlID = v.ViewControlID,

                ActionID = v.ActionID,

            }).ToList();
            return result;
        }
    }
}
