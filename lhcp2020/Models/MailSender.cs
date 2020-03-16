using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace lhcp2020.Models
{
    public class MailSender
    {
        public void SendMail(IEnumerable<Message> mailMessages)
        {
            var client = new SmtpClient();
            client.Connect("lhchinesepaintings.com", 587, SecureSocketOptions.None);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate("timothy.wu@lhchinesepaintings.com", "xxx");
                        
            foreach (var msg in mailMessages)
            {
                var mail = new MimeMessage();

                mail.From.Add(new MailboxAddress("Timothy Wu", "timothy.wu@lhchinesepaintings.com"));
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
