using System;
using dominio;

namespace negocio
{
    public  class UsuarioNegocio
    {

        public int InsertarNuevo(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into USERS(email, pass) output inserted.Id values(@email, @pass)");
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);


                return datos.ejecutarAccionScalar();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool Loguear(Usuario usuario)
        {
                AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id from USUARIOS where usuario = @user AND pass = @pass");
                datos.setearParametro("user", usuario.User);
                datos.setearParametro("pass", usuario.Pass);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    // ver si acá agregamos llos otros parametros, y ver que pasa si son null
                    return true;
                }

                return false;

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
