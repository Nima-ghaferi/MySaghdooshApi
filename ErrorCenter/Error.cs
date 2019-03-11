using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCenter
{
    public class Error
    {
        public string ErrorMessage { get;}

        public Error(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
