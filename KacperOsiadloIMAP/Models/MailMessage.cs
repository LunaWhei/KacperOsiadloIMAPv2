
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KacperOsiadloIMAP.Models
{
   public class MailMessage
    {
        public string Subject { get; set; }

        public string To { get; set; }
        public string Lp { get; set; }
        public string Date { get; set; }
        public string Body { get; set; }
        public MailKit.UniqueId UID { get; set; }
        public MimeKit.InternetAddressList From { get; set; }
        public string Message_data { get; set; }
        public IEnumerable<MimeEntity> AttachmentsName { get; set; }
    }
}
