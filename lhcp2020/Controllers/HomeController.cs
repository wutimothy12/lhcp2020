﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lhcp2020.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using AspNetCore.Honeypot;
using Microsoft.Extensions.Options;

namespace lhcp2020.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MailConfiguration> mailConfiguration;

        public HomeController(IOptions<MailConfiguration> mailConfiguration)
        {
            this.mailConfiguration = mailConfiguration;

        }
        public IActionResult Index()
        {
            ViewBag.Title = "LHChinesepaintings - silk paintings, custom paintings, original paintings, watercolors paintings";
            return View();
        }

        public IActionResult Bio()
        {
            ViewBag.Title = "Biography of Lin-Hua Wu, Chinese painting, China Pottery Arts, American sign language";
            return View();
        }

        public IActionResult Info()
        {
            ViewBag.Title = "Information - LHChinesepaintings - Chinese Painting general info, Shipping and Product info as chinese silk painting...";
            return View();
        }
        public IActionResult Custom()
        {
            ViewBag.Title = "Custom Art - LHChinesepaintings - customer painting, landscape painting, antique lampshade";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact - LHChinesepaintings - Washington State company, email club, custom order";
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                //Read values of configuration properties
                string fromMailAddres = mailConfiguration.Value.FromMailAddres;
                string mailPW = mailConfiguration.Value.MailPW;

                if (HttpContext.IsHoneypotTrapped())
                {
                    //ModelState.Clear();
                    //ModelState.AddModelError("", "bot detection");

                    //log
                    return View("Thanks");
                }
              try
                {
                    //instantiate a new MimeMessage
                    var message = new MimeMessage();
                    //Setting the To e-mail address
                    message.To.Add(new MailboxAddress("E-mail Recipient Name", contactViewModel.Emaillist));
                    //Setting the From e-mail address
                    message.From.Add(new MailboxAddress("E-mail Form Name", fromMailAddres));
                    //E-mail subject 
                    message.Subject = contactViewModel.Subject;
                    //E-mail message body
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = $"{contactViewModel.Message}<p> Message was sent by: {contactViewModel.Name}<br/>E-mail: {contactViewModel.Email}<br/>To: {contactViewModel.Emaillist}</p>"
                    };

                    
                    //Configure the e-mail
                    using (var emailClient = new SmtpClient())
                    {
                        emailClient.Connect("lhchinesepaintings.com", 587, SecureSocketOptions.None);
                        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                        emailClient.Authenticate(fromMailAddres, mailPW);
                        emailClient.Send(message);
                        emailClient.Disconnect(true);
                    }
                    
                    ViewBag.end = "Thank you in advance for your email to " + contactViewModel.Emaillist +". We look forward to hearing from you.";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    //ViewBag.Message = $" Oops! We have a problem here {ex.Message}";
                    ViewBag.Message = $" Oops! We have a problem here. Please email us by other way or call us. Thanks. ";
                }
                
            }
          
            TempData["Anchor"] = "jump";
            return View();
        }
    }
}