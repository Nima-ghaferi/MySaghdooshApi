using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entities.Response
{
    public class NewUserResp : ResultEntity
    {
        public NewUserResp(int activationId, bool messageSentSuccessful)
        {
            ActivationId = activationId;
            MessageSentSuccessful = messageSentSuccessful;
        }
        
        public int ActivationId { get; set; }
        public bool MessageSentSuccessful { get; set; }
    }
}
