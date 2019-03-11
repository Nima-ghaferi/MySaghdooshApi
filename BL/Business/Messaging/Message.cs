using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BL.Business.Messaging
{
    public class Message
    {
        public static bool SendSms(string phoneNumber, string message)
        {
            var token = new Token().GetToken("userApikey", "secretKey");
            var messageSendObject = new MessageSendObject()
            {
                Messages = new List <String> { "message" }.ToArray(),
                MobileNumbers = new List <String> { "phoneNumber" }.ToArray(),
                LineNumber = "30003472012345",
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };
            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);
            if (messageSendResponseObject.IsSuccessful)
            {
            }
            else
            {
            }                                
            //sened message
            //if sent successfully ; return true
            //esle return false
            return true;
        }

        public static bool SendActivationSms(string phoneNumber, string activationCode)
        {
            var message = String.Format(ActivationCodeMessage, activationCode);
            var res = SendSms(phoneNumber, message);
            return res;
        }

        public static readonly string ActivationCodeMessage = "کد فعال سازی شما {0} می باشد.";
    }
}
