using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.UnitOfWork;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Services
{
    public class DynamicTemplateService
    {
        private IUnitOfWork db;
        public DynamicTemplateService(IUnitOfWork db)
        {
            this.db = db;
        }

        public Template AddTemplate(Template model)
        {
            try
            {
                db.Template.Insert(model);

                return db.Template.Get().Where(x => x.ID == model.ID).Include(x => x.TemplateType).Include(x => x.SubTemplateType).Include(x => x.TemplateColumns).ThenInclude(y => y.TemplateField).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public TempDependency AddTempDependency(TempDependency tempDependency)
        {
            try
            {
                db.TempDependency.Insert(tempDependency);
                return tempDependency;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public List<TempDropDownDependency> AddDropDownDependencyList(List<TempDropDownDependency> tempDependency)
        {
            try
            {
                db.TempDropDownDependency.InsertList(tempDependency);
                return tempDependency;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<TempDependency> AddTempDependencyList(List<TempDependency> tempDependency)
        {
            try
            {
                db.TempDependency.InsertList(tempDependency);
                return tempDependency;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<TempDependency> GetTempDependencyListByChkBoxID(int chkBoxID)
        {
            try
            {
                return db.TempDependency.Get().Where(x => x.ChkBoxID == chkBoxID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<TempDependency> GetTempDependenciesByTempID(int tempID)
        {
            try
            {
                return db.TempDependency.Get().Where(x => x.TempID == tempID).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<TempDropDownDependency> GetTempDropDownDependenciesByTempID(int tempId)
        {
            try
            {
                return db.TempDropDownDependency.Get().Where(x => x.TempID == tempId).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveDropDownDependencyList(int templateId)
        {
            try
            {

                var prevList = db.TempDropDownDependency.Get().Where(x => x.TempID == templateId).ToList();

                if (prevList != null)
                {
                    db.TempDropDownDependency.RemoveList(prevList);
                    db.TempDropDownDependency.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool RemoveTempDependencyListByTempID(int tempID)
        {
            try
            {
                var prevList = db.TempDependency.Get().Where(x => x.TempID == tempID).ToList();

                if (prevList != null)
                {
                    db.TempDependency.RemoveList(prevList);
                    db.TempDependency.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool RemoveTempDependencyList(int checkboxId)
        {
            try
            {
                var prevList = db.TempDependency.Get().Where(x => x.ChkBoxID == checkboxId).ToList();

                if(prevList != null)
                {
                    db.TempDependency.RemoveList(prevList);
                    db.TempDependency.SaveChanges();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<Template> GetAllTemplates()
        {       
            try
            {
                var result = db.Template.Get().Include(x => x.TemplateType).Include(x => x.SubTemplateType)
                    .Include(x => x.TemplateColumns).ThenInclude(c => c.TemplateField)
                    .Include(x => x.TemplateColumns).ThenInclude(y => y.ChkBoxTempDependencies)
                    .Include(x => x.TemplateColumns).ThenInclude(y => y.ColIDTempDependencies)
                    .Include(x => x.TemplateColumns).ThenInclude(y => y.DropDownDependencies)
                    .Include(x => x.TemplateColumns).ThenInclude(y => y.ColIDDropDownDependency)
                    .Where(x => x.IsActive == true);

                //var sql = result.ToSql();

                return result;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        //Legasov
        public IEnumerable<Template> SearchTemplate(string getTemplate)
        {
            var result = db.Template.Get().AsQueryable();
            return result.Where(x => x.TemplateType.Name.StartsWith(getTemplate, StringComparison.OrdinalIgnoreCase) || x.SubTemplateType.Name.StartsWith(getTemplate, StringComparison.OrdinalIgnoreCase)).Include(x => x.TemplateType).Include(x => x.SubTemplateType).AsNoTracking();
        }


        public IQueryable<Template> GetAllTemplateByTempID(int tmpId)
        {

            return db.Template.Get().Where(x => x.TemplateTypeID == tmpId)
                .Include(x => x.TemplateType).Include(x => x.SubTemplateType)
                .Include(x => x.TemplateColumns).ThenInclude(x => x.TemplateField)
                .Include(x => x.TemplateColumns).ThenInclude(c => c.TemplateField)
                .Include(x => x.TemplateColumns).ThenInclude(y => y.ChkBoxTempDependencies)
                .Include(x => x.TemplateColumns).ThenInclude(y => y.ColIDTempDependencies)
                .Include(x => x.TemplateColumns).ThenInclude(y => y.DropDownDependencies)
                .Include(x => x.TemplateColumns).ThenInclude(y => y.ColIDDropDownDependency)
                .Where(x => x.IsActive == true);
        }
        //public int GetTemplateByColID(int colId)
        //{
        //    return db.TemplateColumn.Get().Where(x => x.ID == colId).FirstOrDefault().TemplateID;
        //}
        public  int GetTemplateByColID(int colId)
        {
            return db.TemplateColumn.Get().Where(x => x.ID == colId).FirstOrDefault().TemplateID;
        }

        public Template GetTemplateByID(int ID)
        {

            return db.Template.Get().Where(x => x.ID == ID).Include(x => x.TemplateType).Include(x => x.SubTemplateType).Include(x => x.TemplateColumns).ThenInclude(x => x.TemplateField).FirstOrDefault();
        }

        public Template GetTemplateByName(string temptype, string subtemptype)
        {
            return db.Template.Get().Include(x => x.TemplateType).Include(x => x.SubTemplateType).Where(x => (String.IsNullOrEmpty(temptype) || x.TemplateType.Name == temptype) && x.SubTemplateType.Name == subtemptype).Include(x => x.TemplateColumns).ThenInclude(c => c.TemplateField).FirstOrDefault();
        }


        public TemplateData SaveTemplateData(TemplateData templateData)
        {
            try
            {
                templateData.IsActive = true;
                db.TemplateData.Insert(templateData);
                return templateData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool UpdateTemplateData(TemplateData templateData)
        {
            try
            {
                templateData.IsActive = true;
                db.TemplateData.Update(templateData);
                db.TemplateData.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TemplateData> GetTemplateDataID(int Id)
        {
            var result = db.TemplateData.Get().Where(x => x.TemplateID == Id && x.IsActive == true).AsNoTracking().ToList();

            if (result == null)
                return new List<TemplateData>();
            else
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    if (result[i].GeneID != null)
                        result[i].Genes = result[i].GeneID.Split(",");
                }
                return result;
            }
        }

        public TemplateData GetTemplateDataByID(int dataId)
        {
            var result =  db.TemplateData.Get().Where(x => x.ID == dataId).FirstOrDefault();

            if (result.GeneID != null)
            {
                result.Genes = result.GeneID.Split(",");
            }

            return result;
        }

        public List<TemplateColumn> GetTemplateColumnsByID(int Id)
        {
            var result = db.TemplateColumn.Get().Where(x => x.TemplateID == Id).Include(x => x.TemplateField).Include(x => x.Template).ToList();

            if (result == null)
                return new List<TemplateColumn>();
            else
                return result;
        }

        public bool RemoveTemplateDataByID(int Id)
        {
            var result = db.TemplateData.Get().Where(x => x.ID == Id).FirstOrDefault();
            result.IsActive = false;

            try
            {
                result.IsActive = false;
                db.TemplateData.Update(result);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Template GetTemplateByTempSubTempId(int tempId, int subTempId)
        {
            try
            {
                return db.Template.Get().Where(x => x.TemplateTypeID == tempId && x.SubTemplateTypeID == subTempId && x.IsActive == true).FirstOrDefault();

            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Template GetTemplateByTempId(int tempId)
        {
            try
            {
                return db.Template.Get().Where(x => x.TemplateTypeID == tempId && x.IsActive == true).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool DeleteTemplatebyId(int Id)
        {
            try
            {
                var template = db.Template.Get().Where(x => x.ID == Id).FirstOrDefault();
                template.IsActive = false;
                db.Template.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool DeleteTemplateColumnsByID(double Id)
        {
            try
            {
                ApplicationContext context = new ApplicationContext();

                var ChkBoxCols = context.TemplateColumn.Where(x => x.TemplateID == Id/* && x.TemplateFieldID == 216*/).ToList();
                var dropDownDepList = context.TempDropDownDependency.Where(x => x.TempID == Id).ToList();
                
                if (ChkBoxCols != null)
                {
                    foreach (var checkbox in ChkBoxCols)
                    {
                        var depList = context.TempDependency.Where(x => x.TempID == Id).ToList();

                        if (depList != null)
                        {
                            context.TempDependency.RemoveRange(depList);
                            context.SaveChanges();
                        }
                    }
                }

                if(dropDownDepList != null)
                {
                    context.TempDropDownDependency.RemoveRange(dropDownDepList);
                }

                context.TemplateColumn.RemoveRange(context.TemplateColumn.Where(x => x.TemplateID == Id));
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public List<TemplateColumn> InsertTemplateColumn(List<TemplateColumn> model)
        {
            try
            {
                ApplicationContext context = new ApplicationContext();
                context.TemplateColumn.AddRange(model);
                context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
}
