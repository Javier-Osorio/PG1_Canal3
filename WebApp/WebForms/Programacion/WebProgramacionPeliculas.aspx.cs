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
    public partial class WebProgramacionPeliculas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    CargaBackupPelicula();
                else
                    SetBackupPelicula();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        Dao_Backup_Pelicula dao = new Dao_Backup_Pelicula();
        Backup_peliculas peliculas = new Backup_peliculas();

        void CargaBackupPelicula()
        {
            if (dao.GetBackup_Pelicula())
            {
                tabla_programacion_pelicula.DataSource = dao.DsReturn.Tables["backup_pelicula"];
                tabla_programacion_pelicula.DataBind();
                Session["backup_p"] = dao.DsReturn;
            }
        }

        void SetBackupPelicula()
        {
            tabla_programacion_pelicula.DataSource = ((DataSet)Session["backup_p"]);
            tabla_programacion_pelicula.DataBind();
        }

        protected void tabla_programacion_pelicula_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_programacion_pelicula.PageIndex = e.NewPageIndex;
            tabla_programacion_pelicula.DataBind();
        }

        protected void tabla_programacion_pelicula_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void tabla_programacion_pelicula_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}