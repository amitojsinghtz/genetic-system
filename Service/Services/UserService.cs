using Data.DTO;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork db;

        public UserService(IUnitOfWork db)
        {
            this.db = db;
        }
        public IEnumerable<User> GetUsers()
        {
            int role = db.Role.Get().Where(x => x.Name.Contains("Employee")).FirstOrDefault().ID;
            var result = db.User.Get().Where(x => x.IsActive == true && (x.RoleID != 5)).Include(x => x.Role).Include(x => x.PersonalInformation.Country).Include(x => x.PersonalInformation.City)
                .Include(x => x.PersonalInformation.Department).Include(x => x.PersonalInformation.Branch).Include(x => x.PatientRelative).OrderByDescending(x => x.AddedDate).ToList();
            return result;
        }

        public User GetUser(long id)
        {
            var result = db.User.Get().Where(x => x.ID == id).Include(x => x.PersonalInformation).Include(x => x.PatientPersonalInformation).ThenInclude(y => y.City).Include(x => x.PatientPersonalInformation).ThenInclude(y => y.BloodGroup).Include(x => x.PatientRelative).Include(x => x.MedicalHistory).FirstOrDefault();
            return result;
        }

        public void InsertUser(User user)
        {
            db.User.Insert(user);
        }
        public User Add(User user)
        {
            db.User.Insert(user);
            db.User.SaveChanges();
            return user;
        }

        public void UpdateUser(User user)
        {
            //var result = db.User.Get().Where(x => x.ID == user.ID).FirstOrDefault();
            //result.EnFirstName = user.EnFirstName;
            db.User.Update(user);
            //db.User.SaveChanges();
            db.User.SaveChanges(1, "112.196.66.75", user.ID);
        }

        public void DeleteUser(long id)
        {
            User user = GetUser(id);
            user.IsActive = false;
            UpdateUser(user);
        }

        public List<DepartmentModel> UserByDepartment(bool isActice, int? departmentID)
        {
            List<DepartmentModel> department = new List<DepartmentModel>();
            var users = db.User.Get().Include(x => x.PersonalInformation).ToList();
            var lookup = db.Lookup.Get().Where(x => x.IsActive == true && x.Type.Contains("Department")).ToList();
            if (departmentID != null)
            {
                users = users.Where(x => x.PersonalInformation != null && x.PersonalInformation.DepartmentID == departmentID).ToList();
                lookup = lookup.Where(x => x.ID == departmentID).ToList();
            }
            foreach (var d in lookup)
            {
                var u = users.Where(x => x.PersonalInformation.DepartmentID == d.ID).ToList();
                department.Add(new DepartmentModel
                {
                    Department = d,
                    Users = u
                });
            }
            return department;
        }

        public IEnumerable<User> GetPatients()
        {
            int role = db.Role.Get().Where(x => x.Name.Contains("Patient")).FirstOrDefault().ID;
            var result = db.User.Get().Where(x => x.IsActive == true && x.RoleID == role).Include(x => x.PatientPersonalInformation.BloodGroup)
                .Include(x => x.PatientPersonalInformation.City).Include(x => x.PatientPersonalInformation.PatientType).Include(x => x.PatientPersonalInformation.EmployeeType).Include(x => x.Status).OrderByDescending(x => x.AddedDate);
            return result;
        }
        public IEnumerable<User> GetByRole(int roleID)
        {
            return db.User.Get().Where(x => x.RoleID == roleID).Include(x => x.PersonalInformation);
        }

        public IEnumerable<User> Search()
        {
            int role = db.Role.Get().Where(x => x.Name.Contains("Patient")).FirstOrDefault().ID; ;
            var result = db.User.Get().Where(x => x.IsActive == true && x.RoleID == role).ToList();
            return result;
        }

        public void AddPatientRelative(List<PatientRelative> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientRelative.AddRange(model);
            context.SaveChanges();
        }

        public void UploadFile(int id, string filePath)
        {
            var result = db.User.Get().Where(x => x.ID == id).FirstOrDefault();
            result.ImagePath = filePath;
            db.User.Update(result);
            db.User.SaveChanges();
        }
        public User Login(string UserName, string Password)
        {
            var result = db.User.Get().Where(x => (x.UserName == UserName) && x.Password == Password).Include(x => x.Role).Include(x => x.PersonalInformation).FirstOrDefault();
            return result;
        }
        public void UpdatePatientRelative(List<PatientRelative> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientRelative.UpdateRange(model);
            context.SaveChanges();
        }
        public void UpdateMedicalHistoryList(List<MedicalHistory> medicalHistories)
        {
            try
            {
                db.MedicalHistory.UpdateList(medicalHistories);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
        public User GetPatient(int id)
        {
            return db.User.Get().Where(x => x.ID == id).Include(x => x.PatientPersonalInformation.PatientType)
                .Include(x => x.PatientPersonalInformation.BloodGroup).Include(x => x.PatientPersonalInformation.Gender)
                 .Include(x => x.PatientPersonalInformation.City)
                .FirstOrDefault();
        }
        public IEnumerable<User> GetAll()
        {

            var result = db.User.Get().Include(x => x.PersonalInformation).Include(x => x.PatientPersonalInformation).OrderByDescending(x => x.AddedDate).ToList();
            return result;
        }
    }
}
