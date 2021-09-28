using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;


namespace Service.IServices
{
    public interface ICompanyProfileService
    {
        CompanyProfile GetById(long id);
        CompanyProfile Add(CompanyProfile model);
        void Update(CompanyProfile model);
    }
}
