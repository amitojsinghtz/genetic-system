using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User
    {
        public User()
        {
            PatientRelative = new List<PatientRelative>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? TitleID { get; set; }
        public string ArFirstName { get; set; }
        public string ArSecondName { get; set; }
        public string ArThirdName { get; set; }
        public string ArFamilyName { get; set; }
        public string EnFirstName { get; set; }
        public string EnSecondName { get; set; }
        public string EnThirdName { get; set; }
        public string EnFamilyName { get; set; }
        public string Email { get; set; }
      //  [Microsoft.AspNetCore.Mvc.Remote(action: "IsUserNameInUse", controller: "User")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }
        public int? RoleID { get; set; }
        public int? StatusID { get; set; }
        public int? SecurityGroupID { get; set; }
        public string ImagePath { get; set; }
        public string SignaturePath { get; set; }
        public string Fax { get; set; }
        public string Telephone { get; set; }
        public string Telephone2 { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string ComputerName { get; set; }
        public string IPAddress { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int? PlaceOfBirthID { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
        public virtual PatientAdmission PatientAdmission { get; set; }
        public virtual PatientPersonalInformation PatientPersonalInformation { get; set; }
        public List<PatientRelative> PatientRelative { get; set; }
        public virtual List<MedicalHistory> MedicalHistory { get; set; }
        public virtual Role Role { get; set; }
        public virtual Lookup Status { get; set; }
      //  [Microsoft.AspNetCore.Mvc.Remote(action: "IsRegisterationInUse", controller: "User")]
        public string RegisterationNo { get; set; }
        public virtual Lookup PlaceOfBirth { get; set; }

    }
}
