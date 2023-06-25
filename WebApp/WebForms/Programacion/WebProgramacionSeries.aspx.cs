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
                    CargaBackupSerie();
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
    }
}