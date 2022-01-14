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
        private readonly Models.MailMessage message;
        public MailMessageWindowDisplayed(Models.MailMessage message)
        {
            InitializeComponent();
            this.message = message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MailHeader.Items.Insert(0, "From: "+message.From);
                MailHeader.Items.Insert(1, "To: " + message.To);
                MailHeader.Items.Insert(2, "Subject: " + message.Subject);
                MailHeader.Items.Insert(3, message.Body);
                MailHeader.Items.Insert(4, message.AttachmentsName);
            }
            catch (Exception)
            {

            }

        }
    }
}
