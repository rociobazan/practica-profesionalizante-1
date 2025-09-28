using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace PP1// Asegúrate de que este sea el namespace de tu nuevo proyecto
{
    public partial class Movimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                CargarFiltroCategorias();
                CargarMovimientos();
            }
        }

        protected void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            CargarMovimientos();
        }

        private void CargarMovimientos()
        {
            try
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];
                MovimientoNegocio negocio = new MovimientoNegocio();

                // Recolectamos los valores de todos los filtros
                string nombre = txtFiltroNombre.Text;
                string tipo = ddlFiltroTipo.SelectedValue;
                int idCategoria = int.Parse(ddlFiltroCategoria.SelectedValue);
                string fechaDesde = txtFiltroFechaDesde.Text;
                string fechaHasta = txtFiltroFechaHasta.Text;

                List<Movimiento> lista = negocio.Listar(user.IdUsuario, nombre, tipo, idCategoria, fechaDesde, fechaHasta);

                gvMovimientos.DataSource = lista;
                gvMovimientos.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar el error, por ejemplo, en un Label
            }
        }

        private void CargarFiltroCategorias()
        {
            try
            {
                Usuario user = (Usuario)Session["usuarioLogueado"];
                CategoriaNegocio negocio = new CategoriaNegocio();
                // Usamos el método que trae TODAS las categorías del usuario
                List<Categoria> categorias = negocio.Listar(user.IdUsuario);

                ddlFiltroCategoria.DataSource = categorias;
                ddlFiltroCategoria.DataValueField = "IdCategoria";
                ddlFiltroCategoria.DataTextField = "Nombre";
                ddlFiltroCategoria.DataBind();
                ddlFiltroCategoria.Items.Insert(0, new ListItem("Todas", "0"));
            }
            catch (Exception ex)
            {
                // Manejar el error
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarMovimientos();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            // Reseteamos los controles a su estado inicial
            txtFiltroNombre.Text = string.Empty;
            ddlFiltroTipo.SelectedIndex = 0;
            ddlFiltroCategoria.SelectedIndex = 0;
            txtFiltroFechaDesde.Text = string.Empty;
            txtFiltroFechaHasta.Text = string.Empty;

            // Volvemos a cargar la grilla sin filtros
            CargarMovimientos();
        }

        // Este método se mantiene igual para colorear las filas
        protected void gvMovimientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Movimiento mov = (Movimiento)e.Row.DataItem;
                TableCell celdaMonto = e.Row.Cells[3];

                if (mov.TipoMovimiento.ToLower() == "ingreso")
                {
                    celdaMonto.ForeColor = System.Drawing.Color.FromArgb(0x28, 0xa7, 0x45); // Verde
                    celdaMonto.Font.Bold = true;
                }
                else if (mov.TipoMovimiento.ToLower() == "egreso")
                {
                    celdaMonto.ForeColor = System.Drawing.Color.FromArgb(0xdc, 0x35, 0x45); // Rojo
                    celdaMonto.Font.Bold = true;
                    celdaMonto.Text = (mov.Monto * -1).ToString("C");
                }
            }
        }
    }
}