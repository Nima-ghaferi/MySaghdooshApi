using MySaghdooshApi.Filters;
using MySaghdooshApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MySaghdooshApi.Controllers
{
    [ValidateToken]
    [Route("LikedServices/{pageIndex}")]
    public class LikedServicesController : RichApi
    {
        public HttpResponseMessage Get(int pageIndex)
        {
            var headers = HttpContext.Current.Request.Headers;
            string userMobile = headers["MobileNumber"];
            var serviceProviders = BL.Business.ServiceProvider.SpecificServiceProvider.GetLikedServices(userMobile, pageIndex);
            if (!serviceProviders.IsOk)
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceProviders.Error.ErrorMessage);
            else
                Response = Request.CreateResponse(HttpStatusCode.OK, serviceProviders);
            return Response;
        }
    }
}
