using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DTO
{
    public class DepartmentModel
    {
        public Lookup Department { get; set; }
        public List<User> Users { get; set; }

        public DepartmentModel()
        {
            Users = new List<User>();
        }
    }
}
