namespace EntraTicket.Models
{
    public class MetodoDePago
    {
        public int MetodoID { get; set; }  // Se mapea al campo 'id'
        public string NombreMetodo { get; set; }  // Se mapea al campo 'metodo_de_pago'
        public string Descripcion { get; set; }  // Se mapea al campo 'descripcion'

        // Método estático para obtener una lista de métodos de pago de ejemplo
        public static List<MetodoDePago> ObtenerMetodosDePago()
        {
            return new List<MetodoDePago>
        {
            new MetodoDePago { MetodoID = 1, NombreMetodo = "Tarjeta de Crédito", Descripcion = "Pago mediante tarjeta de crédito" },
            new MetodoDePago { MetodoID = 2, NombreMetodo = "Transferencia Bancaria", Descripcion = "Pago mediante transferencia bancaria" },
            new MetodoDePago { MetodoID = 3, NombreMetodo = "Efectivo", Descripcion = "Pago en efectivo" }
            // Puedes agregar más métodos aquí
        };
        }
    }
}