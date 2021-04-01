using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Options;

namespace lhcp2020.Models
{
	public class EmailService : IEmailService
	{
        private readonly IOptions<MailConfiguration> mailConfiguration;

        public EmailService(IOptions<MailConfiguration> mailConfiguration)
        {
            this.mailConfiguration = mailConfiguration;

        }
		public void Send(string email, string subject, string message)
		{
            //Read values of configuration properties
            string fromMailAddres = mailConfiguration.Value.FromMailAddres;
            string mailPW = mailConfiguration.Value.MailPW;

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Timothy Wu", fromMailAddres));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            //emailMessage.Body = new TextPart("plain") { Text = message };
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                //client.LocalDomain = "some.domain.com";
                client.Connect("lhchinesepaintings.com", 587, SecureSocketOptions.None);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(fromMailAddres, mailPW);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
	
	public interface IEmailService
    {
        void Send(string email, string subject, string message);
    }
}
