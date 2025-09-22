using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


namespace PP1
{
    public partial class NuevoMovimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // PRIMERO Y MÁS IMPORTANTE: Validamos que el usuario esté logueado.
            // Si no hay nadie en la sesión, lo redirigimos al Login y detenemos la ejecución.
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return; // Detenemos para que no se ejecute más código.
            }

            if (!IsPostBack)
            {
                // Ahora que sabemos que el usuario existe, podemos cargar las categorías de forma segura.
                CargarCategorias();
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void rblTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cada vez que el usuario cambia entre Ingreso/Egreso, recargamos las categorías
            CargarCategorias();
            // Y nos aseguramos de que el panel de objetivos se oculte
            pnlObjetivos.Visible = false;
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si el desplegable de categorías no tiene nada, no hacemos nada
            if (ddlCategorias.SelectedItem == null)
            {
                pnlObjetivos.Visible = false;
                return;
            }

            // Comprobamos si la categoría seleccionada es "Objetivos"
            bool esCategoriaObjetivo = ddlCategorias.SelectedItem.Text.ToLower() == "objetivos";

            // Mostramos el desplegable de objetivos SOLO si es un Egreso y la categoría es "Objetivos"
            if (rblTipoMovimiento.SelectedValue == "Egreso" && esCategoriaObjetivo)
            {
                pnlObjetivos.Visible = true;
                CargarObjetivos();
            }
            else
            {
                pnlObjetivos.Visible = false;
            }
        }

        private void CargarCategorias()
        {
            // Como la validación ya se hizo en Page_Load, aquí podemos usar la sesión con seguridad.
            try
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];
                string tipo = rblTipoMovimiento.SelectedValue;

                CategoriaNegocio negocio = new CategoriaNegocio();
                List<Categoria> listaCategorias = negocio.Listar(tipo, user.IdUsuario);

                ddlCategorias.DataSource = listaCategorias;
                ddlCategorias.DataValueField = "Id";
                ddlCategorias.DataTextField = "Nombre";
                ddlCategorias.DataBind();

                if (ddlCategorias.Items.Count == 0)
                {
                    ddlCategorias.Items.Insert(0, new ListItem("No hay categorías de este tipo", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar categorías: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void CargarObjetivos()
        {
            try
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];
                ObjetivoNegocio negocio = new ObjetivoNegocio();
                List<Objetivo> listaObjetivos = negocio.Listar(user.IdUsuario);

                ddlObjetivos.DataSource = listaObjetivos;
                ddlObjetivos.DataValueField = "Id";
                ddlObjetivos.DataTextField = "Nombre";
                ddlObjetivos.DataBind();
                ddlObjetivos.Items.Insert(0, new ListItem("Seleccione un Objetivo", "0"));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar objetivos: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // La lógica para guardar el movimiento en la base de datos irá aquí.
            // Esto implicará crear un MovimientoNegocio.cs que maneje la transacción
            // de insertar el movimiento, actualizar la billetera y, opcionalmente, el objetivo.

            lblMensaje.Text = "¡Movimiento guardado con éxito! (Lógica de guardado pendiente)";
            lblMensaje.ForeColor = System.Drawing.Color.Green;
        }
    }
}