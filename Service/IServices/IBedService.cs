using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IBedService
    {
        IEnumerable<Bed> GetAll();
        Bed GeById(long id);
        void Add(Bed model);
        void Update(Bed model);
        void Delete(long id);
        IEnumerable<Bed> GetByDepartment(int id, int? statusID, bool? Isolated);
    }
}
