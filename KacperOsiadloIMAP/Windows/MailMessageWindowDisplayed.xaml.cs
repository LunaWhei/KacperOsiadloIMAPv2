using KacperOsiadloIMAP.Models;
using KacperOsiadloIMAP.Security;
using System;
using System.Linq;
using System.Windows;

namespace KacperOsiadloIMAP.Windows
{
    /// <summary>
    /// Interaction logic for MailMessageWindowDisplayed.xaml
    /// </summary>
    public partial class MailMessageWindowDisplayed : Window
    {
        private string Attachment_information { get; set; }
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
            if (Mail.AttachmentsName.FirstOrDefault() != null)
            {
                Attachment_information = "Ta wiadomośc posiada załączniki, czy chcesz je pobrać?";
            }
            else
            {
                Attachment_information = "Brak załączników";
            }



            try
            {
                MailHeader.Items.Insert(0, "From: "+Mail.From);
                MailHeader.Items.Insert(1, "To: " + Mail.To);
                MailHeader.Items.Insert(2, "Subject: " + Mail.Subject);
                MailHeader.Items.Insert(3, Decryptor.Decrypt(Mail.Body));
                MailHeader.Items.Insert(4, Attachment_information);
            }
            catch (Exception)
            {

            }

        }
    }
}
