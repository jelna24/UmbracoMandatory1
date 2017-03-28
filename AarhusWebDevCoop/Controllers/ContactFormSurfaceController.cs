using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }
            MailMessage message = new MailMessage();
            message.To.Add("jelna24@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("umbracotest24@gmail.com", "xxxx");
                smtp.EnableSsl = true;
                // send mail
                smtp.Send(message);
            }

            IContent formMessage = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");
            formMessage.SetValue("messageName", model.Name);
            formMessage.SetValue("email", model.Email);
            formMessage.SetValue("subject", model.Subject);
            formMessage.SetValue("messageContent", model.Message);

            Services.ContentService.Save(formMessage);

            //Save and publish with status
            //Services.ContentService.SaveAndPublishWithStatus(comment);

            TempData["success"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}