using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HISSystem.Areas.Admin.Models
{
    public class AppointmentViewModel
    {
        public Appointment Appointment { get; set; }
        public User User { get; set; }
        public PatientAdmission PatientAdmission { get; set; }

    }
}
