using System.Collections.Generic;
using System.Linq;
using EntraTicket.Models; // Asegúrate de tener esta línea para usar la clase Event

namespace EntraTicket.Services
{
    public class EventService
    {
        // Lista en memoria para almacenar los eventos
        private List<Event> eventos;

        // Constructor que inicializa la lista de eventos con datos de ejemplo
        public EventService()
        {
            // Inicializa la lista de eventos con los datos de ejemplo de la clase Event
            eventos = Event.ObtenerEventosDeEjemplo();
        }

        // Método para obtener todos los eventos
        public List<Event> ObtenerTodos()
        {
            return eventos;
        }
        // Método para obtener un evento por su ID
        public Event ObtenerPorId(int id)
        {
            return eventos.FirstOrDefault(e => e.ID == id);
        }

        // Método para agregar un nuevo evento
        public void Agregar(Event nuevoEvento)
        {
            // Asigna un nuevo ID al evento basado en el último ID disponible
            nuevoEvento.ID = eventos.Any() ? eventos.Max(e => e.ID) + 1 : 1;
            eventos.Add(nuevoEvento);
        }

        // Método para actualizar un evento existente sin usar el ID directamente
        public bool Actualizar(Event eventoActualizado)
        {
            var evento = eventos.FirstOrDefault(e =>
                e.Nombre == eventoActualizado.Nombre &&
                e.Fecha == eventoActualizado.Fecha &&
                e.Lugar == eventoActualizado.Lugar);

            if (evento == null) return false;

            // Actualiza los campos del evento
            evento.Descripcion = eventoActualizado.Descripcion;
            evento.Precio = eventoActualizado.Precio;

            return true;
        }

        // Método para eliminar un evento sin usar el ID directamente
        public bool Eliminar(Event eventoAEliminar)
        {
            var evento = eventos.FirstOrDefault(e =>
                e.Nombre == eventoAEliminar.Nombre &&
                e.Fecha == eventoAEliminar.Fecha &&
                e.Lugar == eventoAEliminar.Lugar);

            if (evento == null) return false;

            // Remueve el evento de la lista
            eventos.Remove(evento);
            return true;
        }

    }
}