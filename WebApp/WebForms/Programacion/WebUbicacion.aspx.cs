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
    public partial class WebUbicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    CargaUbicaciones();
                else
                    SetUbicaciones();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }
        Dao_Ubicacion dao = new Dao_Ubicacion();
        Ubicaciones ubi = new Ubicaciones();
        ManejoError error = new ManejoError();

        void CargaUbicaciones()
        {
            if (dao.GetUbicaciones())
            {
                tabla_ubicaciones.DataSource = dao.DsReturn.Tables["ubicacion"];
                tabla_ubicaciones.DataBind();
                Session["ubicacion"] = dao.DsReturn;
            }
        }
        void SetUbicaciones()
        {
            tabla_ubicaciones.DataSource = ((DataSet)Session["ubicacion"]);
            tabla_ubicaciones.DataBind();
        }

        protected void tabla_ubicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_ubicaciones.PageIndex = e.NewPageIndex;
            tabla_ubicaciones.DataBind();
        }

        void limpiarTextos()
        {
            txtNombreRegister.Value = "";
            txtUbicacionRegister.Value = "";
            txtNombreEdit.Value = "";
            txtUbicacionEdit.Value = "";
        }

        protected void tabla_ubicaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_ubicaciones.Rows[e.NewEditIndex];
            codUbi.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;
            txtUbicacionEdit.Value = row.Cells[2].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_ubicaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ubi.ID_ubicacion1 = int.Parse(tabla_ubicaciones.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarUbicaciones(ubi))
            {
                CargaUbicaciones();
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ubi.ID_ubicacion1 = int.Parse(codUbi.Value);
            ubi.Nombre = txtNombreEdit.Value;
            ubi.Path_carpeta = txtUbicacionEdit.Value;

            if (dao.ModificarUbicaciones(ubi))
            {
                CargaUbicaciones();
                limpiarTextos();
                //ClientScript.RegisterStartupScript(GetType(), "modalEditar", "$('#modalEditar').modal('hide');", true);
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro modificado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                limpiarTextos();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se modifico'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ubi.Nombre = txtNombreRegister.Value;
            ubi.Path_carpeta = txtUbicacionRegister.Value;

            if (dao.InsertarUbicaciones(ubi))
            {
                CargaUbicaciones();
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
    }
}