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
        public static Result<NewUserResp> AddNewUser(NewUserReq newUser)
        {
            var addNewAccount = new AddNewAccount(newUser);
            return addNewAccount.AddNewUserOperation();
        }
        private AddNewAccount(NewUserReq newUser)
        {
            _newUser = newUser;
        }

        private NewUserReq _newUser;
        private Result<NewUserResp> AddNewUserOperation()
        {
            Result<DMLResultResp> validationResult = IsNewUserValid(_newUser);
            Result<NewUserResp> result;
            if (!validationResult.IsOk)
            {
                result = new Result<NewUserResp>(null, validationResult.Error, false);
            }
            else
            {
                bool smsSentResult;
                int activationCodeId;
                var user = DA.Queries.UserAccounts.SelectUserByTel(_newUser.Tel);
                if (user == null)
                {                    
                    int userId = DA.Queries.UserAccounts.InsertUser(_newUser);
                    NewActivationReq newActivation = new NewActivationReq(userId, Utility.Random.GetRandomNumber());
                    try
                    {
                        activationCodeId = DA.Queries.UserAccounts.InsertActivation(newActivation);
                    }
                    catch (InsertIntoDataBaseException)
                    {
                        DA.Queries.UserAccounts.DeleteUser(userId);
                        throw;
                    }
                    smsSentResult = Message.SendSms(_newUser.Tel, String.Format(Message.ActivationCodeMessage, newActivation.ActicationCode, _newUser.Name));
                }
                else
                {
                    NewActivationReq newActivation = new NewActivationReq(user.Id, Utility.Random.GetRandomNumber());
                    activationCodeId = DA.Queries.UserAccounts.InsertActivation(newActivation);
                    smsSentResult = Message.SendSms(_newUser.Tel, String.Format(Message.ActivationCodeMessage, newActivation.ActicationCode, _newUser.Name));

                }
                result = new Result<NewUserResp>(new NewUserResp(activationCodeId, smsSentResult), null, true);
            }
            return result;
        }

        //TODO resend activationCode

        private Result<DMLResultResp> IsNewUserValid(NewUserReq newUser)
        {
            Result<DMLResultResp> result;
            if (!HasName(newUser))
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.NewAccountNameIsEmpty), false);
            }
            else if (!HasTel(newUser))
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.NewAccountTelIsEmpty), false);
            }
            else
            {
                result = new Result<DMLResultResp>(null, null, true);
            }
            return result;
        }

        private bool HasTel(NewUserReq newUser)
        {
            return !string.IsNullOrEmpty(newUser.Tel);
        }

        private bool HasName(NewUserReq newUser)
        {
            return !string.IsNullOrEmpty(newUser.Name);
        }
    }
}
