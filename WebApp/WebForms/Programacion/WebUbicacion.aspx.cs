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
                Console.WriteLine(ex.StackTrace);
            }
        }
        Dao_Ubicacion dao = new Dao_Ubicacion();
        Ubicaciones ubi = new Ubicaciones();

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

        protected void tabla_ubicaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tabla_ubicaciones.EditIndex = e.NewEditIndex;
            tabla_ubicaciones.DataBind();
        }

        protected void tabla_ubicaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tabla_ubicaciones.EditIndex = -1;
            tabla_ubicaciones.DataBind();
        }

        protected void tabla_ubicaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = e.RowIndex;
            //DataTable data = ((DataTable)Session["ubicacion"]);
            GridViewRow fila = (sender as LinkButton).NamingContainer as GridViewRow;
            string admkaln = (fila.Cells[1].Controls[0] as TextBox).Text;
            string jnkjbnk = (fila.Cells[2].Controls[0] as TextBox).Text;
            //TextBox asasd = ((TextBox)fila.Cells[2].Controls[0]);
            var vrfww = ((TextBox)fila.Cells[3].Controls[0]).Text;
            var ver = (tabla_ubicaciones.Rows[e.RowIndex].FindControl("txtNombre") as TextBox).Text.Trim();
            var ver2 = (tabla_ubicaciones.Rows[e.RowIndex].FindControl("txtUbicacion") as TextBox).Text.Trim();
            var verID = int.Parse(tabla_ubicaciones.DataKeys[e.RowIndex].Value.ToString());
            //string ver = tabla_ubicaciones.Rows[e.RowIndex].Cells[1].Text;
            //string ver2 = tabla_ubicaciones.Rows[e.RowIndex].Cells[2].Text;

            ubi.ID_ubicacion1 = verID;
            //ubi.Nombre = ver;
            //ubi.Path_carpeta = ver2;

            if (dao.ModificarUbicaciones(ubi))
            {
                CargaUbicaciones();
            }
        }
    }
}