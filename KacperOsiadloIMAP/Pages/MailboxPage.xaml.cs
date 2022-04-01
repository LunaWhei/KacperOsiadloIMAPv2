using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KacperOsiadloIMAP.Models;
using MailKit.Net.Imap;
using KacperOsiadloIMAP.Services;
using MailKit;
using System.Collections.ObjectModel;
using KacperOsiadloIMAP.Windows;
using MimeKit;

namespace KacperOsiadloIMAP
{
    /// <summary>
    /// Interaction logic for MailboxPage.xaml
    /// </summary>
    public partial class MailboxPage : Page
    {

        readonly ObservableCollection<Folder> FolderCollection = new();
        readonly ObservableCollection<MailMessage> MailCollection = new();
        public Window mainWindow;
        public MailboxPage()
        {

            InitializeComponent();
            FolderCollection = new ObservableCollection<Folder>() { };
            MailCollection = new ObservableCollection<MailMessage>() { };
            FolderListBox.ItemsSource = FolderCollection;
            Mailbox_list.ItemsSource = MailCollection;

        }
        public MailboxPage(Window mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            IList<UniqueId> uids = ImapService.GetAllUids();
            List<IMailFolder> FolderList = ImapService.GetImapFolders();
            FolderCollection.Clear();
            MailCollection.Clear();
            foreach (var Folders in FolderList)
            {
                await Task.Run(() =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        
                        FolderCollection.Add(new Folder() { name = Folders.Name, path =Folders.FullName });
                        


                    });
                });
            }
            FolderCollection.OrderBy(x => x.name);
            List<MimeModel> messages = new List<MimeModel>();
            messages = ImapService.GetMessageListBasedOnCurrentFolderAsync();
            foreach (var message in messages)
            {
                await Task.Run(() => {
                    this.Dispatcher.Invoke(() =>
                    {

                    });
                });
                 
            }

        }
        private async void FolderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MailCollection.Clear();
            Folder item = (Folder)FolderListBox.SelectedItem;
            if (item == null)
            {
                item = new Folder() { path = "INBOX" };
            }
            
            var emails = ImapService.GetMessageListBasedOnCurrentFolderAsync(item.path);
            try
            {
                
                if (emails != null)
                {

                        
                        await Task.Run(() =>
                        {
                            foreach (var email in emails)
                            { // mailMessages = ImapService.GetMessageByUid(uid);
                                var mail = email.message;

                                this.Dispatcher.Invoke(() =>
                                {
                                    MailCollection.Add(new MailMessage() {  
                                        subject = mail.Subject,
                                        body = mail.GetTextBody(MimeKit.Text.TextFormat.Plain),
                                        date = mail.Date.ToString(),
                                        from = mail.From,
                                        to = mail.To.ToString(),
                                        attachmentsName = mail.Attachments
                                    });

                                });
                            }
                        });
                    
                }

            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message); 
                
            }

                
            


        }
        private void Mailbox_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MailMessage mailToPass = (MailMessage)Mailbox_list.SelectedItem;
            MailMessageWindowDisplayed mailMessageWindowDisplayed = new(mailToPass);
            mailMessageWindowDisplayed.Show();

        }
    }
}
