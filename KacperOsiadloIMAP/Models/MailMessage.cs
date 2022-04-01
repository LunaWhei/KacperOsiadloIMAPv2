
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
        public string subject { get; set; }

        public string to { get; set; }
        public string Lp { get; set; }
        public string date { get; set; }
        public string body { get; set; }
        public MailKit.UniqueId uid { get; set; }
        public MimeKit.InternetAddressList from { get; set; }
        public string message_data { get; set; }
        public IEnumerable<MimeEntity> attachmentsName { get; set; }
    }
}
