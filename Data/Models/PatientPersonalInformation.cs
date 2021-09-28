using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class PatientPersonalInformation
    {
        public int ID { get; set; }
        public int? RegistrationNo { get; set; }
        public int? UserID { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public int? PatientTypeID { get; set; }
        public int? OutPatientTypeID { get; set; }
        //public int? AppoitmentID { get; set; }
        //public int? PurposeID { get; set; }
        public int? IdentificationID { get; set; }
        public string IdentificationTypeID { get; set; }
        //public string PlaceOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GenderID { get; set; }
        public int? BloodGroupID { get; set; }
        public int? ReservedID { get; set; }
        public int? ReligionID { get; set; }
        public int? SocialStatusID { get; set; }
        public int? PlaceOfBirthID { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string CompanyName { get; set; }
        public int? OccupationLevelID { get; set; }
        public int? InsuranceCompanyID { get; set; }
        public int? EmployeeTypeID { get; set; }
        public bool HaveInsurance { get; set; }
        //public virtual User User {get;set;}
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Lookup BloodGroup { get; set; }
        public virtual Lookup PatientType { get; set; }
        public virtual Lookup Gender { get; set; }
        public virtual Lookup OutPatientType { get; set; }
        public int? SupplierID { get; set; }
        public virtual Lookup InsuranceCompany { get; set; }
        public virtual Lookup EmployeeType { get; set; }
        public virtual Lookup Supplier { get; set; }
        public virtual Country PlaceOfBirth { get; set; }
        
    }
}
