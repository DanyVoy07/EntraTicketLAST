namespace EntraTicket.Models
{
    public class Butaca
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool EstaReservada { get; set; }

        public Butaca(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
            EstaReservada = false;
        }
    }
}