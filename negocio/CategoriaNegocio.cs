using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar(int idUsuario)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre FROM CATEGORIAS WHERE IdUsuario = @idUsuario ORDER BY Nombre");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // Nuevo método sobrecargado para filtrar por tipo y usuario
        public List<Categoria> Listar(string tipo, int idUsuario)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Cambia 'Tipo' por 'TipoMovimiento' en la consulta SQL
                datos.setearConsulta("SELECT IdCategoria, Nombre FROM CATEGORIAS WHERE IdUsuario = @idUsuario AND TipoMovimiento = @tipo ORDER BY Nombre");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@tipo", tipo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}