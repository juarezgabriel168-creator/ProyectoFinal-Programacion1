using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Sistema de Reserva de Butacas";

            Configuracion config = VistaConfiguracion.ObtenerConfiguracion();

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════╗");
            Console.WriteLine("  ║   SISTEMA DE RESERVA DE BUTACAS      ║");
            Console.WriteLine("  ║   Iniciando sistema...               ║");
            Console.WriteLine("  ╚══════════════════════════════════════╝");
            Console.WriteLine();

            bool archivoExistia = System.IO.File.Exists("butacas.txt");

            List<Butaca> butacas = ArchivoButacas.CargarButacas();

            Console.ForegroundColor = ConsoleColor.Green;
            if (archivoExistia)
            {
                Console.WriteLine($"  ✓ Archivo butacas.txt encontrado.");
                Console.WriteLine($"  ✓ {butacas.Count} reserva(s) cargada(s) correctamente.");
            }
            else
            {
                Console.WriteLine("  ✓ Archivo butacas.txt creado. No hay reservas previas.");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("  Presione una tecla para continuar...");
            Console.ReadKey();

            Menu.Ejecutar(butacas, config);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════╗");
            Console.WriteLine("  ║   Gracias por usar el sistema.       ║");
            Console.WriteLine("  ║   ¡Hasta pronto!                     ║");
            Console.WriteLine("  ╚══════════════════════════════════════╝");
            Console.WriteLine();
        }
    }
}
