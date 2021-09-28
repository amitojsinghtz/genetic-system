using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class ClientOrder
    {
        public int ID { get; set; }
        public int? TemplateID { get; set; }
        public string FollowUp { get; set; }
        public string TestType { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderDueDate { get; set; }
        public string OrderSummary { get; set; }
        public int? DoctorID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }
        public int OrderNo { get; set; }
        public int? StatusID { get; set; }
        public bool FollowUpTestAdded { get; set; }
        public virtual Lookup Status { get; set; }
        public virtual User Doctor { get; set; }
        public virtual User User { get; set; }
        public virtual Template Template { get; set; }
        public virtual List<ClientOrderData> ClientOrderData { get; set; }
        public virtual List<ClientOrderTest> ClientOrderTests { get; set; }
        [NotMapped]
        public string[] FollowUpArray { get; set; }
        [NotMapped]
        public string[] TestTypeArray { get; set; }
        [NotMapped]
        public List<string> TestArrayStrings { get; set; }
        [NotMapped]
        public StringBuilder FollowUpStrings { get; set; }
        [NotMapped]
        public StringBuilder TestStrings { get; set; }
    }
}
