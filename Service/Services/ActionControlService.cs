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
    public class ActionControlService : IActionControlService
    {
        private IUnitOfWork db;
        public ActionControlService(IUnitOfWork db)
        {
            this.db = db;
        }

        public ActionControl Add(ActionControl model)
        {
            db.ActionControl.Insert(model);
            return model;
        }

        public void Delete(long id)
        {
            var result = db.ActionControl.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.ActionControl.Update(result);
                db.ActionControl.SaveChanges();
            }
        }

        public List<ActionControl> GetAll()
        {
            //var result = db.ActionControl.GetAll().ToList();
            var result = db.ActionControl.GetAll().Where(x=>x.IsActive==true).ToList();
            return result;
        }

        public ActionControl GetById(long id)
        {
            return db.ActionControl.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Update(ActionControl model)
        {
            db.ActionControl.Update(model);
            db.ActionControl.SaveChanges();

        }
    }
}
