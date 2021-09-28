using Data.Models;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class AttachmentService : IAttachmentService
    {
        private IUnitOfWork db;
        public AttachmentService(IUnitOfWork db)
        {
            this.db = db;
        }

        public Attachment Add(Attachment model)
        {
            db.Attachment.Insert(model);
            db.Attachment.SaveChanges();
            return model;
        }

        public void Delete(long id)
        {
            var model = db.Attachment.Get().Where(x => x.ID == id).FirstOrDefault();
            if(model != null)
            {
                db.Attachment.Remove(model);
                db.Attachment.SaveChanges();
            }
        }

        public List<Attachment> GetAll()
        {
            return db.Attachment.Get().ToList();
        }

        public List<Attachment> GetById(long id, string name)
        {
            var result = db.Attachment.Get().Where(x => x.UserID == id && x.TableName == name).ToList();
            return result;
        }

        public List<Attachment> GetByTableName(string name)
        {
            var result = db.Attachment.Get().Where(x => x.TableName == name).ToList();
            return result;
        }

        public void Update(Attachment model)
        {
            db.Attachment.Update(model);
            db.Attachment.SaveChanges();
        }
    }
}
