using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Http;

namespace HISSystem.Areas.Admin.Models
{
    public class PatientViewModel
    {
        public PatientViewModel()
        {
            PatientRelative = new List<PatientRelative>();
        }
        public List<MedicalHistory> MedicalHistory { get; set; }
        public List<PatientRelative> PatientRelative { get; set; }        
        public User User { get; set; }
        public IFormFile File { get; set; }
    }
}
