using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IActionControlService
    {
        List<ActionControl> GetAll();
        ActionControl GetById(long id);
        ActionControl Add(ActionControl model);
        void Update(ActionControl model);
        void Delete(long id);
    }
}
