using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspIdentityImplementation.Controllers
{
    [Authorize]
    public class BaseApiController : ApiController
    {
    }
}
