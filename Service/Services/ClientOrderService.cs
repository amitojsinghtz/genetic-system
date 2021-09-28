using Data.Models;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.IServices;
using Microsoft.EntityFrameworkCore;
using Repository;
using Data.Helpers;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ClientOrderService : IClientOrderService
    {
        private IUnitOfWork db;
        public ClientOrderService(IUnitOfWork db)
        {
            this.db = db;
        }

        public ClientOrder AddClientOrder(ClientOrder clientOrder)
        {
            try
            {
                clientOrder.User = null;
                clientOrder.Doctor = null;
                clientOrder.IsActive = true;
                int tmpOrdr = 0;
                try
                {
                    tmpOrdr = db.ClientOrder.Get().Max(x => x.OrderNo) + 1;
                    clientOrder.OrderNo = tmpOrdr;
                    clientOrder.StatusID = 398;
                    db.ClientOrder.Insert(clientOrder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    clientOrder.StatusID = 398;
                    clientOrder.OrderNo = 0;
                    db.ClientOrder.Insert(clientOrder);
                }



                return clientOrder;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<ClientOrder> GetPendingOrderForDashboard()
        {
            return db.ClientOrder.Get().Where(x => x.StatusID == 398).OrderByDescending(x => x.OrderDate).Take(5)
                .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status).ToList();
        }

        public ClientOrderTest GetClientOrderTestByTempOrderID(int tempID, int orderID)
        {
            return db.ClientOrderTest.Get().Where(x => x.TestTemplateID == tempID && x.ClientOrderID == orderID).FirstOrDefault();
        }

        public bool MarkOrderTestDone(int tempID, int orderID)
        {
            try { 
            var orderTest =  db.ClientOrderTest.Get().Where(x => x.TestTemplateID == tempID && x.ClientOrderID == orderID).FirstOrDefault();
            orderTest.Done = true;
            db.ClientOrderTest.SaveChanges();

            return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }



        public bool UpdateClientOrderTest(ClientOrderTest orderTest)
        {
            try {
                orderTest.ClientOrder = null;
                orderTest.TestTemplate = null;
                db.ClientOrderTest.Update(orderTest);
                db.ClientOrderTest.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public List<ClientOrder> GetAllOrderForDashboard()
        {
            return db.ClientOrder.Get().OrderByDescending(x => x.OrderDate).Take(5)
                .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status).ToList();
        }
        public List<ClientOrder> GetTestOrderForDashboard()
        {
            var result = db.ClientOrder.Get().Where(x => x.TestType != null).OrderByDescending(x => x.OrderDate).Take(5)
                .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status).ToList();

            foreach(var item in result)
            {
                item.TestArrayStrings = new List<string>();
                item.TestTypeArray = item.TestType.Split(",");

                foreach (var xitem in item.TestTypeArray)
                {
                    var sitem = db.Lookup.Get().Where(x => x.ID == Convert.ToInt32(xitem)).FirstOrDefault().Name;
                    
                    item.TestArrayStrings.Add((sitem + " "));
                }
            }

            return result;
        }
        public ClientOrder GetClientOrderByID(int id)
        {
            try
            {
                var result = db.ClientOrder.Get().Where(x => x.ID == id).Include(x => x.ClientOrderData/*.Where(y => y.IsActive == true)*/).Include(x => x.Template).Include(x => x.ClientOrderTests).
                    ThenInclude(y => y.TestTemplate).ThenInclude(z => z.TestTempType).Include(x => x.ClientOrderTests).ThenInclude(y => y.TestTemplate).ThenInclude(z => z.SubTestTempType).Include(x => x.User).Include(x => x.Doctor).AsNoTracking().FirstOrDefault();

                if (result.FollowUp != null)
                    result.FollowUpArray = result.FollowUp.Split(",");

                if (result.ClientOrderTests != null)
                {
                    result.TestTypeArray = result.ClientOrderTests.Select(x => x.TestTemplateID.ToString()).ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ClientOrder CloseClientOrderByID(int id)
        {
            try
            {
                var result = db.ClientOrder.Get().Where(x => x.ID == id).FirstOrDefault();
                result.StatusID = 397;
                db.ClientOrder.Update(result);
                db.ClientOrder.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IEnumerable<ClientOrder> GetClientOrdersByUserID(int id)
        {
            var result = db.ClientOrder.Get().Where(x => x.UserID == id).Include(x => x.Doctor).Include(x => x.Template).ThenInclude(x => x.TemplateType).Include(x => x.Status).Include(x => x.ClientOrderTests);

            foreach(var item in result.Where(x => x.ClientOrderTests != null))
            {
                item.TestArrayStrings = new List<string>();
                item.TestTypeArray = item.ClientOrderTests.Select(x => Convert.ToString(x.TestTemplateID)).ToArray();

                foreach(var xitem in item.ClientOrderTests)
                {
                    var sitem = db.TestTemp.Get().Where(x => x.ID == Convert.ToInt32(xitem.TestTemplateID)).Include(x => x.TestTempType).Include(x => x.SubTestTempType).FirstOrDefault();

                    string Name = sitem.TestTempType.Name + ">>" + sitem.SubTestTempType.Name; 

                    item.TestArrayStrings.Add((Name + " "));
                }
            }

            return result;
        }

        public IEnumerable<ClientOrder> SearchClientOrderByType(int orderType, string searchKey)
        {
            if(searchKey != null) { 
            if (orderType == 398)
            {
                return db.ClientOrder.Get().Where(x => x.StatusID == orderType && x.OrderNo.ToString().StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || (x.User.EnFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase))
            || x.User.ArFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.Doctor.EnFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.Doctor.ArFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.User.Mobile.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.User.PatientPersonalInformation.City.Name.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.Template.TemplateType.Name.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.OrderDate).Take(5)
                   .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                   .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status);
            }
            else
            {
                return db.ClientOrder.Get().Where(x => x.OrderNo.ToString().StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || (x.User.EnFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase))
            || x.User.ArFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.Doctor.EnFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.Doctor.ArFirstName.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.User.Mobile.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.User.PatientPersonalInformation.City.Name.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase)
            || x.Template.TemplateType.Name.StartsWith(searchKey, StringComparison.OrdinalIgnoreCase))
              .OrderByDescending(x => x.OrderDate).Take(5)
                 .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                 .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status);

            }
            }
            else
            {
                if (orderType == 398)
                {
                    return db.ClientOrder.Get().Where(x => x.StatusID == 398).OrderByDescending(x => x.OrderDate).Take(5)
                .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status).ToList();
                }
                else
                {
                    return db.ClientOrder.Get().OrderByDescending(x => x.OrderDate).Take(5)
                .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status).ToList();

                }
            }

        }

        public IEnumerable<ClientOrder> SearchClientOrderTestType(string searchkey)
        {
            if(searchkey != null) { 
            var result =  db.ClientOrder.Get().Where(x => x.TestType != null && x.OrderNo.ToString().StartsWith(searchkey, StringComparison.OrdinalIgnoreCase)
            || x.User.EnFirstName.StartsWith(searchkey, StringComparison.OrdinalIgnoreCase) || x.User.ArFirstName.StartsWith(searchkey, StringComparison.OrdinalIgnoreCase)
            || x.Doctor.EnFirstName.StartsWith(searchkey, StringComparison.OrdinalIgnoreCase) || x.Doctor.ArFirstName.StartsWith(searchkey, StringComparison.OrdinalIgnoreCase)
            || x.User.Mobile.StartsWith(searchkey, StringComparison.OrdinalIgnoreCase) || x.User.PatientPersonalInformation.City.Name.StartsWith(searchkey, StringComparison.OrdinalIgnoreCase)
            || x.Template.TemplateType.Name.StartsWith(searchkey, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.OrderDate).Take(5)
                   .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
                   .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status);

                foreach (var item in result)
                {
                    item.TestArrayStrings = new List<string>();
                    item.TestTypeArray = item.TestType.Split(",");

                    foreach (var xitem in item.TestTypeArray)
                    {
                        var sitem = db.Lookup.Get().Where(x => x.ID == Convert.ToInt32(xitem)).FirstOrDefault().Name;

                        item.TestArrayStrings.Add((sitem + " "));
                    }
                }

                return result;
            }
            else
            {
                var result =  db.ClientOrder.Get().Where(x => x.TestType != null).OrderByDescending(x => x.OrderDate).Take(5)
             .Include(x => x.User).ThenInclude(p => p.PatientPersonalInformation).ThenInclude(c => c.City)
             .Include(x => x.Template).ThenInclude(t => t.TemplateType).Include(x => x.Status).ToList();

                foreach (var item in result)
                {
                    item.TestArrayStrings = new List<string>();
                    item.TestTypeArray = item.TestType.Split(",");

                    foreach (var xitem in item.TestTypeArray)
                    {
                        var sitem = db.Lookup.Get().Where(x => x.ID == Convert.ToInt32(xitem)).FirstOrDefault().Name;

                        item.TestArrayStrings.Add((sitem + " "));
                    }
                }

                return result;
            }
        }

        public IEnumerable<ClientOrder> GetClientOrderList()
        {
            try
            {
                ApplicationContext context = new ApplicationContext();

                var result = context.ClientOrder.AsQueryable();
                return result.Where(x => x.IsActive == true).Include(x => x.Status).Include(x => x.User).ThenInclude(x => x.PatientPersonalInformation).ThenInclude(x => x.City).Include(x => x.Doctor).Include(x => x.Template).ThenInclude(c => c.TemplateType).OrderByDescending(x => x.ID).Take(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public int GetMaxOrderNo()
        {
            try
            {
                ApplicationContext context = new ApplicationContext();
                return context.ClientOrder.Max(x => x.OrderNo);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IQueryable<ClientOrderData> GetClientOrderDataByOrderID(int orderId)
        {
            try
            {
                ApplicationContext context = new ApplicationContext();
                return context.ClientOrderData.Where(x => x.ClientOrderID == orderId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RemoveClientOrderTestByOrderID(int orderId)
        {
            try
            {

                var result =  db.ClientOrderTest.Get().Where(x => x.ClientOrderID == orderId);
                if (result != null)
                    db.ClientOrderTest.RemoveList(result.ToList());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<ClientOrder> SearchClientOrder(Search model)
        {
            try
            {
                ApplicationContext context = new ApplicationContext();

                var result = context.ClientOrder.Where(x => x.IsActive == true);

                if (model.ID != 0)
                    result = result.Where(x => x.ID.ToString().StartsWith(model.ID.ToString(), StringComparison.OrdinalIgnoreCase));
                if (model.OrderNo != 0)
                    result = result.Where(x => x.OrderNo.ToString().StartsWith(model.OrderNo.ToString(), StringComparison.OrdinalIgnoreCase)).Include(x => x.User);
                if (!string.IsNullOrEmpty(model.PatientName))
                    result = result.Where(x => (x.User != null) && ((x.User.EnFirstName != null) && x.User.EnFirstName.StartsWith(model.PatientName, StringComparison.OrdinalIgnoreCase) ||
                    (x.User != null) && x.User.ArFirstName != null && x.User.ArFirstName.StartsWith(model.PatientName, StringComparison.OrdinalIgnoreCase)));
                if (!string.IsNullOrEmpty(model.PatientCity))
                    result = result.Where(x => (x.User != null) && (x.User.PatientPersonalInformation != null) && (x.User.PatientPersonalInformation.City != null) &&
                    x.User.PatientPersonalInformation.City.Name.StartsWith(model.PatientCity, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(model.RegistrationNo) && !(model.RegistrationNo == "0"))
                    result = result.Where(x => (x.User != null) && x.User.RegisterationNo.StartsWith(model.RegistrationNo, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(model.PatientMobile))
                    result = result.Where(x => x.User.Mobile.StartsWith(model.PatientMobile));
                if (model.BirthDate != null)
                    result = result.Where(x => (x.User != null) && (x.User.PatientPersonalInformation != null) &&
                    (x.User.PatientPersonalInformation.DateOfBirth != null) &&
                    (x.User.PatientPersonalInformation.DateOfBirth.Value.Date) == model.BirthDate.Value.Date);

                return result.Include(x => x.User).ThenInclude(x => x.PatientPersonalInformation).ThenInclude(x => x.City).Include(x => x.Doctor).Include(x => x.Status).Include(x => x.Template).ThenInclude(c => c.TemplateType).OrderByDescending(x => x.ID).Take(50);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void RemoveClientOrderDataList(List<ClientOrderData> orderDataList)
        {
            try
            {
                ApplicationContext context = new ApplicationContext();
                context.ClientOrderData.RemoveRange(orderDataList);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool UpdateClientOrder(ClientOrder clientOrder)
        {
            try
            {
                clientOrder.User = null;
                clientOrder.Doctor = null;
                clientOrder.IsActive = true;
                clientOrder.StatusID = 398;
                db.ClientOrder.Update(clientOrder);
                db.ClientOrder.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteClientOrder(int orderId)
        {
            try
            {
                ClientOrder clientOrder = db.ClientOrder.Get().Where(x => x.ID == orderId).FirstOrDefault();
                clientOrder.IsActive = false;
                db.ClientOrder.Update(clientOrder);
                db.ClientOrder.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public List<ClientOrderData> GetClientOrderDataByTempOrderID(int tempID, int orderID)
        {
            var result = db.ClientOrderData.Get().Where(x => x.TemplateID == tempID && x.ClientOrderID == orderID).Include(x => x.ConsumptionType).Include(x => x.Element).ToList();

            for (int i = 0; i < result.Count(); i++)
            {
                if (result[i].GeneID != null)
                    result[i].Genes = result[i].GeneID.Split(",");
            }

            return result;
        }
    }
}
