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
            var token = new Token().GetToken("6b32b30b434e98ee7c36c6a4", "lglifeisgood");
            var messageSendObject = new MessageSendObject()
            {
                Messages = new List <String> { message }.ToArray(),
                MobileNumbers = new List <String> { phoneNumber }.ToArray(),
                LineNumber = "30004747477152",
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };
            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);
            return messageSendResponseObject.IsSuccessful;
        }

        public static bool SendActivationSms(string phoneNumber, string activationCode)
        {
            var message = String.Format(ActivationCodeMessage, activationCode);
            var res = SendSms(phoneNumber, message);
            return res;
        }

        public static readonly string ActivationCodeMessage = "ساقدوش من \nسلام {1}\nکد فعال سازی شما {0} می باشد.";
    }
}
