using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    internal class Configuracion
    {
        public int CantidadFilas { get; set; } 
        public int AsientosPorFila { get; set; }
        public double PrecioNormal { get; set; }
        public double PrecioVip { get; set; } 
        public int FilaInicioVip { get; set; } 
        public char FilaMinima => 'A';

        public char FilaMaxima => (char)('A' + CantidadFilas - 1);

        public char FilaInicioVipChar => (char)('A' + FilaInicioVip);

        public Configuracion(int cantidadFilas, int asientosPorFila,
                             double precioNormal, double precioVip,
                             int filaInicioVip)
        {
            CantidadFilas = cantidadFilas;
            AsientosPorFila = asientosPorFila;
            PrecioNormal = precioNormal;
            PrecioVip = precioVip;
            FilaInicioVip = filaInicioVip;
        }

        public bool EsFilaVip(char fila)
        {
            return char.ToUpper(fila) >= FilaInicioVipChar;
        }

        public override string ToString()
        {
            return $"{CantidadFilas},{AsientosPorFila}," +
                   $"{PrecioNormal.ToString("F2")}," +
                   $"{PrecioVip.ToString("F2")}," +
                   $"{FilaInicioVip}";
        }
    }
}
