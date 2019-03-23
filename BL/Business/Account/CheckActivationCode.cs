using BE;
using BE.Entities.Requests;
using BE.Entities.Response;
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
            var validityCheck = isActivationCodeValid(_activationCode);
            if (!validityCheck.IsOk)
            {
                result = new Result<TokenResp>(null, validityCheck.Error, false);
                return result;
            }
            result = TokenUtils.getNewToken(_activationCode);
            return result;
        }
        private Result<DMLResultResp> isActivationCodeValid(ActivationCodeReq activationCode)
        {
            Result<DMLResultResp> result;
            var checkValidity = DA.Queries.UserAccounts.ExistActivationByUser(activationCode);
            if (checkValidity)
            {
                result = new Result<DMLResultResp>(null, null, true);
            }
            else
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.ActivationCodeIsNotValid), false);
            }
            return result;
        }

        

    }
}
