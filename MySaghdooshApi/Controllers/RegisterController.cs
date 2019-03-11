using BE.Entities.Requests;
using MySaghdooshApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySaghdooshApi.Controllers
{
    public class RegisterController : RichApi
    {
        [Route("register")]
        public HttpResponseMessage Post(NewUser newUser)
        {
            var result = BL.Business.Account.AddNewAccount.AddNewUser(newUser);
            if (!result.IsOk)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result.Error.ErrorMessage);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}
