using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BE.Entities.Request;
using BE.Entities.Requests;
using ErrorCenter;
using ErrorCenter.Messages;
using BL.Business.Messaging;
using BL.Business.Utility;
using BE.Entities.Response;

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
                DA.Entities.User user = null;
                try
                {
                    user = DA.Queries.UserAccounts.SelectUserByTel(_newUser.Tel);
                }
                catch (Exception e)
                {
                    Logger.Log.Error(e.Message, e);
                    result = new Result<NewUserResp>(null, new Error(ErrorMessage.LoadUserByTelError), false);
                    return result;
                }
                
                if (user == null)
                {
                    int userId;
                    try
                    {
                        userId = DA.Queries.UserAccounts.InsertUser(_newUser);
                    }
                    catch (Exception e)
                    {
                        Logger.Log.Error(e.Message, e);
                        result = new Result<NewUserResp>(null, new Error(ErrorMessage.AddNewUserError), false);
                        return result;
                    }
                    
                    NewActivationReq newActivation = new NewActivationReq(userId, Utility.Random.GetRandomNumber());
                    try
                    {
                        activationCodeId = DA.Queries.UserAccounts.InsertActivation(newActivation);
                    }
                    catch (InsertIntoDataBaseException e)
                    {
                        DA.Queries.UserAccounts.DeleteUser(userId);
                        Logger.Log.Error(e.Message, e);
                        result = new Result<NewUserResp>(null, new Error(ErrorMessage.AddNewActivationError), false);
                        return result;
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
