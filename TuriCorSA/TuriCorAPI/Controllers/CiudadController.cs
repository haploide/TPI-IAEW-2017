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
    public class CiudadController : ApiController
    {
        //[Scope("read")]
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
        public IHttpActionResult Get(int idCiudad, int idPais)
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var ciudades = cliente.ConsultarCiudades(new ServiceReferenceReservaVehiculos.ConsultarCiudadesRequest()
                {
                    IdPais = idPais,

                });

               
                if (ciudades == null)
                {
                    return NotFound();
                }
                foreach (var c in ciudades.Ciudades)
                {
                    if (c.Id == idCiudad)
                    {
                        return Ok(c);
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
