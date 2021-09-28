using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class PreAssessmentVitalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int PatientPreAssesmentVitalID { get; set; }
        public bool VitalDetailValue { get; set; }
        public int VitalDetailID { get; set; }
        public virtual Lookup VitalDetail { get; set; }
    }
}
