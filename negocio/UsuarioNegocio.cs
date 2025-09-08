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
                // CORRECCIÓN: Usamos 'email' y 'pass' que son más estándar y coinciden con tu referencia.
                datos.setearConsulta("Select IdUsuario, Email, Nombre, Apellido from USUARIOS where Email = @email AND Pass = @pass");

                // CORRECIÓN: Pasamos la propiedad Email del objeto, no 'User'.
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@pass", usuario.Pass);
                datos.ejecutarLectura();

                // CORRECCIÓN: Usamos 'if' en lugar de 'while'. Un login solo debe encontrar un usuario.
                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    // Asignamos los demás datos al objeto para tenerlos disponibles en la sesión
                    if (!(datos.Lector["Nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["Apellido"];

                    // ¡Importante! Asignamos el email que vino de la DB.
                    usuario.Email = (string)datos.Lector["Email"];

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
