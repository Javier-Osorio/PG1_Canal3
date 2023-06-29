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
    public partial class WebNombrePelicula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                    CargaNombrePelicula();
                else
                    SetNombrePelicula();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        Dao_Nombre_Pelicula dao = new Dao_Nombre_Pelicula();
        Nombre_pelicula pelicula = new Nombre_pelicula();

        void CargaNombrePelicula()
        {
            if (dao.GetNombre_Pelicula())
            {
                tabla_nombre_pelicula.DataSource = dao.DsReturn.Tables["nombres_peliculas"];
                tabla_nombre_pelicula.DataBind();
                Session["nombre_peli"] = dao.DsReturn;
            }
        }
        void SetNombrePelicula()
        {
            tabla_nombre_pelicula.DataSource = ((DataSet)Session["nombre_peli"]);
            tabla_nombre_pelicula.DataBind();
        }
        void LimpiarTexto()
        {
            txtNombreEdit.Value = "";
            txtNombreRegister.Value = "";
        }

        protected void tabla_nombre_pelicula_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_nombre_pelicula.PageIndex = e.NewPageIndex;
            tabla_nombre_pelicula.DataBind();
        }

        protected void tabla_nombre_pelicula_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_nombre_pelicula.Rows[e.NewEditIndex];
            codNombre.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_nombre_pelicula_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            pelicula.ID_Nombre_pelicula1 = int.Parse(tabla_nombre_pelicula.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarNombrePelicula(pelicula))
            {
                CargaNombrePelicula();
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
            pelicula.Nombre = txtNombreRegister.Value;
            if (dao.InsertarNombrePelicula(pelicula))
            {
                CargaNombrePelicula();
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
            pelicula.ID_Nombre_pelicula1 = int.Parse(codNombre.Value);
            pelicula.Nombre = txtNombreEdit.Value;
            if (dao.ModificarNombrePelicula(pelicula))
            {
                CargaNombrePelicula();
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