using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface ILogTableService
    {
        List<LogTable> GetAll();
        List<LogTable> GetAllByTargetId(long id, string tableName);
        LogTable GetById(long id);
        void Delete(long id);
        List<LogData> GetDetails(int id);
    }
}
