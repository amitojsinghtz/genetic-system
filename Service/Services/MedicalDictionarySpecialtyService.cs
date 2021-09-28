using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services
{
    public class MedicalDictionarySpecialtyService : IMedicalDictionarySpecialtyService
    {
        private IUnitOfWork db;
        public MedicalDictionarySpecialtyService(IUnitOfWork db)
        {
            this.db = db;
        }
    }
}
