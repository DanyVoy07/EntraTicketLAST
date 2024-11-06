namespace EntraTicket.Models
{
    public class logeo
    {
        public string idusuario { get; set; }
        public string nombre { get; set; }
        public string contraseña { get; set; }

        public static List<logeo> DB()
        {
            var list = new List<logeo>()
            {
                new logeo
                {
                    idusuario = "1",
                    nombre = "Cuchu",
                    contraseña = "123"
                },
                new logeo
                {
                    idusuario = "2",
                    nombre = "Macarron",
                    contraseña = "321"
                },
                new logeo
                {
                    idusuario = "3",
                    nombre = "Puan",
                    contraseña = "666"
                }
            };

            return list;
        }
    }
}
