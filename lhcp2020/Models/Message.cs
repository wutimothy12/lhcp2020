using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lhcp2020.Models
{
    public class Message
    {
        public MailRecipient Recipient { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
    }
}
