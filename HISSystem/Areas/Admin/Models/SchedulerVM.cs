using Data.Models;
using DevExtreme.NETCore.Demos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class SchedulerVM
    {
        public IEnumerable<AppointmentExp> AppointmentExps { get; set; }
        public List<CustDropDown> Users { get; set; }
        public List<CustDropDown> TestTemps { get; set; }
        public List<CustDropDown> Genes { get; set; }
    }

    public class CustDropDown
    {
        public string Name;
        public int ID;
    }
}
