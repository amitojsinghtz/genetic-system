using Data.Models;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class ReminderService
    {
        private IUnitOfWork db;
        public ReminderService(IUnitOfWork db)
        {
            this.db = db;
        }

        public Reminder GetReminderByID(int id)
        {
            return db.Reminder.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public Reminder Add(Reminder reminder)
        {
            db.Reminder.Insert(reminder);
            return reminder;
        }

        public Reminder Remove(Reminder reminder)
        {
            db.Reminder.Remove(reminder);
            db.Reminder.SaveChanges();
            return reminder;
        }

        public Reminder Update(Reminder reminder)
        {
            db.Reminder.Update(reminder);
            db.Reminder.SaveChanges();
            return reminder;
        }

        public List<Reminder> GetAllReminders()
        {
            return db.Reminder.GetAll().ToList();
        }
    }
}
