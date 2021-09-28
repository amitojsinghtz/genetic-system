using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HISSystem.Areas.Admin.Models
{
    public class DropDownSearchModel
    {
        public int? BuildingID { get; set; }
        public int? FloorID { get; set; }
        public int? RoomID { get; set; }
    }
}
