using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface ILookupService
    {
        IEnumerable<Lookup> GetLookups();
        Lookup GetLookup(long id);
        void InsertUser(Lookup lookup);
        void UpdateUser(Lookup lookup);
        void DeleteUser(long id);
        IEnumerable<Lookup> GetByType(string type);
    }
}
