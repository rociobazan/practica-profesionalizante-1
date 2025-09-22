using dominio;
using Microsoft.Data.SqlClient;
using System;


namespace negocio
{
    public class MovimientoNegocio
    {
        private string connectionString;

        public MovimientoNegocio()
        {
            // Obtenemos la cadena de conexión para manejarla manualmente
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexionDB"].ToString();
        }

        public void Agregar(Movimiento nuevo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. INSERTAR EL NUEVO MOVIMIENTO
                    string queryMovimiento = "INSERT INTO MOVIMIENTOS (Nombre, IdBilletera, IdCategoria, IdObjetivo, Monto, TipoMovimiento, Descripcion, UrlImagen, Fecha) VALUES (@nombre, @idBilletera, @idCategoria, @idObjetivo, @monto, @tipo, @desc, @url, @fecha)";
                    using (SqlCommand cmd = new SqlCommand(queryMovimiento, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                        cmd.Parameters.AddWithValue("@idBilletera", nuevo.IdBilletera);
                        cmd.Parameters.AddWithValue("@idCategoria", nuevo.IdCategoria);
                        cmd.Parameters.AddWithValue("@idObjetivo", (object)nuevo.IdObjetivo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@monto", nuevo.Monto);
                        cmd.Parameters.AddWithValue("@tipo", nuevo.TipoMovimiento);
                        cmd.Parameters.AddWithValue("@desc", (object)nuevo.Descripcion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@url", (object)nuevo.UrlImagen ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@fecha", nuevo.Fecha);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. ACTUALIZAR SALDO DE LA BILLETERA
                    decimal montoParaActualizar = nuevo.TipoMovimiento == "Ingreso" ? nuevo.Monto : -nuevo.Monto;
                    string queryBilletera = "UPDATE BILLETERAS SET SaldoActual = SaldoActual + @monto WHERE IdBilletera = @idBilletera";
                    using (SqlCommand cmd = new SqlCommand(queryBilletera, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@monto", montoParaActualizar);
                        cmd.Parameters.AddWithValue("@idBilletera", nuevo.IdBilletera);
                        cmd.ExecuteNonQuery();
                    }

                    // 3. ACTUALIZAR OBJETIVO (SI APLICA)
                    if (nuevo.IdObjetivo.HasValue && nuevo.IdObjetivo > 0)
                    {
                        string queryObjetivo = "UPDATE OBJETIVOS SET MontoAlcanzado = MontoAlcanzado + @monto WHERE IdObjetivo = @idObjetivo";
                        using (SqlCommand cmd = new SqlCommand(queryObjetivo, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@monto", nuevo.Monto);
                            cmd.Parameters.AddWithValue("@idObjetivo", nuevo.IdObjetivo);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Si todo salió bien, confirmamos la transacción
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Si algo falló, revertimos todos los cambios
                    transaction.Rollback();
                    throw; // Relanzamos la excepción para que la capa de presentación la atrape
                }
            }
        }
    }
}