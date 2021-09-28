using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAll();
        Country GeById(long id);
        void Add(Country branch);
        void Update(Country branch);
        void Delete(long id);
    }
}
