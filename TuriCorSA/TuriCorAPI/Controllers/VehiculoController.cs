using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace TuriCorAPI.Controllers
{
    [EnableCors(origins: "http://localhost:2253", headers: "*", methods: "*")]
    public class VehiculoController : ApiController
    {

        public IHttpActionResult Get()
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var vehiculos = cliente.ConsultarVehiculosDisponibles(new ServiceReferenceReservaVehiculos.ConsultarVehiculosRequest()
                {
                    IdCiudad = 2,
                    FechaHoraRetiro = new DateTime(2017, 1, 1),
                    FechaHoraDevolucion= new DateTime(2017,3,27)
                });

                if (vehiculos == null)
                {
                    return NotFound();
                }
                return Ok(vehiculos);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


    }
}
