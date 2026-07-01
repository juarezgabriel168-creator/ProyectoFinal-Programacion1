// Vistas/Vistas.cs
using System;
using System.Collections.Generic;

namespace ReservaButacas
{
    static class Vistas
    {
        private static char LeerFila(string mensaje)
        {
            char fila;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine().ToUpper();

                if (entrada.Length == 1 && ServicioButacas.ValidarFila(entrada[0]))
                    return char.ToUpper(entrada[0]);

                Console.WriteLine("  Fila no válida. Ingrese una letra entre A y Z.");
            }
        }

        private static int LeerNumero(string mensaje)
        {
            int numero;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out numero) &&
                    ServicioButacas.ValidarNumero(numero))
                    return numero;

                Console.WriteLine("  Número no válido. Ingrese un número positivo.");
            }
        }

        private static string LeerNombre(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string nombre = Console.ReadLine();

                if (ServicioButacas.ValidarNombre(nombre))
                    return nombre;

                Console.WriteLine("  El nombre no puede estar vacío.");
            }
        }

        private static double LeerPrecio(string mensaje)
        {
            double precio;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (double.TryParse(entrada, out precio) &&
                    ServicioButacas.ValidarPrecio(precio))
                    return precio;

                Console.WriteLine("  Precio no válido. Ingrese un valor mayor a cero.");
            }
        }

        private static bool LeerConfirmacion(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine().ToUpper().Trim();

                if (entrada == "S") return true;
                if (entrada == "N") return false;

                Console.WriteLine("  Ingrese S para sí o N para no.");
            }
        }

        public static void VistaReservar(List<Butaca> butacas)
        {
            Console.Clear();
            Console.WriteLine("  ── RESERVAR ASIENTO ──────────────────");
            Console.WriteLine();

            char fila = LeerFila("  Ingrese fila (A-Z): ");
            int numero = LeerNumero("  Ingrese número de asiento: ");
            string nombre = LeerNombre("  Ingrese nombre del espectador: ");
            double precio = LeerPrecio("  Ingrese precio: $");
            bool esVip = LeerConfirmacion("  ¿Es asiento VIP? (S/N): ");

            bool exito = ServicioButacas.Reservar(butacas, fila, numero,
                                                   nombre, precio, esVip);
            if (exito)
            {
                ArchivoButacas.GuardarButacas(butacas);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✓ Reserva creada para {nombre} en {char.ToUpper(fila)}-{numero}.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ El asiento {char.ToUpper(fila)}-{numero} ya está ocupado.");
                Console.ResetColor();
            }
        }

        public static void VistaReubicar(List<Butaca> butacas)
        {
            Console.Clear();
            Console.WriteLine("  ── REUBICAR ESPECTADOR ───────────────");
            Console.WriteLine();

            Console.WriteLine("  Asiento actual:");
            char filaActual = LeerFila("  Ingrese fila actual (A-Z): ");
            int numeroActual = LeerNumero("  Ingrese número actual: ");

            Butaca butaca = ServicioButacas.BuscarButaca(butacas,
                                                          filaActual, numeroActual);
            if (butaca == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ No existe reserva en {char.ToUpper(filaActual)}-{numeroActual}.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"  Espectador encontrado: {butaca.NombreEspectador}");
            Console.WriteLine();
            Console.WriteLine("  Nuevo asiento:");
            char filaNueva = LeerFila("  Ingrese nueva fila (A-Z): ");
            int numeroNuevo = LeerNumero("  Ingrese nuevo número: ");

            bool exito = ServicioButacas.Reubicar(butacas, filaActual, numeroActual,
                                                   filaNueva, numeroNuevo);
            if (exito)
            {
                ArchivoButacas.GuardarButacas(butacas);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✓ {butaca.NombreEspectador} reubicado a {char.ToUpper(filaNueva)}-{numeroNuevo}.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ El asiento {char.ToUpper(filaNueva)}-{numeroNuevo} ya está ocupado.");
                Console.ResetColor();
            }
        }

        public static void VistaCancelar(List<Butaca> butacas)
        {
            Console.Clear();
            Console.WriteLine("  ── CANCELAR RESERVA ──────────────────");
            Console.WriteLine();

            char fila = LeerFila("  Ingrese fila (A-Z): ");
            int numero = LeerNumero("  Ingrese número de asiento: ");

            Butaca butaca = ServicioButacas.BuscarButaca(butacas, fila, numero);

            if (butaca == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ No existe reserva en {char.ToUpper(fila)}-{numero}.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"  Espectador: {butaca.NombreEspectador}");
            Console.WriteLine($"  Asiento:    {butaca.Fila}-{butaca.Numero}");
            Console.WriteLine($"  Precio:     ${butaca.Precio:F2}");
            Console.WriteLine($"  VIP:        {(butaca.EsVip ? "Sí" : "No")}");
            Console.WriteLine();

            bool confirmar = LeerConfirmacion("  ¿Confirma la cancelación? (S/N): ");

            if (!confirmar)
            {
                Console.WriteLine();
                Console.WriteLine("  Operación cancelada.");
                return;
            }

            bool exito = ServicioButacas.CancelarReserva(butacas, fila, numero);

            if (exito)
            {
                ArchivoButacas.GuardarButacas(butacas);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✓ Reserva de {butaca.NombreEspectador} cancelada correctamente.");
                Console.ResetColor();
            }
        }

        public static void VistaMapa(List<Butaca> butacas)
        {
            Console.Clear();
            Console.WriteLine("  ── MAPA DE LA SALA ───────────────────");
            Console.WriteLine();

            const int FILAS = 6;
            const int COLUMNAS = 10;

            Console.Write("  Leyenda:  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[ ] Libre  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[X] Ocupado  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[V] VIP");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("       ");
            for (int col = 1; col <= COLUMNAS; col++)
                Console.Write($"  {col,2} ");
            Console.WriteLine();
            Console.WriteLine();

            for (int f = 0; f < FILAS; f++)
            {
                char letraFila = (char)('A' + f);

                Console.Write($"   {letraFila}   ");

                for (int col = 1; col <= COLUMNAS; col++)
                {
                    Butaca butaca = ServicioButacas.BuscarButaca(butacas,
                                                                  letraFila, col);
                    if (butaca == null)
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[ ] ");
                    }
                    else if (butaca.EsVip)
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[V] ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[X] ");
                    }

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("  " + new string('─', 44));
            Console.WriteLine("  " + CentrarTexto("PANTALLA / ESCENARIO", 44));
            Console.WriteLine("  " + new string('─', 44));
            Console.WriteLine();

            int totalAsientos = FILAS * COLUMNAS;
            int ocupados = butacas.Count;
            int libres = totalAsientos - ocupados;
            int vip = 0;

            foreach (Butaca b in butacas)
                if (b.EsVip) vip++;

            Console.WriteLine($"  Total de asientos : {totalAsientos}");
            Console.WriteLine($"  Ocupados          : {ocupados}");
            Console.WriteLine($"  Libres            : {libres}");
            Console.WriteLine($"  VIP ocupados      : {vip}");
        }

        private static string CentrarTexto(string texto, int ancho)
        {
            if (texto.Length >= ancho)
                return texto;

            int espaciosIzq = (ancho - texto.Length) / 2;
            return texto.PadLeft(texto.Length + espaciosIzq).PadRight(ancho);
        }
    }
}