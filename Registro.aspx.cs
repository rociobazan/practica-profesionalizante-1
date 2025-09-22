using System;
using System.Web.UI;
using dominio;
using negocio;

namespace PP1
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validamos que las contraseñas coincidan
                if (txtPassword.Text != txtConfirmarPassword.Text)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Las contraseñas no coinciden.";
                    return; // Detenemos la ejecución
                }

                // Creamos los objetos necesarios
                Usuario nuevoUsuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                // Llenamos el objeto con los datos del formulario
                nuevoUsuario.Email = txtEmail.Text;
                nuevoUsuario.Pass = txtPassword.Text; // Se guarda el texto plano
                nuevoUsuario.User = txtUsuario.Text;
                nuevoUsuario.Nombre = txtNombre.Text;
                nuevoUsuario.Apellido = txtApellido.Text;

                // Llamamos a la capa de negocio para insertar en la DB
                negocio.InsertarNuevo(nuevoUsuario);

                // Mostramos un mensaje de éxito y redirigimos después de unos segundos
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "¡Registro exitoso! Redirigiendo al Login...";

                string script = "setTimeout(function(){ window.location.href = 'Login.aspx'; }, 2000);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", script, true);
            }
            catch (Exception ex)
            {
                // Mostramos un mensaje de error si algo falla
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al registrar: " + ex.Message;
            }
        }
    }
}