using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IViewControlService
    {
        IEnumerable<ViewControl> GetAll();
        ViewControl GeById(long id);
        void Add(ViewControl model);
        void Update(ViewControl model);
        void Delete(long id);
        List<ActionPermission> GetAllActionPermission();
        List<AccessPermission> GetAllViewPermission();
    }
}
