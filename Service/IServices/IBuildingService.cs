using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IBuildingService
    {
        IEnumerable<Building> GetAll();
        Building GeById(long id);
        void Add(Building model);
        void Update(Building model);
        void Delete(long id);
    }
}
