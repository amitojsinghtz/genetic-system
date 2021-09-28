using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using Data.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Repository;

namespace Service.Services
{
    public class TemplateService : ITemplateService
    {
        private IUnitOfWork db;
        public TemplateService(IUnitOfWork db)
        {
            this.db = db;
        }
        public List<EpilepsyTemplate> GetEpilepsyTemplate()
        {
            return db.EpilepsyTemplate.Get().Include(x => x.Gene).ToList();
        }
        public EpilepsyTemplate Add(EpilepsyTemplate model)
        {
            db.EpilepsyTemplate.Insert(model);
            return model;
        }
        public void Delete(long id)
        {
            var result = db.EpilepsyTemplate.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.EpilepsyTemplate.Update(result);
                db.EpilepsyTemplate.SaveChanges();
            }
        }
        public EpilepsyTemplate GetById(long id)
        {
            return db.EpilepsyTemplate.Get().Where(x => x.ID == id).FirstOrDefault();
        }
        public void Update(EpilepsyTemplate model)
        {
            db.EpilepsyTemplate.Update(model);
        }
        public List<FitnessTemplate> GetFitnessTemplate()
        {
            var result = db.FitnessTemplate.Get().Include(x => x.ConsumptionType).Include(x => x.EffectedGeneList).ToList();
            
            for (var i = 0; i < result.Count(); i++)
            {
                result[i].EffectedGenes = result[i].EffectedGeneList.Select(x => (x.GeneID).ToString()).ToArray();
            }

            return result;

        }
        public FitnessTemplate AddFitnessTemplate(FitnessTemplate model)
        {
            db.FitnessTemplate.Insert(model);
            return model;
        }
        public List<EffectedGeneFitness> InsertEffectedGeneFitnessList(List<EffectedGeneFitness> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.EffectedGeneFitness.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<EffectedGeneFitness> GetEffectedGeneFitness(int Id)
        {
            return db.EffectedGeneFitness.Get().Where(x => x.FitnessID == Id).Include(x => x.Gene).ToList();

        }
        public void DeleteFitnessTemplate(int Id)
        {
            var result = db.FitnessTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.FitnessTemplate.Update(result);
                db.FitnessTemplate.SaveChanges();
            }

        }
        public void DeleteEffectedGeneFitness(double Id) 
        {
          var result = db.EffectedGeneFitness.Get().Where(x => x.FitnessID == Id).ToList();
            foreach (var item in result)
            {
                item.IsActive = false;
                db.EffectedGeneFitness.Update(item);
                db.EffectedGeneFitness.SaveChanges();
            }
       }
        public FitnessTemplate GetFitnessTemplateById(long Id)
        {

            return db.FitnessTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();

        }
        public List<EffectedGeneFitness> GetEffectedGeneFitnessListById(long Id)
        {
            return db.EffectedGeneFitness.Get().Where(x => x.FitnessID == Id).ToList();
        }
        public FitnessTemplate UpdateFitnessTemplate(FitnessTemplate model)
        {
            db.FitnessTemplate.Update(model);
            return model;
        }
        public List<FatConsumptionTemplate> GetFatConsumptionTemplate()
        {
            return db.FatConsumptionTemplate.Get().Include(x => x.ConsumptionType).Include(x => x.EffectedGene).ToList();


        }
        public FatConsumptionTemplate AddFatConsumption(FatConsumptionTemplate model)
        {
            db.FatConsumptionTemplate.Insert(model);
            return model;
        }
        public FatConsumptionTemplate GetFatConsumptionById(long Id)
        {
            return db.FatConsumptionTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
        }
        public FatConsumptionTemplate UpdateFatConsumption(FatConsumptionTemplate model)
        {
            db.FatConsumptionTemplate.Update(model);
            return model;
        }
        public void DeleteFatConsumptionTemplate(long Id)
        {
            var result = db.FatConsumptionTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.FatConsumptionTemplate.Update(result);
                db.FatConsumptionTemplate.SaveChanges();
            }
        }
        public MineralTemplate AddMineralTemplate(MineralTemplate model)
        {

            db.MineralTemplate.Insert(model);
            return model;
        }
        public List<EffectedGeneMineral> InsertEffectedGeneMineralList(List<EffectedGeneMineral> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.EffectedGeneMineral.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<MineralTemplate> GetMineralTemplates()
        {

            var result =  db.MineralTemplate.Get().Include(x => x.Element).Include(x => x.EffectedGeneList).ToList();

            for(var i = 0; i < result.Count(); i++)
            {
                result[i].EffectedGenes = result[i].EffectedGeneList.Select(x => (x.GeneID).ToString()).ToArray();
            }

            return result;
        }
        public List<EffectedGeneMineral> GetEffectedGeneMineralByMineralId(long Id)
        {
            return db.EffectedGeneMineral.Get().Where(x => x.MineralID == Id).Include(x => x.Gene).ToList();

        }
        public void DeleteMineralTemplate(long Id)
        {
            var result = db.MineralTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.MineralTemplate.Update(result);
                db.MineralTemplate.SaveChanges();
            }
        
        }
        public  void DeleteEffectedGeneMineral(long Id)
        {
            var result = db.EffectedGeneMineral.Get().Where(x => x.MineralID == Id).ToList();
            foreach (var item in result)
            {
                item.IsActive = false;
                db.EffectedGeneMineral.Update(item);
                db.EffectedGeneMineral.SaveChanges();
            }
        }
        public MineralTemplate GetMineralTemplateById(long Id)
        {
            return db.MineralTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
        
        }
        public MineralTemplate UpdateMineralTemplate(MineralTemplate model)
        {
            db.MineralTemplate.Update(model);
            return model;
        }
        public List<MethyationTemplate> GetMethayationTemplate()
        {
            return db.MethyationTemplate.Get().Include(x => x.Gene).ToList();
        }
        public MethyationTemplate AddMethyationTemplate(MethyationTemplate model)
        {
            db.MethyationTemplate.Insert(model);
            return model;
        }
        public MethyationTemplate GetMethyationTemplateById(long Id)
        {
            return db.MethyationTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
        }
        public MethyationTemplate UpdateMethyationTemplate(MethyationTemplate model)
        {
             db.MethyationTemplate.Update(model);
            return model;
        }
        public void DeleteMetyationTemplate(long Id)
        {
            var result = db.MethyationTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.MethyationTemplate.Update(result);
                db.MethyationTemplate.SaveChanges();
            }
        }
        public List<DietTemplate> GetDietTemplate()
        {
            var result =  db.DietTemplate.Get().Include(x => x.ConsumptionType).Include(x => x.EffectedGeneList).ToList();

            for (var i = 0; i < result.Count(); i++)
            {
                result[i].EffectedGenes = result[i].EffectedGeneList.Select(x => (x.GeneID).ToString()).ToArray();
            }

            return result;
        }
        public DietTemplate AddDietTemplate(DietTemplate model)
        {
            db.DietTemplate.Insert(model);
            return model;
        }
        public List<EffectedGeneDiet> InsertEffectedGeneDietList(List<EffectedGeneDiet> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.EffectedGeneDiet.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<EffectedGeneDiet> GetEffectedGeneDiet(int Id)
        {
            return db.EffectedGeneDiet.Get().Where(x => x.DietID == Id).Include(x => x.Gene).ToList();

        }
        public void DeleteDietTemplate(int Id)
        {
            var result = db.DietTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.DietTemplate.Update(result);
                db.DietTemplate.SaveChanges();
            }

        }
        public void DeleteEffectedGeneDiet(double Id)
        {
            var result = db.EffectedGeneDiet.Get().Where(x => x.DietID == Id).ToList();
            foreach (var item in result)
            {
                item.IsActive = false;
                db.EffectedGeneDiet.Update(item);
                db.EffectedGeneDiet.SaveChanges();
            }
        }
        public DietTemplate GetDietTemplateById(long Id)
        {

            return db.DietTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();

        }
        public List<EffectedGeneDiet> GetEffectedGeneDietListById(long Id)
        {
            return db.EffectedGeneDiet.Get().Where(x => x.DietID == Id).ToList();
        }
        public DietTemplate UpdateDietTemplate(DietTemplate model)
        {
            db.DietTemplate.Update(model);
            return model;
        }


        ///////////// For Metabolism /////////////
        public List<MetabolismTemplate> GetMetabolismTemplate()
        {
            var result = db.MetabolismTemplate.Get().Include(x => x.ConsumptionType).Include(x => x.EffectedGeneList).Where(x => x.IsActive == true).ToList();

            for (var i = 0; i < result.Count(); i++)
            {
                result[i].EffectedGenes = result[i].EffectedGeneList.Select(x => (x.GeneID).ToString()).ToArray();
            }

            return result;


        }
        public MetabolismTemplate Add(MetabolismTemplate model)
        {
            db.MetabolismTemplate.Insert(model);
            return model;
        }
        public void DeleteMetabolismTemplate(long id)
        {
            var result = db.MetabolismTemplate.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.MetabolismTemplate.Update(result);
                db.MetabolismTemplate.SaveChanges();
            }
        }
        public void DeleteEffectedGeneMetabolism(double Id)
        {
            var result = db.EffectedGeneMetabolism.Get().Where(x => x.MetabolismID == Id).ToList();
            foreach (var item in result)
            {
                item.IsActive = false;
                db.EffectedGeneMetabolism.Update(item);
                db.EffectedGeneMetabolism.SaveChanges();
            }
        }
        public MetabolismTemplate GetByIdMetabolismTemplate(long id)
        {
            return db.MetabolismTemplate.Get().Where(x => x.ID == id && x.IsActive == true).FirstOrDefault();

        }
        public MetabolismTemplate UpdateMetabolismTemplate(MetabolismTemplate model)
        {
            db.MetabolismTemplate.Update(model);
            return model;
        }
        public List<EffectedGeneMetabolism> EffectedGeneMetabolismList(long id)
        {
            return db.EffectedGeneMetabolism.Get().Where(x => x.MetabolismID == id && x.IsActive == true).ToList();
        }
        public List<EffectedGeneMetabolism> InsertEffectedGeneMetabolism(List<EffectedGeneMetabolism> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.EffectedGeneMetabolism.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public List<EffectedGeneMetabolism> GetEffectedGeneMetabolismById(long id)
        {
            return db.EffectedGeneMetabolism.Get().Where(x => x.MetabolismID == id).Include(x => x.Gene).ToList();
        }


        /////// For Prostate Template
        public ProstateTemplate AddProstateTemplate(ProstateTemplate prostateTemplate)
        {
            db.ProstateTemplate.Insert(prostateTemplate);
            return prostateTemplate;
        }
        public ProstateTemplate GetProstateTemplateById(long id)
        {
            return db.ProstateTemplate.Get().Where(x => x.ID == id).FirstOrDefault();
        }
        public List<ProstateTemplate> GetProstateTemplateList()
        {
            return db.ProstateTemplate.Get().Include(x => x.Gene).ToList();
        }

        public void UpdateProstateTemplate(ProstateTemplate prostateTemplate)
        {
            db.ProstateTemplate.Update(prostateTemplate);
        }
        public void DeleteProstateTemplate(long id)
        {
            var result = db.ProstateTemplate.Get().Where(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                try
                {
                    db.ProstateTemplate.Update(result);
                    db.ProstateTemplate.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ex;
                }

            }
        }
        public List<VitaminTemplate> GetVitaminTemplate()
        {
            var result = db.VitaminTemplate.Get().Include(x => x.FeederType).Include(x => x.EffectedGeneList).ToList();

            for (var i = 0; i < result.Count(); i++)
            {
                result[i].EffectedGenes = result[i].EffectedGeneList.Select(x => (x.GeneID).ToString()).ToArray();
            }

            return result;
        }
        public List<EffectedGeneVitamin> GetEffectedGeneVitamin(long Id)
        {
            return db.EffectedGeneVitamin.Get().Where(x => x.VitaminID == Id).Include(x=>x.Gene).ToList();
        }
        public VitaminTemplate AddVitaminTemplate(VitaminTemplate model)
        {
            db.VitaminTemplate.Insert(model);
            return model;
        }
        public List<EffectedGeneVitamin> InsertEffectedGeneVitaminList(List<EffectedGeneVitamin> model)
        {
            ApplicationContext context = new ApplicationContext();
            context.EffectedGeneVitamin.AddRange(model);
            context.SaveChanges();
            return model;
        }
        public VitaminTemplate GetVitaminTemplateById(long Id)
        {

            return db.VitaminTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
        
        }
        public VitaminTemplate UpdateVitaminTemplate(VitaminTemplate model)
        {
            db.VitaminTemplate.Update(model);
            return model;
        }
        public void DeleteEffectedGeneVitamin(long Id)
        {
            var result = db.EffectedGeneVitamin.Get().Where(x => x.VitaminID == Id).ToList();
            foreach (var item in result)
            {
                item.IsActive = false;
                db.EffectedGeneVitamin.Update(item);
                db.EffectedGeneVitamin.SaveChanges();
            }
        }
        public void DeleteVitaminTemplate(long Id)
        {
            var result = db.VitaminTemplate.Get().Where(x => x.ID == Id).FirstOrDefault();
           if(result !=null)
            {
                result.IsActive = false;
                db.VitaminTemplate.Update(result);
                db.VitaminTemplate.SaveChanges();
            }

        }
    }

}
