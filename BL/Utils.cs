using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Utils
    {
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static int GetRandomNumber()
        {
            var random = new Random();
            return random.Next(0, 100000);            
        }
    }
}
