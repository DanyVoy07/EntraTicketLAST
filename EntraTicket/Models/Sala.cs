using System.Collections.Generic;

namespace EntraTicket.Models
{
    public class Sala
    {
        private Butaca[,] butacas;
        public int Filas { get; }
        public int Columnas { get; }

        public Sala(int filas, int columnas)
        {
            Filas = filas;
            Columnas = columnas;
            butacas = new Butaca[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    butacas[i, j] = new Butaca(i + 1, j + 1);
                }
            }
        }

        public List<Butaca> ObtenerButacas()
        {
            var listaButacas = new List<Butaca>();
            for (int i = 0; i < Filas; i++)
            {
                for (int j = 0; j < Columnas; j++)
                {
                    listaButacas.Add(butacas[i, j]);
                }
            }
            return listaButacas;
        }

        public bool SeleccionarButaca(int fila, int columna)
        {
            if (fila <= 0 || fila > Filas || columna <= 0 || columna > Columnas)
            {
                return false; // La butaca no existe
            }

            if (butacas[fila - 1, columna - 1].EstaReservada)
            {
                return false; // La butaca ya está reservada
            }

            butacas[fila - 1, columna - 1].EstaReservada = true;
            return true;
        }
    }
}
