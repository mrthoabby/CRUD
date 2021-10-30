<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Task.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <title>Iniciar Sesion</title>
</head>
<body>
    <div class="container-fluid">
        <div class="row justify-content-center mt-5 p-3">

            <form class="col-4 p-4 border border-1" id="Login" runat="server" method="post">
                <h2 class="sr-only mb-4">Autenticación</h2>
                <div class="form-group mt-2">
                    <asp:TextBox ID="textUserName" runat="server" placeholder="Nombre de usuario" class="form-control" />
                    <asp:RequiredFieldValidator class="text-danger p-2" ID="user" runat="server" ControlToValidate="textUserName" ErrorMessage="Por favor ingrese un nombre de usuario" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group mt-2">
                    <asp:TextBox TextMode="Password" ID="textPassword" runat="server" placeholder="Contraseña" class="form-control" />
                    <asp:RequiredFieldValidator class="text-danger p-2" ID="pass" runat="server" ControlToValidate="textPassword" ErrorMessage="Por favor ingrese una contraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group mt-4 mt-3">
                    <asp:Button class="btn btn-primary btn-block col-12" ID="buttonLogin" runat="server" Text="Iniciar Sesión" OnClick="BtnLogin_Click" />
                </div>
                <div class="form-group mt-4 mt-3">
                    <asp:Label runat="server" ID="resultError" CssClass="alert alert-danger mt-4"  Visible="false" />
                </div>

            </form>
        </div>
    </div>

</body>
</html>
