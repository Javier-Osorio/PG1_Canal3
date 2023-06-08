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
                tabla_ubicacion.DataSource = dao.DsReturn.Tables["ubicacion"];
                tabla_ubicacion.DataBind();
                Session["ubicacion"] = dao.DsReturn;
            }
        }
        void SetUbicaciones()
        {
            tabla_ubicacion.DataSource = ((DataSet)Session["ubicacion"]);
            tabla_ubicacion.DataBind();
        }

        protected void tabla_ubicacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_ubicacion.PageIndex = e.NewPageIndex;
            tabla_ubicacion.DataSource = ((DataSet)Session["ubicacion"]);
            tabla_ubicacion.DataBind();
        }
    }
}