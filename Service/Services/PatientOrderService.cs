using Data.DTO;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class PatientOrderService : IPatientOrderService
    {
        private IUnitOfWork db;
        public PatientOrderService(IUnitOfWork db)
        {
            this.db = db;
        }
        public PatientOrder AddOrder(PatientOrder model)
        {

            db.PatientOrder.Insert(model);
            return model;
        }

        public PatientOrder UpdateOrder(PatientOrder model)
        {

            db.PatientOrder.Update(model);
            db.PatientOrder.SaveChanges();
            return model;
        }
        public PatientOrder GetPatientOrderNo()
        {
            return db.PatientOrder.Get().LastOrDefault();
        }


        public List<PatientOrderMineral> AddPatientOrderMineralList(List<PatientOrderMineral> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderMineral.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<PatientOrderMetabolism> AddPatientOrderMetabolismList(List<PatientOrderMetabolism> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderMetabolism.AddRange(model);
            context.SaveChanges();
            return model;
        }

        public List<PatientOrderFitness> AddPatientOrderFitnessList(List<PatientOrderFitness> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderFitness.AddRange(model);
            context.SaveChanges();
            return model;
        }

        public List<PatientOrderVitamin> AddPatientOrderVitaminList(List<PatientOrderVitamin> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderVitamin.AddRange(model);
            context.SaveChanges();
            return model;
        }

        public List<PatientOrderDiet> AddPatientOrderDietList(List<PatientOrderDiet> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderDiet.AddRange(model);
            context.SaveChanges();
            return model;
        }

        public List<PatientOrderEpilepsy> AddPatientOrderEpilepsyList(List<PatientOrderEpilepsy> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderEpilepsy.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<PatientOrderFatConsumption> AddPatientOrderFatConsumptionList(List<PatientOrderFatConsumption> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderFatConsumption.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<PatientOrderMethyation> AddPatientOrderMethyationList(List<PatientOrderMethyation> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderMethyation.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<PatientOrderProstate> AddPatientOrderProstateList(List<PatientOrderProstate> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderProstate.AddRange(model);
            context.SaveChanges();
            return model;
        }



        public List<PatientOrder> GetPatientOrder()
        {
            return db.PatientOrder.Get().Include(x => x.Patient).Where(x => x.IsActive == true).ToList();
        }

        public PatientOrder GetPatientOrderById(int orderId)
        {
            return db.PatientOrder.Get().Where(x => x.ID == orderId && x.IsActive == true).Include(x => x.Patient).FirstOrDefault();
        }

        public PatientOrderGeneral GetGeneralOrderData(int patientOrderId)
        {
            PatientOrderGeneral generalTemplateData = new PatientOrderGeneral();
            generalTemplateData.PatientOrderFitnessList = db.PatientOrderFitness.Get().Where(x => x.OrderID == patientOrderId).Include(x => x.PatientOrderEffectedGeneFitnessList).ToList();
            generalTemplateData.PatientOrderMetabolismList = db.PatientOrderMetabolism.Get().Where(x => x.OrderID == patientOrderId).Include(x => x.PatientOrderEffectedGeneMetabolismList).ToList();
            generalTemplateData.PatientOrderMineralList = db.PatientOrderMineral.Get().Where(x => x.OrderID == patientOrderId).Include(x => x.PatientOrderEffectedGeneMineralList).ToList();
            generalTemplateData.PatientOrderDietList = db.PatientOrderDiet.Get().Where(x => x.OrderID == patientOrderId).Include(x => x.PatientOrderEffectedGeneDietList).ToList();
            generalTemplateData.PatientOrderVitaminList = db.PatientOrderVitamin.Get().Where(x => x.OrderID == patientOrderId).Include(x => x.PatientOrderEffectedGeneVitaminList).ToList();

            for (var i = 0; i < generalTemplateData.PatientOrderFitnessList.Count(); i++)
            {
                generalTemplateData.PatientOrderFitnessList[i].EffectedGenes = generalTemplateData.PatientOrderFitnessList[i].PatientOrderEffectedGeneFitnessList.Select(x => (x.GeneID).ToString()).ToArray();
            }
            for (var i = 0; i < generalTemplateData.PatientOrderMineralList.Count(); i++)
            {
                generalTemplateData.PatientOrderMineralList[i].EffectedGenes = generalTemplateData.PatientOrderMineralList[i].PatientOrderEffectedGeneMineralList.Select(x => (x.GeneID).ToString()).ToArray();
            }
            for (var i = 0; i < generalTemplateData.PatientOrderMetabolismList.Count(); i++)
            {
                generalTemplateData.PatientOrderMetabolismList[i].EffectedGenes = generalTemplateData.PatientOrderMetabolismList[i].PatientOrderEffectedGeneMetabolismList.Select(x => (x.GeneID).ToString()).ToArray();
            }
            for (var i = 0; i < generalTemplateData.PatientOrderDietList.Count(); i++)
            {
                generalTemplateData.PatientOrderDietList[i].EffectedGenes = generalTemplateData.PatientOrderDietList[i].PatientOrderEffectedGeneDietList.Select(x => (x.GeneID).ToString()).ToArray();
            }
            for (var i = 0; i < generalTemplateData.PatientOrderVitaminList.Count(); i++)
            {
                generalTemplateData.PatientOrderVitaminList[i].EffectedGenes = generalTemplateData.PatientOrderVitaminList[i].PatientOrderEffectedGeneVitaminList.Select(x => (x.GeneID).ToString()).ToArray();
            }

            return generalTemplateData;
        }

        private List<PatientOrderEpilepsy> GetEpilepsyOrderData(int patientOrderId)
        {
            List<PatientOrderEpilepsy> patientOrderEpilepsies = db.PatientOrderEpilepsy.Get().Where(x => x.OrderID == patientOrderId).Include(x => x.Gene).ToList();
            return patientOrderEpilepsies;
        }
        public List<PatientOrderEpilepsy> GetPatientOrderEpilepsyById(long Id)
        {
            return db.PatientOrderEpilepsy.Get().Where(x => x.OrderID == Id).Include(x => x.Gene).ToList();
        }
        public List<PatientOrderFatConsumption> GetPatientOrderFatConsumptionById(long Id)
        {
            return db.PatientOrderFatConsumption.Get().Where(x => x.OrderID == Id).ToList();
        }



        public List<PatientOrderMethyation> GetPatientOrderMethyationById(long Id)
        {
            return db.PatientOrderMethyation.Get().Where(x => x.OrderID == Id).ToList();
        }
        public List<PatientOrderProstate> GetPatientOrderProstateById(long Id)
        {
            return db.PatientOrderProstate.Get().Where(x => x.OrderID == Id).ToList();
        }
        public void UpdatePatientOrderMineralList(List<PatientOrderMineral> patientOrderMinerals)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderMineral.UpdateRange(patientOrderMinerals);
        }
        public void UpdatePatientOrderFitnessList(List<PatientOrderFitness> patientOrderFitnesses)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderFitness.UpdateRange(patientOrderFitnesses);
            context.SaveChanges();
        }
        public void UpdatePatientOrderMetabolismList(List<PatientOrderMetabolism> patientOrderMetabolisms)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderMetabolism.UpdateRange(patientOrderMetabolisms);
        }
        public void UpdatePatientOrderDietList(List<PatientOrderDiet> patientOrderDiets)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderDiet.UpdateRange(patientOrderDiets);
        }
        public void UpdatePatientOrderVitaminList(List<PatientOrderVitamin> patientOrderVitamins)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderVitamin.UpdateRange(patientOrderVitamins);
        }
        public void UpdatePatientOrderEpilepsyList(List<PatientOrderEpilepsy> patientOrderEpilepsies)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderEpilepsy.UpdateRange(patientOrderEpilepsies);
            context.SaveChanges();
        }
        public void UpdatePatientOrderMethyationList(List<PatientOrderMethyation> patientOrderMethyations)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderMethyation.UpdateRange(patientOrderMethyations);
            context.SaveChanges();
        }
        public void UpdatePatientOrderProstateList(List<PatientOrderProstate> patientOrderProstates)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderProstate.UpdateRange(patientOrderProstates);
            context.SaveChanges();
        }
        public void UpdatePatientOrderFatConsumptionList(List<PatientOrderFatConsumption> patientOrderFatConsumptions)
        {
            ApplicationContext context = new ApplicationContext();
            context.PatientOrderFatConsumption.UpdateRange(patientOrderFatConsumptions);
            context.SaveChanges();
        }
        public List<PatientOrderEffectedGeneFitness> GetPatientOrderEffectedGeneFitnesses(int id)
        {
            return db.PatientOrderEffectedGeneFitness.Get().Where(x => x.OrderFitnessID == id).Include(x => x.Gene).ToList();
        }
        public List<PatientOrderEffectedGeneMineral> GetPatientOrderEffectedGeneMineral(int id)
        {
            return db.PatientOrderEffectedGeneMineral.Get().Where(x => x.OrderMineralID == id).Include(x => x.Gene).ToList();
        }
        public List<PatientOrderEffectedGeneMetabolism> GetPatientOrderEffectedGeneMetabolism(int id)
        {
            return db.PatientOrderEffectedGeneMetabolism.Get().Where(x => x.OrderMetabolismID == id).Include(x => x.Gene).ToList();
        }
        public List<PatientOrderEffectedGeneVitamin> GetPatientOrderEffectedGeneVitamin(int id)
        {
            return db.PatientOrderEffectedGeneVitamin.Get().Where(x => x.OrderVitaminID== id).Include(x => x.Gene).ToList();
        }
        public List<PatientOrderEffectedGeneDiet> GetPatientOrderEffectedGeneDiet(int id)
        {
            return db.PatientOrderEffectedGeneDiet.Get().Where(x => x.OrderDietID== id).Include(x => x.Gene).ToList();
        }
    }
}
