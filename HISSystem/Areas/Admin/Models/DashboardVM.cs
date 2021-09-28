using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticSystem.Areas.Admin.Models
{
    public class DashboardVM
    {
        public List<ClientOrder> TestOrders { get; set; }
        public List<ClientOrder> AllOrders { get; set; }
        public List<ClientOrder> PendingOrders { get; set; }
    }
}
