using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lhcp2020.Models
{
    public class SelectRecipientEditorViewModel
    {
        public bool Selected { get; set; }
        public int MailRecipientId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
