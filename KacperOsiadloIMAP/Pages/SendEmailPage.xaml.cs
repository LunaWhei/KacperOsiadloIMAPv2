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
        public SendEmailPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            log_window.Text = GH.MainUser.Return_values();
        }

        private async void Send_mail_Click(object sender, RoutedEventArgs e)
        {
            SmtpService email = new(GH.MainUser)
            {
                Body = Message_field.Text.ToString(),
                To = To_field.Text.ToString(),
                Subject = Subject_field.Text.ToString()
            };

            try
            {
                await email.SendEmail();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wysyłka nie powiodła się: {0}", ex.ToString());

            }

           
        }

        private void Close_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
