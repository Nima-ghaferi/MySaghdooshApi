using MySaghdooshApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BE.Entities.Response;

namespace MySaghdooshApi.Controllers
{
    public class ServerInfoController : RichApi
    {
        [Route("serverInfo")]
        public HttpResponseMessage Post(ServerInfo serverInfo)
        {
            var result = BL.Business.Server.AddNewServer(serverInfo);
            if (!result.IsOk)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result.Error.ErrorMessage);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

    }
}
