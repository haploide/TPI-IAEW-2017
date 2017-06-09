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
   
    //[Scope("read")]
    public class ReservaController : ApiController
    {
        private TuricorEntities _db = new TuricorEntities();

        public IHttpActionResult Get()
        {
            try
            {
                if (_db.Reserva == null || !_db.Reserva.Any())
                {
                    return NotFound();
                }
                return Ok(_db.Reserva);
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
                if (_db.Reserva == null || !_db.Reserva.Any())
                {
                    return NotFound();
                }
                Reserva res = _db.Reserva.FirstOrDefault(p => p.Id == id);
                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Get(bool incluirCancel)
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var reservas = cliente.ConsultarReservas(new ServiceReferenceReservaVehiculos.ConsultarReservasRequest()
                {
                    IncluirCanceladas = incluirCancel

                });

                if (reservas == null)
                {
                    return NotFound();
                }
                return Ok(reservas);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Post([FromBody]Reserva res, DateTime fechaDev, DateTime fechaRet)
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var reserva = cliente.ReservarVehiculo(new ServiceReferenceReservaVehiculos.ReservarVehiculoRequest()
                {
                    ApellidoNombreCliente = res.Cliente.Apellido + " " + res.Cliente.Nombre,

                    FechaHoraDevolucion = fechaDev,
                    FechaHoraRetiro =fechaRet,
                    IdVehiculoCiudad = res.IdVehiculoCiudad,
                    //LugarDevolucion =,
                    //LugarRetiro =,
                    NroDocumentoCliente = res.Cliente.NroDocumento,

                });

                res.CodigoReserva = reserva.Reserva.CodigoReserva;

                if (_db.Reserva == null || !_db.Reserva.Any())
                {
                    return NotFound();
                }
                if (res == null)
                {
                    return BadRequest();
                }

                _db.Reserva.Add(res);

                _db.SaveChanges();

               

                return Created("api/Reserva/" + res.Id, res);

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Put(int id, [FromBody]Reserva res)
        {
            try
            {
                if (res == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != res.Id)
                {
                    return BadRequest();
                }
                if (_db.Reserva.Count(e => e.Id == id) == 0)
                {
                    return NotFound();
                }
                _db.Entry(res).State = System.Data.Entity.EntityState.Modified;

                _db.SaveChanges();

                return Ok(res);


            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var res = _db.Reserva;
                if (res == null)
                {
                    return NotFound();
                }
                foreach (var r in res)
                {
                    if (r.Id  == id)
                    {
                        var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();
                        
                        _db.Reserva.Remove(r);

                        _db.SaveChanges();

                        var reserva = cliente.CancelarReserva(new ServiceReferenceReservaVehiculos.CancelarReservaRequest()
                        {
                            CodigoReserva = r.CodigoReserva 
                        });


                        return Ok(r);

                    }            
                }

                return Ok();



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
