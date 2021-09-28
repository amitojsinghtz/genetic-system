using Data.Models;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public  class EmailService : IEmailService
    {
        private IUnitOfWork db;
        public EmailService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Email> GetEmails()
        {
            return db.Email.GetAll();
        }
        public Email Add(Email model)
        {
            db.Email.Insert(model);
            return model;
        }
        public EmailTrigger AddTrigger(EmailTrigger model)
        {
            db.EmailTrigger.Insert(model);
            return model;
        }
        public Email GeById(long id)
        {
            return db.Email.Get().Where(x => x.ID == id).FirstOrDefault();
        }
        public void Update(Email model)
        {
            db.Email.Update(model);
        }
        public void Delete(long id)
        {
            var result = db.Email.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.Email.Update(result);
                db.Email.SaveChanges();
            }
        }
        public IEnumerable<EmailTrigger> GetAllEmailTriggers()
        {
           return db.EmailTrigger.Get().Include(x => x.Email).Where(x => x.IsActive == true).ToList();
            
        
        }
        public EmailTrigger GetTriggersById(long id)
        {
            return db.EmailTrigger.Get().Where(x => x.ID == id).FirstOrDefault();
        }
        
       public void UpdateTrigger(EmailTrigger model)
        {
            db.EmailTrigger.Update(model);
        }
        public void DeleteTrigger(long id)
        {
            var result = db.EmailTrigger.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.EmailTrigger.Update(result);
                db.EmailTrigger.SaveChanges();
            }
        }
        public EmailTrigger getTriggerByType(string Type)
        {

           return  db.EmailTrigger.Get().Where(x => x.IsActive == true && x.TriggerName== Type).Include(x => x.Email).FirstOrDefault();

        }



    }
}
