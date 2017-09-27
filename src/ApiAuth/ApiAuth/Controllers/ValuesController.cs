using System.Collections.Generic;
using System.Web.Http;
using ApiAuth.Security;

namespace ApiAuth.Controllers
{
    public class ValuesController : ApiController
    {
        public new SecurityPrincipal User => base.User as SecurityPrincipal;

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new[] { User.Caller.Id.ToString(), User.Caller.Name };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return User.Caller.Name;
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
    }
}
