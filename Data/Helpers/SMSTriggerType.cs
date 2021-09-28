using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Helpers
{
     public enum SMSTriggerType
     {
        [Display(Name = "Registeration")]
        Registeration = 1,
        [Display(Name = "Client")]
        Client,
     }
    
}
