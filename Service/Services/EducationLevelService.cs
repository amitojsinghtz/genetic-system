using Data.DTO;
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
    public class EducationLevelService : IEducationLevel
    {
        private IUnitOfWork db;

        public EducationLevelService(IUnitOfWork db)
        {
            this.db = db;
        }

        public List<EducationLevel> GetAll()
        {
            var result = db.EducationLevel.Get().ToList();
            return result;
        }

        public EducationLevel GetById(long id)
        {
            var result = db.EducationLevel.Get().Where(x => x.ID == id).FirstOrDefault();
            return result;
        }
    }
}
