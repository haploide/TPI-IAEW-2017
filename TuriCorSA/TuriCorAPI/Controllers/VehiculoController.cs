using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using AuthorizationServer.App_Start;
using System.Collections.Generic;
using TuriCorAPI.ServiceReferenceReservaVehiculos;

namespace TuriCorAPI.Controllers
{
     
    
    public class VehiculoController : ApiController
    {
        //[Scope("read")]
        public IHttpActionResult Get(int Id, DateTime fechaHoraRetiro, DateTime fechaHoraDevolucion)
        {
            List<VehiculoModel> listaVehiculos = new List<VehiculoModel>();
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
                foreach(VehiculoModel ve in vehiculos.VehiculosEncontrados )
                {
                    ve.PrecioPorDia = ve.PrecioPorDia * (decimal)1.20 ;
                    if (ve.TieneAireAcon==true)
                    {
                        ve.TieneAireAcon = Convert.ToBoolean("SI");

                    }

                    listaVehiculos.Add(ve);
                }

                return Ok(listaVehiculos);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }




    }
}
