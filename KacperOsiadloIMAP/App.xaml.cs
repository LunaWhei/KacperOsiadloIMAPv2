using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KacperOsiadloIMAP
{
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public int Id { get; set; } = 0;

        public Models.User User { get; internal set; }
        public Models.TextFormatSettings textFormatSettings { get;internal set; }

        public App()
        {
            User = new Models.User();
            textFormatSettings = new Models.TextFormatSettings();
        }
    }
}
