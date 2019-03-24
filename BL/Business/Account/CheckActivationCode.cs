using BE;
using BE.Entities.Requests;
using BE.Entities.Request;
using BL.Business.Utility;
using ErrorCenter;
using ErrorCenter.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Business.Account
{
    public class CheckActivationCode
    {
        public static Result<TokenResp> CheckActivation(ActivationCodeReq activationCode)
        {
            var checkActivationCode = new CheckActivationCode(activationCode);
            return checkActivationCode.CheckActivationCodeOperation();
        }

        private CheckActivationCode(ActivationCodeReq activationCode)
        {
            _activationCode = activationCode;
        }
        private ActivationCodeReq _activationCode;

        private Result<TokenResp> CheckActivationCodeOperation()
        {
            Result<TokenResp> result;
            var validityCheck = IsActivationCodeValid(_activationCode);
            if (!validityCheck.IsOk)
            {
                result = new Result<TokenResp>(null, validityCheck.Error, false);
                return result;
            }
            result = TokenUtils.GetNewToken(_activationCode);
            return result;
        }
        private Result<DMLResultResp> IsActivationCodeValid(ActivationCodeReq activationCode)
        {
            Result<DMLResultResp> result;
            try
            {
                var checkValidity = DA.Queries.UserAccounts.ExistActivationByUser(activationCode);
                if (checkValidity)
                {
                    result = new Result<DMLResultResp>(null, null, true);
                }
                else
                {
                    result = new Result<DMLResultResp>(null, new Error(ErrorMessage.ActivationCodeIsNotValid), false);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message, e);
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.LoadActivationByUserError), false);
            }
            
            return result;
        }

        

    }
}
