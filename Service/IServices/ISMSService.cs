using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface ISMSService
    {
        IEnumerable<SMS> GetSMS();
        SMS Add(SMS model);
        SMS GeById(long id);
        void Update(SMS model);
        void Delete(long id);
        SMSTrigger AddTrigger(SMSTrigger model);
        SMSTrigger GetTriggersById(long id);
        void UpdateTrigger(SMSTrigger model);
        void DeleteTrigger(long id);
        IEnumerable<SMSGroup> GetSMSGroups();
        void DeleteGroupClient(long id);
        void UpdateSMSGroup(SMSGroup model);
    }
}
