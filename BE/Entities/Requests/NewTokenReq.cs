using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Requests
{
    public class NewTokenReq
    {
        public NewTokenReq(int userId, string tokenStr, DateTime tokenTime)
        {
            UserId = userId;
            TokenStr = tokenStr;
            TokenTime = tokenTime;
        }

        public int UserId { get; set; }
        public string TokenStr { get; set; }
        public DateTime TokenTime { get; set; }
    }
}
