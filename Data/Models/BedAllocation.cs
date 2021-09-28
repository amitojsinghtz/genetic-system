using System;

namespace Data.Models
{
    public class BedAllocation:BaseEntity
    {
        public int? UserID { get; set; }
        public int? BedID { get; set; }
        public int? StatusID { get; set; }
        public int? Duration { get; set; }
        public DateTime AllocatedDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
