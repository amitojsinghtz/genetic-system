using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Room
    {
        public int ID { get; set; }
        public int RoomNumber { get; set; }
        public string Name { get; set; }
        public int FloorID { get; set; }
    }
}
