<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebLogIn.aspx.cs" Inherits="WebApp.WebLogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar Sesión</title>
    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <!-- Font Awesome -->
    <link href="plugins/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <!-- icheck bootstrap -->
    <link href="plugins/icheck-bootstrap/icheck-bootstrap.min.css" rel="stylesheet" />
    <!-- Theme style -->
    <link href="dist/css/adminlte.min.css" rel="stylesheet" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="plugins/sweetalert2/sweetalert2.min.css" />
    
</head>
<body class="hold-transition login-page">
    <div class="login-box">
        <!-- /.login-logo -->
        <div class="card card-outline card-primary">
            <div class="card-header text-center">
                <h1><b>Departamento</b> programacion y manejo de contenidos</h1>
            </div>
            <div class="card-body">
                <p class="login-box-msg">Iniciar Sesión</p>

                <form id="frmLogin" runat="server">
                    <div class="input-group mb-3">
                        <input id="txtCorreo" runat="server" type="email" class="form-control" placeholder="correo@gmail.com" autocomplete="on" required="required" />
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-envelope"></span>
                            </div>
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <input id="txtPass" type="password" runat="server" class="form-control" placeholder="" autocomplete="off" required="required" />
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-lock"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">                        
                        <!-- /.col -->
                        <div class="col-4">
                            <input id="btnIngresar" runat="server" class="btn btn-primary btn-block"  type="submit" value="Ingresar" onserverclick="btnIngresar_Click" />
                        </div>
                        <!-- /.col -->
                    </div>
                      <!-- SweetAlert2 -->
                    <script src="plugins/sweetalert2/sweetalert2.all.min.js"></script>
                </form>
                <!-- /.social-auth-links -->
                <br />
                <p class="mb-1">
                    <%--<a href="WebRecuperar.aspx">Olvide mi contraseña</a>--%>
                </p>
            </div>
            <!-- /.card-body -->
        </div>
        <!-- /.card -->
    </div>
    <!-- /.login-box -->    
</body>
</html>