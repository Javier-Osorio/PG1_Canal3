<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebUbicacion.aspx.cs" Inherits="WebApp.WebForms.Programacion.WebUbicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Ubicacion Backups</h3>

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
                    <asp:GridView ID="tabla_ubicaciones" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="ID_UBICACION" CssClass="table table-hover text-nowrap" OnRowEditing="tabla_ubicaciones_RowEditing" OnRowCancelingEdit="tabla_ubicaciones_RowCancelingEdit" OnRowUpdating="tabla_ubicaciones_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="ID Ubicacion">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("ID_UBICACION")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("NOMBRE")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNombre" Text='<%# Eval("NOMBRE")%>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNombreFooter" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicacion">
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("PATH_UBICACION")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUbicacion" Text='<%# Eval("PATH_UBICACION")%>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUbicacionFooter" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
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
                    </asp:GridView>
                </div>
            </div>
            <!-- /.card -->
        </div>
    </div>
    <!-- /.row -->

</asp:Content>
