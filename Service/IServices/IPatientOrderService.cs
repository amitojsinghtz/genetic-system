using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
    public interface IPatientOrderService
    {
        PatientOrder AddOrder(PatientOrder model);
        List<PatientOrderEpilepsy> AddPatientOrderEpilepsyList(List<PatientOrderEpilepsy> model);
        List<PatientOrderFatConsumption> AddPatientOrderFatConsumptionList(List<PatientOrderFatConsumption> model);
        List<PatientOrderMethyation> AddPatientOrderMethyationList(List<PatientOrderMethyation> model);
        List<PatientOrderProstate> AddPatientOrderProstateList(List<PatientOrderProstate> model);
        List<PatientOrder> GetPatientOrder();
    }
}
