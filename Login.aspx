<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PP1.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Iniciar Sesión</title>
    
    <style type="text/css">
        /* Estilos generales para quitar márgenes y ocupar toda la pantalla */
        body, html {
            margin: 0;
            padding: 0;
            height: 100%;
            font-family: Arial, Helvetica, sans-serif;
            overflow: hidden; /* Evita barras de scroll innecesarias */
        }

        /* Contenedor principal con la imagen de fondo */
        .login-container {
           
            background-image: url('Images/Login.png'); 

            /* Esta es la propiedad clave */
            background-size: cover;

            /* Centra la imagen para que el recorte (si ocurre) sea parejo en los bordes */
            background-position: center center;

            /* Evita que la imagen se repita si es más pequeña que la pantalla */
            background-repeat: no-repeat; 

            height: 100vh;
            width: 100vw;
            display: flex;
            justify-content: flex-end;
            align-items: center;
        }

        /* La caja semi-transparente del formulario */
        .login-box {
            width: 320px;
            background-color: rgba(0, 0, 0, 0.75); /* Fondo negro con 75% de opacidad */
            border-radius: 15px;
            padding: 40px;
            margin-right: 10%; /* Espacio desde el borde derecho */
            box-shadow: 0 8px 16px rgba(0,0,0,0.4);
            color: white;
        }

        /* Círculo para el ícono de usuario */
        .user-icon-wrapper {
            width: 90px;
            height: 90px;
            border-radius: 50%; /* Lo hace un círculo perfecto */
            background-color: #d4a753; /* Color dorado */
            margin: 0 auto 30px auto; /* Centra el círculo y le da espacio abajo */
            display: flex;
            justify-content: center;
            align-items: center;
            overflow: hidden; /* Esconde cualquier parte de la imagen que se salga */
        }

        .user-icon-wrapper img {
            width: 60%; /* Ajusta el tamaño del ícono dentro del círculo */
        }

        /* Estilos para los grupos de Label + TextBox */
        .input-group {
            margin-bottom: 20px;
            text-align: left;
        }

        .input-group label {
            display: block;
            margin-bottom: 8px;
            font-size: 0.9em;
            color: #f0f0f0;
        }

        /* Estilos para los campos de texto */
        .login-input {
            width: 100%;
            padding: 12px 10px;
            border: 1px solid #555;
            border-radius: 25px; /* Bordes redondeados */
            background-color: #333;
            color: white;
            box-sizing: border-box; /* Asegura que el padding no afecte el ancho total */
            font-size: 1em;
        }

        .login-input:focus {
            outline: none;
            border-color: #d4a753; /* Borde dorado al seleccionar el campo */
            box-shadow: 0 0 5px rgba(212, 167, 83, 0.5);
        }

        /* Estilos para el checkbox */
        .checkbox-group {
            text-align: left;
            margin-bottom: 25px;
            font-size: 0.8em;
            color: #ccc;
        }

        /* Estilos para el botón de Login */
        .login-button {
            width: 100%;
            padding: 12px;
            background-color: #d4a753; /* Color dorado */
            border: none;
            border-radius: 25px; /* Bordes redondeados */
            color: #333; /* Texto oscuro para que contraste con el fondo dorado */
            font-size: 1.1em;
            font-weight: bold;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .login-button:hover {
            background-color: #c09440; /* Tono más oscuro al pasar el mouse */
        }

        /* Estilos para el link de registro */
        .register-link {
            display: block; /* Asegura que ocupe su propia línea */
            text-align: center; /* Centra el texto del link */
            margin-top: 20px; /* ¡Esta es la línea clave! Añade espacio arriba */
            color: #d4a753; /* Color dorado para que combine */
            text-decoration: none; /* Quita el subrayado feo */
            font-size: 0.9em;
        }

        .register-link:hover {
            text-decoration: underline; /* Añade el subrayado solo al pasar el mouse */
            color: #ffffff; /* Cambia a color blanco al pasar el mouse */
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-box">
                
                <div class="user-icon-wrapper">
                    <img src="Images/user_icon.png" alt="Usuario" />
                </div>

                <div class="input-group">
                    <asp:Label ID="lblEmail" runat="server" Text="Usuario" AssociatedControlID="txtEmail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="login-input"></asp:TextBox>
                </div>

                <div class="input-group">
                    <asp:Label ID="lblContraseina" runat="server" Text="Contraseña" AssociatedControlID="txtContrasenia"></asp:Label>
                    <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" CssClass="login-input"></asp:TextBox>
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="login-button" OnClick="btnLogin_Click" />

                <asp:HyperLink ID="hlRegistrarse" runat="server" NavigateUrl="~/Registro.aspx" CssClass="register-link">
                    ¿No tienes una cuenta? Regístrate aquí
                </asp:HyperLink>
                
            </div>
        </div>
    </form>
</body>
</html>