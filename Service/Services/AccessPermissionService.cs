using Data.DTO;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class AccessPermissionService : IAccessPermissionService
    {
        private IUnitOfWork db;
        public AccessPermissionService(IUnitOfWork db)
        {
            this.db = db;
        }

        public AccessPermission Add(AccessPermission model)
        {
            db.AccessPermission.Insert(model);
            return model;
        }

        public void Delete(AccessPermission model)
        {
            var data = db.AccessPermission.Get().Where(c => c.RoleID == model.RoleID && c.ViewControlID == model.ViewControlID && c.IsActive == true).FirstOrDefault();
            data.ModifiedDate = System.DateTime.UtcNow;
            data.IsActive = false;
            db.AccessPermission.Update(data);
            db.AccessPermission.SaveChanges();
        }

        public List<AccessPermission> GetAll()
        {
            var result = db.AccessPermission.Get().Where(x => x.IsActive == true).Include(x => x.ViewControl).Include(x => x.Role).ToList();
            return result;
        }

        public AccessPermission GetById(long id)
        {
            return db.AccessPermission.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Update(AccessPermission model)
        {
            db.AccessPermission.Update(model);
        }

        public List<AccessPermissionModel> GetByView()
        {
            List<AccessPermissionModel> model = new List<AccessPermissionModel>();
            List<PagePermission> accesspermission = new List<PagePermission>();

            var result = db.AccessPermission.Get().Where(x => x.IsActive == true).Include(x => x.ViewControl).Include(x => x.Role).ToList();

            foreach (var item in result)
            {
                accesspermission.Add(new PagePermission
                {
                    User = item.Role.Name,
                    ID = item.ID,
                    IsEnabled = item.IsEnabled
                });

                model.Add(new AccessPermissionModel
                {
                    Name = item.ViewControl.Name,
                    ID = item.ViewControl.ID,
                    IsActive = item.ViewControl.IsActive,
                    Uri = item.ViewControl.Uri,
                    PagePermission = accesspermission,
                });
            }
            return model;
        }


    }
}
