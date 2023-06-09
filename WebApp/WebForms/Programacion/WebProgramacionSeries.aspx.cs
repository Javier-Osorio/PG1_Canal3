﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Dao.Programacion;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.WebForms.Programacion
{
    public partial class WebProgramacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaBackupSerie();
                    LlenarListados();
                }
                else
                    SetBackupSerie();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }
        Dao_Backup_Serie dao = new Dao_Backup_Serie();
        Backup_series series = new Backup_series();
        Dao_Nombre_Serie dao_Nombre = new Dao_Nombre_Serie();
        Dao_Casa_Productora dao_Casa = new Dao_Casa_Productora();
        Dao_Tipo_Serie dao_Tipo_Serie = new Dao_Tipo_Serie();
        Dao_Ubicacion dao_Ubicacion = new Dao_Ubicacion();
        ManejoError error = new ManejoError();

        void CargaBackupSerie()
        {
            if (dao.GetBackup_Serie())
            {
                tabla_programacion_serie.DataSource = dao.DsReturn.Tables["backup_serie"];
                tabla_programacion_serie.DataBind();
                Session["backup_s"] = dao.DsReturn;
            }
        }
        void SetBackupSerie()
        {
            tabla_programacion_serie.DataSource = ((DataSet)Session["backup_s"]);
            tabla_programacion_serie.DataBind();
        }

        void LlenarListados()
        {
            if (dao_Nombre.GetNombre_Serie())
            {
                ddlNombreMaterial.DataSource = dao_Nombre.DsReturn.Tables["nombres_series"];                
                ddlNombreMaterial.DataTextField = dao_Nombre.DsReturn.Tables["nombres_series"].Columns["NOMBRE"].ToString();
                ddlNombreMaterial.DataValueField = dao_Nombre.DsReturn.Tables["nombres_series"].Columns["ID_NOMBRE_SERIE"].ToString();
                ddlNombreMaterial.DataBind();
            }
            if (dao_Casa.GetCasa_Productora())
            {
                ddlCasaProductora.DataSource = dao_Casa.DsReturn.Tables["casa_productora"];
                ddlCasaProductora.DataTextField = dao_Casa.DsReturn.Tables["casa_productora"].Columns["NOMBRE"].ToString();
                ddlCasaProductora.DataValueField = dao_Casa.DsReturn.Tables["casa_productora"].Columns["ID_CASA_PRODUCTORA"].ToString();
                ddlCasaProductora.DataBind();
            }
            if (dao_Tipo_Serie.GetTipo_Serie())
            {
                ddlTipoSerie.DataSource = dao_Tipo_Serie.DsReturn.Tables["tipos_series"];
                ddlTipoSerie.DataTextField = dao_Tipo_Serie.DsReturn.Tables["tipos_series"].Columns["NOMBRE"].ToString();
                ddlTipoSerie.DataValueField = dao_Tipo_Serie.DsReturn.Tables["tipos_series"].Columns["ID_TIPO_SERIE"].ToString();
                ddlTipoSerie.DataBind();
            }
            if (dao_Ubicacion.GetUbicaciones())
            {
                ddlUbicacion.DataSource = dao_Ubicacion.DsReturn.Tables["ubicacion"];
                ddlUbicacion.DataTextField = dao_Ubicacion.DsReturn.Tables["ubicacion"].Columns["NOMBRE"].ToString();
                ddlUbicacion.DataValueField = dao_Ubicacion.DsReturn.Tables["ubicacion"].Columns["ID_UBICACION"].ToString();
                ddlUbicacion.DataBind();
            }
        }

        void limpiarTextos()
        {
            txtEpisodioMax.Value = "";
            txtEpisodioMin.Value = "";
            txtObservaciones.Value = "";
            ddlEstado.SelectedIndex = 0;
            ddlCasaProductora.SelectedIndex = 0;
            ddlNombreMaterial.SelectedIndex = 0;
            ddlTipoSerie.SelectedIndex = 0;
            ddlUbicacion.SelectedIndex = 0;
        }
        void limpiarDDLS()
        {
            ddlEstadoEditar.Items.Clear();
            ddlCasaEditar.Items.Clear();
            ddlNombreEditar.Items.Clear();
            ddlTipoEditar.Items.Clear();
            ddlUbicacionEditar.Items.Clear();
        }
        void LlenarDDLSEditar()
        {
            if (dao_Nombre.GetNombre_Serie())
            {
                ListItem item;
                foreach (DataRow list in dao_Nombre.DsReturn.Tables["nombres_series"].Rows)
                {
                    string idnom = list["ID_NOMBRE_SERIE"].ToString();
                    string nom = list["NOMBRE"].ToString();
                    item = new ListItem(nom, idnom);
                    ddlNombreEditar.Items.Add(item);
                }
            }
            if (dao_Casa.GetCasa_Productora())
            {
                ListItem item;
                foreach (DataRow list in dao_Casa.DsReturn.Tables["casa_productora"].Rows)
                {
                    string idcasa = list["ID_CASA_PRODUCTORA"].ToString();
                    string casa = list["NOMBRE"].ToString();
                    item = new ListItem(casa, idcasa);
                    ddlCasaEditar.Items.Add(item);
                }
            }
            if (dao_Tipo_Serie.GetTipo_Serie())
            {
                ListItem item;
                foreach (DataRow list in dao_Tipo_Serie.DsReturn.Tables["tipos_series"].Rows)
                {
                    string idtipo = list["ID_TIPO_SERIE"].ToString();
                    string tipo = list["NOMBRE"].ToString();
                    item = new ListItem(tipo, idtipo);
                    ddlTipoEditar.Items.Add(item);
                }
            }
            if (dao_Ubicacion.GetUbicaciones())
            {
                ListItem item;
                foreach (DataRow list in dao_Ubicacion.DsReturn.Tables["ubicacion"].Rows)
                {
                    string idubic = list["ID_UBICACION"].ToString();
                    string ubic = list["NOMBRE"].ToString();
                    item = new ListItem(ubic, idubic);
                    ddlUbicacionEditar.Items.Add(item);
                }
            }
            ListItem listItem;
            listItem = new ListItem("COMPLETO","1");
            ddlEstadoEditar.Items.Add(listItem);
            listItem = new ListItem("EN BLOQUES", "0");
            ddlEstadoEditar.Items.Add(listItem);
        }

        protected void tabla_programacion_serie_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_programacion_serie.PageIndex = e.NewPageIndex;
            tabla_programacion_serie.DataBind();
        }

        protected void tabla_programacion_serie_RowEditing(object sender, GridViewEditEventArgs e)
        {
            limpiarDDLS();
            GridViewRow row = tabla_programacion_serie.Rows[e.NewEditIndex];
            int id = int.Parse(row.Cells[0].Text);
            if (dao.listBackup_Serie(id))
            {
                ListItem item;
                codBackupSerie.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_BACKUP_SERIE"].ToString();
                string idnom = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_NOMBRE_SERIE"].ToString();
                string nom = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["NOMBRE_SERIE"].ToString();
                item = new ListItem(nom, idnom);
                ddlNombreEditar.Items.Add(item);
                txtEpisodioMinEditar.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["CANTIDAD_EPISODIO_MIN"].ToString();
                txtEpisodioMaxEditar.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["CANTIDAD_EPISODIO_MAX"].ToString();
                txtObserEditar.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["OBSERVACIONES"].ToString();
                string idtipo = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_TIPO_SERIE"].ToString();
                string tipo = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["TIPO_SERIE"].ToString();
                item = new ListItem(tipo, idtipo);
                ddlTipoEditar.Items.Add(item);
                string idcasa = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_CASA_PRODUCTORA"].ToString();
                string casa = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["CASA_PRODUCTORA"].ToString();
                item = new ListItem(casa, idcasa);
                ddlCasaEditar.Items.Add(item);
                string idubica = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_UBICACION"].ToString();
                string ubica = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["UBICACION_CINTA"].ToString();
                item = new ListItem(ubica, idubica);
                ddlUbicacionEditar.Items.Add(item);
                string idestado = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_ESTADO"].ToString();
                string estado = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ESTADO"].ToString();
                item = new ListItem(estado, idestado);
                ddlEstadoEditar.Items.Add(item);

                LlenarDDLSEditar();
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_programacion_serie_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            series.ID_backup_serie1 = int.Parse(tabla_programacion_serie.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarBackupSerie(series))
            {
                CargaBackupSerie();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro eliminado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se elimino'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            series.ID_nombre1 = int.Parse(ddlNombreMaterial.SelectedValue);
            series.Cantidad_episodio_min = int.Parse(txtEpisodioMin.Value);
            series.Cantidad_episodio_max = int.Parse(txtEpisodioMax.Value);
            series.Observaciones = txtObservaciones.Value;
            series.ID_tipo_serie1 = int.Parse(ddlTipoSerie.SelectedValue);
            series.ID_casa_productora1 = int.Parse(ddlCasaProductora.SelectedValue);
            series.ID_ubicacion1 = int.Parse(ddlUbicacion.SelectedValue);
            series.Estado = int.Parse(ddlEstado.SelectedValue);

            if (dao.InsertarBackupSerie(series))
            {
                CargaBackupSerie();
                limpiarTextos();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Se registro correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                limpiarTextos();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se guardo'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            series.ID_nombre1 = int.Parse(ddlNombreEditar.SelectedValue);
            series.Cantidad_episodio_min = int.Parse(txtEpisodioMinEditar.Value);
            series.Cantidad_episodio_max = int.Parse(txtEpisodioMaxEditar.Value);
            series.Observaciones = txtObserEditar.Value;
            series.ID_tipo_serie1 = int.Parse(ddlTipoEditar.SelectedValue);
            series.ID_casa_productora1 = int.Parse(ddlCasaEditar.SelectedValue);
            series.ID_ubicacion1 = int.Parse(ddlUbicacionEditar.SelectedValue);
            series.Estado = int.Parse(ddlEstadoEditar.SelectedValue);
            series.ID_backup_serie1 = int.Parse(codBackupSerie.Value);

            if (dao.ModificarBackupSerie(series))
            {
                CargaBackupSerie();
                limpiarDDLS();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro modificado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                limpiarDDLS();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se modifico'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }
    }
}