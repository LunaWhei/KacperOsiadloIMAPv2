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
                Login_page.IsSelected = true;
            }

            
        }



        private async void Save_login_data_Click(object sender, RoutedEventArgs e)
        {

            GH.MainUser.Login = Login.Text.ToString();
            GH.MainUser.Password = Password.Text.ToString();
            GH.MainUser.ImapPort = Convert.ToInt32(Imap_port.Text);
            GH.MainUser.Imap = ImapServer.Text.ToString();
            GH.MainUser.Smtp = Smtp.Text.ToString();
            GH.MainUser.EmailAdress = Email.Text.ToString();
            GH.MainUser.SmtpPort = Convert.ToInt32(Smtp_port.Text);
            MailboxPage mailboxpage = new();


            using var client = new ImapClient();
            try
            {
                await client.ConnectAsync(GH.MainUser.Imap, GH.MainUser.ImapPort, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(GH.MainUser.Login, GH.MainUser.Password);
                Is_logged_in = true;
                MailboxFrame.Navigate(mailboxpage);
                Mailbox.IsSelected = true;
                //FolderListFrame.Navigate(FolderListFrame);
            }
            catch (Exception)
            {

                MessageBox.Show("Niestety nie udało nawiązać się połączenia");
                throw;
            }

        }


        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            
            MailboxPage mailboxpage = new();
            MailboxFrame.Navigate(mailboxpage);

        }

        private void New_Mail_Click(object sender, RoutedEventArgs e)
        {
            SendEmailPage p1 = new();
            MailboxFrame.Navigate(p1);
            
        }

        private void Settings_Selected(object sender, RoutedEventArgs e)
        {
            if (!Is_logged_in)
            {
                Login_page.IsSelected = true;
            }
        }

        private void Login_Selected(object sender, RoutedEventArgs e)
        {

        }

    }
}
