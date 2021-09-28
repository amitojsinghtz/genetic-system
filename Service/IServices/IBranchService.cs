using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IBranchService
    {
        IEnumerable<Branch> GetAll();
        Branch GeById(long id);
        void Add(Branch branch);
        void Update(Branch branch);
        void Delete(long id);
    }
}
