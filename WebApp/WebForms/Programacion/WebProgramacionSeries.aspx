﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SitioWeb.Master" AutoEventWireup="true" CodeBehind="WebProgramacionSeries.aspx.cs" Inherits="WebApp.WebForms.Programacion.WebProgramacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <link rel="stylesheet" runat="server" href="../../plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <link rel="stylesheet" runat="server" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"/>
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

        function validarFormularioRegister() {
            var epi_min = document.getElementById('<%=txtEpisodioMin.ClientID%>').value;
            var cambiarEpi_min = document.getElementById('<%=txtEpisodioMin.ClientID%>');
            var epi_max = document.getElementById('<%=txtEpisodioMax.ClientID%>').value;
            var cambiarEpi_max = document.getElementById('<%=txtEpisodioMax.ClientID%>');


            if (epi_min.trim() === '' && epi_max.trim() === '') {
                cambiarEpi_min.classList.add('is-invalid');
                cambiarEpi_max.classList.add('is-invalid');
                return false; // Evita que se envíe el formulario
            } else if (epi_min.trim() != '' && epi_max.trim() === '') {
                cambiarEpi_min.classList.remove('is-invalid');
                cambiarEpi_max.classList.add('is-invalid');
                return false;
            } else if (epi_min.trim() === '' && epi_max.trim() != '') {
                cambiarEpi_min.classList.add('is-invalid');
                cambiarEpi_max.classList.remove('is-invalid');
                return false;
            }
            // Si todos los campos son válidos, permitir el envío del formulario
            cambiarEpi_min.classList.remove('is-invalid');
            cambiarEpi_max.classList.remove('is-invalid');
            return true;
        }

        function validarFormularioEdit() {
            var epi_min = document.getElementById('<%=txtEpisodioMinEditar.ClientID%>').value;
            var cambiarEpi_min = document.getElementById('<%=txtEpisodioMinEditar.ClientID%>');
            var epi_max = document.getElementById('<%=txtEpisodioMaxEditar.ClientID%>').value;
            var cambiarEpi_max = document.getElementById('<%=txtEpisodioMaxEditar.ClientID%>');


            if (epi_min.trim() === '' && epi_max.trim() === '') {
                cambiarEpi_min.classList.add('is-invalid');
                cambiarEpi_max.classList.add('is-invalid');
                return false; // Evita que se envíe el formulario
            } else if (epi_min.trim() != '' && epi_max.trim() === '') {
                cambiarEpi_min.classList.remove('is-invalid');
                cambiarEpi_max.classList.add('is-invalid');
                return false;
            } else if (epi_min.trim() === '' && epi_max.trim() != '') {
                cambiarEpi_min.classList.add('is-invalid');
                cambiarEpi_max.classList.remove('is-invalid');
                return false;
            }
            // Si todos los campos son válidos, permitir el envío del formulario
            cambiarEpi_min.classList.remove('is-invalid');
            cambiarEpi_max.classList.remove('is-invalid');
            return true;
        }

        function limpiarInputsR() {
            document.getElementById('<%=txtEpisodioMin.ClientID%>').value = '';
            document.getElementById('<%=txtEpisodioMax.ClientID%>').value = '';
            document.getElementById('<%=txtObservaciones.ClientID%>').value = '';

           $('#modalRegistrar').modal('hide');
        }
        //$(function() {
        //    $('#select2').select2({});
        //});
    </script> 
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Backup Series</h3>
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
                    <asp:GridView ClientIDMode="Static" AutoGenerateColumns="false" DataKeyNames="ID_BACKUP_SERIE" ID="tabla_programacion_serie" runat="server" AllowPaging="true" CssClass="table table-hover text-nowrap"
                        OnPageIndexChanging="tabla_programacion_serie_PageIndexChanging" OnRowEditing="tabla_programacion_serie_RowEditing"
                        OnRowDeleting="tabla_programacion_serie_RowDeleting" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false">
                        <Columns>
                            <asp:BoundField HeaderText="ID_Backup_Serie" DataField="ID_BACKUP_SERIE" ReadOnly="true" />
                            <asp:BoundField HeaderText="Nombre" DataField="NOMBRE_SERIE" />
                            <asp:BoundField HeaderText="Fecha_Backup" DataField="FECHA_BACKUP" />
                            <asp:BoundField HeaderText="Episodio_Min" DataField="CANTIDAD_EPISODIO_MIN" />
                            <asp:BoundField HeaderText="Episodio_Max" DataField="CANTIDAD_EPISODIO_MAX" />
                            <asp:BoundField HeaderText="Observaciones" DataField="OBSERVACIONES" />
                            <asp:BoundField HeaderText="Tipo_Serie" DataField="TIPO_SERIE" />
                            <asp:BoundField HeaderText="Casa_Productora" DataField="CASA_PRODUCTORA" />
                            <asp:BoundField HeaderText="Ubicacion_Cinta " DataField="UBICACION_CINTA" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <%# (string)Eval("ESTADO")=="COMPLETO" ? "<span class=\"badge bg-success\">COMPLETO</span>" : "<span class=\"badge bg-warning\">EN BLOQUES</span>" %>
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

                <asp:Panel ID="pnlSeriesRegister" runat="server" DefaultButton="btnRegistrar">
                    <div class="modal-header">
                    <h4 class="modal-title">Nuevo Registro</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="">Nombre de Serie:</label>
                        <asp:DropDownList ID="ddlNombreMaterial" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtEpisodioMin">Episodio Min:</label>
                                <input runat="server" type="number" class="form-control" id="txtEpisodioMin" placeholder="" min="1" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtEpisodioMax">Episodio Max:</label>
                                <input runat="server" type="number" class="form-control" id="txtEpisodioMax" placeholder="" min="1" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtObservaciones">Observacion:</label>
                        <textarea runat="server" id="txtObservaciones" cols="20" rows="3" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label for="">Tipo de Serie:</label>
                        <asp:DropDownList ID="ddlTipoSerie" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="">Casa Productora:</label>
                        <asp:DropDownList ID="ddlCasaProductora" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="">Ubicacion Cinta:</label>
                        <asp:DropDownList ID="ddlUbicacion" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="">Estado:</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="COMPLETO"></asp:ListItem>
                            <asp:ListItem Value="0" Text="EN BLOQUES"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="limpiarInputsR()">Cancelar</button>
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

                <asp:Panel ID="pnlSeriesEdit" runat="server" DefaultButton="btnActualizar">
                    <div class="modal-header">
                    <h4 class="modal-title">Editar Registro</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="">Nombre de Serie:</label>
                        <asp:DropDownList ID="ddlNombreEditar" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtEpiMin">Episodio Min:</label>
                                <input runat="server" type="number" class="form-control" id="txtEpisodioMinEditar" placeholder="" min="1" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtEpiMax">Episodio Max:</label>
                                <input runat="server" type="number" class="form-control" id="txtEpisodioMaxEditar" placeholder="" min="1" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtObserEditar">Observacion:</label>
                        <textarea runat="server" id="txtObserEditar" cols="20" rows="3" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label for="">Tipo de Serie:</label>
                        <asp:DropDownList ID="ddlTipoEditar" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="">Casa Productora:</label>
                        <asp:DropDownList ID="ddlCasaEditar" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="">Ubicacion Cinta:</label>
                        <asp:DropDownList ID="ddlUbicacionEditar" runat="server" CssClass="form-control">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="">Estado:</label>
                        <asp:DropDownList ID="ddlEstadoEditar" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <input runat="server" id="codBackupSerie" type="hidden"/>
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
