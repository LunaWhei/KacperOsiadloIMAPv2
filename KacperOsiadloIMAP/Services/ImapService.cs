using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using KacperOsiadloIMAP.Models;
using MailKit.Security;
using MailKit;
using MailKit.Search;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections;
using System.IO;
using MimeKit;

namespace KacperOsiadloIMAP.Services
{
    public class ImapService
    {

        public static IList<UniqueId> GetAllUids(string FolderName = "INBOX")
        {
            using var client = new ImapClient();

            client.Connect(GH.MainUser.Imap, GH.MainUser.ImapPort, SecureSocketOptions.Auto);
            client.Authenticate(GH.MainUser.Login, GH.MainUser.Password);
            
            if (FolderName=="INBOX")
            {

                try
                {
                    client.Inbox.Open(FolderAccess.ReadOnly);
                    IList<UniqueId> UidList = new List<UniqueId>();
                    var items = client.Inbox.Fetch(0, -1, MessageSummaryItems.UniqueId);
                    if (items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            UidList.Add(item.UniqueId);




                        }
                        var uids = client.Inbox.Search(SearchQuery.Uids(UidList));
                        return uids;
                    }
                }
                catch (Exception)
                {

                    return null;
                }
                return null;
                
                

            }
            else
            {
                try
                {
                    IList<UniqueId> UidList = new List<UniqueId>();
                    client.Inbox.GetSubfolder(FolderName).Open(FolderAccess.ReadOnly);
                    var items = client.Inbox.GetSubfolder(FolderName).Fetch(0, -1, MessageSummaryItems.UniqueId);
                  //  var items = client.Inbox.GetSubfolder(FolderName).Fetch()
                    foreach (var item in items)
                    {
                        UidList.Add(item.UniqueId);


                    }
                    var uids = client.Inbox.GetSubfolder(FolderName).Search(SearchQuery.Uids(UidList));

                    return uids;
                }
                catch (Exception)
                {

                    return null;
                }
                
              




            }


            
        }
        public static List<IMailFolder> GetImapFolders()
        {

            using var client = new ImapClient();
            client.Connect(GH.MainUser.Imap, GH.MainUser.ImapPort, SecureSocketOptions.Auto);
            client.Authenticate(GH.MainUser.Login, GH.MainUser.Password);
            var personal = client.GetFolders(client.PersonalNamespaces[0], true).ToList();
            if (!personal.Any(mb => mb.Name.Equals("inbox", StringComparison.InvariantCultureIgnoreCase)))
                personal.Add(client.Inbox);

            var folders = new List<IMailFolder>(personal);
            folders = folders.Where(f =>
                        !f.Attributes.HasFlag(FolderAttributes.NoSelect) &&
                        !f.Attributes.HasFlag(FolderAttributes.NonExistent))
                    .Distinct()
                    .ToList();

            return folders;
        }
        public static MailMessage GetMessageByUid(UniqueId uid,string FolderName="INBOX")
        {
            
            using var client = new ImapClient();
            client.Connect(GH.MainUser.Imap, GH.MainUser.ImapPort, SecureSocketOptions.Auto);
            client.Authenticate(GH.MainUser.Login, GH.MainUser.Password);
            client.Inbox.Open(FolderAccess.ReadOnly);
            var message = client.Inbox.GetMessage(uid);
            if (FolderName != "INBOX")
            {
                client.Inbox.GetSubfolder(FolderName).Open(FolderAccess.ReadOnly);
                
                message = client.Inbox.GetSubfolder(FolderName).GetMessage(uid); ;
            }
            
            
            var fileName = "This Mail has no attachments";
            foreach (MimeEntity attachment in message.Attachments)
            {
                fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;

                using var stream = File.Create(fileName);
                if (attachment is MessagePart)
                {
                    var rfc822 = (MessagePart)attachment;

                    rfc822.Message.WriteTo(stream);

                }
                else
                {
                    var part = (MimePart)attachment;

                    part.Content.DecodeTo(stream);

                }
            }

            MailMessage MessageList = new() { Lp = message.MessageId, From = message.From, Subject = message.Subject, Date = message.Date.ToString(), To = message.To.ToString(), Body = message.GetTextBody(MimeKit.Text.TextFormat.Plain), AttachmentsName = fileName };

            client.Disconnect(true);
            return MessageList;
        }
        public static void SetFlagSeenMessage(MailKit.UniqueId UID)
        {
            using var client = new ImapClient();
            client.Connect(GH.MainUser.Imap, GH.MainUser.ImapPort, SecureSocketOptions.Auto);
            client.Authenticate(GH.MainUser.Login, GH.MainUser.Password);
        
        }

    }
}

