using MimeKit;
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

namespace KacperOsiadloIMAP
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class SendEmailPage : Page
    {
        MainWindow main;
        public SendEmailPage(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            
            
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private async void Send_mail_Click(object sender, RoutedEventArgs e)
        {
            SmtpService email = new(GH.MainUser)
            {
                body = Message_field.Text.ToString(),
                to = To_field.Text.ToString(),
                subject = Subject_field.Text.ToString()
            };

            try
            {
                await email.SendEmail();
                main.New_Mail.Visibility = Visibility.Visible;
                MailboxPage p = new();
                main.MailboxFrame.Navigate(p);
                MessageBox.Show("Wiadomość wysłana prawidłowo");

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Wysyłka nie powiodła się: {0}", ex.ToString()));

            }


           
        }

        private void Close_button_Click(object sender, RoutedEventArgs e)
        {
            main.New_Mail.Visibility = Visibility.Visible;
            MailboxPage p = new();
            main.MailboxFrame.Navigate(p);
        }
    }
}
