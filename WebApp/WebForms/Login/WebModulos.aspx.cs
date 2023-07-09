using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Modelo_Controlador.Dao.LogIn;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp.WebForms.Login
{
    public partial class WebModulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaModulos();
                }

                else
                    SetModulos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        Dao_Modulos dao = new Dao_Modulos();
        Modulos modulo = new Modulos();

        void CargaModulos()
        {
            if (dao.GetModulos())
            {
                tabla_modulos.DataSource = dao.DsReturn.Tables["modulos"];
                tabla_modulos.DataBind();
                Session["modul"] = dao.DsReturn;
            }
        }

        void SetModulos()
        {
            tabla_modulos.DataSource = ((DataSet)Session["modul"]);
            tabla_modulos.DataBind();
        }

        protected void tabla_modulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_modulos.PageIndex = e.NewPageIndex;
            tabla_modulos.DataBind();
        }

        protected void tabla_modulos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_modulos.Rows[e.NewEditIndex];
            codModulo.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;
            string ver = row.Cells[2].Text;
            if (ver != "&nbsp;")
            {
                txtPathEdit.Value = row.Cells[2].Text;
            }            
            //ddlModuloPadreEdit


            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_modulos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            modulo.ID_modulo1 = int.Parse(tabla_modulos.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarModulo(modulo))
            {
                CargaModulos();
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

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }
    }
}