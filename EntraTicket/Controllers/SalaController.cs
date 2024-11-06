using Microsoft.AspNetCore.Mvc;
using EntraTicket.Models;
using System.Collections.Generic;

namespace EntraTicket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private static readonly Sala sala = new Sala(5, 5); // Sala de ejemplo de 5x5

        [HttpGet("butacas")]
        public ActionResult<List<Butaca>> GetButacas()
        {
            return sala.ObtenerButacas();
        }

        [HttpPost("reservar")]
        public ActionResult ReservarButaca([FromBody] ButacaRequest request)
        {
            if (sala.SeleccionarButaca(request.Fila, request.Columna))
            {
                return Ok("Butaca reservada exitosamente.");
            }
            else
            {
                return BadRequest("La butaca no está disponible o es inválida.");
            }
        }
    }

    public class ButacaRequest
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
    }
}
