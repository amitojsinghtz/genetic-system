using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Data.Helpers;
using Data.Models;
using GeneticSystem.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.UnitOfServices;

namespace GeneticSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SMSController : Controller
    {
        public const int PageSize = 50;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IUnitOfService db;
        public SMSController(IUnitOfService db, IHostingEnvironment _appEnvironment)
        {
            this.db = db;
            this._appEnvironment = _appEnvironment;
        }
        public IActionResult Index()
        {
            var sms = new PagedData<SMS>();
            var smsList = db.SMSService.GetSMS().Where(x => x.IsActive == true).ToList();
            sms.Data = (smsList).Take(PageSize);
            sms.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)smsList.Count() / PageSize));
            return View(sms);
           
        }
        public IActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Add(SMS model)
        {
            model.CreatedBy = Request.Cookies["ID"];
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var status = db.SMSService.Add(model);
            return RedirectToAction("index");


        }
        public IActionResult Update(int id)
        {
            var data = db.SMSService.GeById(id);
            return PartialView(data);
        }
        [HttpPost]
        public IActionResult Update(SMS model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            db.SMSService.Update(model);
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            db.SMSService.Delete(id);
            return RedirectToAction("index");
        }
        public IActionResult GetSMS(int page, string searchstring)
        {
            var sms = new PagedData<SMS>();
            var smsList = db.SMSService.GetSMS().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                smsList = smsList.Where(x => x.Name != null && x.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase) ||
                (x.Body != null && x.Body.Contains(searchstring, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            sms.Data = (smsList).Skip(PageSize * (page - 1)).Take(PageSize);
            sms.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)smsList.Count() / PageSize));

            return PartialView(sms);
        }
        public IActionResult Trigger()
        {
            var triggers = new PagedData<SMSTrigger>();

            var data = db.SMSService.GetAllSMSTriggers();
            triggers.Data = (data).Take(PageSize);
            triggers.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)data.Count() / PageSize));

            return View(triggers);
        }
        public IActionResult AddTriggerSMS()
        {
            var model = new SMSTrigger();
            ViewBag.AllSMS = db.SMSService.GetSMS().Where(x => x.IsActive == true).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult AddTriggesSMS(SMSTrigger model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            var result = db.SMSService.AddTrigger(model);
            return RedirectToAction("Trigger");
        }
        public IActionResult UpdateSMSTrigger(int Id)
        {
            var model = db.SMSService.GetTriggersById(Id);
            ViewBag.AllSMS = db.SMSService.GetSMS().Where(x => x.IsActive == true).ToList();
            return PartialView(model);      
        }
        [HttpPost]
        public IActionResult UpdateSMSTriggers(SMSTrigger model)
        {
            model.IsActive = true;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            db.SMSService.UpdateTrigger(model);
            return RedirectToAction("Trigger");

        }
        public IActionResult DeleteTrigger(int id)
        {
            db.SMSService.DeleteTrigger(id);
            return RedirectToAction("Trigger");
        }
        public IActionResult GetTrigger(int page, string searchstring)
        {
            var SMSTrigger = new PagedData<SMSTrigger>();
            var SMStriggerList = db.SMSService.GetAllSMSTriggers().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
                SMStriggerList = SMStriggerList.Where(x => x.SMS != null && x.SMS.Name != null && x.SMS.Name.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
            }
            SMSTrigger.Data = (SMStriggerList).Skip(PageSize * (page - 1)).Take(PageSize);
            SMSTrigger.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)SMStriggerList.Count() / PageSize));

            return PartialView(SMSTrigger);

        }
        public IActionResult Send(SMSSendModel smsViewmodel)
        {
            var smsconfig1 = db.SMSService.GetSmsConfig();
            var smsapi = smsconfig1.ApiKey;
            try
            {
                var bodyFormat = " Dear User your detail are username: {0}, Password: {1}, From: {2} ";

                var body = smsViewmodel.Body;
                var body1 = smsViewmodel.Body1;
                var body2 = smsViewmodel.Body2;
                var smsBody = string.Format(bodyFormat,smsViewmodel.Body, smsViewmodel.Body1, smsViewmodel.Body2);
             
                //string strUrl = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/sendmessage/?apikey="+smsapi+"&to="+smsViewmodel.ToMobile+"&msg="+smsViewmodel.Body;
                string strUrl = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/sendmessage/?apikey="+smsapi+"&to="+smsViewmodel.ToMobile+"&msg="+ smsBody;

                // Create a request object  
                WebRequest request = HttpWebRequest.Create(strUrl);
                // Get the response back  
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(true);
        }

        public IActionResult CustSend(SMSSendModel smsViewmodel)
        {
            var smsconfig1 = db.SMSService.GetSmsConfig();
            var smsapi = smsconfig1.ApiKey;
            try
            {
                var bodyFormat = "Dear User: ";

                var body = smsViewmodel.Body;
                var body1 = smsViewmodel.Body1;
                var body2 = smsViewmodel.Body2;
                var smsBody = string.Format(bodyFormat, smsViewmodel.Body, smsViewmodel.Body1, smsViewmodel.Body2);

                //string strUrl = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/sendmessage/?apikey="+smsapi+"&to="+smsViewmodel.ToMobile+"&msg="+smsViewmodel.Body;
                string strUrl = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/sendmessage/?apikey=" + smsapi + "&to=" + smsViewmodel.ToMobile + "&msg=" + smsViewmodel.Body;

                // Create a request object  
                WebRequest request = HttpWebRequest.Create(strUrl);
                // Get the response back  
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(true);
        }
        public IActionResult SMSGroups()
        {
            var sms = new PagedData<SMSGroup>();
            ViewBag.ClientList = db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();
            var smsgroupList = db.SMSService.GetSMSGroups().Where(x => x.IsActive == true).ToList();
            sms.Data = (smsgroupList).Take(PageSize);
            sms.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)smsgroupList.Count() / PageSize));
            return View(sms);
        }
        public IActionResult AddSMSGroup()
        {
            ViewBag.ClientList = db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddSmsGroups(AddGroupViewModel model)
        {
            SMSGroup smsgroupmodel = new SMSGroup();
            smsgroupmodel.GroupName = model.GroupName;
            smsgroupmodel.IsActive = true;
            smsgroupmodel.CreatedOn = DateTime.UtcNow;
            smsgroupmodel.UpdatedOn = DateTime.UtcNow;
            var addnewgroup = db.SMSService.AddSMSGroup(smsgroupmodel);
            var addnewgroupid = addnewgroup.ID;
            foreach (var item in model.GroupClientLists)
            {
                item.GroupID = addnewgroupid;
                item.IsActive = true;
            }
            db.SMSService.InsertGroupClientList(model.GroupClientLists);
            return Json(true);
        
        }
        public IActionResult UpdateSMSGroup(int Id)
        {
            var smsgroupmodel = new AddGroupViewModel(); 
           var smsgroup= db.SMSService.getSMSGroupById(Id);
            if (smsgroup != null)
            {
                smsgroupmodel.GroupName = smsgroup.GroupName;

               var groupclientlist= db.SMSService.getGroupClientById(smsgroup.ID).Where(x=>x.IsActive==true).ToList();
                smsgroupmodel.GroupClientLists = groupclientlist;
                smsgroupmodel.ID = smsgroup.ID;
            }
            ViewBag.ClientList = db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();
            return View(smsgroupmodel);
        }
        [HttpPost]
        public IActionResult UpdateSMSGroups(AddGroupViewModel model)
        {
            SMSGroup smsgroupmodel = new SMSGroup();
            db.SMSService.DeleteGroupClient(model.ID);
            smsgroupmodel.ID = model.ID;
            smsgroupmodel.GroupName = model.GroupName;
            smsgroupmodel.IsActive = true;
            smsgroupmodel.CreatedOn = DateTime.UtcNow;
            smsgroupmodel.UpdatedOn = DateTime.UtcNow;
            db.SMSService.UpdateSMSGroup(smsgroupmodel);
           
            var addnewgroupid = model.ID;
            foreach (var item in model.GroupClientLists)
            {
                item.GroupID = addnewgroupid;
                item.IsActive = true;
            }
            db.SMSService.InsertGroupClientList(model.GroupClientLists);
            return Json(true);
        }
        public IActionResult DeleteSmsGroup(int Id)
        {
            db.SMSService.DeleteSMSGroup(Id);
            db.SMSService.DeleteGroupClient(Id);
            return RedirectToAction("SMSGroups");

        }
        public IActionResult Getsmsgroup(int page, string searchstring)
        {
            var sms = new PagedData<SMSGroup>();
            ViewBag.ClientList = db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();
            var smsgroupList = db.SMSService.GetSMSGroups().Where(x => x.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(searchstring))
            {
            smsgroupList = smsgroupList.Where(x => x.GroupName != null && x.GroupName.Contains(searchstring.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
            }
            sms.Data = (smsgroupList).Skip(PageSize * (page - 1)).Take(PageSize);
            sms.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)smsgroupList.Count() / PageSize));
            return PartialView(sms);
        }
        public IActionResult SMSGroupSend()
        {
            var groupsendmodel = new SendSMSGroupModel();
            ViewBag.Groups= db.SMSService.GetSMSGroups().Where(x => x.IsActive == true).ToList();
            ViewBag.Clients= db.UserService.GetPatients().OrderByDescending(x => x.AddedDate).ToList();

            return View(groupsendmodel);
        
        }
        public IActionResult SendSMSToGroup(SendSMSGroupModel model)
         {
            var smsconfig1 = db.SMSService.GetSmsConfig();
            var smsapi = smsconfig1.ApiKey;
            var groupclientlist = db.SMSService.getGroupClientById(model.GroupID);
            foreach (var item in groupclientlist)
            {
              var mobile=  db.UserService.GetUser(item.UserID).Mobile;
                try
                {
                    string strUrl = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/sendmessage/?apikey="+smsapi+"&to="+mobile+"&msg=" + model.Message;
                    // Create a request object  
                    WebRequest request = HttpWebRequest.Create(strUrl);
                    // Get the response back  
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = (Stream)response.GetResponseStream();
                    StreamReader readStream = new StreamReader(s);
                    string dataString = readStream.ReadToEnd();
                    response.Close();
                    s.Close();
                    readStream.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (model.UserID >0)
            {
                var mobile = db.UserService.GetUser(model.UserID).Mobile;
              
               
                try
                {
                    string strUrl = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/sendmessage/?apikey="+smsapi+"&to="+mobile+"&msg="+model.Message;
                    // Create a request object  
                    WebRequest request = HttpWebRequest.Create(strUrl);
                    // Get the response back  
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = (Stream)response.GetResponseStream();
                    StreamReader readStream = new StreamReader(s);
                    string dataString = readStream.ReadToEnd();
                    response.Close();
                    s.Close();
                    readStream.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            return RedirectToAction("SMSGroupSend");
   
        }
        public IActionResult SMSConfig()
        {
            var smsconfig = new SMSConfig();
            var smsconfig1= db.SMSService.GetSmsConfig();
            if (smsconfig1 != null)
            {
                smsconfig = smsconfig1;
            
            }
          
            return View(smsconfig);
        
        }
        public IActionResult GetApiBalance(string ApiKey)
        {
            try
            {
                string strUrl = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/getbalance/?apikey=" + ApiKey + "&providerID=1";
                string strUrl1 = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/getbalance/?apikey=" + ApiKey + "&providerID=2";
                string strUrl2 = "http://smsservice.hadara.ps:4545/SMS.ashx/bulkservice/sessionvalue/getbalance/?apikey=" + ApiKey + "&providerID=4";
                WebRequest request = HttpWebRequest.Create(strUrl);
                WebRequest request1 = HttpWebRequest.Create(strUrl1);
                WebRequest request2 = HttpWebRequest.Create(strUrl2);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                Stream s1 = (Stream)response1.GetResponseStream();
                Stream s2 = (Stream)response2.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                StreamReader readStream1 = new StreamReader(s1);
                StreamReader readStream2 = new StreamReader(s2);
                string dataString = readStream.ReadToEnd();
                string dataString1 = readStream1.ReadToEnd();
                string dataString2 = readStream2.ReadToEnd();
                response.Close();
                response1.Close();
                response2.Close();
                s.Close();
                s1.Close();
                s2.Close();
                readStream.Close();
                readStream1.Close();
                readStream2.Close();
                int firstStringPosition = dataString.LastIndexOf("<AvilabeBalance>");
                int secondStringPosition = dataString.IndexOf("</AvilabeBalance>");
                int firstStringPosition1 = dataString1.LastIndexOf("<AvilabeBalance>");
                int secondStringPosition1 = dataString1.IndexOf("</AvilabeBalance>");
                int firstStringPosition2 = dataString2.LastIndexOf("<AvilabeBalance>");
                int secondStringPosition2 = dataString2.IndexOf("</AvilabeBalance>");

                string balance = dataString.Substring(firstStringPosition, 37);
                string balance1 = dataString1.Substring(firstStringPosition1, 37);

                string balance2 = dataString2.Substring(firstStringPosition2, 37);
                var xray = balance.Split('>')[1];
                xray = xray.Split('<')[0];
                var xray1 = balance1.Split('>')[1];
                xray1 = xray1.Split('<')[0];
                var xray2 = balance2.Split('>')[1];
                xray2 = xray2.Split('<')[0];
                string[] balances = new string[3];
                balances[0] = xray;
                balances[1] = xray1;
                balances[2] = xray2;

                return Json(balances);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }
        [HttpPost]
        public IActionResult AddApiKey(SMSConfig model)
        {
            if (model.ID > 0)
            {
                db.SMSService.UpdateSmsConfig(model);
            }
            else
            {
                db.SMSService.AddSmsApi(model);
            }
            
            
            return RedirectToAction("SMSConfig"); 
        }
    }
}