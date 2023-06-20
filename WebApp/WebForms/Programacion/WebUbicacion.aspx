<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebUbicacion.aspx.cs" Inherits="WebApp.WebForms.Programacion.WebUbicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />

    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card card-primary">
                    <div class="card-header">
                        <h3 class="card-title">Ubicacion BackUps<small></small></h3>
                    </div>
                    <!-- /.card-header -->
                    <!-- form start -->
                        <div class="card-body">
                            <div class="form-group">
                                <label for="nombre">Nombre:</label>
                                <input runat="server" type="text" name="" class="form-control" id="txtNombre" placeholder="" />
                            </div>
                            <div class="form-group">
                                <label for="ubicacion">Ubicacion:</label>
                                <input runat="server" type="text" name="" class="form-control" id="txtUbicacion" placeholder="" />
                            </div>
                            <asp:HiddenField runat="server" ID="idOculto"/>
                        </div>
                        <!-- /.card-body -->
                        <div class="card-footer">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        </div>
                </div>
                <br />
                <!-- /.card -->
                <div class="card-header">
                    <%--<h3 class="card-title">Ubicacion Backups</h3>--%>

                    <div class="card-tools">
                        <div class="input-group input-group-sm" style="width: 150px;">
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
                    <asp:GridView ClientIDMode="Static" AutoGenerateColumns="false" DataKeyNames="ID_UBICACION" ID="tabla_ubicaciones" runat="server" AllowPaging="true" CssClass="table table-hover text-nowrap"
                        OnPageIndexChanging="tabla_ubicaciones_PageIndexChanging" OnRowEditing="tabla_ubicaciones_RowEditing" OnRowCancelingEdit="tabla_ubicaciones_RowCancelingEdit"
                        OnRowDeleting="tabla_ubicaciones_RowDeleting" OnRowUpdating="tabla_ubicaciones_RowUpdating">
                        <Columns>
                            <asp:BoundField HeaderText="Id Ubicacion" DataField="ID_UBICACION" ReadOnly="true"/>
                            <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                            <asp:BoundField HeaderText="Ubicacion" DataField="PATH_UBICACION" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Text="Editar" CommandName="Edit"></asp:LinkButton>
                                    <asp:LinkButton runat="server" Text="Eliminar" CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton runat="server" Text="Aceptar" CommandName="Update"></asp:LinkButton>
                                    <asp:LinkButton runat="server" Text="Cancelar" CommandName="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
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

</asp:Content>
