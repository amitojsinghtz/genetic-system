using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    interface IPatientEncounterService
    {
        PatientEncounter InsertPatientEncounter(PatientEncounter model);


    }
}

