<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebUbicacion.aspx.cs" Inherits="WebApp.WebForms.Programacion.WebUbicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <asp:GridView ID="tabla_ubicacion" runat="server" CssClass="table table-hover text-nowrap" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" OnPageIndexChanging="tabla_ubicacion_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="ID_Ubicacion" SortExpression="ID_UBICACION">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" Text='<%# Eval("ID_UBICACION")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("ID_UBICACION")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" SortExpression="NOMBRE">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Eval("NOMBRE")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("NOMBRE")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicacion" SortExpression="PATH_UBICACION">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" Text='<%# Eval("PATH_UBICACION")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("PATH_UBICACION")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="ID_UBICACION" HeaderText="ID_Ubicacion" ReadOnly="true" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                            <asp:BoundField DataField="PATH_UBICACION" HeaderText="Ubicacion" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnPopUp" runat="server" Text="Prueba" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="&laquo;" LastPageText="&raquo;" />
                    </asp:GridView>
                </div>
            </div>
            <!-- /.card -->
        </div>
    </div>
    <!-- /.row -->

</asp:Content>
