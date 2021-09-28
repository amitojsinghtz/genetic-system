using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
   public interface IEmailService
    {
        IEnumerable<Email> GetEmails();
        Email Add(Email model);
        Email GeById(long id);
        void Update(Email model);
        void Delete(long id);
       EmailTrigger AddTrigger(EmailTrigger model);
      EmailTrigger GetTriggersById(long id);
        void UpdateTrigger(EmailTrigger model);
        void DeleteTrigger(long id);
    }
}
