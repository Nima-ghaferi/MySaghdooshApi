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
    [Route("ServiceProvider/{providerId}/like")]
    public class LikeServicePrpviderController : RichApi
    {
        public HttpResponseMessage Post(int providerId)
        {
            var headers = HttpContext.Current.Request.Headers;
            string userMobile = headers["MobileNumber"];
            var likeRequest = new ServiceProviderLikeReq(userMobile, providerId);
            var serviceProviders = BL.Business.ServiceProvider.SpecificServiceProvider.LikeServiceProvider(likeRequest);
            if (!serviceProviders.IsOk)
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceProviders.Error.ErrorMessage);
            else
                Response = Request.CreateResponse(HttpStatusCode.OK, serviceProviders);
            return Response;
        }
    }
}
