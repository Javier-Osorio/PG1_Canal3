<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebProgramacionPeliculas.aspx.cs" Inherits="WebApp.WebForms.Programacion.WebProgramacionPeliculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Backup Peliculas</h3>

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
                    <table class="table table-hover text-nowrap">
                        <thead>
                            <tr>
                                <th>ID_Pelicula</th>
                                <th>Nombre</th>
                                <th>Fecha_Backup</th>
                                <th>Observaciones</th>
                                <th>Tipo_Pelicula</th>
                                <th>Casa_Productora</th>
                                <th>Ubicacion</th>
                                <th>Estado</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>Spiderman</td>
                                <td>11/12/2002</td>
                                <td>Observaciones de la pelicula</td>
                                <td>Animado</td>
                                <td>Sony</td>
                                <td>Cinta LTO</td>
                                <td><span class="badge bg-success">COMPLETO</span></td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>El Exorcista</td>
                                <td>21/11/2022</td>
                                <td>Observaciones de la pelicula</td>
                                <td>Terror</td>
                                <td>Warner Bros</td>
                                <td>Cinta LTO</td>
                                <td><span class="badge bg-warning">EN BLOQUES</span></td>
                            </tr>
                            
                        </tbody>
                    </table>
                </div>
                <!-- /.card-body -->
                <div class="card-footer clearfix">
                    <ul class="pagination pagination-sm m-0 float-right">
                        <li class="page-item"><a class="page-link" href="#">&laquo;</a></li>
                        <li class="page-item"><a class="page-link" href="#">1</a></li>
                        <li class="page-item"><a class="page-link" href="#">2</a></li>
                        <li class="page-item"><a class="page-link" href="#">3</a></li>
                        <li class="page-item"><a class="page-link" href="#">&raquo;</a></li>
                    </ul>
                </div>
            </div>
            <!-- /.card -->
        </div>
    </div>
    <!-- /.row -->
</asp:Content>
