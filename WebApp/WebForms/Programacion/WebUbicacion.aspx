<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebUbicacion.aspx.cs" Inherits="WebApp.WebForms.Programacion.WebUbicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <asp:GridView ID="tabla_ubicacion" runat="server" CssClass="table table-hover text-nowrap" PageSize="10" AllowPaging="true" OnPageIndexChanging="tabla_ubicacion_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ID_UBICACION" HeaderText="ID_Ubicacion" ReadOnly="true"/>
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre"/>
                            <asp:BoundField DataField="PATH_UBICACION" HeaderText="Ubicacion"/>
                        </Columns>
                        <PagerStyle CssClass=""/>
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="&laquo;" LastPageText="&raquo;" />
                    </asp:GridView>
                    <%--<table class="table table-hover text-nowrap">
                        <thead>
                            <tr>
                                <th>ID_Ubicacion</th>
                                <th>Nombre</th>
                                <th>Path</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>GA 01 al 05</td>
                                <td>url/GA 01 al 05</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>GA 01 al 05</td>
                                <td>url/GA 01 al 05</td>
                            </tr>
                            
                        </tbody>
                    </table>--%>
                </div>
                <!-- /.card-body -->
                <%--<div class="card-footer clearfix">
                    <ul class="pagination pagination-sm m-0 float-right">
                        <li class="page-item"><a class="page-link" href="#">&laquo;</a></li>
                        <li class="page-item"><a class="page-link" href="#">1</a></li>
                        <li class="page-item"><a class="page-link" href="#">2</a></li>
                        <li class="page-item"><a class="page-link" href="#">3</a></li>
                        <li class="page-item"><a class="page-link" href="#">&raquo;</a></li>
                    </ul>
                </div>--%>
            </div>
            <!-- /.card -->
        </div>
    </div>
    <!-- /.row -->
</asp:Content>
