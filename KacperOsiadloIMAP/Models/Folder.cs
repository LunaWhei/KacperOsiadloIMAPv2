using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KacperOsiadloIMAP.Models
{
   public class Folder
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }

        public int size { get; set; } 

    }
}
