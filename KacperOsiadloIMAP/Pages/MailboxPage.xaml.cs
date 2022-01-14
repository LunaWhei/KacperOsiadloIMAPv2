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

            ImapService imapService = new();
            MailMessage mailMessages = new();
            IList<MailKit.UniqueId> uids = ImapService.GetAllUids();
            List<MailKit.IMailFolder> FolderList = ImapService.GetImapFolders();
            FolderCollection.Clear();
            foreach (var Folders in FolderList)
            {
                await Task.Run(() =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        
                        FolderCollection.Add(new Folder() { Name = Folders.Name, Path =Folders.Name });
                        
                    });
                });
            }
            foreach (var uid in uids)
            {
                await Task.Run(() => {
                    MailMessage mail = ImapService.GetMessageByUid(uid,FolderName:"INBOX");
                    this.Dispatcher.Invoke(() =>
                    {
                        MailCollection.Add(new MailMessage() { Lp = mail.Lp, Subject = mail.Subject, Date = mail.Date, From = mail.From, To = mail.To,Body= mail.Body });
                    });
                });
                 
            }
            






        }
        private async void FolderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MailCollection.Clear();
            
            Folder item = (Folder)FolderListBox.SelectedItem;
            ImapService imapService = new();
            MailMessage mailMessages = new();
            IList<MailKit.UniqueId> uids = ImapService.GetAllUids(FolderName:item.Name);
            Littlelog.Text = item.Name;
            try
            {
                foreach (var uid in uids)
                {
                    await Task.Run(() =>
                    {
                       // mailMessages = ImapService.GetMessageByUid(uid);
                        MailMessage mail = ImapService.GetMessageByUid(uid,FolderName:item.Name);
                        this.Dispatcher.Invoke(() =>
                        {
                            MailCollection.Add(new MailMessage() { Lp = mail.Lp, Subject = mail.Subject, Date = mail.Date, From = mail.From, To = mail.To, AttachmentsName = mail.AttachmentsName });

                        });
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
            ImapService.SetFlagSeenMessage(mailToPass.UID);
            MailMessageWindowDisplayed mailMessageWindowDisplayed = new(mailToPass);
            mailMessageWindowDisplayed.Show();

        }
    }
}
