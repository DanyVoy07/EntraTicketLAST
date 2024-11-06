using System.Data.SqlClient;
using System.Threading.Tasks;
using EntraTicket.Models;

namespace EntraTicket.Data
{
    public class EventRepository
    {
        private readonly string _connectionString;

        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddEventAsync(Event evt)
        {
            var query = "INSERT INTO eventos (nombre, descripcion, fecha, lugar, precio) VALUES (@Nombre, @Descripcion, @Fecha, @Lugar, @Precio)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", evt.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", evt.Descripcion);
                    command.Parameters.AddWithValue("@Fecha", evt.Fecha);
                    command.Parameters.AddWithValue("@Lugar", evt.Lugar);
                    command.Parameters.AddWithValue("@Precio", evt.Precio);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
