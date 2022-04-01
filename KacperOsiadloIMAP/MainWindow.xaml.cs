using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace KacperOsiadloIMAP
{
     
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        { 
        bool Is_logged_in = false;

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            if (!Is_logged_in)
            {
                Settings.IsSelected = true;
                MailboxTab.IsEnabled = false;
            }

            
        }



        private async void Save_login_data_Click(object sender, RoutedEventArgs e)
        {

            GH.MainUser.login = Login.Text.ToString();
            GH.MainUser.password = Password.Text.ToString();
            GH.MainUser.imapPort = Convert.ToInt32(Imap_port.Text);
            GH.MainUser.imap = ImapServer.Text.ToString();
            GH.MainUser.smtp = Smtp.Text.ToString();
            GH.MainUser.emailAddress = Email.Text.ToString();
            GH.MainUser.smtpPort = Convert.ToInt32(Smtp_port.Text);
            MailboxPage mailboxpage = new();


            using var client = new ImapClient();
            try
            {
                await client.ConnectAsync(GH.MainUser.imap, GH.MainUser.imapPort, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(GH.MainUser.login, GH.MainUser.password);
                Is_logged_in = true;
                MailboxFrame.Navigate(mailboxpage);
                MailboxTab.IsEnabled = true;
                MailboxTab.IsSelected = true;
                //FolderListFrame.Navigate(FolderListFrame);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Niestety nie udało nawiązać się połączenia" + ex.ToString()) ;
                
            }

        }


        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            
            MailboxPage mailboxpage = new();
            MailboxFrame.Navigate(mailboxpage);

        }

        private void New_Mail_Click(object sender, RoutedEventArgs e)
        {
            SendEmailPage p1 = new(this);
            New_Mail.Visibility = Visibility.Hidden;
            MailboxFrame.Navigate(p1);
            
        }


        private void TabSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Settings.IsSelected)
            {
                MailboxTab.IsEnabled = false;
            }
            
        }
    }
}
