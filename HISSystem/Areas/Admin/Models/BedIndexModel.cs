using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;

namespace HISSystem.Areas.Admin.Models
{
    public class BedIndexModel
    {
        public PagedData<Bed> PagedBed { get; set; }


        //public beds = PagedData<Bed>() {get;set;}
        public Bed bed { get; set; }
    }
}
