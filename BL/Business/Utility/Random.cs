using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Business.Utility
{
    public class Random
    {
        public static int GetRandomNumber()
        {
            var random = new System.Random();
            return random.Next(10000, 99999);            
        }
    }
}
