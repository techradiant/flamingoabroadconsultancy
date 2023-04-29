using FAC.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace FAC.Web.Controllers
{
   
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
        [Route("about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AboutSection(string currentPage)
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.CurrentPage = currentPage;
            return PartialView("_AboutPartial");
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost, Route("contact")]        
        public ActionResult Contact(ContactViewModel vm)
        {
            string response = "Thank you for reaching out to us. We  will get back to you soon.";
            try
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendLine("Hi Team,");
                messageBuilder.AppendLine("There is an enquiry through website.  Below are the details:");
                messageBuilder.AppendLine($"Name: {vm.Name}");
                messageBuilder.AppendLine($"Email: {vm.Email}");
                messageBuilder.AppendLine($"Phone: {vm.Phone}");
                messageBuilder.AppendLine($"Subject: {vm.Subject}");                
                messageBuilder.AppendLine($"Message: {vm.Message}");
                messageBuilder.AppendLine($"Please review it");
                messageBuilder.AppendLine($"Thanks,");
                messageBuilder.AppendLine($"Admin");

                using (SmtpClient client = new SmtpClient())
                {
                    MailMessage message = new MailMessage();
                    message.Subject = vm.Subject;
                    ConfigurationManager.AppSettings["EnquiryReceiverEmails"].Split(new[] { ',' }).ForEach(emailAddress =>
                    {
                        message.To.Add(new MailAddress(emailAddress.Trim()));
                    });
                    message.Body = messageBuilder.ToString();
                    //message.IsBodyHtml = true;
                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                response = "Failed! Please send an email to info@flamingoabroadconsultancy.com for enquiry.";
            }

            return Json(new { message = response });
        }
    }
}