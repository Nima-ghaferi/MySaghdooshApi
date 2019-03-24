using MySaghdooshApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BE.Entities.Request;
using MySaghdooshApi.Filters;

namespace MySaghdooshApi.Controllers
{
    public class ServerInfoController : RichApi
    {
        [ValidateToken]
        [Route("serverInfo")]
        public HttpResponseMessage Post(ServerInfoReq serverInfo)
        {
            if (ModelState.IsValid)
            {
                var result = BL.Business.Server.AddNewServer(serverInfo);
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
