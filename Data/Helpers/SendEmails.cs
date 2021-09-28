using Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace Data.Helpers
{
   public class SendEmails
    {
		public static bool SendInvoiceMail(EmailSendModel model, MemoryStream mem, IConfiguration iConfig)
		{
			var users = new List<string>();
			users.Add(model.ToEmail);
			if (model.AlterEmail != null)
			{
				users.Add(model.AlterEmail);
			}
			foreach (var item in users)
			{
				try
				{
					string emailFrom = iConfig.GetValue<string>("EmailConfig:EmailFrom");
					string emailPassword = iConfig.GetValue<string>("EmailConfig:EmailPassword");
					string emailHost = iConfig.GetValue<string>("EmailConfig:EmailHost");
					int emailPort = iConfig.GetValue<int>("EmailConfig:EmailPort");
					bool enableSsl = iConfig.GetValue<bool>("EmailConfig:EnableSSL");

					MailMessage mail = new MailMessage(emailFrom, item);
					mail.Subject = model.Subject;
					//mail.Body = model.Body;
					var body = "<p>Username: {0} </p><p>Password:{1}</p><p>{2}</p>";
					var xray = string.Format(model.Body, model.Body1, model.Body2);
					mail.Body = string.Format(body,model.Body, model.Body1, model.Body2);
					mail.IsBodyHtml = true;
					if (model.File2!=null)
					{
						//Current stream
						mem.Seek(0, System.IO.SeekOrigin.Begin);
						foreach (var item1 in model.File2)
						{
							var att = new System.Net.Mail.Attachment(mem, item1.FileName, item1.ContentType);
							mail.Attachments.Add(att);

						}
					}
					var smtp = new SmtpClient
					{
						Host = emailHost,
						Port = emailPort,
						EnableSsl = enableSsl,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						Credentials = new NetworkCredential(emailFrom, emailPassword),
						Timeout = 20000
					};
					smtp.Send(mail);
				}
				catch (Exception ex)
				{

					throw ex;
				}

			}
			return true;
		}
	}
}

