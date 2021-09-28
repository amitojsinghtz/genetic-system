using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
   public interface IRoomService
    {
        IEnumerable<Room> GetAll();
        Room GeById(long id);
        void Add(Room model);
        void Update(Room model);
        void Delete(long id);
    }
}
