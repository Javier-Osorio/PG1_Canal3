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

        protected void tabla_ubicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_ubicaciones.PageIndex = e.NewPageIndex;
            tabla_ubicaciones.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ubi.Nombre = txtNombre.Value;
            ubi.Path_carpeta = txtUbicacion.Value;

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
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no guardado'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        void limpiarTextos()
        {
            txtNombre.Value = "";
            txtUbicacion.Value = "";
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

        protected void tabla_ubicaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void tabla_ubicaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idprueba = int.Parse(tabla_ubicaciones.DataKeys[e.RowIndex].Value.ToString());
            string pruebaNombre = ((TextBox)tabla_ubicaciones.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString();
            string pruebaPath = ((TextBox)tabla_ubicaciones.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString();
            string StrQry = "<script language='javascript'>";
            StrQry += "alert('"+ idprueba +"; "+pruebaNombre +"; "+pruebaPath +"')";
            StrQry += "</script>";
            ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
        }
    }
}