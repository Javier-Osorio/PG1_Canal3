<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebNuevoUsuario.aspx.cs" Inherits="WebApp.WebNuevoUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Nuevo Registro de Usuario</title>
    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <!-- icheck bootstrap -->
    <link href="plugins/icheck-bootstrap/icheck-bootstrap.min.css" rel="stylesheet" />
    <!-- Theme style -->
    <link href="dist/css/adminlte.min.css" rel="stylesheet" />
</head>
<body class="hold-transition register-page">
    <div class="register-box">
        <div class="card card-outline card-primary">
            <div class="card-header text-center">
                <a href="#" class="h1"><b>Nuevo</b> Registro</a>
            </div>
            <div class="card-body">
                <%--<p class="login-box-msg">Register a new membership</p>--%>

                <form id="frmNuevo" runat="server" onsubmit="">
                    <div class="input-group mb-3">
                        <input runat="server" type="text" class="form-control" placeholder="Nombre" id="txtNombre" required="required"/>
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-user"></span>
                            </div>
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <input runat="server" type="text" class="form-control" placeholder="Apellido" id="txtApellido" required="required" />
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-user"></span>
                            </div>
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <input runat="server" type="email" class="form-control" placeholder="Correo Electronico" id="txtCorreo" required="required" />
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-envelope"></span>
                            </div>
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <input runat="server" type="password" class="form-control" title="La contraseña debe tener entre 7 a 15 caracteres con al menos 1 letra minuscula, 1 mayuscula, 1 numero y 1 caracter especial (@,!,*...)" 
                            placeholder="Contraseña" required="required" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{7,}" id="txtPassword" onchange="
                            this.setCustomValidity(this.validity.patternMismatch ? this.title : '');
                            if(this.checkValidity()) {
                              form.txtPasswordConfirm.pattern = RegExp.escape(this.value);
                            }" />
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-lock"></span>
                            </div>
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <input runat="server" type="password" class="form-control" title="Ingrese la misma contraseña" 
                            placeholder="Confirmar contraseña" required="required" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{7,}" id="txtPasswordConfirm" onchange="
                             this.setCustomValidity(this.validity.patternMismatch ? this.title : '');
                            if(this.checkValidity()) {
                              form.txtPasswordConfirm.pattern = RegExp.escape(this.value);
                            }else if(form.txtPassword.value != form.txtPasswordConfirm.value) {
                              alert('Las contraseñas deben coincidar');
                            }" />
                        <div class="input-group-append">
                            <div class="input-group-text">
                                <span class="fas fa-lock"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <%--<div class="col-8">
                            <div class="icheck-primary">
                                <input type="checkbox" id="agreeTerms" name="terms" value="agree">
                                <label for="agreeTerms">
                                    I agree to the <a href="#">terms</a>
                                </label>
                            </div>
                        </div>--%>
                        <!-- /.col -->
                        <div class="col-4">
                            <button type="submit" class="btn btn-primary btn-block">Registrar</button>
                        </div>
                        <!-- /.col -->
                    </div>
                </form>
                <br />
                <a href="WebLogIn.aspx" class="text-center">Regresar Login</a>
            </div>
            <!-- /.form-box -->
        </div>
        <!-- /.card -->
    </div>
    <!-- /.register-box -->

    <!-- jQuery -->
    <script src="../../plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- AdminLTE App -->
    <script src="../../dist/js/adminlte.min.js"></script>
    
    <script>
    
    </script>

</body>
</html>
