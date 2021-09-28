using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IAttachmentService
    {
        List<Attachment> GetAll();
        List<Attachment> GetByTableName(string name);
        List<Attachment> GetById(long id, string name);
        Attachment Add(Attachment model);
        void Update(Attachment model);
        void Delete(long id);
    }
}
