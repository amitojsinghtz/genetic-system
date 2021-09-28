using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User Add(User model);
        User GetUser(long id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(long id);
        List<DepartmentModel> UserByDepartment(bool isActice, int? id);
        IEnumerable<User> GetPatients();
        IEnumerable<User> GetByRole(int roleID);
        IEnumerable<User> Search();
        void AddPatientRelative(List<PatientRelative> model);
        void UpdatePatientRelative(List<PatientRelative> model);
        void UploadFile(int id, string filePath);
        User Login(string UserName, string Password);
         User GetPatient(int id);
         IEnumerable<User> GetAll();



    }
}
