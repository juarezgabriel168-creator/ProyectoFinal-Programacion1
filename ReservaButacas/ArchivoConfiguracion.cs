using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    static class ArchivoConfiguracion
    {
        private const string RUTA = "configuracion.txt";

        public static Configuracion CargarConfiguracion()
        {
            if (!File.Exists(RUTA))
                return null;

            try
            {
                string linea = File.ReadAllText(RUTA).Trim();

                if (string.IsNullOrWhiteSpace(linea))
                    return null;

                string[] campos = linea.Split(',');

                if (campos.Length != 5)
                    return null;

                var inv = System.Globalization.CultureInfo.InvariantCulture;
                int cantFilas = int.Parse(campos[0]);
                int asientosPorFila = int.Parse(campos[1]);
                double precioNormal = double.Parse(campos[2], inv);
                double precioVip = double.Parse(campos[3], inv);
                int filaInicioVip = int.Parse(campos[4]);

                return new Configuracion(cantFilas, asientosPorFila,
                                         precioNormal, precioVip,
                                         filaInicioVip);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Error al cargar configuración: {ex.Message}");
                return null;
            }
        }

        public static void GuardarConfiguracion(Configuracion config)
        {
            try
            {
                File.WriteAllText(RUTA, config.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Error al guardar configuración: {ex.Message}");
            }
        }
    }
}
