using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

namespace lhcp2020.Models
{
    public class MailSender
    {
        private readonly IOptions<MailConfiguration> mailConfiguration;

        public MailSender(IOptions<MailConfiguration> mailConfiguration)
        {
            this.mailConfiguration = mailConfiguration;

        }
        public void SendMail(IEnumerable<Message> mailMessages)
        {
            //Read values of configuration properties
            string fromMailAddres = mailConfiguration.Value.FromMailAddres;
            string mailPW = mailConfiguration.Value.MailPW;

            var client = new SmtpClient();
            client.Connect("lhchinesepaintings.com", 587, SecureSocketOptions.None);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(fromMailAddres, mailPW);
                        
            foreach (var msg in mailMessages)
            {
                var mail = new MimeMessage();

                mail.From.Add(new MailboxAddress("Timothy Wu", fromMailAddres));
                mail.To.Add(new MailboxAddress("", msg.Recipient.Email));
                mail.Subject = msg.Subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = msg.MessageBody;
                mail.Body = bodyBuilder.ToMessageBody();

                try
                {
                    client.Send(mail);

                   
                }
                catch (Exception ex)
                {

                    throw ex;
                    // Or, more likely, do some logging or something
                }
            }
            client.Disconnect(true);
           
        }

    }
}
