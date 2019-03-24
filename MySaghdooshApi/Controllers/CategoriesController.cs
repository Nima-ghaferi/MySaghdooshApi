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
    public class CategoriesController : RichApi
    {
        [ValidateToken]
        [Route("categories")]
        public HttpResponseMessage Get()
        {
            var categories = BL.Business.General.GetAllCategories();
            if (!categories.IsOk)
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, categories.Error.ErrorMessage);
            else
                Response = Request.CreateResponse(HttpStatusCode.OK, categories);
            return Response;
        }
    }
}
