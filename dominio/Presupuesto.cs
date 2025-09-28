using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Presupuesto
    {   public int IdPresupuesto { get; set; }
        public string Concepto { get; set; } 
        public decimal MontoEstimado { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int IdUsuario { get; set; }
    }
}
