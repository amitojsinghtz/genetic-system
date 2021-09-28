using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class PatientRelative : BaseEntity
    {        
        public string RelativeName { get; set; }
        public bool SameAddress { get; set; }
        public string Address { get; set; }
        public int? RelationID { get; set; }
        public string Telephone { get; set; }
        public string Comment { get; set; }
        public int? StatusID { get; set; }
        public int? UserID { get; set; }
    }
}
