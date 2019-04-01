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
    public class CategoryServicesController : RichApi
    {
        [ValidateToken]
        [Route("CategoryServices/{categoryId}/{pageIndex}")]
        public HttpResponseMessage Get(int categoryId, int pageIndex)
        {
            var serviceProviders = BL.Business.ServiceProvider.CategoryServiceProviders.GetCategoryServices(categoryId, pageIndex);
            if (!serviceProviders.IsOk)
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, serviceProviders.Error.ErrorMessage);
            else
                Response = Request.CreateResponse(HttpStatusCode.OK, serviceProviders);
            return Response;
        }

    }
}
