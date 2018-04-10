using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspIdentityImplementation.Controllers
{
    public class ValuesDataController : BaseApiController
    {
        public List<string> Get()
        {
            return new List<string>
            {
                "a",
                "b",
                "c"
            };
        } 
    }
}
