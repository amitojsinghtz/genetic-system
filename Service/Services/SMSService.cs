using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
   public class SMSService:ISMSService
    {
        private IUnitOfWork db;
        public SMSService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<SMS> GetSMS()
        {
            return db.SMS.GetAll();
        }
        public SMS Add(SMS model)
        {
            db.SMS.Insert(model);
            return model;
        }
        public SMSTrigger AddTrigger(SMSTrigger model)
        {
            db.SMSTrigger.Insert(model);
            return model;
        }
        public SMS GeById(long id)
        {
            return db.SMS.Get().Where(x => x.ID == id).FirstOrDefault();
        }
        public void Update(SMS model)
        {
            db.SMS.Update(model);
        }
        public void Delete(long id)
        {
            var result = db.SMS.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.SMS.Update(result);
                db.SMS.SaveChanges();
            }
        }
        public IEnumerable<SMSTrigger> GetAllSMSTriggers()
        {
            return db.SMSTrigger.Get().Include(x => x.SMS).Where(x => x.IsActive == true).ToList();


        }
        public SMSTrigger GetTriggersById(long id)
        {
            return db.SMSTrigger.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public void UpdateTrigger(SMSTrigger model)
        {
            db.SMSTrigger.Update(model);
        }
        public void DeleteTrigger(long id)
        {
            var result = db.SMSTrigger.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.SMSTrigger.Update(result);
                db.SMSTrigger.SaveChanges();
            }
        }
        public SMSTrigger getTriggerByType(string Type)
        {

            return db.SMSTrigger.Get().Where(x => x.IsActive == true && x.TriggerName == Type).Include(x => x.SMS).FirstOrDefault();

        }
        public IEnumerable<SMSGroup> GetSMSGroups()
        {
            return db.SMSGroup.GetAll();
        }
        public SMSGroup AddSMSGroup(SMSGroup model)
        {
            db.SMSGroup.Insert(model);
            return model;
        }
        public List<GroupClientList> InsertGroupClientList(List<GroupClientList> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.GroupClientList.AddRange(model);
            context.SaveChanges();

            return model;
        }
        public SMSGroup getSMSGroupById(long id)
        {
            return db.SMSGroup.Get().Where(x => x.ID == id).FirstOrDefault();
        }
        public List<GroupClientList> getGroupClientById(long id)
        {
            return db.GroupClientList.Get().Where(x => x.GroupID == id).ToList();

        }
        public void UpdateSMSGroup(SMSGroup model)
        {
           db.SMSGroup.Update(model);

        }
        public void DeleteGroupClient(long id)
        {
            var result = db.GroupClientList.Get().Where(x => x.GroupID == id).ToList();
            foreach (var item in result)
            {
                item.IsActive = false;
                db.GroupClientList.Update(item);
                db.GroupClientList.SaveChanges();
            }
        }
        public void DeleteSMSGroup(long id)
        {
            var result = db.SMSGroup.Get().Where(x => x.ID == id).FirstOrDefault();
                result.IsActive = false;
                db.SMSGroup.Update(result);
                db.SMSGroup.SaveChanges(); 
        }
        public SMSConfig AddSmsApi(SMSConfig Api)
        {
             db.SMSConfig.Insert(Api);
            return Api;        
        }
        public SMSConfig GetSmsConfig()
        {
            return db.SMSConfig.GetAll().FirstOrDefault();
        
        
        }
        public void UpdateSmsConfig(SMSConfig model)
        {
             db.SMSConfig.Update(model);
        }
    }

    }

