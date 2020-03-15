using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lhcp2020.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text.RegularExpressions;

namespace lhcp2020.Controllers
{
    public class MailRecipientsController : Controller
    {
        private readonly AppIdentityDbContext db;
        private readonly IEmailService emailService;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        string message;
        string subjet;

        public MailRecipientsController(AppIdentityDbContext context, IEmailService email, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            db = context;
            emailService = email;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }


        [Authorize]
        public async Task<ActionResult> Index()
        {
            var model = new MailRecipientsViewModel();

            // Get a list of all the recipients:
            var recipients = await db.MailRecipients.ToListAsync();
            foreach (var item in recipients)
            {

                // Put the relevant data into the ViewModel:
                var newRecipient = new SelectRecipientEditorViewModel()
                {
                    MailRecipientId = item.MailRecipientId,
                    UserName = item.UserName,
                    Email = item.Email,
                    Selected = true
                };

                // Add to the list contained by the "wrapper" ViewModel:
                model.MailRecipients.Add(newRecipient);
            }
            // Pass to the view and return:
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailRecipient mailrecipient = await db.MailRecipients.FirstOrDefaultAsync(x => x.MailRecipientId == id);
            if (mailrecipient == null)
            {
                return NotFound();
            }
            return View(mailrecipient);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("UserName,Email")] MailRecipient mailrecipient)
        {
            if (ModelState.IsValid)
            {
                db.MailRecipients.Add(mailrecipient);
                await db.SaveChangesAsync();
                string result = await _razorViewToStringRenderer.RenderViewToStringAsync("Email/Register", mailrecipient);
                emailService.Send(mailrecipient.Email, "Registration Confirmation", result);
                emailService.Send("timothy.wu@masterpiecestudioz.com", "New Member", "Praise Lord here is new member "+ mailrecipient.UserName);
                TempData["name"] = mailrecipient.UserName;
                return RedirectToAction("Thank");
            }

            return View(mailrecipient);
        }
        public ActionResult Thank()
        {
          return View();
        }

        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailRecipient mailrecipient = await db.MailRecipients.FirstOrDefaultAsync(x => x.MailRecipientId == id);
            if (mailrecipient == null)
            {
                return NotFound();
            }
            return View(mailrecipient);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("MailRecipientId,UserName,Email")]
            MailRecipient mailrecipient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailrecipient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mailrecipient);
        }


        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailRecipient mailrecipient = await db.MailRecipients.FirstOrDefaultAsync(x => x.MailRecipientId == id);
            if (mailrecipient == null)
            {
                return NotFound();
            }
            return View(mailrecipient);
        }


        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MailRecipient mailrecipient = await db.MailRecipients.FirstOrDefaultAsync(x => x.MailRecipientId == id);/*FindAsync(id);*/
            db.MailRecipients.Remove(mailrecipient);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize]
        public ActionResult SendMail(MailRecipientsViewModel recipients, string textarea, string mailsubjet)
        {
            var sub = mailsubjet;
            subjet = sub;
            var msg = textarea;
            message = msg;
            // Retrieve the ids of the recipients selected:
            var selectedIds = recipients.getSelectedRecipientIds();

            // Grab the recipient records:
            var selectedMailRecipients = this.LoadRecipientsFromIds(selectedIds);

            // Build the message container for each:
            var messageContainers = this.createRecipientMailMessages(selectedMailRecipients);

            // Send the mail:
            var sender = new MailSender();
            sender.SendMail(messageContainers);

            // Reload the index form:
            return RedirectToAction("Index");

            // Mail-sending code will happen here . . .
            //System.Threading.Thread.Sleep(2000);
            //return RedirectToAction("Index");
        }


        IEnumerable<MailRecipient> LoadRecipientsFromIds(IEnumerable<int> selectedIds)
        {
            var selectedMailRecipients = from r in db.MailRecipients
                                         where selectedIds.Contains(r.MailRecipientId)
                                         select r;
            return selectedMailRecipients;
        }


        IEnumerable<Message> createRecipientMailMessages(IEnumerable<MailRecipient> selectedMailRecipients)
        {
            var messageContainers = new List<Message>();
            
            foreach (var recipient in selectedMailRecipients)
            {
                var msg = new Message()
                {
                    Recipient = recipient,
                    Subject = subjet,
                    MessageBody = this.getMessageText(recipient)
                };
                messageContainers.Add(msg);
            }
            return messageContainers;
        }


       
        string getMessageText(MailRecipient recipient)
        {

            //return "<p>Hello " + recipient.FullName + "<p/>" + WebUtility.HtmlDecode(message);
            message = Regex.Replace(message, @"&lt;%\s*username\s*%&gt;",
             (recipient.UserName.Length > 0 ? recipient.UserName : "friend"),
              RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return message;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    internal class HttpStatusCodeResult : ActionResult
    {
        private object badRequest;

        public HttpStatusCodeResult(object badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}