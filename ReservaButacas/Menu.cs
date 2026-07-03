using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    static class Menu
    {
        public static void MostrarMenu(Configuracion config)
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   SISTEMA DE RESERVA DE BUTACAS      ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  1. Reservar asiento                 ║");
            Console.WriteLine("║  2. Ver mapa de la sala              ║");
            Console.WriteLine("║  3. Reubicar espectador              ║");
            Console.WriteLine("║  4. Cancelar reserva                 ║");
            Console.WriteLine("║  0. Salir                            ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine($"   Sala: {config.CantidadFilas} filas × " + $"{config.AsientosPorFila} asientos  |  " + $"Normal ${config.PrecioNormal:F2}  " + $"VIP ${config.PrecioVip:F2}");
            Console.Write("   Ingrese una opción: ");
        }

        public static int LeerOpcion()
        {
            int opcion;
            while (true)
            {
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out opcion) &&
                    opcion >= Constantes.OPCION_MIN &&
                    opcion <= Constantes.OPCION_MAX)
                    return opcion;

                Console.WriteLine($"  Opción no válida. Ingrese entre " + $"{Constantes.OPCION_MIN} y {Constantes.OPCION_MAX}.");
                Console.Write("  Ingrese una opción: ");
            }
        }

        public static void Ejecutar(List<Butaca> butacas, Configuracion config)
        {
            int opcion;

            do
            {
                Console.Clear();
                MostrarMenu(config);
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        Vistas.VistaReservar(butacas, config);
                        break;
                    case 2:
                        Vistas.VistaMapa(butacas, config);
                        break;
                    case 3:
                        Vistas.VistaReubicar(butacas, config);
                        break;
                    case 4:
                        Vistas.VistaCancelar(butacas, config);
                        break;
                    case 0:
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine();
                    Console.Write("  Presione una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }
    }
}
