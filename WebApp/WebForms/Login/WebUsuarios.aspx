<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebUsuarios.aspx.cs" Inherits="WebApp.WebForms.Login.WebUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" runat="server" href="../../plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link rel="stylesheet" runat="server" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script src="../../dist/js/validacionForms.js"></script>

    <script = "text/javascript" >        

        function confirmarEliminar() {
            var respuesta = confirm("¿Deseas eliminar el registro?");

            if (respuesta == true) {
                return true;
            } else {
                return false;
            }
        }

        function veificarContra(con, con2) {
            
        }

        function validarFormularioRegisterUsuarios() {
            var nombre = document.getElementById('<%=txtNombreRegister.ClientID%>').value;
            var apellido = document.getElementById('<%=txtApellidoRegister.ClientID%>').value;
            var correo = document.getElementById('<%=txtCorreoRegister.ClientID%>').value;
            var contra = document.getElementById('<%=txtContraRegister.ClientID%>').value;
            var conContra = document.getElementById('<%=txtContraConfirmRegister.ClientID%>').value;
            var cambiarNombre = document.getElementById('<%=txtNombreRegister.ClientID%>');
            var cambiarApellido = document.getElementById('<%=txtApellidoRegister.ClientID%>');
            var cambiarCorreo = document.getElementById('<%=txtCorreoRegister.ClientID%>');
            var cambiarPass = document.getElementById('<%=txtContraRegister.ClientID%>');
            var cambiarconContra = document.getElementById('<%=txtContraConfirmRegister.ClientID%>');

            var regexEmail = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
            var regexContra = '^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_=+[\]{}|;:,.<>/?]).{7,15}$';


            if (nombre.trim() === '' && apellido.trim() === '' && correo.trim() === '' && contra.trim() === '' && conContra.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.add('is-invalid');
                cambiarCorreo.classList.add('is-invalid');
                cambiarPass.classList.add('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false; // Evita que se envíe el formulario
            } else if (nombre.trim() != '' && apellido.trim() === '' && correo.trim() === '' && contra.trim() === '' && conContra.trim() === '') {
                cambiarNombre.classList.remove('is-invalid');
                cambiarApellido.classList.add('is-invalid');
                cambiarCorreo.classList.add('is-invalid');
                cambiarPass.classList.add('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && apellido.trim() != '' && correo.trim() === '' && contra.trim() === '' && conContra.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.remove('is-invalid');
                cambiarCorreo.classList.add('is-invalid');
                cambiarPass.classList.add('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && apellido.trim() === '' && correo.trim() != '' && contra.trim() === '' && conContra.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.add('is-invalid');
                cambiarCorreo.classList.remove('is-invalid');
                cambiarPass.classList.add('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && apellido.trim() === '' && correo.trim() === '' && contra.trim() != '' && conContra.trim() === '') { 
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.add('is-invalid');
                cambiarCorreo.classList.add('is-invalid');
                cambiarPass.classList.remove('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && apellido.trim() === '' && correo.trim() === '' && contra.trim() === '' && conContra.trim() != '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.add('is-invalid');
                cambiarCorreo.classList.add('is-invalid');
                cambiarPass.classList.add('is-invalid');
                cambiarconContra.classList.remove('is-invalid');
                return false;
            } else if (nombre.trim() != '' && apellido.trim() != '' && correo.trim() === '' && contra.trim() === '' && conContra.trim() === '') {
                cambiarNombre.classList.remove('is-invalid');
                cambiarApellido.classList.remove('is-invalid');
                cambiarCorreo.classList.add('is-invalid');
                cambiarPass.classList.add('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && apellido.trim() != '' && correo.trim() != '' && contra.trim() === '' && conContra.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.remove('is-invalid');
                cambiarCorreo.classList.remove('is-invalid');
                cambiarPass.classList.add('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && apellido.trim() === '' && correo.trim() != '' && contra.trim() != '' && conContra.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.add('is-invalid');
                cambiarCorreo.classList.remove('is-invalid');
                cambiarPass.classList.remove('is-invalid');
                cambiarconContra.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && apellido.trim() === '' && correo.trim() === '' && contra.trim() != '' && conContra.trim() != '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarApellido.classList.add('is-invalid');
                cambiarCorreo.classList.add('is-invalid');
                cambiarPass.classList.remove('is-invalid');
                cambiarconContra.classList.remove('is-invalid');
                return false;
            }
            //} else if (nombre.trim() != '' && apellido.trim() != '' && correo.trim() != '' && contra.trim() != '' && conContra.trim() != '') {
            //    cambiarNombre.classList.remove('is-invalid');
            //    cambiarApellido.classList.remove('is-invalid');
            //    cambiarCorreo.classList.remove('is-invalid');
            //    cambiarPass.classList.remove('is-invalid');
            //    cambiarconContra.classList.remove('is-invalid');
                
            //}
            
            //return true;
        }

        function validarFormularioEditUsuarios() {
            var nombre = document.getElementById('<%=txtNombreEdit.ClientID%>').value;
            var apellido = document.getElementById('<%=txtApellidoEdit.ClientID%>').value;
            var correo = document.getElementById('<%=txtCorreoEdit.ClientID%>').value;
            var cambiarNombre = document.getElementById('<%=txtNombreEdit.ClientID%>');
            var cambiarApellido = document.getElementById('<%=txtApellidoEdit.ClientID%>');
            var cambiarCorreo = document.getElementById('<%=txtCorreoEdit.ClientID%>');

            if (nombre.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                return false; // Evita que se envíe el formulario
            }
            // Si todos los campos son válidos, permitir el envío del formulario
            cambiarNombre.classList.remove('is-invalid');
            return true;
        }
    </script> 
    <div class="row">
        <div class="col-12">
            <div class="card">
                <!-- /.card -->
                <div class="card-header">
                    <h3 class="card-title">Usuarios - Inicio Sesion</h3>
                    <br />
                    <br />
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalRegistrar">Registrar</button>
                    <div class="card-tools">
                        <%--<div class="input-group input-group-sm" style="width: 200px;">
                            <input type="text" name="table_search" class="form-control float-right" placeholder="Search">

                            <div class="input-group-append">
                                <button type="submit" class="btn btn-default">
                                    <i class="fas fa-search"></i>
                                </button>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <!-- /.card-header -->
                <div class="card-body table-responsive p-0">
                    <asp:GridView ClientIDMode="Static" AutoGenerateColumns="false" DataKeyNames="ID_USUARIO" ID="tabla_usuarios" runat="server" AllowPaging="true" CssClass="table table-hover text-nowrap"
                        OnPageIndexChanging="tabla_usuarios_PageIndexChanging" OnRowEditing="tabla_usuarios_RowEditing"
                        OnRowDeleting="tabla_usuarios_RowDeleting" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="ID_USUARIO" ReadOnly="true" />
                            <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                            <asp:BoundField HeaderText="Apellido" DataField="APELLIDO" />
                            <asp:BoundField HeaderText="Usuario" DataField="USUARIO" />
                            <asp:BoundField HeaderText="Correo electronico" DataField="CORREO" />
                            <asp:BoundField HeaderText="Contraseña" DataField="CONTRA" />
                            <asp:BoundField HeaderText="Rol" DataField="ROL" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <%# (string)Eval("ESTADO")=="ACTIVO" ? "<span class=\"badge bg-success\">ACTIVO</span>" : "<span class=\"badge bg-danger\">INACTIVO</span>" %>                                                                       
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="Edit" CssClass="btn btn-info btn-sm"><i class="fas fa-pencil-alt"></i> Editar</asp:LinkButton>
                                    <asp:LinkButton runat="server" CommandName="Delete" CssClass="btn btn-danger btn-sm" OnClientClick="return confirmarEliminar();"><i class="fas fa-trash"></i> Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" />
                    </asp:GridView>
                </div>
            </div>
            <!-- /.card -->
        </div>
    </div>
    <!-- /.row -->

    <!--modal-content -->
    <div class="modal fade" id="modalRegistrar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:Panel ID="pnlUsuariosRegister" runat="server" DefaultButton="btnRegistrar">
                    <div class="modal-header">
                    <h4 class="modal-title">Nuevo Registro</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="txtNombreRegister">Nombre:</label>
                        <input runat="server" type="text" name="" class="form-control" id="txtNombreRegister" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellidoRegister">Apellido:</label>
                        <input runat="server" type="text" name="" class="form-control" id="txtApellidoRegister" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="txtCorreoRegister">Correo electronico:</label>
                        <input runat="server" type="email" name="" class="form-control" id="txtCorreoRegister" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="txtContraRegister">Contraseña:</label>
                        <input runat="server" type="password" name="" class="form-control" id="txtContraRegister" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="txtContraConfirmRegister">Confirmar Contraseña:</label>
                        <input runat="server" type="password" name="" class="form-control" id="txtContraConfirmRegister" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="">Rol:</label>
                        <asp:DropDownList ID="ddlRolRegister" runat="server" CssClass="form-control">
                        </asp:DropDownList>                        
                    </div>
                    <div class="form-group">
                        <label for="">Estado:</label>
                        <asp:DropDownList ID="ddlEstadoRegister" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="ACTIVO"></asp:ListItem>
                            <asp:ListItem Value="0" Text="INACTIVO"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnRegistrar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" OnClientClick="return validarFormularioRegisterUsuarios();" />
                </div>
                </asp:Panel>
                
            </div>

        </div>
    </div>
    <!-- /.modal-content -->

    <!--modal-content -->
    <div class="modal fade" id="modalEditar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:Panel ID="pnlUsuariosEdit" runat="server" DefaultButton="btnActualizar">
                    <div class="modal-header">
                    <h4 class="modal-title">Editar Registro</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="txtNombreEdit">Nombre:</label>
                        <input runat="server" type="text" name="" class="form-control" id="txtNombreEdit" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellidoEdit">Apellido:</label>
                        <input runat="server" type="text" name="" class="form-control" id="txtApellidoEdit" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="txtCorreoEdit">Correo electronico:</label>
                        <input runat="server" type="email" name="" class="form-control" id="txtCorreoEdit" placeholder="" />
                    </div>
                    <%--<div class="form-group">
                        <label for="txtContraEdit">Contraseña:</label>
                        <input runat="server" type="password" name="" class="form-control" id="txtContraEdit" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label for="txtContraConfirmEdit">Confirmar Contraseña:</label>
                        <input runat="server" type="password" name="" class="form-control" id="txtContraConfirmEdit" placeholder="" />
                    </div>--%>
                    <div class="form-group">
                        <label for="">Rol:</label>
                        <asp:DropDownList ID="ddlRolEdit" runat="server" CssClass="form-control">
                        </asp:DropDownList>                        
                    </div>
                    <div class="form-group">
                        <label for="">Estado:</label>
                        <asp:DropDownList ID="ddlEstadoEdit" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="ACTIVO"></asp:ListItem>
                            <asp:ListItem Value="0" Text="INACTIVO"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <input runat="server" id="codUsuario" type="hidden" />
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnActualizar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnActualizar_Click" OnClientClick="return validarFormularioEditUsuarios();" />
                </div>
                </asp:Panel>
                
            </div>

        </div>
    </div>
    <!-- /.modal-content -->
</asp:Content>
