using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class ImagesViewModel
    {
            public int Id { get; set; }
            public int ImageType { get; set; }
            public byte[] ImagePath { get; set; }
            public int CompanyId { get; set; }
    }
}
