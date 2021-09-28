using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class PatientAdmission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? UserID { get; set; }
        public int? DepartmentID { get; set; }
        public int? AppointmentID { get; set; }
        public int? PurposeID { get; set; }
        public int? ElectiveID { get; set; }
        public int? AdmissionID { get; set; }
        public int? AdmissionTypeID { get; set; }
        public int? BuildingID { get; set; }
        public int? FloorID { get; set; }
        public int? RoomID { get; set; }
        public int? BedID { get; set; }
        public int? TypeID { get; set; }
        public int? ReservedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int? AddedBy { get; set; }
        public int? RefferedBy { get; set; }
        public string Note { get; set; }
        public int? DoctorID { get; set; }
        public int? PatientTypeID { get; set; }
        public int? SpecialityID { get; set; }
        public string Mobile { get; set; }
        public int? OutPatientTypeID { get; set; }
        public virtual Lookup AdmissionType { get; set; }
        public virtual Lookup PatientType { get; set; }
        public virtual Lookup OutPatientType { get; set; }
    }
}
