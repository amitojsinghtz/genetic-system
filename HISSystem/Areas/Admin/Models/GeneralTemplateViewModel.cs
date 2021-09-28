using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class GeneralTemplateViewModel
    {
        public GeneralTemplateViewModel()
        {
            this.MineralTemplates = new List<MineralTemplate>();
            this.MetabolismTemplates = new List<MetabolismTemplate>();
            this.VitaminTemplates = new List<VitaminTemplate>();
            this.DietTemplates = new List<DietTemplate>();
            this.FitnessTemplates = new List<FitnessTemplate>();
        }

        //For Getting Template Data And Binding For Add
        public List<MineralTemplate> MineralTemplates { get; set; }
        public List<MetabolismTemplate> MetabolismTemplates { get; set; }
        public List<VitaminTemplate> VitaminTemplates { get; set; }
        public List<DietTemplate> DietTemplates { get; set; }
        public List<FitnessTemplate> FitnessTemplates { get; set; }
    }

}
