using System.Collections.Generic;

namespace EntraTicket.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public string Fecha { get; set; }
        public decimal Precio { get; set; }

        // Lista de ejemplo de eventos
        public static List<Event> ObtenerEventosDeEjemplo()
        {
            return new List<Event>
            {
                new Event
                {
                    ID = 1,
                    Nombre = "Concierto de Rock",
                    Descripcion = "Concierto de bandas locales",
                    Lugar = "Teatro Central",
                    Fecha = "2024-11-10",
                    Precio = 400
                },
                new Event
                {
                    ID = 2,
                    Nombre = "Feria de Libros",
                    Descripcion = "Feria anual de libros",
                    Lugar = "Plaza Principal",
                    Fecha = "2024-12-01",
                    Precio = 500
                },
                new Event
                {
                    ID = 3,
                    Nombre = "Obra de Teatro",
                    Descripcion = "Obra clásica de Shakespeare",
                    Lugar = "Auditorio Nacional",
                    Fecha = "2024-12-20",
                    Precio = 700
                }
            };
        }
    }
}
