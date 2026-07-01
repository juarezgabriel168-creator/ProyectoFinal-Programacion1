using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    static class Menu
    {
        public static void MostrarMenu()
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
            Console.Write("   Ingrese una opción: ");
        }

        public static int LeerOpcion()
        {
            int opcion;

            while (true)
            {
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out opcion) && opcion >= 0 && opcion <= 4)
                    return opcion;

                Console.WriteLine("  Opción no válida. Ingrese un número entre 0 y 4.");
                Console.Write("  Ingrese una opción: ");
            }
        }
       
        public static void Ejecutar(List<Butaca> butacas)
        {
            int opcion;

            do
            {
                Console.Clear();
                MostrarMenu();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        Vistas.VistaReservar(butacas);
                        break;
                    case 2:
                        Vistas.VistaMapa(butacas);
                        break;
                    case 3:
                        Vistas.VistaReubicar(butacas);
                        break;
                    case 4:
                        Vistas.VistaCancelar(butacas);
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
