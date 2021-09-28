using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TestDynamicTemplateService
    {
        private IUnitOfWork db;
        public TestDynamicTemplateService(IUnitOfWork db)
        {
            this.db = db;
        }

        public TestTemplate AddTemplate(TestTemplate model)
        {
            try
            {
                db.TestTemplate.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public IEnumerable<TestTemplate> GetAllTemplates()
        {

            return db.TestTemplate.Get().Include(x => x.TestTemplateType).Include(x => x.SubTestTemplateType).Include(x => x.TestTemplateColumns).ThenInclude(x => x.TestTemplateField);
        }


        //Legasov
        public IEnumerable<TestTemplate> SearchTemplate(string getTemplate)
        {
            var result = db.TestTemplate.Get().AsQueryable();
            return result.Where(x => x.TestTemplateType.Name.StartsWith(getTemplate, StringComparison.OrdinalIgnoreCase) || x.SubTestTemplateType.Name.StartsWith(getTemplate, StringComparison.OrdinalIgnoreCase)).Include(x => x.TestTemplateType).Include(x => x.SubTestTemplateType).AsNoTracking();
        }


        public IQueryable<TestTemplate> GetAllTemplateByTempID(int tmpId)
        {

            return db.TestTemplate.Get().Where(x => x.TestTemplateTypeID == tmpId).Include(x => x.TestTemplateType).Include(x => x.SubTestTemplateType).Include(x => x.TestTemplateColumns).ThenInclude(x => x.TestTemplateField);
        }

        public async Task<TestTemplate> GetTemplateByName(string temptype, string subtemptype)
        {
            var result = db.TestTemplate.Get().AsQueryable();

            return await result.Include(x => x.TestTemplateType).Include(x => x.SubTestTemplateType).Where(x => (String.IsNullOrEmpty(temptype) || x.TestTemplateType.Name == temptype) && x.SubTestTemplateType.Name == subtemptype).Include(x => x.TestTemplateColumns).ThenInclude(y => y.TestTemplateField).FirstOrDefaultAsync();
        }

        public TestTemplate GetTemplateByID(int Id)
        {
            return db.TestTemplate.Get().Where(x => x.ID == Id).Include(x => x.TestTemplateType).Include(x => x.SubTestTemplateType).Include(x => x.TestTemplateColumns).FirstOrDefault();
        }

        public TestTemplateData SaveTemplateData(TestTemplateData templateData)
        {
            try
            {
                db.TestTemplateData.Insert(templateData);
                return templateData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool UpdateTemplateData(TestTemplateData templateData)
        {
            try
            {
                db.TestTemplateData.Update(templateData);
                db.TestTemplateData.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TestTemplateData> GetTemplateDataID(int Id)
        {
            var result = db.TestTemplateData.Get().Where(x => x.TestTemplateID == Id).AsNoTracking().ToList();

            if (result == null)
                return new List<TestTemplateData>();
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

        public List<TestTemplateColumn> GetTemplateColumnsByID(int Id)
        {
            var result = db.TestTemplateColumn.Get().Where(x => x.TestTemplateID == Id).Include(x => x.TestTemplateField)./*Include(x => x.TestTemplate)*/ToList();

            if (result == null)
                return new List<TestTemplateColumn>();
            else
                return result;
        }

        public bool RemoveTemplateDataByID(int Id)
        {
            var result = db.TestTemplateData.Get().Where(x => x.ID == Id).FirstOrDefault();
            result.IsActive = false;

            try
            {
                result.IsActive = false;
                db.TestTemplateData.Update(result);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TestTemplate GetTemplateByTempSubTempId(int tempId, int subTempId)
        {
            try
            {
                return db.TestTemplate.Get().Where(x => x.TestTemplateTypeID == tempId && x.SubTestTemplateTypeID == subTempId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool DeleteTemplateColumnsByID(double Id)
        {
            try
            {
                ApplicationContext context = new ApplicationContext();
                context.TestTemplateColumn.RemoveRange(context.TestTemplateColumn.Where(x => x.TestTemplateID == Id));
                context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public List<TestTemplateColumn> InsertTemplateColumn(List<TestTemplateColumn> model)
        {
            try
            {
                ApplicationContext context = new ApplicationContext();
                context.TestTemplateColumn.AddRange(model);
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
