using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class TestTemplateData
    {
        public int ID { get; set; }
        public int TestTemplateID { get; set; }
        public int? ElementID { get; set; }
        public int? ConsumptionTypeID { get; set; }
        public int? Result { get; set; }
        public string GeneID { get; set; }
        public bool HaveMutationEffected { get; set; }
        public string EffectedGood { get; set; }
        public string EffectedBad { get; set; }
        public string GeneticMutation { get; set; }
        public string PersonalRiskFactor { get; set; }
        public string Hereditary { get; set; }
        public string ResultSummary { get; set; }
        public string Recommendation { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual TestTemplate TestTemplate { get; set; }
        public virtual Lookup Element { get; set; }
        public virtual Lookup ConsumptionType { get; set; }

        [NotMapped]
        public string[] Genes { get; set; }
    }
}
