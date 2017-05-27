using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TuriCorAPI.Models;
using System.Web.Http.Cors;

namespace TuriCorAPI.Controllers
{
    [EnableCors(origins: "http://localhost:2253", headers: "*", methods: "*")]
    public class ClienteController : ApiController
    {
        private TuricorEntities _db = new TuricorEntities();
        public IHttpActionResult Get()
        {
            try
            {
                if (_db.Cliente == null || !_db.Cliente.Any())
                {
                    return NotFound();
                }
                return Ok(_db.Cliente);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
