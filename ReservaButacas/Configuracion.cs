using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaButacas
{
    class Configuracion
    {
        // ── Propiedades ──────────────────────────────────────────
        public int CantidadFilas { get; set; }  // ej: 6 → filas A a F
        public int AsientosPorFila { get; set; }  // ej: 10
        public double PrecioNormal { get; set; }  // precio ticket normal
        public double PrecioVip { get; set; }  // precio ticket VIP
        public int FilaInicioVip { get; set; }  // índice (0-based) desde donde empieza VIP
                                                // ej: 3 → filas D,E,F son VIP

        // ── Propiedades calculadas ───────────────────────────────

        // Letra de la primera fila de la sala (siempre 'A')
        public char FilaMinima => 'A';

        // Letra de la última fila de la sala según CantidadFilas
        public char FilaMaxima => (char)('A' + CantidadFilas - 1);

        // Letra desde donde empiezan los asientos VIP
        public char FilaInicioVipChar => (char)('A' + FilaInicioVip);

        // ── Constructor ──────────────────────────────────────────
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

        // ── Determina si una fila es VIP ─────────────────────────
        public bool EsFilaVip(char fila)
        {
            return char.ToUpper(fila) >= FilaInicioVipChar;
        }

        // ── ToString en formato CSV ──────────────────────────────
        // Ejemplo: 6,10,1500.00,2500.00,3
        // InvariantCulture garantiza punto decimal en cualquier sistema.
        public override string ToString()
        {
            var inv = System.Globalization.CultureInfo.InvariantCulture;
            return $"{CantidadFilas},{AsientosPorFila}," +
                   $"{PrecioNormal.ToString("F2", inv)}," +
                   $"{PrecioVip.ToString("F2", inv)}," +
                   $"{FilaInicioVip}";
        }
    }
}
