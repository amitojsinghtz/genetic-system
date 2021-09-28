using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(long id);
        Appointment Add(Appointment model);
        void Update(Appointment model);
        void Delete(long id);
    }
}
