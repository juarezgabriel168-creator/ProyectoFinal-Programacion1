using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    static class ServicioButacas
    {
        public static bool ValidarFila(char fila)
        {
            return char.ToUpper(fila) >= 'A' && char.ToUpper(fila) <= 'Z';
        }

        public static bool ValidarNumero(int numero)
        {
            return numero > 0;
        }

        public static bool ValidarNombre(string nombre)
        {
            return !string.IsNullOrWhiteSpace(nombre);
        }

        public static bool ValidarPrecio(double precio)
        {
            return precio > 0;
        }

        public static bool ExisteDuplicado(List<Butaca> butacas,
                                           char fila, int numero)
        {
            char filaNormalizada = char.ToUpper(fila);
            foreach (Butaca b in butacas)
            {
                if (b.Fila == filaNormalizada && b.Numero == numero)
                    return true;
            }
            return false;
        }

        public static Butaca BuscarButaca(List<Butaca> butacas,
                                          char fila, int numero)
        {
            char filaNormalizada = char.ToUpper(fila);
            foreach (Butaca b in butacas)
            {
                if (b.Fila == filaNormalizada && b.Numero == numero)
                    return b;
            }
            return null;
        }

        public static bool Reservar(List<Butaca> butacas, char fila,
                                    int numero, string nombre,
                                    double precio, bool esVip)
        {
            if (ExisteDuplicado(butacas, fila, numero))
                return false;

            butacas.Add(new Butaca(fila, numero, nombre, precio, esVip));
            return true;
        }

        public static bool Reubicar(List<Butaca> butacas,
                                    char filaActual, int numeroActual,
                                    char filaNueva, int numeroNuevo)
        {
            Butaca butaca = BuscarButaca(butacas, filaActual, numeroActual);

            if (butaca == null)
                return false;

            bool mismoAsiento = char.ToUpper(filaActual) == char.ToUpper(filaNueva)
                                && numeroActual == numeroNuevo;

            if (!mismoAsiento && ExisteDuplicado(butacas, filaNueva, numeroNuevo))
                return false;

            butaca.Fila = char.ToUpper(filaNueva);
            butaca.Numero = numeroNuevo;
            return true;
        }

        public static bool CancelarReserva(List<Butaca> butacas,
                                           char fila, int numero)
        {
            Butaca butaca = BuscarButaca(butacas, fila, numero);

            if (butaca == null)
                return false;

            butacas.Remove(butaca);
            return true;
        }
    }
}
