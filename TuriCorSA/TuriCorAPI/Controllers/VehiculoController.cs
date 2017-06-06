using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using AuthorizationServer.App_Start; 
namespace TuriCorAPI.Controllers
{
    // [EnableCors(origins: "http://localhost:2253", headers: "*", methods: "*")]
    [Scope("read")]
    public class VehiculoController : ApiController
    {
        public IHttpActionResult Get(int Id, DateTime fechaHoraRetiro, DateTime fechaHoraDevolucion)
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var vehiculos = cliente.ConsultarVehiculosDisponibles(new ServiceReferenceReservaVehiculos.ConsultarVehiculosRequest()
                {
                    IdCiudad = Id,
                    FechaHoraRetiro = fechaHoraRetiro,
                    FechaHoraDevolucion = fechaHoraDevolucion
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
