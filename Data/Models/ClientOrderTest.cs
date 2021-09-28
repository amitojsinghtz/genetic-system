using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ClientOrderTest
    {
        public int ID { get; set; }
        public int TestTemplateID { get; set; }
        public int ClientOrderID { get; set; }
        public bool Done { get; set; }
        public string Result { get; set; }
        public virtual TestTemp TestTemplate { get; set; }
        public virtual ClientOrder ClientOrder { get; set; }
    }
}
