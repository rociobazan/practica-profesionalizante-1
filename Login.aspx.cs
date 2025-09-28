using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace PP1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                // CORRECCIÓN: Asignamos a 'Email', no a 'User', para ser consistentes.
                // Asumo que tus TextBoxes se llaman 'txtEmail' y 'txtContrasenia'.
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtContrasenia.Text;

                if (negocio.Loguear(usuario))
                {
                    // El objeto 'usuario' ahora viene lleno con los datos de la DB.
                    // Lo guardamos en Session.
                    Session.Add("usuarioLogueado", usuario);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    // CORRECCIÓN: Manejamos el caso de login incorrecto.
                    // Guardamos un mensaje de error en la sesión y redirigimos a una página de error.
                    // (Asegúrate de tener una página llamada Error.aspx que pueda mostrar este mensaje).
                    Session.Add("error", "Usuario o contraseña incorrectos.");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                // Si algo falla (ej: la conexión a la DB), también lo manejamos.
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}