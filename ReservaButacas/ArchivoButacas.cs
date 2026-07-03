using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ReservaButacas
{
    static class ArchivoButacas
    {
        private const string RUTA = "butacas.txt";

        public static List<Butaca> CargarButacas()
        {
            List<Butaca> butacas = new List<Butaca>();

            if (!File.Exists(RUTA))
            {
                File.WriteAllText(RUTA, string.Empty);
                return butacas;
            }

            try
            {
                string[] lineas = File.ReadAllLines(RUTA);

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] campos = linea.Split(',');

                    if (campos.Length != 5)
                        continue;

                    char fila = char.Parse(campos[0]);
                    int numero = int.Parse(campos[1]);
                    string nombre = campos[2];
                    double precio = double.Parse(campos[3], System.Globalization.CultureInfo.InvariantCulture);
                    bool esVip = bool.Parse(campos[4].Trim().ToLower());

                    butacas.Add(new Butaca(fila, numero, nombre, precio, esVip));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
            }

            return butacas;
        }

        public static void GuardarButacas(List<Butaca> butacas)
        {
            try
            {
                List<string> lineas = new List<string>();

                foreach (Butaca b in butacas)
                    lineas.Add(b.ToString());

                File.WriteAllLines(RUTA, lineas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }
    }
}
