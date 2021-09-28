using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class BedService : IBedService
    {
        private IUnitOfWork db;
        private ApplicationContext _context = new ApplicationContext();
        public BedService(IUnitOfWork db)
        {
            this.db = db;
        }

       

        public void Delete(long id)
        {
            var result = db.Bed.Get().Where(x => x.ID == id).FirstOrDefault();
            result.IsActive = false;
            db.Bed.Update(result);
            db.Bed.SaveChanges();
        }

        public Bed GeById(long id)
        {
            return db.Bed.Get().Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Bed> GetAll()
        {
            return db.Bed.GetAll();
        }

        public IEnumerable<Bed> GetByDepartment(int id, int? statusID, bool? Isolated)
        {
            var context = _context.Bed.Include(x => x.Status).ToList();
            var result = new List<Bed>();
            //db.Bed.GetAll().ToList();

            result = context.Where(x => (id == 0 || x.DepartmentID == id) && (statusID == null || x.StatusID == statusID) && (Isolated == null || x.Isolated == Isolated)).ToList();

            return result;
        }

        public void Update(Bed model)
        {
            try
            {
                db.Bed.Update(model);
            }
            catch (Exception ex)
            {

            }
        }
        public void Add(Bed model)
        {
            //try
            //{
            db.Bed.Insert(model);
            db.Bed.SaveChanges();
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}
