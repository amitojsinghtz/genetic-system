using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? DoctorID { get; set; }
        public int? UserID { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<DateTime>Time { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public int? DepartmentID { get; set; }
        public int? SpecialityID { get; set; }
        public int? RoomID { get; set; }
        public int? PurposeID { get; set; }
        public Nullable<bool> ByDoctor { get; set; }
        public Nullable<bool> SelfAppointment { get; set; }
        public Nullable<int> AppointmentStatusID { get; set; }
        public virtual User User { get; set; }
        public virtual User Doctor { get; set; }
        public virtual Room Room { get; set; }
        public virtual Lookup Department { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Note { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual Lookup AppointmentStatus { get; set; }
    }
}
