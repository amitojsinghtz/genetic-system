using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace GeneticSystem.Areas.Admin.Models
{
    public class AddFitnessViewModel
    {
        public int ID { get; set; }
        public int? ConsumptionTypeID { get; set; }
        public List<EffectedGeneFitness> EffectedGeneFitness { get; set; }
        public string EffectedGood { get; set; }
        public string EffectedBad { get; set; }
    }
}
