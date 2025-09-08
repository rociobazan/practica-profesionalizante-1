using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Movimiento
    {
        public int IdMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; } // "Ingreso" o "Egreso"
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string UrlImagen { get; set; } // Opcional, para comprobantes

        // Relaciones importantes
        public int IdBilletera { get; set; }
        public Categoria Categoria { get; set; } // Objeto Categoria anidado
        public Objetivo Objetivo { get; set; }   // Objeto Objetivo anidado (puede ser null)
    }
}
