using Data.DTO;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Service.Services
{
    public class FollowUpService
    {
        private IUnitOfWork db;
        public FollowUpService(IUnitOfWork db)
        {
            this.db = db;
        }

        public List<FollowUpByDocConv> AddMessage(FollowUpByDocConv conv)
        {
            conv.CreatedOn = DateTime.UtcNow;
            conv.IsActive = true;
            db.FollowUpByDocConv.Insert(conv);

            return db.FollowUpByDocConv.Get().Where(x => x.IsActive == true && x.OrderID == conv.OrderID).Include(x => x.Sender).ToList();
        }

        public bool AddFollowUpTestTempDataList(List<FollowUpTestTempData> dataList)
        {
            try
            {
                db.FollowUpTestTempData.InsertList(dataList);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool AddOrderTests(List<ClientOrderTest> dataList)
        {
            try
            {
                db.ClientOrderTest.InsertList(dataList);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public List<FollowUpByDocConv> GetByDocConvs(int orderID)
        {
            return db.FollowUpByDocConv.Get().Where(x => x.IsActive == true && x.OrderID == orderID).Include(x => x.Sender).ToList();
        }
        public List<FollowUpByDocResult> GetDocResults(int orderID)
        {
            var result = db.FollowUpByDocResult.Get().Where(x => x.OrderID == orderID).ToList(); ;

            for(int i = 0; i < result.Count(); i++)
            {
                if(result[i].Test != null)
                {
                    result[i].TestArray = result[i].Test.Split(",");
                }

                if(result[i].TestArray != null)
                {
                    result[i].Test = "";

                    for (int j = 0; j < result[i].TestArray.Count(); j++)
                    {
                        result[i].Test = result[i].Test + (db.Lookup.Get().Where(x => x.ID == Convert.ToInt32(result[i].TestArray[j])).FirstOrDefault().Name + " ");
                    }
                }
            }

            return db.FollowUpByDocResult.Get().Where(x => x.OrderID == orderID).ToList();
        }

        public string GetTestTemps(int orderID)
        {
            return db.ClientOrder.Get().Where(x => x.ID == orderID).FirstOrDefault().TestType;
        }

        public List<ClientOrderTest> GetPendingTests(int orderID)
        {
            return db.ClientOrderTest.Get().Where(x => x.ClientOrderID == orderID && x.Done == false).Include(x => x.TestTemplate).ThenInclude(y => y.TestTempType).Include(x => x.TestTemplate).ThenInclude(y => y.SubTestTempType).ToList();
        }

        public bool RemovePendingOrders(int orderID)
        {
            try
            {
                var pendingOrders = db.ClientOrderTest.Get().Where(x => x.ClientOrderID == orderID && x.Done == false).ToList();

                if(pendingOrders != null)
                {
                    db.ClientOrderTest.RemoveList(pendingOrders);
                }

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ClientOrderTest> GetCompletedTests(int orderID)
        {
            return db.ClientOrderTest.Get().Where(x => x.ClientOrderID == orderID && x.Done == true).Include(x => x.TestTemplate).ThenInclude(y => y.TestTempType).Include(x => x.TestTemplate).ThenInclude(y => y.SubTestTempType).ToList();
        }

        public List<string> GetCompletedTestName(int orderID)
        {
            return db.ClientOrderTest.Get().Where(x => x.ClientOrderID == orderID && x.Done == true).Select(x => x.TestTemplate.TestTempType.Name ?? "" + ">>" + x.TestTemplate.SubTestTempType.Name ?? "").ToList();
        }

        public IEnumerable<FollowUpTestTempData> GetFollowUpTempDataByTempId(int tempId, int orderId)
        {
            var result = db.FollowUpTestTempData.Get().Where(x => x.TestTempID == tempId && x.OrderID == orderId).Include(x => x.TestTempCol).ThenInclude(y => y.TempColType).Include(x => x.TestTempCol).ThenInclude(y => y.DataSource);

            foreach (var item in result)
            {
                if (item.TestTempCol.TempColTypeID == 2 && item.StringValue != null)
                {
                    item.multiSelect = item.StringValue.Split(",");
                }
            }

            return result;
        }
    }
}
