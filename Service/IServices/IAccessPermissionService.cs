using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IAccessPermissionService
    {
        List<AccessPermission> GetAll();
        AccessPermission GetById(long id);
        AccessPermission Add(AccessPermission model);
        void Update(AccessPermission model);
        void Delete(AccessPermission model);
        List<AccessPermissionModel> GetByView();
    }
}
