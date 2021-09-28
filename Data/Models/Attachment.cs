using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentName { get; set; }
        public string TableName { get; set; }
        public int UserID { get; set; }
    }
}
