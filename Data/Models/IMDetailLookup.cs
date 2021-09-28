using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
   public class IMDetailLookup
    {
       public int ID { get; set; }
       public string Type { get; set; }
       public bool HasAdditionalField1 { get; set; }
        public bool HasAdditionalField2 { get; set; }
        public string AdditionalFieldText1 { get; set; }
        public string AdditionalFieldText2 { get; set; }
        public bool IsActive { get; set; }
        public string Parent { get; set; }
        public string SubParent { get; set; }

    }
}
