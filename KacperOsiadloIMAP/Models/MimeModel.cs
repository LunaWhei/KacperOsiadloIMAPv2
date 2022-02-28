using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KacperOsiadloIMAP.Models
{
    public class MimeModel
    {
        public MimeMessage message { get; set; }
        public MailKit.UniqueId uid { get; set; }
    }
}
