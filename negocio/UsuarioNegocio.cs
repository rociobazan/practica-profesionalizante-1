using dominio;
using Microsoft.Data.SqlClient;
using System;

namespace negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Apellido, [User], Email FROM USUARIOS WHERE Email = @email AND Pass = @pass");
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@pass", usuario.Pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = datos.Lector["Nombre"] is DBNull ? "" : (string)datos.Lector["Nombre"];
                    usuario.Apellido = datos.Lector["Apellido"] is DBNull ? "" : (string)datos.Lector["Apellido"];
                    usuario.User = datos.Lector["User"] is DBNull ? "" : (string)datos.Lector["User"];
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

        public void InsertarNuevo(Usuario nuevo)
        {
            // Obtenemos la cadena de conexión para manejar la transacción manualmente
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexionDB"].ToString();

            // Usamos 'using' para asegurar que la conexión se cierre siempre
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Iniciamos la transacción
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // --- PASO 1: INSERTAR EL NUEVO USUARIO Y OBTENER SU ID ---
                    // Usamos 'SCOPE_IDENTITY()' para que la consulta nos devuelva el ID del usuario recién creado.
                    string queryUsuario = "INSERT INTO USUARIOS (Email, Pass, Nombre, Apellido, [User]) OUTPUT INSERTED.IdUsuario VALUES (@email, @pass, @nombre, @apellido, @user)";
                    int nuevoIdUsuario;

                    using (SqlCommand cmd = new SqlCommand(queryUsuario, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@email", nuevo.Email);
                        cmd.Parameters.AddWithValue("@pass", nuevo.Pass); // Recuerda que aquí guardas en texto plano
                        cmd.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", nuevo.Apellido);
                        cmd.Parameters.AddWithValue("@user", nuevo.User);

                        // Ejecutamos la consulta y guardamos el ID que nos devuelve
                        nuevoIdUsuario = (int)cmd.ExecuteScalar();
                    }

                    // --- PASO 2: CREAR LA BILLETERA ASOCIADA A ESE NUEVO ID ---
                    string queryBilletera = "INSERT INTO BILLETERAS (IdUsuario, Nombre, SaldoActual) VALUES (@idUsuario, @nombre, 0.00)";
                    using (SqlCommand cmd = new SqlCommand(queryBilletera, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", nuevoIdUsuario);
                        cmd.Parameters.AddWithValue("@nombre", "Billetera Principal"); // Le damos un nombre por defecto
                        cmd.ExecuteNonQuery();
                    }

                    // Si ambos comandos se ejecutaron sin errores, confirmamos la transacción
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Si algo falló, revertimos todos los cambios para no dejar datos corruptos
                    transaction.Rollback();
                    throw; // Relanzamos la excepción para que la página muestre un error
                }
            }
        }
    }
}