using System;
using dominio;

namespace negocio
{
    public class BilleteraNegocio
    {
        public int ObtenerIdBilleteraPorUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Asumimos que el usuario tiene una sola billetera por ahora
                datos.setearConsulta("SELECT TOP 1 IdBilletera FROM BILLETERAS WHERE IdUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IdBilletera"];
                }
                // Si no se encuentra, devolvemos 0 o lanzamos una excepción
                return 0;
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