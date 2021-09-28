using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IPersonalInformation
    {
        IEnumerable<PersonalInformation> GetAll();
        PersonalInformation GeById(long id);
        void Add(PersonalInformation model);
        void Update(PersonalInformation model);
        void Delete(long id);
        PersonalInformation GeByUser(long id);
    }
}
