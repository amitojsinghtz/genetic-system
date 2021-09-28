using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TestTempService : ITestTempService
    {
        private IUnitOfWork db;
        public TestTempService(IUnitOfWork db)
        {
            this.db = db;
        }

        public TestTemp AddTestTemp(TestTemp testTemp)
        {
            try
            {
                db.TestTemp.Insert(testTemp);
                return testTemp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool AddTestTempDataList(List<TestTempData> testTempDatas)
        {
            try
            {
                db.TestTempData.InsertList(testTempDatas);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool MarkTestTempInUse(int testTempID)
        {
            try
            {
                var testTemp = db.TestTemp.Get().Where(x => x.ID == testTempID).FirstOrDefault();
                testTemp.InUse = true;
                db.TestTemp.Update(testTemp);
                db.TestTemp.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool AddTestTempCol(List<TestTempCol> testTemp)
        {
            try
            {
                db.TestTempCol.InsertList(testTemp);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //public async Task<List<TestTemp>> GetTestTempsAsync()
        //{
        //    return await db.TestTemp.Get().Include(x => x.TestTempType).Include(x => x.SubTestTempType).ToListAsync();
        //}

        public IEnumerable<TestTemp> GetTestTemps()
        {
            return db.TestTemp.Get().Include(x => x.TestTempType).Include(x => x.SubTestTempType).AsNoTracking();
        }

        public TestTemp GetTemplateById(int tempId)
        {
            return db.TestTemp.Get().Where(x => x.ID == tempId).Include(x => x.TestTempType).Include(x => x.SubTestTempType).Include(x => x.TestTempCols).ThenInclude(t => t.DataSource).Include(x => x.TestTempCols).ThenInclude(t => t.TempColType).FirstOrDefault();
        }

        public int GetRowID(int tempId)
        {
            int? check = 0;
            var checkList = db.TestTempData.Get().Where(x => x.TestTempID == tempId);

            if (checkList != null && checkList.Count() > 0)
                check = checkList.Max(x => x.RowNo);
            else
                check = 0;
            

            check = check == 0 ? 1 : (check + 1);

            return Convert.ToInt32(check);
        }
        public IEnumerable<TestTempData> GetTempDataByTempId(int tempId)
        {
            var result = db.TestTempData.Get().Where(x => x.TestTempID == tempId).Include(x => x.TestTempCol).ThenInclude(y => y.TempColType).Include(x => x.TestTempCol).ThenInclude(y => y.DataSource);

            foreach(var item in result)
            {
                if (item.TestTempCol.TempColTypeID == 2 && item.StringValue != null)
                {
                    item.multiSelect = item.StringValue.Split(",");
                }
            }

            return result;
        }

        public TestTemp GetTestTempByTypeId(int typeID)
        {
            return db.TestTemp.Get().Where(x => x.TestTempTypeID == typeID).Include(x => x.TestTempType).FirstOrDefault();
        }

        public TestTemp GetTestTempById(int Id)
        {
            return db.TestTemp.Get().Where(x => x.ID== Id).Include(x => x.TestTempType).FirstOrDefault();
        }
        public bool SaveTemplDataList(List<TestTempData> tempDatas)
        {
            try
            {
                db.TestTempData.InsertList(tempDatas);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateTemplateDataList(List<TestTempData> templateData)
        {
            try
            {
                db.TestTempData.UpdateList(templateData);
                db.TestTemplateData.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public bool UpdateTemplateCol(List<TestTempCol> templateCol)
        {
            try
            {
                db.TestTempCol.UpdateList(templateCol);
                db.TestTempCol.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public bool DeleteTemplateDataList(List<TestTempData> templateData)
        {
            try
            {
                for (int i = 0; i < templateData.Count(); i++)
                    templateData[i].IsActive = false;

                db.TestTempData.UpdateList(templateData);
                db.TestTemplateData.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public async Task<bool> DeleteTempDataByColID(int colID)
        {
            try { 
            var dataList = await db.TestTempData.Get().Where(x => x.TestTempColID == colID).ToListAsync();
            if (dataList != null)
            {
                db.TestTempData.RemoveList(dataList);
                db.TestTempData.SaveChanges();
            }
            return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteTempColByID(int colID)
        {
            try { 
            var tempCol = await db.TestTempCol.Get().Where(x => x.ID == colID).FirstOrDefaultAsync();
            if (tempCol != null)
            {
                db.TestTempCol.Delete(tempCol);
                db.TestTempCol.SaveChanges();
            }
            return true;
        }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TestTemp> SearchTemplate(string getTemplate)
        {
            return db.TestTemp.Get().Where(x => x.TestTempType.Name.StartsWith(getTemplate, StringComparison.OrdinalIgnoreCase) || x.SubTestTempType.Name.StartsWith(getTemplate, StringComparison.OrdinalIgnoreCase)).Include(x => x.TestTempType).Include(x => x.SubTestTempType).AsNoTracking();
        }

        public List<TestTempCol> GetTestTempColsByTempId(int tempID)
        {
            return db.TestTempCol.Get().Where(x => x.TestTempID == tempID).Include(x => x.TempColType).Include(x => x.DataSource).AsNoTracking().ToList();
        }

        public List<TestTempData> GetTestTempDataByRowNoAndTempID(int rowNo, int tempID)
        {
            return db.TestTempData.Get().Where(x => x.RowNo == rowNo && x.TestTempID == tempID).ToList();
        }
    }
}
