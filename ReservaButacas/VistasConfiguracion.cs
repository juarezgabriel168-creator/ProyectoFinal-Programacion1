using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    static class VistaConfiguracion
    {
        public static Configuracion ObtenerConfiguracion()
        {
            Configuracion existente = ArchivoConfiguracion.CargarConfiguracion();

            if (existente != null)
                return existente;

            Console.Clear();
            MostrarEncabezado();
            Console.WriteLine("  Primera ejecución. Configure el sistema antes de continuar.");
            Console.WriteLine();

            return PedirConfiguracion();
        }

        private static void MostrarEncabezado()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║     CONFIGURACIÓN DEL SISTEMA        ║");
            Console.WriteLine("║     Acceso exclusivo del propietario ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine();
        }

        private static void MostrarConfiguracionActual(Configuracion config)
        {
            Console.WriteLine("  Configuración actual:");
            Console.WriteLine($"  ┌─ Filas de la sala   : {config.CantidadFilas} ({config.FilaMinima} a {config.FilaMaxima})");
            Console.WriteLine($"  ├─ Asientos por fila  : {config.AsientosPorFila}");
            Console.WriteLine($"  ├─ Precio normal      : ${config.PrecioNormal:F2}");
            Console.WriteLine($"  ├─ Precio VIP         : ${config.PrecioVip:F2}");
            Console.WriteLine($"  └─ Filas VIP          : {config.FilaInicioVipChar} a {config.FilaMaxima}");
        }

        private static Configuracion PedirConfiguracion()
        {
            Console.WriteLine("  Complete la nueva configuración:");
            Console.WriteLine();

            int cantFilas = LeerEntero(
                $"  Cantidad de filas ({Constantes.FILAS_MINIMAS}-{Constantes.FILAS_MAXIMAS}): ",
                Constantes.FILAS_MINIMAS,
                Constantes.FILAS_MAXIMAS
            );

            char filaMax = (char)('A' + cantFilas - 1);
            Console.WriteLine($"  → La sala tendrá filas de A a {filaMax}.");
            Console.WriteLine();

            int asientosPorFila = LeerEntero(
                $"  Asientos por fila ({Constantes.ASIENTOS_MINIMOS}-{Constantes.ASIENTOS_MAXIMOS}): ",
                Constantes.ASIENTOS_MINIMOS,
                Constantes.ASIENTOS_MAXIMOS
            );

            Console.WriteLine($"  → Total de asientos: {cantFilas * asientosPorFila}.");
            Console.WriteLine();

            double precioNormal = LeerPrecio("  Precio ticket normal: $");
            Console.WriteLine();

            double precioVip = LeerPrecioVip("  Precio ticket VIP:    $", precioNormal);
            Console.WriteLine();

            int filaInicioVip;
            if (cantFilas == 1)
            {
                filaInicioVip = cantFilas; 
                Console.WriteLine("  → Con una sola fila, todos los asientos serán normales.");
            }
            else
            {
                filaInicioVip = LeerFilaInicioVip(cantFilas);
                char filaVipChar = (char)('A' + filaInicioVip);
                Console.WriteLine($"  → Filas normales: A a {(char)('A' + filaInicioVip - 1)}.");
                Console.WriteLine($"  → Filas VIP:      {filaVipChar} a {filaMax}.");
            }

            Console.WriteLine();

            Configuracion config = new Configuracion(cantFilas, asientosPorFila, precioNormal, precioVip, filaInicioVip);

            MostrarResumen(config);

            Console.Write("  ¿Confirmar esta configuración? (S/N): ");
            string conf = Console.ReadLine().Trim().ToUpper();

            if (conf != "S")
            {
                Console.WriteLine();
                Console.WriteLine("  Configuración cancelada. Reintentando...");
                Console.WriteLine();
                Console.Write("  Presione una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                MostrarEncabezado();
                return PedirConfiguracion();
            }

            ArchivoConfiguracion.GuardarConfiguracion(config);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ✓ Configuración guardada correctamente.");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("  Presione una tecla para ingresar al sistema...");
            Console.ReadKey();

            return config;
        }

        private static void MostrarResumen(Configuracion config)
        {
            Console.WriteLine("  ┌─────────────────────────────────────┐");
            Console.WriteLine("  │         RESUMEN DE CONFIGURACIÓN    │");
            Console.WriteLine("  ├─────────────────────────────────────┤");
            Console.WriteLine($"  │  Filas          : {config.CantidadFilas} ({config.FilaMinima}-{config.FilaMaxima})".PadRight(42) + "│");
            Console.WriteLine($"  │  Asientos/fila  : {config.AsientosPorFila}".PadRight(42) + "│");
            Console.WriteLine($"  │  Total asientos : {config.CantidadFilas * config.AsientosPorFila}".PadRight(42) + "│");
            Console.WriteLine($"  │  Precio normal  : ${config.PrecioNormal:F2}".PadRight(42) + "│");
            Console.WriteLine($"  │  Precio VIP     : ${config.PrecioVip:F2}".PadRight(42) + "│");

            if (config.FilaInicioVip >= config.CantidadFilas)
                Console.WriteLine("  │  Zona VIP       : ninguna (todos normal)".PadRight(42) + "│");
            else
                Console.WriteLine($"  │  Zona VIP       : {config.FilaInicioVipChar}-{config.FilaMaxima}".PadRight(42) + "│");

            Console.WriteLine("  └─────────────────────────────────────┘");
            Console.WriteLine();
        }



        private static int LeerEntero(string mensaje, int min, int max)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out valor) && valor >= min && valor <= max)
                    return valor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ Ingrese un número entre {min} y {max}.");
                Console.ResetColor();
            }
        }

        private static bool TryParsePrecio(string entrada, out double valor)
        {
            string normalizada = entrada.Trim().Replace(',', '.');
            return double.TryParse(normalizada, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valor);
        }

        private static double LeerPrecio(string mensaje)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (TryParsePrecio(entrada, out valor) && valor >= Constantes.PRECIO_MINIMO)
                    return valor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ Ingrese un precio mayor a cero.");
                Console.ResetColor();
            }
        }

        private static double LeerPrecioVip(string mensaje, double precioNormal)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (TryParsePrecio(entrada, out valor) && valor >= Constantes.PRECIO_MINIMO)
                {
                    if (valor < precioNormal)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  ⚠ El precio VIP (${valor:F2}) es menor al normal (${precioNormal:F2}).");
                        Console.Write("  ¿Continuar de todas formas? (S/N): ");
                        Console.ResetColor();
                        string resp = Console.ReadLine().Trim().ToUpper();
                        if (resp == "S") return valor;
                        continue;
                    }
                    return valor;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ Ingrese un precio mayor a cero.");
                Console.ResetColor();
            }
        }

        private static int LeerFilaInicioVip(int cantFilas)
        {

            char filaMax = (char)('A' + cantFilas - 1);

            Console.WriteLine($"  Las filas disponibles son A a {filaMax}.");
            Console.WriteLine("  Indique desde qué fila comienzan los asientos VIP.");
            Console.WriteLine("  (La fila A no puede ser VIP — debe haber al menos una fila normal.)");

            while (true)
            {
                Console.Write($"  Fila inicio VIP (B a {filaMax}): ");
                string entrada = Console.ReadLine().Trim().ToUpper();

                if (entrada.Length == 1)
                {
                    char filaChar = entrada[0];
                    int indice = filaChar - 'A';

                    if (indice >= 1 && indice < cantFilas)
                        return indice;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ Ingrese una letra entre B y {filaMax}.");
                Console.ResetColor();
            }
        }
    }
}
