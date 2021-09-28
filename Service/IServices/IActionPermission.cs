using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IActionPermission
    {
        List<ActionPermission> GetAll();
        ActionPermission GetById(long id);
        ActionPermission Add(ActionPermission model);
        void Update(ActionPermission model);
        void Delete(ActionPermission model);
        
    }
}
