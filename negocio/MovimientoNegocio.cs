using dominio;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;


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
                    string queryMovimiento = "INSERT INTO MOVIMIENTOS (Nombre, IdBilletera, IdCategoria, IdObjetivo, IdUsuario, Monto, TipoMovimiento, Descripcion, UrlImagen, Fecha) VALUES (@nombre, @idBilletera, @idCategoria, @idObjetivo, @idUsuario, @monto, @tipo, @desc, @url, @fecha)";
                    using (SqlCommand cmd = new SqlCommand(queryMovimiento, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                        cmd.Parameters.AddWithValue("@idBilletera", nuevo.IdBilletera);
                        cmd.Parameters.AddWithValue("@idCategoria", nuevo.IdCategoria);
                        cmd.Parameters.AddWithValue("@idObjetivo", (object)nuevo.IdObjetivo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@idUsuario", nuevo.IdUsuario);
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
        public List<Movimiento> Listar(int idUsuario, string nombre = "", string tipo = "", int idCategoria = 0, string fechaDesde = "", string fechaHasta = "")
        {
            List<Movimiento> lista = new List<Movimiento>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT M.IdMovimiento, M.Nombre, M.Monto, M.TipoMovimiento, M.Descripcion, M.Fecha, C.Nombre AS NombreCategoria 
                    FROM MOVIMIENTOS AS M
                    INNER JOIN CATEGORIAS AS C ON M.IdCategoria = C.IdCategoria
                    WHERE M.IdUsuario = @idUsuario ";

                // Añadimos los filtros a la consulta de forma dinámica
                if (!string.IsNullOrEmpty(nombre))
                    consulta += " AND M.Nombre LIKE @nombre";

                if (!string.IsNullOrEmpty(tipo))
                    consulta += " AND M.TipoMovimiento = @tipo";

                if (idCategoria > 0)
                    consulta += " AND M.IdCategoria = @idCategoria";

                if (!string.IsNullOrEmpty(fechaDesde))
                    consulta += " AND M.Fecha >= @fechaDesde";

                if (!string.IsNullOrEmpty(fechaHasta))
                    // Añadimos un día para incluir la fecha "hasta" completa
                    consulta += " AND M.Fecha < DATEADD(day, 1, @fechaHasta)";

                consulta += " ORDER BY M.Fecha DESC";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idUsuario", idUsuario);

                // Seteamos los parámetros solo si los filtros tienen valor
                if (!string.IsNullOrEmpty(nombre))
                    datos.setearParametro("@nombre", "%" + nombre + "%"); // Usamos LIKE para búsqueda parcial

                if (!string.IsNullOrEmpty(tipo))
                    datos.setearParametro("@tipo", tipo);

                if (idCategoria > 0)
                    datos.setearParametro("@idCategoria", idCategoria);

                if (!string.IsNullOrEmpty(fechaDesde))
                    datos.setearParametro("@fechaDesde", fechaDesde);

                if (!string.IsNullOrEmpty(fechaHasta))
                    datos.setearParametro("@fechaHasta", fechaHasta);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Movimiento aux = new Movimiento();
                    aux.IdMovimiento = (int)datos.Lector["IdMovimiento"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Monto = (decimal)datos.Lector["Monto"];
                    aux.TipoMovimiento = (string)datos.Lector["TipoMovimiento"];
                    aux.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.NombreCategoria = (string)datos.Lector["NombreCategoria"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }
    }
}