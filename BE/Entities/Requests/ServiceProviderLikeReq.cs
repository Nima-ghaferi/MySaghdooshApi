using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Requests
{
    public class ServiceProviderLikeReq
    {
        public ServiceProviderLikeReq(string userMobileNumber, int serverId)
        {
            UserMobileNumber = userMobileNumber;
            ServerId = serverId;
        }
        public string UserMobileNumber { get; set; }
        public int ServerId { get; set; }

    }
}
