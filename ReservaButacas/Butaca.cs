using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    class Butaca
    {
        public char Fila { get; set; }
        public int Numero { get; set; }
        public string NombreEspectador { get; set; }
        public double Precio { get; set; }
        public bool EsVip { get; set; }

        public Butaca(char fila, int numero, string nombreEspectador,
                      double precio, bool esVip)
        {
            Fila = char.ToUpper(fila);
            Numero = numero;
            NombreEspectador = nombreEspectador;
            Precio = precio;
            EsVip = esVip;
        }

        public override string ToString()
        {
            return $"{Fila},{Numero},{NombreEspectador}," +
                   $"{Precio.ToString("F2")},{EsVip.ToString().ToLower()}";
        }
    }
}
