using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar(string tipoMovimiento, int idUsuario)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre FROM CATEGORIAS WHERE TipoMovimiento = @tipo AND IdUsuario = @idUsuario");
                datos.setearParametro("@tipo", tipoMovimiento);
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