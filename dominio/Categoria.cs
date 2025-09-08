using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string TipoMovimiento { get; set; } // Define si es para "Ingreso" o "Egreso"
        public int IdUsuario { get; set; }

    }
}
