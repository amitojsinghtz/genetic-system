using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class LogTableService : ILogTableService
    {
        private IUnitOfWork db;
        public LogTableService(IUnitOfWork db)
        {
            this.db = db;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<LogTable> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<LogTable> GetAllByTargetId(long id, string tableName)
        {
            var result = db.LogTable.Get().Where(x => x.TargetID == id && x.TableName == tableName).Include(x=>x.LogData).OrderByDescending(x => x.AddedDate).ToList();
            return result;
        }
        public LogTable GetById(long id)
        {
            throw new NotImplementedException();
        }

        public List<LogData> GetDetails(int id)
        {
            var result = db.LogData.Get().Where(x => x.LogTableID == id).ToList();
            return result;
        }
    }
}
