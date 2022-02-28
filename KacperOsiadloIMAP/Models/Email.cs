using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Imap;

namespace KacperOsiadloIMAP.Models
{

    //TODO Move this to services
    class SmtpService
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        private User User { get; set; }

        public SmtpService (User user)
        {

            this.User = user;


        }

        //Sending messages fine
        public async Task SendEmail()
        {
            using var client = new SmtpClient();
            using var imap = new ImapClient();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(User.Identity, User.EmailAdress));
            message.To.Add(new MailboxAddress("carl", To));
            message.Subject = Subject;

            message.Body = new TextPart("plain")
            {
                Text = Body
            };
            client.Connect(User.Smtp, User.SmtpPort, SecureSocketOptions.Auto);
            imap.Connect(User.Imap, User.ImapPort, SecureSocketOptions.Auto);
            await client.AuthenticateAsync(User.Login, User.Password);
            if (client.IsAuthenticated)
            {

                client.Send(message);
               await imap.AuthenticateAsync(User.Login, User.Password);
               var folder = imap.Inbox.GetSubfolder("Sent");
                folder.Open(FolderAccess.ReadWrite);
                folder.Append(message);
                imap.Disconnect(true);
                client.Disconnect(true);
            }
        } 
    }
}
