using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TuriCorAPI.Controllers
{
    [EnableCors(origins: "http://localhost:2253", headers: "*", methods: "*")]
    public class CiudadController : ApiController
    {
        public IHttpActionResult Get(int id) 
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var ciudades= cliente.ConsultarCiudades(new ServiceReferenceReservaVehiculos.ConsultarCiudadesRequest()
                {
                    IdPais = id,
                   
                });

                if (ciudades == null)
                {
                    return NotFound();
                }
                return Ok(ciudades);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
