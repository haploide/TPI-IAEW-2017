using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TuriCorAPI.Models;
using AuthorizationServer.App_Start;

namespace TuriCorAPI.Controllers
{
    [EnableCors(origins: "http://localhost:8660", headers: "*", methods: "*")]  // permite sacar info fuera del servidor 
    [Scope("read")]
    public class VendedorController : ApiController
    {
        private TuricorEntities _db = new TuricorEntities();
        public IHttpActionResult Get()
        {
            try
            {
                if (_db.Vendedor == null || !_db.Vendedor.Any())
                {
                    return NotFound();
                }
                return Ok(_db.Vendedor);
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
                if (_db.Vendedor == null || !_db.Vendedor.Any())
                {
                    return NotFound();
                }
                Vendedor ven = _db.Vendedor.FirstOrDefault(p => p.Id == id);
                if (ven == null)
                {
                    return NotFound();
                }
                return Ok(ven);

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
