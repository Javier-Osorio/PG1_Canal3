using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Dao.LogIn;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp.WebForms.Login
{
    public partial class WebRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaRoles();                   
                }

                else
                    SetRoles();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        Dao_Roles dao = new Dao_Roles();
        Roles rol = new Roles();
        ManejoError error = new ManejoError();

        void CargaRoles()
        {
            if (dao.GetRoles())
            {
                tabla_roles.DataSource = dao.DsReturn.Tables["roles"];
                tabla_roles.DataBind();
                Session["rol"] = dao.DsReturn;
            }
        }
        void SetRoles()
        {
            tabla_roles.DataSource = ((DataSet)Session["rol"]);
            tabla_roles.DataBind();
        }

        protected void tabla_roles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_roles.PageIndex = e.NewPageIndex;
            tabla_roles.DataBind();
        }

        protected void tabla_roles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ddlEstadoEdit.Items.Clear();

            GridViewRow row = tabla_roles.Rows[e.NewEditIndex];
            codRol.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;
            string verEstado = row.Cells[2].Text;
            ListItem listItem;
            if (verEstado == "ACTIVO")
            {
                listItem = new ListItem("ACTIVO", "1");
                ddlEstadoEdit.Items.Add(listItem);
            }
            else
            {
                listItem = new ListItem("INACTIVO", "0");
                ddlEstadoEdit.Items.Add(listItem);
            }

            
            listItem = new ListItem("ACTIVO", "1");
            ddlEstadoEdit.Items.Add(listItem);
            listItem = new ListItem("INACTIVO", "0");
            ddlEstadoEdit.Items.Add(listItem);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_roles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            rol.ID_rol1 = int.Parse(tabla_roles.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarRoles(rol))
            {
                CargaRoles();
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
            rol.Nombre = txtNombreRegister.Value;
            rol.Estado = int.Parse(ddlEstadoRegister.SelectedValue);
            rol.Usuario_creacion = Session["logueado"].ToString();

            if (dao.InsertarRoles(rol))
            {
                CargaRoles();
                txtNombreRegister.Value = "";
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Se registro correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                txtNombreRegister.Value = "";
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se guardo'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            rol.Nombre = txtNombreRegister.Value;
            rol.Estado = int.Parse(ddlEstadoRegister.SelectedValue);
            rol.Usuario_modificacion = Session["logueado"].ToString();

            if (dao.ModificarRoles(rol))
            {
                CargaRoles();
                txtNombreRegister.Value = "";
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro modificado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                txtNombreRegister.Value = "";
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se modifico'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }
    }
}