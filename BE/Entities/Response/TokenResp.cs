using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Request
{
    public class TokenResp : ResultEntity
    {
        public TokenResp(string token)
        {
            Token = token;
        }

        public string Token { get;}
    }
}
