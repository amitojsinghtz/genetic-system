using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class CompanyProfile
    {
        public int ID { get; set; }   
        public string CompanyArName { get; set; }
        public string CompanyEnName { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string AddressAr { get; set; }
        public string AddressEn { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

    }
}
