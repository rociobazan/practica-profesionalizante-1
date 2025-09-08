using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Objetivo
    {
        public int IdObjetivo { get; set; }
        public string Nombre { get; set; }
        public decimal MontoObjetivo { get; set; }
        public decimal MontoActual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } // "Activo", "Completado"
        public int IdUsuario { get; set; }
    }
}
