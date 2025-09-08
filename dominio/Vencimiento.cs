using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Vencimiento
    {
        public int IdVencimiento { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; } // "Pendiente", "Pagado"
        public int IdUsuario { get; set; }
    }
}
