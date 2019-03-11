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
        [Route("categories")]
        public HttpResponseMessage Get()
        {
            var categories = BL.Business.General.GetAllCategories();
            if (!categories.IsOk)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, categories.Error.ErrorMessage);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, categories);
            return response;
        }
    }
}
