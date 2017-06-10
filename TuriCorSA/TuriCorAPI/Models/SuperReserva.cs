using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuriCorAPI.Models
{
    public class SuperReserva
    {

        public string CodigoReserva { get; set; }
        public string FechaReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdVehiculoCiudad { get; set; }
        public int IdCiudad { get; set; }
        public int IdPais { get; set; }
        public string ApellidoNombreCliente { get; set; }
        public System.DateTime FechaHoraDevolucion { get; set; }
        public System.DateTime FechaHoraRetiro { get; set; }
        public string LugarDevolucion { get; set; }
        public string LugarRetiro { get; set; }
        public string NroDocumentoCliente { get; set; }
        
    }
}