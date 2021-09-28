using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class MedicalDictionaryViewModel
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public string Telephone { get; set; }
        public string Telephone2 { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Address { get; set; }
        public int CityID { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int ClassID { get; set; }
        public byte[] ImagePath { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public virtual Lookup Type { get; set; }
        public List<MedicalDictionarySpecialty> MedicalDictionarySpecialty { get; set; }
        public string[] Specialty { get; set; }
        //public virtual Lookup Class { get; set; }
        public string ImageString { get; set; }
    }
}
