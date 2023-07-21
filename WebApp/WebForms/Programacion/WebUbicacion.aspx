<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebUbicacion.aspx.cs" Inherits="WebApp.WebForms.Programacion.WebUbicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />

    <script = "text/javascript" >        

        function confirmarEliminar() {
            var respuesta = confirm("¿Deseas eliminar el registro?");

            if (respuesta == true) {
                return true;
            } else {
                return false;
            }
        }

        function validarFormularioRegister() {
            var nombre = document.getElementById('<%=txtNombreRegister.ClientID%>').value;
            var ubicar = document.getElementById('<%=txtUbicacionRegister.ClientID%>').value;
            var cambiarNombre = document.getElementById('<%=txtNombreRegister.ClientID%>');
            var cambiarUbi = document.getElementById('<%=txtUbicacionRegister.ClientID%>');

            if (nombre.trim() === '' && ubicar.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarUbi.classList.add('is-invalid');
                return false; // Evita que se envíe el formulario
            } else if (nombre.trim() != '' && ubicar.trim() === '') {
                cambiarNombre.classList.remove('is-invalid');
                cambiarUbi.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && ubicar.trim() != '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarUbi.classList.remove('is-invalid');
                return false;
            }
            // Si todos los campos son válidos, permitir el envío del formulario

            cambiarUbi.classList.remove('is-invalid');
            cambiarNombre.classList.remove('is-invalid');
            return true;
        }

        function validarFormularioEdit() {
            var nombre = document.getElementById('<%=txtNombreEdit.ClientID%>').value;
            var ubicar = document.getElementById('<%=txtUbicacionEdit.ClientID%>').value;
            var cambiarNombre = document.getElementById('<%=txtNombreEdit.ClientID%>');
            var cambiarUbi = document.getElementById('<%=txtUbicacionEdit.ClientID%>');

            if (nombre.trim() === '' && ubicar.trim() === '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarUbi.classList.add('is-invalid');
                return false; // Evita que se envíe el formulario
            } else if (nombre.trim() != '' && ubicar.trim() === '') {
                cambiarNombre.classList.remove('is-invalid');
                cambiarUbi.classList.add('is-invalid');
                return false;
            } else if (nombre.trim() === '' && ubicar.trim() != '') {
                cambiarNombre.classList.add('is-invalid');
                cambiarUbi.classList.remove('is-invalid');
                return false;
            }
            // Si todos los campos son válidos, permitir el envío del formulario

            cambiarUbi.classList.remove('is-invalid');
            cambiarNombre.classList.remove('is-invalid');
            return true;
        }

        function limpiarInputsR() {
            document.getElementById('<%=txtNombreRegister.ClientID%>').value = '';
            document.getElementById('<%=txtUbicacionRegister.ClientID%>').value = '';

           $('#modalRegistrar').modal('hide');
        }
        
    </script> 
    <div class="row">
        <div class="col-12">
            <div class="card">                
                <!-- /.card -->
                <div class="card-header">
                    <h3 class="card-title">Ubicacion Backups</h3>
                    <br /><br />
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalRegistrar">Registrar</button>
                    <div class="card-tools">
                        <div class="input-group input-group-sm" style="width: 200px;">
                            <input type="text" name="table_search" class="form-control float-right" placeholder="Search">
                            
                            <div class="input-group-append">
                                <button type="submit" class="btn btn-default">
                                    <i class="fas fa-search"></i>
                                </button>
                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnPruebaPDF" runat="server" Text="Generar PDF Prueba" OnClick="btnPruebaPDF_Click" CssClass="btn btn-block btn-outline-primary" />
                    </div>
                </div>
                <!-- /.card-header -->
                <div class="card-body table-responsive p-0">
                    <asp:GridView ClientIDMode="Static" AutoGenerateColumns="false" DataKeyNames="ID_UBICACION" ID="tabla_ubicaciones" runat="server" AllowPaging="true" CssClass="table table-hover text-nowrap"
                        OnPageIndexChanging="tabla_ubicaciones_PageIndexChanging" OnRowEditing="tabla_ubicaciones_RowEditing"
                        OnRowDeleting="tabla_ubicaciones_RowDeleting" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false">
                        <Columns>
                            <asp:BoundField HeaderText="Id Ubicacion" DataField="ID_UBICACION" ReadOnly="true" />
                            <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                            <asp:BoundField HeaderText="Ubicacion" DataField="PATH_UBICACION" />
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

                <asp:Panel ID="pnlUbicacionRegister" runat="server" DefaultButton="btnRegistrar">
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
                            <label for="txtUbicacionRegister">Ubicacion:</label>
                            <input runat="server" type="text" name="" class="form-control" id="txtUbicacionRegister" placeholder="" />
                        </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-danger" onclick="limpiarInputsR()">Cancelar</button>
                    <asp:Button ID="btnRegistrar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" OnClientClick="return validarFormularioRegister();" />
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

                <asp:Panel ID="pnlUbicacionEdit" runat="server" DefaultButton="btnActualizar">
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
                            <label for="txtUbicacionEdit">Ubicacion:</label>
                            <input runat="server" type="text" name="" class="form-control" id="txtUbicacionEdit" placeholder="" />
                        </div>
                        <input runat="server" id="codUbi" type="hidden"/>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnActualizar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnActualizar_Click" OnClientClick="return validarFormularioEdit();" />
                </div>
                </asp:Panel>
                
            </div>
            
        </div>
    </div>
    <!-- /.modal-content -->

</asp:Content>
