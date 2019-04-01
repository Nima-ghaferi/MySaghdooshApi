using BE;
using BE.Entities.Request;
using BE.Entities.Response;
using ErrorCenter;
using ErrorCenter.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MySaghdooshApi.Filters
{
    public class ValidateToken : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var headers = HttpContext.Current.Request.Headers;
            string token = headers["Token"];
            string userMobile = headers["MobileNumber"];
            var validationResult = IsRequestValid(userMobile, token);
            if (!validationResult.IsOk)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, validationResult.Error.ErrorMessage);
            }
        }

        private Result<DMLResultResp> IsRequestValid(string phoneNumber, string token)
        {
            Result<DMLResultResp> result;
            string decrypt;
            string mobileFromToken;
            try
            {
                decrypt = BL.Business.Utility.TokenUtils.Decrypt(token);
            }
            catch (Exception)
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.TokenStringIsNotValid), false);
                return result;
            }
            try
            {
                mobileFromToken = decrypt.Substring(5, 11);
            }
            catch (Exception)
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.TokenMobileIsNotValid), false);
                return result;
            }
            if (mobileFromToken != phoneNumber)
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.TokenMobileIsNotValidAndMobileIsNotSame), false);
                return result;
            }
            if (!CheckTokenBusinessLogic(phoneNumber, token))
            {
                result = new Result<DMLResultResp>(null, new Error(ErrorMessage.TokenValidationLogicFailed), false);
                return result;
            }
            result = new Result<DMLResultResp>(null, null, true);
            return result;
        }

        private bool CheckTokenBusinessLogic(string phoneNumber, string token)
        {
            return BL.Business.Utility.TokenUtils.CheckTokenValidation(phoneNumber, token);
        }
    }
}