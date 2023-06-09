﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebRoles.aspx.cs" Inherits="WebApp.WebForms.Login.WebRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" runat="server" href="../../plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link rel="stylesheet" runat="server" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js" />
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>

    <script = "text/javascript" >        

        function confirmarEliminar() {
            var respuesta = confirm("¿Deseas eliminar el registro?");

            if (respuesta == true) {
                return true;
            } else {
                return false;
            }
        }
    </script> 
    <div class="row">
        <div class="col-12">
            <div class="card">
                <!-- /.card -->
                <div class="card-header">
                    <h3 class="card-title">Roles - Inicio Sesion</h3>
                    <br />
                    <br />
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
                    </div>
                </div>
                <!-- /.card-header -->
                <div class="card-body table-responsive p-0">
                    <asp:GridView ClientIDMode="Static" AutoGenerateColumns="false" DataKeyNames="ID_ROL" ID="tabla_roles" runat="server" AllowPaging="true" CssClass="table table-hover text-nowrap"
                        OnPageIndexChanging="tabla_roles_PageIndexChanging" OnRowEditing="tabla_roles_RowEditing"
                        OnRowDeleting="tabla_roles_RowDeleting" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false">
                        <Columns>
                            <asp:BoundField HeaderText="Id_Rol" DataField="ID_ROL" ReadOnly="true" />
                            <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
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
                        <asp:Label ID="lblMensajeRegistro" runat="server" ForeColor="Red"></asp:Label>
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
                    <asp:Button ID="btnRegistrar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                </div>
            </div>

        </div>
    </div>
    <!-- /.modal-content -->

    <!--modal-content -->
    <div class="modal fade" id="modalEditar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
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
                        <label for="">Estado:</label>
                        <asp:DropDownList ID="ddlEstadoEdit" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <input runat="server" id="codRol" type="hidden" />
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnActualizar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnActualizar_Click" />
                </div>
            </div>

        </div>
    </div>
    <!-- /.modal-content -->
</asp:Content>
