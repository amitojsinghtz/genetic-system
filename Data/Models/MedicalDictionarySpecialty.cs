using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class MedicalDictionarySpecialty
    {
        public int ID { get; set; }
        public int SpecialtyID { get; set; }
        public int MedicalDictionaryID { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual Lookup Specialty { get; set; }
    }
}
