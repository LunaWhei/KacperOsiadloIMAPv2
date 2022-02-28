using KacperOsiadloIMAP.Models;
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
using System.Windows.Shapes;

namespace KacperOsiadloIMAP.Windows
{
    /// <summary>
    /// Interaction logic for MailMessageWindowDisplayed.xaml
    /// </summary>
    public partial class MailMessageWindowDisplayed : Window
    {
        public MailMessageWindowDisplayed()
        {
            InitializeComponent();
        }
        private readonly MailMessage Mail;
        public MailMessageWindowDisplayed(MailMessage Mail)
        {
            InitializeComponent();
            this.Mail = Mail;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                MailHeader.Items.Insert(0, "From: "+Mail.From);
                MailHeader.Items.Insert(1, "To: " + Mail.To);
                MailHeader.Items.Insert(2, "Subject: " + Mail.Subject);
                MailHeader.Items.Insert(3, Mail.Body);
                MailHeader.Items.Insert(4, Mail.AttachmentsName);
            }
            catch (Exception)
            {

            }

        }
    }
}
