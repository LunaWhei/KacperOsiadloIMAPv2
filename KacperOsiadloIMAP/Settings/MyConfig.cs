using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KacperOsiadloIMAP.Settings
{
    [Serializable]
    public class MyConfig
    {
        public WindowConfig MainWindow { get; set; }
        public Theme Theme { get; set; }

        public MyConfig()
        {
            MainWindow = new WindowConfig();
            MainWindow.Width = 600;
            MainWindow.Height = 400;

            Theme = Theme.Light;
        }
    }
}
