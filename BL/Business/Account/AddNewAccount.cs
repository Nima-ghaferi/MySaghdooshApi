using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BE.Entities.Response;
using BE.Entities.Requests;
using ErrorCenter;
using ErrorCenter.Messages;
using BL.Business.Messaging;

namespace BL.Business.Account
{
    public class AddNewAccount
    {
        public static Result<NewUserResp> AddNewUser(NewUser newUser)
        {
            var addNewAccount = new AddNewAccount();
            return addNewAccount.AddNewUserOperation(newUser);
        }

        private AddNewAccount()
        {
        }

        private Result<NewUserResp> AddNewUserOperation(NewUser newUser)
        {
            Result<DMLResult> validationResult = IsNewUserValid(newUser);
            Result<NewUserResp> result;
            if (!validationResult.IsOk)
            {
                result = new Result<NewUserResp>(null, validationResult.Error, false);
            }
            else
            {
                int activationCodeId;
                int userId = DA.Queries.UserAccounts.InsertUser(newUser);
                NewActivation newActivation = new NewActivation(userId, Utils.GetRandomNumber());
                try
                {
                    activationCodeId = DA.Queries.UserAccounts.InsertActivation(newActivation);
                }
                catch (InsertIntoDataBaseException)
                {
                    DA.Queries.UserAccounts.DeleteUser(userId);
                    throw;
                }
                bool smsSentResult = Message.SendSms(newUser.Tel, String.Format(Message.ActivationCodeMessage, newActivation.ActicationCode));
                result = new Result<NewUserResp>(new NewUserResp(userId, activationCodeId, smsSentResult), null, true);
            }
            return result;
        }

        //TODO resend activationCode

        private Result<DMLResult> IsNewUserValid(NewUser newUser)
        {
            Result<DMLResult> result;
            if (!HasName(newUser))
            {
                result = new Result<DMLResult>(null, new Error(ErrorMessage.NewAccountNameIsEmpty), false);
            }
            else if (!HasTel(newUser))
            {
                result = new Result<DMLResult>(null, new Error(ErrorMessage.NewAccountTelIsEmpty), false);
            }
            else
            {
                result = new Result<DMLResult>(null, null, true);
            }
            return result;
        }

        private bool HasTel(NewUser newUser)
        {
            return !string.IsNullOrEmpty(newUser.Tel);
        }

        private bool HasName(NewUser newUser)
        {
            return !string.IsNullOrEmpty(newUser.Name);
        }
    }
}
