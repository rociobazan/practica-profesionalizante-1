using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class ObjetivoNegocio
    {
        public List<Objetivo> Listar(int idUsuario)
        {
            // Lógica similar a CategoriaNegocio para traer los objetivos del usuario
            // SELECT IdObjetivo, Nombre FROM OBJETIVOS WHERE IdUsuario = @idUsuario
            // ...
            // Devolver una List<Objetivo>
            return new List<Objetivo>(); // Implementar la lógica de DB aquí
        }
    }
}
