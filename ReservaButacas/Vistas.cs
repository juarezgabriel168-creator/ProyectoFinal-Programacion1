// Vistas/Vistas.cs
using System;
using System.Collections.Generic;

namespace ReservaButacas
{
    static class Vistas
    {

        private static char LeerFila(string mensaje, Configuracion config)
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine().ToUpper();

                if (entrada.Length == 1 &&
                    ServicioButacas.ValidarFila(entrada[0], config))
                    return char.ToUpper(entrada[0]);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ Fila no válida. Ingrese entre " +
                                   $"{config.FilaMinima} y {config.FilaMaxima}.");
                Console.ResetColor();
            }
        }

        private static int LeerNumero(string mensaje, Configuracion config)
        {
            int numero;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out numero) &&
                    ServicioButacas.ValidarNumero(numero, config))
                    return numero;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ Número no válido. Ingrese entre " +
                                   $"1 y {config.AsientosPorFila}.");
                Console.ResetColor();
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

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ El nombre no puede estar vacío.");
                Console.ResetColor();
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

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ Ingrese S o N.");
                Console.ResetColor();
            }
        }

        private static string CentrarTexto(string texto, int ancho)
        {
            if (texto.Length >= ancho) return texto;
            int izq = (ancho - texto.Length) / 2;
            return texto.PadLeft(texto.Length + izq).PadRight(ancho);
        }

        public static void VistaReservar(List<Butaca> butacas,
                                         Configuracion config)
        {
            Console.Clear();
            Console.WriteLine("  ── RESERVAR ASIENTO ──────────────────");
            Console.WriteLine();

            MostrarZonas(config);
            Console.WriteLine();

            char fila = LeerFila("  Ingrese fila: ", config);
            int numero = LeerNumero("  Ingrese número de asiento: ", config);

            bool esVip = config.EsFilaVip(fila);
            double precio = esVip ? config.PrecioVip : config.PrecioNormal;

            Console.WriteLine();
            Console.WriteLine($"  Tipo de asiento : {(esVip ? "VIP" : "Normal")}");
            Console.WriteLine($"  Precio asignado : ${precio:F2}");
            Console.WriteLine();

            string nombre = LeerNombre("  Nombre del espectador: ");

            bool exito = ServicioButacas.Reservar(
                butacas, fila, numero, nombre, config);

            Console.WriteLine();
            if (exito)
            {
                ArchivoButacas.GuardarButacas(butacas);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✓ Reserva creada para {nombre} en " +
                                   $"{char.ToUpper(fila)}-{numero} " +
                                   $"({(esVip ? "VIP" : "Normal")}, ${precio:F2}).");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ El asiento {char.ToUpper(fila)}-{numero} " +
                                   $"ya está ocupado.");
            }
            Console.ResetColor();
        }

        public static void VistaReubicar(List<Butaca> butacas,
                                          Configuracion config)
        {
            Console.Clear();
            Console.WriteLine("  ── REUBICAR ESPECTADOR ───────────────");
            Console.WriteLine();

            MostrarListaReservas(butacas);

            if (butacas.Count == 0)
                return;

            Console.WriteLine("  Asiento actual:");
            char filaActual = LeerFila("  Fila actual: ", config);
            int numeroActual = LeerNumero("  Número actual: ", config);

            Butaca butaca = ServicioButacas.BuscarButaca(
                butacas, filaActual, numeroActual);

            if (butaca == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ No existe reserva en " +
                                   $"{char.ToUpper(filaActual)}-{numeroActual}.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"  Espectador: {butaca.NombreEspectador}");
            Console.WriteLine();

            MostrarZonas(config);
            Console.WriteLine();

            Console.WriteLine("  Nuevo asiento:");
            char filaNueva = LeerFila("  Nueva fila: ", config);
            int numeroNuevo = LeerNumero("  Nuevo número: ", config);

            bool nuevoEsVip = config.EsFilaVip(filaNueva);
            double nuevoPrecio = nuevoEsVip ? config.PrecioVip : config.PrecioNormal;

            Console.WriteLine();
            Console.WriteLine($"  Nuevo tipo   : {(nuevoEsVip ? "VIP" : "Normal")}");
            Console.WriteLine($"  Nuevo precio : ${nuevoPrecio:F2}");
            Console.WriteLine();

            bool exito = ServicioButacas.Reubicar(
                butacas, filaActual, numeroActual,
                filaNueva, numeroNuevo, config);

            if (exito)
            {
                ArchivoButacas.GuardarButacas(butacas);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✓ {butaca.NombreEspectador} reubicado a " +
                                   $"{char.ToUpper(filaNueva)}-{numeroNuevo} " +
                                   $"({(nuevoEsVip ? "VIP" : "Normal")}, ${nuevoPrecio:F2}).");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ El asiento {char.ToUpper(filaNueva)}-{numeroNuevo} " +
                                   $"ya está ocupado.");
            }
            Console.ResetColor();
        }

        public static void VistaCancelar(List<Butaca> butacas,
                                          Configuracion config)
        {
            Console.Clear();
            Console.WriteLine("  ── CANCELAR RESERVA ──────────────────");
            Console.WriteLine();

            MostrarListaReservas(butacas);

            if (butacas.Count == 0)
                return;

            char fila = LeerFila("  Fila del asiento a cancelar: ", config);
            int numero = LeerNumero("  Número del asiento a cancelar: ", config);

            Butaca butaca = ServicioButacas.BuscarButaca(butacas, fila, numero);

            if (butaca == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ No existe reserva en " +
                                   $"{char.ToUpper(fila)}-{numero}.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"  Espectador : {butaca.NombreEspectador}");
            Console.WriteLine($"  Asiento    : {butaca.Fila}-{butaca.Numero}");
            Console.WriteLine($"  Tipo       : {(butaca.EsVip ? "VIP" : "Normal")}");
            Console.WriteLine($"  Precio     : ${butaca.Precio:F2}");
            Console.WriteLine();

            bool confirmar = LeerConfirmacion(
                "  ¿Confirma la cancelación? (S/N): ");

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
                Console.WriteLine($"  ✓ Reserva de {butaca.NombreEspectador} " +
                                   $"cancelada correctamente.");
                Console.ResetColor();
            }
        }

        public static void VistaMapa(List<Butaca> butacas,
                                      Configuracion config)
        {
            Console.Clear();
            Console.WriteLine("  ── MAPA DE LA SALA ───────────────────");
            Console.WriteLine();

            char filaInicio = config.FilaMinima;
            char filaFin = config.FilaMaxima;
            int colInicio = 1;
            int colFin = config.AsientosPorFila;

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
            for (int col = colInicio; col <= colFin; col++)
                Console.Write($"  {col,2} ");
            Console.WriteLine();
            Console.WriteLine();

            for (char letraFila = filaInicio; letraFila <= filaFin; letraFila++)
            {
                bool filaEsVip = config.EsFilaVip(letraFila);
                string etiqueta = filaEsVip ? $"{letraFila}(V)" : $"{letraFila}    ";
                Console.Write($"  {etiqueta} ");

                for (int col = colInicio; col <= colFin; col++)
                {
                    Butaca butaca = ServicioButacas.BuscarButaca(
                        butacas, letraFila, col);

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

            int anchoMapa = 44;
            Console.WriteLine("  " + new string('─', anchoMapa));
            Console.WriteLine("  " + CentrarTexto("PANTALLA / ESCENARIO", anchoMapa));
            Console.WriteLine("  " + new string('─', anchoMapa));
            Console.WriteLine();

            int totalAsientos = config.CantidadFilas * config.AsientosPorFila;
            int ocupados = butacas.Count;
            int libres = totalAsientos - ocupados;
            int vipOcupados = 0;

            foreach (Butaca b in butacas)
                if (b.EsVip) vipOcupados++;

            Console.WriteLine($"  Total de asientos : {totalAsientos}");
            Console.WriteLine($"  Ocupados          : {ocupados}");
            Console.WriteLine($"  Libres            : {libres}");
            Console.WriteLine($"  VIP ocupados      : {vipOcupados}");
            Console.WriteLine();

            Console.WriteLine($"  Precio normal     : ${config.PrecioNormal:F2}");
            Console.WriteLine($"  Precio VIP        : ${config.PrecioVip:F2}");

            if (config.FilaInicioVip < config.CantidadFilas)
                Console.WriteLine($"  Zona VIP          : fila {config.FilaInicioVipChar} " +
                                   $"a {config.FilaMaxima}");
            else
                Console.WriteLine("  Zona VIP          : no configurada");
        }

        private static void MostrarListaReservas(List<Butaca> butacas)
        {
            if (butacas.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  No hay reservas registradas.");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("  Presione una tecla para volver...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"  Reservas activas ({butacas.Count}):");
            Console.WriteLine();
            Console.WriteLine("  ┌────────┬──────────────────────────────┬──────────┬──────────┐");
            Console.WriteLine("  │ Asiento│ Espectador                   │ Tipo     │ Precio   │");
            Console.WriteLine("  ├────────┼──────────────────────────────┼──────────┼──────────┤");

            foreach (Butaca b in butacas)
            {
                string asiento = $"{b.Fila}-{b.Numero}".PadRight(6);
                string nombre = b.NombreEspectador.Length > 28
                                    ? b.NombreEspectador.Substring(0, 28)
                                    : b.NombreEspectador.PadRight(28);
                string tipo = (b.EsVip ? "VIP" : "Normal").PadRight(8);
                string precio = $"${b.Precio:F2}".PadRight(8);

                Console.WriteLine($"  │ {asiento} │ {nombre} │ {tipo} │ {precio} │");
            }

            Console.WriteLine("  └────────┴──────────────────────────────┴──────────┴──────────┘");
            Console.WriteLine();
        }

        private static void MostrarZonas(Configuracion config)
        {
            Console.WriteLine("  Zonas de precio:");
            Console.ForegroundColor = ConsoleColor.White;

            if (config.FilaInicioVip >= config.CantidadFilas)
            {
                Console.WriteLine($"  • Normal (A-{config.FilaMaxima}): " +
                                   $"${config.PrecioNormal:F2}");
            }
            else
            {
                Console.WriteLine($"  • Normal  " +
                                   $"(A-{(char)('A' + config.FilaInicioVip - 1)})" +
                                   $": ${config.PrecioNormal:F2}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  • VIP     " +
                                   $"({config.FilaInicioVipChar}-{config.FilaMaxima})" +
                                   $": ${config.PrecioVip:F2}");
            }
            Console.ResetColor();
        }
    }
}