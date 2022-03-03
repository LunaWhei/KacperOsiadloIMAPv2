using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KacperOsiadloIMAP.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int ImapPort { get; set; }
        public string Imap { get; set; }
        public string EmailAdress { get; set; }
        public string  Identity { get; set; }
        public string Smtp { get; set; }
        public  int SmtpPort { get; set; }
        

        public string Return_values()
        {
            return Login+"\n"+Password+"\n"+ImapPort.ToString()+"\n"+Imap+"\n"+Smtp+"\n"+SmtpPort+"\n"+EmailAdress;
        }
    }

}
