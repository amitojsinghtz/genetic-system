using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("cust_image")]
    public class CustImage
    {
        [Key]
        public int Id { get; set; }
        public int ImageType { get; set; }
        public byte[] ImagePath { get; set; }
        public int CompanyId { get; set; }
    }
}
