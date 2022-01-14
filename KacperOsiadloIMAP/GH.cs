using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KacperOsiadloIMAP
{
    public class GH
    {
        public static App App => System.Windows.Application.Current as App;

        public static Models.User MainUser
        {
            get
            {
                return App.User;
            }
        }
    }
}
