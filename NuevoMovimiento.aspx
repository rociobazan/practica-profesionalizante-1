<%@ Page Title="Nuevo Movimiento" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NuevoMovimiento.aspx.cs" Inherits="PP1.NuevoMovimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Reutilizamos y adaptamos los estilos de las páginas anteriores */
        .form-container {
            min-height: 80vh;
            width: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 40px 15px;
            box-sizing: border-box;
        }
        .form-box {
            width: 100%;
            max-width: 600px;
            background-color: rgba(0, 0, 0, 0.80);
            border-radius: 15px;
            padding: 40px;
            box-shadow: 0 8px 16px rgba(0,0,0,0.4);
            color: white;
        }
        .form-box h2 {
            color: #d4a753;
            margin-bottom: 30px;
            font-weight: bold;
            text-align: center;
        }
        .input-group { margin-bottom: 20px; text-align: left; }
        .input-group label { display: block; margin-bottom: 8px; font-size: 0.9em; color: #f0f0f0; }
        .form-control-custom {
            width: 100%; padding: 12px 10px; border: 1px solid #555;
            border-radius: 25px; background-color: #333; color: white;
            box-sizing: border-box; font-size: 1em;
        }
        .form-control-custom:focus {
            outline: none; border-color: #d4a753;
            box-shadow: 0 0 5px rgba(212, 167, 83, 0.5);
        }
        .form-button {
            width: 100%; max-width: 300px; margin: 20px auto 0 auto; display: block;
            padding: 12px; background-color: #d4a753; border: none;
            border-radius: 25px; color: #333; font-size: 1.1em;
            font-weight: bold; cursor: pointer; transition: background-color 0.3s ease;
        }
        .form-button:hover { background-color: #c09440; }
        .radio-group label { margin-right: 15px; } /* Estilo para los RadioButton */
        .message-label { display: block; text-align: center; margin-top: 15px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- El ScriptManager es OBLIGATORIO para que funcione el UpdatePanel (AJAX) --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="form-container">
        <div class="form-box">
            <h2>Nuevo Movimiento</h2>

            <%-- El UpdatePanel permite que los desplegables se actualicen sin recargar toda la página --%>
            <asp:UpdatePanel ID="updPanelMovimiento" runat="server">
                <ContentTemplate>
                    <div class="input-group">
                        <label>Tipo de Movimiento</label>
                        <asp:RadioButtonList ID="rblTipoMovimiento" runat="server" RepeatDirection="Horizontal" ForeColor="White" CssClass="radio-group" AutoPostBack="true" OnSelectedIndexChanged="rblTipoMovimiento_SelectedIndexChanged">
                            <asp:ListItem Text="Ingreso" Value="Ingreso" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Egreso" Value="Egreso"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="input-group">
                        <asp:Label ID="lblCategoria" runat="server" Text="Categoría"></asp:Label>
                        <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-control-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <%-- Este panel solo aparecerá cuando sea necesario --%>
                    <asp:Panel ID="pnlObjetivos" runat="server" Visible="false">
                        <div class="input-group">
                            <asp:Label ID="lblObjetivo" runat="server" Text="Asociar a un Objetivo (Opcional)"></asp:Label>
                            <asp:DropDownList ID="ddlObjetivos" runat="server" CssClass="form-control-custom"></asp:DropDownList>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- El resto del formulario va fuera del UpdatePanel --%>
            <div class="input-group">
                <asp:Label ID="lblMonto" runat="server" Text="Monto"></asp:Label>
                <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control-custom" TextMode="Number" step="0.01" placeholder="0.00"></asp:TextBox>
            </div>

            <div class="input-group">
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control-custom" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>

            <div class="input-group">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha del Movimiento"></asp:Label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control-custom" TextMode="Date"></asp:TextBox>
            </div>

            <div class="input-group">
                <asp:Label ID="lblImagen" runat="server" Text="Adjuntar Comprobante (Opcional)"></asp:Label>
                <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control-custom" />
            </div>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Movimiento" CssClass="form-button" OnClick="btnGuardar_Click" />
            <asp:Label ID="lblMensaje" runat="server" CssClass="message-label" EnableViewState="false"></asp:Label>
        </div>
    </div>
</asp:Content>