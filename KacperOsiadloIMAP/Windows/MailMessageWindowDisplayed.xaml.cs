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
            if (Mail.attachmentsName.FirstOrDefault() != null)
            {
                Attachment_information = "Ta wiadomośc posiada załączniki, czy chcesz je pobrać?";
            }
            else
            {
                Attachment_information = "Brak załączników";
            }



            try
            {
                MailHeader.Items.Insert(0, "Nadawca: "+Mail.from);
                MailHeader.Items.Insert(1, "Odbiorca: " + Mail.to);
                MailHeader.Items.Insert(2, "Temat: " + Mail.subject);
                MailHeader.Items.Insert(3, Decryptor.Decrypt(Mail.body));
                MailHeader.Items.Insert(4, Attachment_information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Wyświetlenie wiadomości nie powiodła się: {0}", ex.ToString()));
            }

        }
    }
}
