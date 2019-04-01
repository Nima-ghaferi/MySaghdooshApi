using BE.Entities.Requests;
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
    [Route("ServiceProvider/{providerId}/dislike")]
    public class DislikeServicePrpviderController : RichApi
    {
        public HttpResponseMessage Post(int providerId)
        {
            var headers = HttpContext.Current.Request.Headers;
            string userMobile = headers["MobileNumber"];
            var dislikeRequest = new ServiceProviderLikeReq(userMobile, providerId);
            var serviceProviders = BL.Business.ServiceProvider.SpecificServiceProvider.DislikeServiceProvider(dislikeRequest);
            if (!serviceProviders.IsOk)
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceProviders.Error.ErrorMessage);
            else
                Response = Request.CreateResponse(HttpStatusCode.OK, serviceProviders);
            return Response;
        }
    }
}
