using Data.Models;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork db;
        public RoomService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Add(Room model)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Room GeById(long id)
        {
            return db.Room.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Room.GetAll();
        }

        public void Update(Room model)
        {
            throw new NotImplementedException();
        }
    }
}
