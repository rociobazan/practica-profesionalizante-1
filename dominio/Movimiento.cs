using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Movimiento
    {
        public string Nombre { get; set; }
        public int IdMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string UrlImagen { get; set; }

        // --- RELACIONES CORREGIDAS ---
        // Ahora usamos los IDs directamente, que es lo que el resto del código necesita.
        public int IdBilletera { get; set; }
        public int IdCategoria { get; set; }
        public int? IdObjetivo { get; set; } // int? permite que el valor sea nulo (null)
    }
}