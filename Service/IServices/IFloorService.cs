using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IFloorService
    {
        IEnumerable<Floor> GetAll();
        Floor GeById(long id);
        void Add(Floor model);
        void Update(Floor model);
        void Delete(long id);
    }
}
