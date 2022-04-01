using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KacperOsiadloIMAP.Models
{
    public class User
    {
        public string login { get; set; }
        public string password { get; set; }
        public int imapPort { get; set; }
        public string imap { get; set; }
        public string emailAddress { get; set; }
        public string  identity { get; set; }
        public string smtp { get; set; }
        public  int smtpPort { get; set; }
        

        public string Return_values()
        {
            return login+"\n"+password+"\n"+imapPort.ToString()+"\n"+imap+"\n"+smtp+"\n"+smtpPort+"\n"+emailAddress;
        }
    }

}
