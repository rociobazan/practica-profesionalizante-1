<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="PP1.Registro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Crear Cuenta</title>
    
    <style type="text/css">
        /* Estilos generales para quitar márgenes y ocupar toda la pantalla */
        body, html {
            margin: 0;
            padding: 0;
            height: 100%;
            font-family: Arial, Helvetica, sans-serif;
            background-image: url('Images/Login.png'); 
            background-size: cover;
            background-position: center center;
            background-repeat: no-repeat;
            background-attachment: fixed; /* Mantiene el fondo fijo */
            overflow-y: auto; /* Permite scroll si el contenido es muy grande */
        }

        /* Contenedor que centra la caja de registro */
        .main-container {
            min-height: 100vh; /* Ocupa al menos toda la altura de la ventana */
            width: 100%;
            display: flex;
            justify-content: center; 
            align-items: center;
            padding: 40px 15px;
            box-sizing: border-box;
        }

        /* La caja semi-transparente del formulario */
        .register-box {
            width: 100%;
            max-width: 750px;
            background-color: rgba(0, 0, 0, 0.80);
            border-radius: 15px;
            padding: 40px;
            box-shadow: 0 8px 16px rgba(0,0,0,0.4);
            color: white;
            text-align: center;
        }

        .register-box h2 {
            color: #d4a753;
            margin-bottom: 30px;
            font-weight: bold;
        }

        /* Contenedor de la grilla para los campos */
        .form-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }
        
        .input-group {
            text-align: left;
            margin-bottom: 0; 
        }

        .input-group label {
            display: block;
            margin-bottom: 8px;
            font-size: 0.9em;
            color: #f0f0f0;
        }

        .register-input {
            width: 100%;
            padding: 12px 10px;
            border: 1px solid #555;
            border-radius: 25px;
            background-color: #333;
            color: white;
            box-sizing: border-box;
            font-size: 1em;
        }

        .register-input:focus {
            outline: none;
            border-color: #d4a753;
            box-shadow: 0 0 5px rgba(212, 167, 83, 0.5);
        }
        
        .register-button {
            width: 100%;
            max-width: 300px;
            margin: 10px auto 0 auto;
            display: block;
            padding: 12px;
            background-color: #d4a753;
            border: none;
            border-radius: 25px;
            color: #333;
            font-size: 1.1em;
            font-weight: bold;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .register-button:hover {
            background-color: #c09440;
        }

        .login-link {
            display: block;
            text-align: center;
            margin-top: 20px;
            color: #d4a753;
            text-decoration: none;
            font-size: 0.9em;
        }

        .login-link:hover {
            text-decoration: underline;
            color: #ffffff;
        }

        /* Media query para responsividad en celulares */
        @media (max-width: 768px) {
            .form-grid {
                grid-template-columns: 1fr; /* Vuelve a una sola columna */
            }
            .register-box {
                width: 90%;
                padding: 25px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-container">
            <div class="register-box">
                
                <h2>Crear Cuenta</h2>

                <div class="form-grid">

                    <div class="input-group">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="register-input"></asp:TextBox>
                    </div>
        
                    <div class="input-group">
                        <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="register-input"></asp:TextBox>
                    </div>
    
                    <div class="input-group">
                        <asp:Label ID="lblUsuario" runat="server" Text="Nombre de Usuario"></asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="register-input" placeholder="Elige un nombre de usuario"></asp:TextBox>
                    </div>
                    
                    <div class="input-group">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="register-input" TextMode="Email" placeholder="ejemplo@correo.com"></asp:TextBox>
                    </div>
                    
                    <div class="input-group">
                        <asp:Label ID="lblPassword" runat="server" Text="Contraseña"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="register-input" TextMode="Password"></asp:TextBox>
                    </div>
                    
                    <div class="input-group">
                        <asp:Label ID="lblConfirmarPassword" runat="server" Text="Confirmar Contraseña"></asp:Label>
                        <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="register-input" TextMode="Password"></asp:TextBox>
                    </div>
    
                </div>

                <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="register-button" />
                
                <asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="~/Login.aspx" CssClass="login-link">
                    ¿Ya tienes una cuenta? Inicia Sesión
                </asp:HyperLink>
    
            </div>
        </div>
    </form>
</body>
</html>