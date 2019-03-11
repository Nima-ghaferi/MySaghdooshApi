using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Requests
{
    public class NewActivation
    {
        public NewActivation(int userId, int acticationCode)
        {
            UserId = userId;
            ActicationCode = acticationCode;
        }

        public int UserId { get; set; }
        public int ActicationCode { get; set; }
    }
}
