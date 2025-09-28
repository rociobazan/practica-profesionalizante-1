<%@ Page Title="Mis Movimientos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Movimientos.aspx.cs" Inherits="PP1.Movimientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container-main { display: flex; justify-content: center; padding: 40px 15px; }
        .content-box { width: 100%; max-width: 1100px; background-color: rgba(0, 0, 0, 0.80); border-radius: 15px; padding: 30px; color: white; }
        .content-box h2 { color: #d4a753; text-align: center; margin-bottom: 25px; }
        
        .filter-container { display: grid; grid-template-columns: repeat(auto-fit, minmax(150px, 1fr)); gap: 15px; margin-bottom: 30px; align-items: end; }
        .filter-group { display: flex; flex-direction: column; }
        .filter-group label { margin-bottom: 5px; font-size: 0.9em; }
        .filter-control {
            width: 100%; padding: 10px; border: 1px solid #555; border-radius: 20px;
            background-color: #333; color: white; box-sizing: border-box;
        }
        .filter-button {
            padding: 10px; background-color: #d4a753; border: none; border-radius: 20px;
            color: #333; font-weight: bold; cursor: pointer; transition: background-color 0.3s;
            height: 40px; /* Alineación vertical */
        }
        .filter-button:hover { background-color: #c09440; }
        
        .grid-view { width: 100%; border-collapse: collapse; }
        .grid-view th, .grid-view td { padding: 12px; text-align: left; border-bottom: 1px solid #555; }
        .grid-view th { background-color: #333; color: #d4a753; }
        .monto-ingreso { color: #28a745; font-weight: bold; }
        .monto-egreso { color: #dc3545; font-weight: bold; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-main">
        <div class="content-box">
            <h2>Historial de Movimientos</h2>

            <%-- 👇 ENVOLVEMOS TODO EN UN UPDATEPANEL 👇 --%>
            <asp:UpdatePanel ID="upMovimientos" runat="server">
                <ContentTemplate>
                    <div class="filter-container">
                        <div class="filter-group">
                            <label for="txtFiltroNombre">Buscar por Nombre</label>
                            <%-- 👇 AÑADIMOS AutoPostBack y OnTextChanged 👇 --%>
                            <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="filter-control" AutoPostBack="true" OnTextChanged="txtFiltroNombre_TextChanged"></asp:TextBox>
                        </div>
                        <%-- El resto de tus filtros (Tipo, Categoría, Fechas) se mantienen igual --%>
                        <div class="filter-group">
                            <label for="ddlFiltroTipo">Tipo</label>
                            <asp:DropDownList ID="ddlFiltroTipo" runat="server" CssClass="filter-control">
                                <asp:ListItem Text="Todos" Value="" />
                                <asp:ListItem Text="Ingreso" Value="Ingreso" />
                                <asp:ListItem Text="Egreso" Value="Egreso" />
                            </asp:DropDownList>
                        </div>
                        <div class="filter-group">
                            <label for="ddlFiltroCategoria">Categoría</label>
                            <asp:DropDownList ID="ddlFiltroCategoria" runat="server" CssClass="filter-control"></asp:DropDownList>
                        </div>
                        <div class="filter-group">
                            <label for="txtFiltroFechaDesde">Desde</label>
                            <asp:TextBox ID="txtFiltroFechaDesde" runat="server" CssClass="filter-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="filter-group">
                            <label for="txtFiltroFechaHasta">Hasta</label>
                            <asp:TextBox ID="txtFiltroFechaHasta" runat="server" CssClass="filter-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="filter-group">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="filter-button" OnClick="btnFiltrar_Click" />
                        </div>
                         <div class="filter-group">
                            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar" CssClass="filter-button" OnClick="btnLimpiarFiltros_Click" CausesValidation="false" />
                        </div>
                    </div>

                    <asp:GridView ID="gvMovimientos" runat="server" AutoGenerateColumns="false" CssClass="grid-view"
                        GridLines="None" OnRowDataBound="gvMovimientos_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="NombreCategoria" HeaderText="Categoría" />
                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="TipoMovimiento" HeaderText="Tipo" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>