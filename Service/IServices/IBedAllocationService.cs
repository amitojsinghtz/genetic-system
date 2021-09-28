using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
   public interface IBedAllocationService
    {
        IEnumerable<BedAllocation> GetAll();
        BedAllocation GeById(long id);
        void Add(BedAllocation model);
        void Update(BedAllocation model);
        void Delete(long id);
    }
}
