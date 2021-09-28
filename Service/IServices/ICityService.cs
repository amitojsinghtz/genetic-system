using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface ICityService
    {
        IEnumerable<City> GetAll();
        City GeById(long id);
        void Add(City model);
        void Update(City model);
        void Delete(long id);
    }
}
