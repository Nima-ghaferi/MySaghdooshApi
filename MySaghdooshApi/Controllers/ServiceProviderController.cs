﻿using MySaghdooshApi.Filters;
using MySaghdooshApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySaghdooshApi.Controllers
{
    [ValidateToken]
    [Route("ServiceProvider/{providerId}")]
    public class ServiceProviderController : RichApi
    {
        public HttpResponseMessage Get(int providerId)
        {
            var serviceProviders = BL.Business.ServiceProvider.SpecificServiceProvider.GetServiceProvider(providerId);
            if (!serviceProviders.IsOk)
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceProviders.Error.ErrorMessage);
            else
                Response = Request.CreateResponse(HttpStatusCode.OK, serviceProviders);
            return Response;
        }
    }
}
