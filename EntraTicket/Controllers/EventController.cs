using Microsoft.AspNetCore.Mvc;
using EntraTicket.Models;
using EntraTicket.Services;

namespace EntraTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController()
        {
            _eventService = new EventService();
        }

        // GET: api/event
        [HttpGet]
        public ActionResult<IEnumerable<Event>> Get()
        {
            var eventos = _eventService.ObtenerTodos();
            return Ok(eventos);
        }

        // GET: api/event/5
        [HttpGet("{id}")]
        public ActionResult<Event> Get(int id)
        {
            var evento = _eventService.ObtenerPorId(id);
            if (evento == null) return NotFound();
            return Ok(evento);
        }

        // POST: api/event
        [HttpPost]
        public ActionResult Post([FromBody] Event nuevoEvento)
        {
            _eventService.Agregar(nuevoEvento);
            return CreatedAtAction(nameof(Get), new { id = nuevoEvento.ID }, nuevoEvento);
        }

        // PUT: api/event
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Event eventoActualizado)
        {
            eventoActualizado.ID = id;
            if (!_eventService.Actualizar(eventoActualizado)) return NotFound();
            return NoContent();
        }


        // DELETE: api/event
        [HttpDelete]
        public ActionResult Delete([FromBody] Event eventoAEliminar)
        {
            if (!_eventService.Eliminar(eventoAEliminar)) return NotFound();
            return NoContent();
        }




    }
}