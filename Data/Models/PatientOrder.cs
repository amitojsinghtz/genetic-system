using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class PatientOrder
    {
        public int ID { get; set; }
        public int OrderNo { get; set; }
        public int PatientID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int GeneTemplateID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int DoctorID { get; set; }
        public string FollowUpNeeded { get; set; }
        public string Summary { get; set; }
        public bool IsActive { get; set; }
        public virtual User Patient { get; set; }
        public virtual User Doctor { get; set; }
    }
}
