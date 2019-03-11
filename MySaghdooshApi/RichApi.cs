using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MySaghdooshApi.Utility
{
    public class RichApi : ApiController
    {
        protected string Token
        {
            get
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues("token");
                var token = headerValues.FirstOrDefault();
                return token;
            }
        }
    }
}