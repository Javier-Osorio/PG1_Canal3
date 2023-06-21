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
    public partial class WebCasa_productora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    CargaCasaProductora();
                else
                    SetCasaProductora();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        Dao_Casa_Productora dao = new Dao_Casa_Productora();
        Casa_productoras casa = new Casa_productoras();

        void CargaCasaProductora()
        {
            if (dao.GetCasa_Productora())
            {
                tabla_casa_productora.DataSource = dao.DsReturn.Tables["casa_productora"];
                tabla_casa_productora.DataBind();
                Session["casa"] = dao.DsReturn;
            }
        }
        void SetCasaProductora()
        {
            tabla_casa_productora.DataSource = ((DataSet)Session["casa"]);
            tabla_casa_productora.DataBind();
        }

        protected void tabla_casa_productora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_casa_productora.PageIndex = e.NewPageIndex;
            tabla_casa_productora.DataBind();
        }

        void LimpiarTextos()
        {
            txtNombreRegister.Value = "";
            txtNombreEdit.Value = "";
        }

        protected void tabla_casa_productora_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_casa_productora.Rows[e.NewEditIndex];
            codCasa.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_casa_productora_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            casa.ID_casa_productora1 = int.Parse(tabla_casa_productora.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarCasaProductora(casa))
            {
                CargaCasaProductora();
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
            casa.Nombre = txtNombreRegister.Value;
            if (dao.InsertarCasaProductora(casa))
            {
                CargaCasaProductora();
                LimpiarTextos();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro guardado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                LimpiarTextos();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se guardo'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            casa.ID_casa_productora1 = int.Parse(codCasa.Value);
            casa.Nombre = txtNombreEdit.Value;
            if (dao.ModificarCasaProductora(casa))
            {
                CargaCasaProductora();
                LimpiarTextos();
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