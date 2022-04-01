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

            
            try
            {
                client.Connect(GH.MainUser.imap, GH.MainUser.imapPort, SecureSocketOptions.Auto);
                client.Authenticate(GH.MainUser.login, GH.MainUser.password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Niestety nie udało nawiązać się połączenia" + ex.ToString());
                
            }
           // client.Authenticate(GH.MainUser.Login, GH.MainUser.Password);
            
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
            try
            {
                client.Connect(GH.MainUser.imap, GH.MainUser.imapPort, SecureSocketOptions.Auto);
                client.Authenticate(GH.MainUser.login, GH.MainUser.password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Niestety nie udało nawiązać się połączenia" + ex.ToString());

            }

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

        public static List<MimeModel>  GetMessageListBasedOnCurrentFolderAsync(string FolderName="INBOX")
        {
            List<MimeModel> Results = new();
            using var client = new ImapClient();
            try
            {
            client.Connect(GH.MainUser.imap, GH.MainUser.imapPort, SecureSocketOptions.Auto);
            client.Authenticate(GH.MainUser.login, GH.MainUser.password);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Niestety nie udało nawiązać się połączenia" + ex.ToString());

            }

            var folder = client.Inbox;
            if (FolderName !="INBOX")
            {
                FolderName = FolderName.Remove(0, 6);
                folder = client.Inbox.GetSubfolder(FolderName);
            }
            folder.Open(FolderAccess.ReadWrite);
            IList<UniqueId> uniqueIds = folder.Search(SearchQuery.All);
            foreach (var uid in uniqueIds)
            {
                Results.Add(new MimeModel {
                    message = folder.GetMessage(uid),
                    uid = uid 
                });
   
            }
            return Results;



        }
            public static void SetFlagSeenMessage(MailKit.UniqueId UID)
        {
            using var client = new ImapClient();
            client.Connect(GH.MainUser.imap, GH.MainUser.imapPort, SecureSocketOptions.Auto);
            client.Authenticate(GH.MainUser.login, GH.MainUser.password);
        
        }

    }
}

