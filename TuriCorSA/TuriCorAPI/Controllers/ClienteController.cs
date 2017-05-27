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

        public IHttpActionResult Get(int id)
        {
            try
            {
                if (_db.Cliente == null || !_db.Cliente.Any())
                {
                    return NotFound();
                }
                Cliente cli = _db.Cliente.FirstOrDefault(p => p.Id == id);
                if (cli == null)
                {
                    return NotFound();
                }
                return Ok(cli);

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
