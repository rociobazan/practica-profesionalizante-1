using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
