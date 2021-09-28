using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class TemplateData
    {
        public int ID { get; set; }
        public int TemplateID { get; set; }
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
        public string Recommendation2 { get; set; }
        public string Recommendation3 { get; set; }
        public string FollowUp { get; set; }
        public string EffectedMiddle { get; set; }
        public int? LevelChange { get; set; }
        public bool HaveMutationEffected2 { get; set; }
        public bool HaveMutationEffected3 { get; set; }
        public virtual Template Template { get; set; }
        public virtual Lookup Element { get; set; }
        public virtual Lookup ConsumptionType { get; set; }
        [NotMapped]
        public string[] Genes{ get; set; }
    }
}
