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
    public partial class WebNombreSerie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    CargaNombreSerie();
                else
                    SetNombreSerie();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }
        Dao_Nombre_Serie dao = new Dao_Nombre_Serie();
        Nombre_serie serie = new Nombre_serie();
        ManejoError error = new ManejoError();

        void CargaNombreSerie()
        {
            if (dao.GetNombre_Serie())
            {
                tabla_nombre_serie.DataSource = dao.DsReturn.Tables["nombres_series"];
                tabla_nombre_serie.DataBind();
                Session["nombre_serie"] = dao.DsReturn;
            }
        }
        void SetNombreSerie()
        {
            tabla_nombre_serie.DataSource = ((DataSet)Session["nombre_serie"]);
            tabla_nombre_serie.DataBind();
        }
        void LimpiarTexto()
        {
            txtNombreEdit.Value = "";
            txtNombreRegister.Value = "";
        }

        protected void tabla_nombre_serie_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_nombre_serie.PageIndex = e.NewPageIndex;
            tabla_nombre_serie.DataBind();
        }

        protected void tabla_nombre_serie_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_nombre_serie.Rows[e.NewEditIndex];
            codNombre.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_nombre_serie_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            serie.ID_Nombre_Serie1 = int.Parse(tabla_nombre_serie.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarNombreSerie(serie))
            {
                CargaNombreSerie();
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
            serie.Nombre = txtNombreRegister.Value;
            if (dao.InsertarNombreSerie(serie))
            {
                CargaNombreSerie();
                LimpiarTexto();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro guardado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                LimpiarTexto();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se guardo'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            serie.ID_Nombre_Serie1 = int.Parse(codNombre.Value);
            serie.Nombre = txtNombreEdit.Value;
            if (dao.ModificarNombreSerie(serie))
            {
                CargaNombreSerie();
                LimpiarTexto();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro modificado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se modifico'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }
    }
}