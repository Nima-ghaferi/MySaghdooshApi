using BE.Entities.Requests;
using MySaghdooshApi.Filters;
using MySaghdooshApi.Utility;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySaghdooshApi.Controllers
{
    
    public class CheckActivationCodeController : RichApi
    {
        [ValidateToken]
        [Route("checkActivationCode")]
        public HttpResponseMessage Post(ActivationCodeReq activationCode)
        {
            if (ModelState.IsValid)
            {
                var result = BL.Business.Account.CheckActivationCode.CheckActivation(activationCode);
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
