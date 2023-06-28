using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                Console.WriteLine(ex.StackTrace);
            }
        }
        Dao_Backup_Serie dao = new Dao_Backup_Serie();
        Backup_series series = new Backup_series();
        Dao_Nombre_Material dao_Nombre = new Dao_Nombre_Material();
        Dao_Casa_Productora dao_Casa = new Dao_Casa_Productora();
        Dao_Tipo_Serie dao_Tipo_Serie = new Dao_Tipo_Serie();
        Dao_Ubicacion dao_Ubicacion = new Dao_Ubicacion();

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
            if (dao_Nombre.GetNombre_Material())
            {
                ddlNombreMaterial.DataSource = dao_Nombre.DsReturn.Tables["nombre_material"];                
                ddlNombreMaterial.DataTextField = dao_Nombre.DsReturn.Tables["nombre_material"].Columns["NOMBRE"].ToString();
                ddlNombreMaterial.DataValueField = dao_Nombre.DsReturn.Tables["nombre_material"].Columns["ID_NOMBRE"].ToString();
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

        protected void tabla_programacion_serie_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_programacion_serie.PageIndex = e.NewPageIndex;
            tabla_programacion_serie.DataBind();
        }

        protected void tabla_programacion_serie_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void tabla_programacion_serie_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

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
    }
}