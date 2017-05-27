using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace TuriCorAPI.Controllers
{
    [EnableCors(origins: "http://localhost:2253", headers: "*", methods: "*")]
    public class VehiculoController : ApiController
    {
    }
}
