using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PersonalInformation
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        
        public int RegisterationNo { get; set; }
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }
        public int? SpecialityID { get; set; }
        public int? BranchID { get; set; }
        public int? ResponsibleOfficerID { get; set; }
        public int? EmploymentTypeID { get; set; }
        public int? EmploymentClassID { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public int? IdentificationID { get; set; }
        public string IdentificationTypeID { get; set; }
        public string PlaceOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        public int? GenderID { get; set; }
        public int? ReligionID { get; set; }
        public int? ReservedID { get; set; }
        public int? PurposeID { get; set; }
        public int? SocialStatusID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfHiring { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfStartJob { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ProbationTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfEndContract { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfEndJob { get; set; }
        public bool CompanyCommitment { get; set; }
        public bool SubjectToFingerPrint { get; set; }
        public int? SpecialTax { get; set; }
        public int? EducationLevelID { get; set; }
        public int? SubDepartmentID { get; set; }
        public virtual EducationLevel EducationLevel { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Lookup Department { get; set; }
        public virtual Lookup Position { get; set; }
        public virtual Lookup EmploymentType { get; set; }
        public virtual Lookup EmploymentClass { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Lookup SubDepartment { get; set; }
        public virtual Lookup Speciality { get; set; }

        public DateTime? ProbationPeriodStartDate { get; set; }
        public DateTime? ProbationPeriodEndDate { get; set; }
    }
}
