using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AuthorizationServer.App_Start;

namespace TuriCorAPI.Controllers
{
    
    //[Scope("read")]
    public class PaisController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var pais = cliente.ConsultarPaises();
                
                if (pais == null)
                {
                    return NotFound();
                }
                return Ok(pais);
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
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var pais = cliente.ConsultarPaises();
               
              
                if (pais == null)
                {
                    return NotFound();
                }
                foreach (var p in pais.Paises)
                {
                    if (p.Id == id)
                    {
                        return Ok(p);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

    }
}
