using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace DevExtreme.NETCore.Demos.Models
{
    public class AppointmentExp
    {
        [Key]
        [JsonProperty(PropertyName = "AppointmentId")]
        public int AppointmentId { get; set; }
        [JsonProperty(PropertyName = "Text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "StartDate")]
        public DateTime? StartDate { get; set; }
        [JsonProperty(PropertyName = "EndDate")]
        public DateTime? EndDate { get; set; }
        [JsonProperty(PropertyName = "AllDay")]
        public bool AllDay { get; set; }
        [JsonProperty(PropertyName = "RecurrenceRule")]
        public string RecurrenceRule { get; set; }
        [JsonProperty(PropertyName = "ClientID")]
        public int? ClientID { get; set; }
        [JsonProperty(PropertyName = "GeneID")]
        public int? GeneID { get; set; }
        [JsonProperty(PropertyName = "TestID")]
        public int? TestID { get; set; }
        [JsonProperty(PropertyName = "RecurrenceException")]
        public string RecurrenceException { get; set; }
        [NotMapped]
        public string[] TestTempArray { get; set; }
    }
}