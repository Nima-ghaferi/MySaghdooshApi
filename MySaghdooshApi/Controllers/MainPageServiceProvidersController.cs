using MySaghdooshApi.Filters;
using MySaghdooshApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySaghdooshApi.Controllers
{
    public class MainPageServiceProvidersController : RichApi
    {
        [ValidateToken]
        [Route("MainPageServiceProviders/{pageIndex}")]
        public HttpResponseMessage Get(int pageIndex)
        {
            var serviceProviders = BL.Business.ServiceProvider.MainPageServiceProviders.GetMainPageProviders(pageIndex);
            if (!serviceProviders.IsOk)
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceProviders.Error.ErrorMessage);
            else
                Response = Request.CreateResponse(HttpStatusCode.OK, serviceProviders);
            return Response;
        }
    }
}
