using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IServices
{
   public interface ITemplateService
    {
        List<EpilepsyTemplate> GetEpilepsyTemplate();
        EpilepsyTemplate Add(EpilepsyTemplate model);
        void Delete(long id);
        EpilepsyTemplate GetById(long id);
        void Update(EpilepsyTemplate model);
        FitnessTemplate AddFitnessTemplate(FitnessTemplate model);
        void DeleteEffectedGeneFitness(double Id);
        void DeleteFitnessTemplate(int Id);
        FitnessTemplate GetFitnessTemplateById(long Id);
        List<FatConsumptionTemplate> GetFatConsumptionTemplate();
        FatConsumptionTemplate AddFatConsumption(FatConsumptionTemplate model);
        FatConsumptionTemplate GetFatConsumptionById(long Id);
        void DeleteFatConsumptionTemplate(long Id);
        MineralTemplate AddMineralTemplate(MineralTemplate model);
        List<MineralTemplate> GetMineralTemplates();
        void DeleteMineralTemplate(long Id);
         void DeleteEffectedGeneMineral(long Id);
        MineralTemplate UpdateMineralTemplate(MineralTemplate model);
        List<MethyationTemplate> GetMethayationTemplate();
        MethyationTemplate AddMethyationTemplate(MethyationTemplate model);
        MethyationTemplate GetMethyationTemplateById(long Id);
        MethyationTemplate UpdateMethyationTemplate(MethyationTemplate model);
        List<DietTemplate> GetDietTemplate();
        DietTemplate AddDietTemplate(DietTemplate model);
        List<EffectedGeneDiet> InsertEffectedGeneDietList(List<EffectedGeneDiet> model);
        List<EffectedGeneDiet> GetEffectedGeneDiet(int Id);
         void DeleteDietTemplate(int Id);
        void DeleteEffectedGeneDiet(double Id);
        DietTemplate GetDietTemplateById(long Id);
        List<EffectedGeneDiet> GetEffectedGeneDietListById(long Id);
        DietTemplate UpdateDietTemplate(DietTemplate model);

        ////Jitin
        List<MetabolismTemplate> GetMetabolismTemplate();
        MetabolismTemplate Add(MetabolismTemplate model);
        void DeleteMetabolismTemplate(long id);
        void DeleteEffectedGeneMetabolism(double Id);
        MetabolismTemplate GetByIdMetabolismTemplate(long id);
        MetabolismTemplate UpdateMetabolismTemplate(MetabolismTemplate model);
        List<EffectedGeneMetabolism> EffectedGeneMetabolismList(long id);
        List<EffectedGeneMetabolism> InsertEffectedGeneMetabolism(List<EffectedGeneMetabolism> model);
        List<EffectedGeneMetabolism> GetEffectedGeneMetabolismById(long id);

        /////// For Prostate Template
        ProstateTemplate AddProstateTemplate(ProstateTemplate prostateTemplate);
        ProstateTemplate GetProstateTemplateById(long id);
        List<ProstateTemplate> GetProstateTemplateList();
        void UpdateProstateTemplate(ProstateTemplate prostateTemplate);
        void DeleteProstateTemplate(long id);

        //sh
        VitaminTemplate AddVitaminTemplate(VitaminTemplate model);


    }
}
