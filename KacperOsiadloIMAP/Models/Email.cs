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
using KacperOsiadloIMAP.Security;

namespace KacperOsiadloIMAP.Models
{

    //TODO Move this to services
    class SmtpService
    {
        public string subject { get; set; }
        public string to { get; set; }
        public string body { get; set; }
        private User user { get; set; }

       
        public SmtpService (User user)
        {

            this.user = user;


        }

        //Sending messages fine
        public async Task SendEmail()
        {
            using var client = new SmtpClient();
            using var imap = new ImapClient();

            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(user.identity, user.emailAddress));
            message.To.Add(new MailboxAddress("carl", to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = Encryptor.Encrypt(body)
            };
            client.Connect(user.smtp, user.smtpPort, SecureSocketOptions.Auto);
            imap.Connect(user.imap, user.imapPort, SecureSocketOptions.Auto);
            await client.AuthenticateAsync(user.login, user.password);
            if (client.IsAuthenticated)
            {

                client.Send(message);
               await imap.AuthenticateAsync(user.login, user.password);
               var folder = imap.Inbox.GetSubfolder("Sent");
                folder.Open(FolderAccess.ReadWrite);
                folder.Append(message);
                imap.Disconnect(true);
                client.Disconnect(true);
            }
        } 
    }
}
