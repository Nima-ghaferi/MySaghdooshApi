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
        public HttpResponseMessage Post(NewUserReq newUser)
        {
            if (ModelState.IsValid)
            {
                var result = BL.Business.Account.AddNewAccount.AddNewUser(newUser);
                if (!result.IsOk)
                    Response = Request.CreateResponse(HttpStatusCode.InternalServerError, result.Error.ErrorMessage);
                else
                    Response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Response;
        }
    }
}
