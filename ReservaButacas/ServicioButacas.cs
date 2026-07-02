using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    static class ServicioButacas
    {
        public static bool ValidarFila(char fila, Configuracion config)
        {
            char filaNormalizada = char.ToUpper(fila);
            return filaNormalizada >= config.FilaMinima &&
                   filaNormalizada <= config.FilaMaxima;
        }

        public static bool ValidarNumero(int numero, Configuracion config)
        {
            return numero >= 1 && numero <= config.AsientosPorFila;
        }

        public static bool ValidarNombre(string nombre)
        {
            return !string.IsNullOrWhiteSpace(nombre);
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

        public static bool Reservar(List<Butaca> butacas,
                                    char fila, int numero,
                                    string nombre,
                                    Configuracion config)
        {
            if (ExisteDuplicado(butacas, fila, numero))
                return false;

            bool esVip = config.EsFilaVip(fila);
            double precio = esVip ? config.PrecioVip : config.PrecioNormal;

            butacas.Add(new Butaca(fila, numero, nombre, precio, esVip));
            return true;
        }

        public static bool Reubicar(List<Butaca> butacas,
                                    char filaActual, int numeroActual,
                                    char filaNueva, int numeroNuevo,
                                    Configuracion config)
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
            butaca.EsVip = config.EsFilaVip(filaNueva);
            butaca.Precio = butaca.EsVip ? config.PrecioVip : config.PrecioNormal;

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
