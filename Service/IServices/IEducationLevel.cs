using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IEducationLevel
    {
        List<EducationLevel> GetAll();
        EducationLevel GetById(long Id);

    }
}