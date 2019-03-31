using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Vreme
    {

        public static string Date()
        {
            var a = DateTime.Today;
            return a.ToShortDateString();
        }
    }
}
