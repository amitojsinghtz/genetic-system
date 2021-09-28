using Data.Models;
using System.Collections.Generic;

namespace Service.IServices
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GeById(long id);
        void Add(Role model);
        void Update(Role model);
        void Delete(long id);
        int GetByName(string name);
    }
}
