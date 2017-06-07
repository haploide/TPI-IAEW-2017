using AuthorizationServer.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace TuriCorAPI.Controllers
{
    [EnableCors(origins: "http://localhost:8660", headers: "*", methods: "*")]
    public class MeController : ApiController
    {
        [Scope("read")]
        public string get()
        {
            return this.User.Identity.Name;
        }

        [Scope("write")]
        public string post()
        {
            return this.User.Identity.Name;
        }
    }
}
