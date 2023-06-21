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
    public partial class WebNombre_material : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    CargaNombreMaterial();
                else
                    SetNombreMaterial();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        Dao_Nombre_Material dao = new Dao_Nombre_Material();
        Nombre_material mat = new Nombre_material();

        void CargaNombreMaterial()
        {
            if (dao.GetNombre_Material())
            {
                tabla_nombre_material.DataSource = dao.DsReturn.Tables["nombre_material"];
                tabla_nombre_material.DataBind();
                Session["material"] = dao.DsReturn;
            }
        }
        void SetNombreMaterial()
        {
            tabla_nombre_material.DataSource = ((DataSet)Session["material"]);
            tabla_nombre_material.DataBind();
        }

        protected void tabla_nombre_material_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_nombre_material.PageIndex = e.NewPageIndex;
            tabla_nombre_material.DataBind();
        }

        void LimpiarTexto()
        {
            txtNombreRegister.Value = "";
            txtNombreEdit.Value = "";
        }

        protected void tabla_nombre_material_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_nombre_material.Rows[e.NewEditIndex];
            codMaterial.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_nombre_material_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            mat.ID_Nombre1 = int.Parse(tabla_nombre_material.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarNombreMaterial(mat))
            {
                CargaNombreMaterial();
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
            mat.Nombre = txtNombreRegister.Value;
            if (dao.InsertarNombreMaterial(mat))
            {
                CargaNombreMaterial();
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
            mat.ID_Nombre1 = int.Parse(codMaterial.Value);
            mat.Nombre = txtNombreEdit.Value;
            if (dao.ModificarNombreMaterial(mat))
            {
                CargaNombreMaterial();
                LimpiarTexto();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro modificado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                LimpiarTexto();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se modifico'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }
    }
}