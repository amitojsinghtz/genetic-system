using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Bed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int? BedNumber { get; set; }
        public int? RoomID { get; set; }
        public int? FloorID { get; set; }
        public int? BuildingID { get; set; }
        public int? DepartmentID { get; set; }
        public bool Isolated { get; set; }
        public int? StatusID { get; set; }
        public BedAllocation BedAllocation { get; set; }
        public virtual Lookup Status { get; set; }
        public virtual Lookup Room { get; set; }
        public virtual Lookup Floor { get; set; }
        public virtual Lookup Building { get; set; }
        public virtual Lookup Department { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
