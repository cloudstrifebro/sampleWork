using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Web.Helpers;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;

namespace AspIdApi.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public IHttpActionResult CreateToken([FromBody] dynamic credentials)
        {
            var header = new JwtHeader
            {
                
            };
            var payload = new JwtPayload
            {
                
            };
            var token = new JwtSecurityToken(header, payload);
            return Ok(token);
        }
    }
}
