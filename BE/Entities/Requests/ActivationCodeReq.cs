using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Requests
{
    public class ActivationCodeReq
    {
        public ActivationCodeReq(string tel, string code)
        {
            Tel = tel;
            Code = code;
        }

        public string Tel { get; set; }
        public string Code { get; set; }
    }
}
