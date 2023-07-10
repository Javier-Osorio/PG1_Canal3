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
    public partial class WebPrivilegios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaPrivilegios();
                    LlenarDDLsRegister();
                }

                else
                    SetPrivilegios();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        Dao_Privilegios dao = new Dao_Privilegios();
        ManejoError error = new ManejoError();
        Privilegios privilegios = new Privilegios();

        void CargaPrivilegios()
        {
            if (dao.GetPrivilegios())
            {
                tabla_privilegios.DataSource = dao.DsReturn.Tables["privilegios"];
                tabla_privilegios.DataBind();
                Session["privi"] = dao.DsReturn;
            }
        }

        void SetPrivilegios()
        {
            tabla_privilegios.DataSource = ((DataSet)Session["privi"]);
            tabla_privilegios.DataBind();
        }

        void LimpiarDDLsEdit()
        {
            ddlEstadoEdit.Items.Clear();
            ddlModuloEdit.Items.Clear();
            ddlRolEdit.Items.Clear();
        }

        void limpiarDDLsRegistro()
        {
            ddlRolRegister.SelectedIndex = 0;
            ddlEstadoRegister.SelectedIndex = 0;
            ddlModuloRegister.SelectedIndex = 0;
        }

        void LlenarDDLsRegister()
        {
            if (dao.GetModuloPrivilegio())
            {
                ddlModuloRegister.DataSource = dao.DsReturn.Tables["modul_privi"];
                ddlModuloRegister.DataValueField = dao.DsReturn.Tables["modul_privi"].Columns["ID_MODULO"].ToString();
                ddlModuloRegister.DataTextField = dao.DsReturn.Tables["modul_privi"].Columns["NOMBRE"].ToString();
                ddlModuloRegister.DataBind();
            }

            if (dao.GetRolPrivilegio())
            {
                ddlRolRegister.DataSource = dao.DsReturn.Tables["rol_privi"];
                ddlRolRegister.DataValueField = dao.DsReturn.Tables["rol_privi"].Columns["ID_ROL"].ToString();
                ddlRolRegister.DataTextField = dao.DsReturn.Tables["rol_privi"].Columns["NOMBRE"].ToString();
                ddlRolRegister.DataBind();
            }
        }

        void LlenarDDLsEdit()
        {
            ListItem item;
            if (dao.GetModuloPrivilegio())
            {
                foreach (DataRow list in dao.DsReturn.Tables["modul_privi"].Rows)
                {
                    string idmod = list["ID_MODULO"].ToString();
                    string nom = list["NOMBRE"].ToString();
                    item = new ListItem(nom, idmod);
                    ddlModuloEdit.Items.Add(item);
                }
            }

            if (dao.GetRolPrivilegio())
            {
                foreach (DataRow list in dao.DsReturn.Tables["rol_privi"].Rows)
                {
                    string idrol = list["ID_ROL"].ToString();
                    string nom = list["NOMBRE"].ToString();
                    item = new ListItem(nom, idrol);
                    ddlRolEdit.Items.Add(item);
                }
            }

            ListItem listItem;
            listItem = new ListItem("ACTIVO", "1");
            ddlEstadoEdit.Items.Add(listItem);
            listItem = new ListItem("INACTIVO", "0");
            ddlEstadoEdit.Items.Add(listItem);
        }

        protected void tabla_privilegios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_privilegios.PageIndex = e.NewPageIndex;
            tabla_privilegios.DataBind();
        }

        protected void tabla_privilegios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiarDDLsEdit();
            GridViewRow row = tabla_privilegios.Rows[e.NewEditIndex];
            int cod = int.Parse(row.Cells[0].ToString());
            if (dao.GetListPrivilegio(cod))
            {
                ListItem item;
                string idmod = dao.DsReturn.Tables["edit_privi"].Rows[0]["ID_MODULO"].ToString();
                string mod = dao.DsReturn.Tables["edit_privi"].Rows[0]["MODULO"].ToString();
                item = new ListItem(mod, idmod);
                ddlModuloEdit.Items.Add(item);

                string idrol = dao.DsReturn.Tables["edit_privi"].Rows[0]["ID_ROL"].ToString();
                string rol = dao.DsReturn.Tables["edit_privi"].Rows[0]["ROL"].ToString();
                item = new ListItem(rol, idrol);
                ddlRolEdit.Items.Add(item);

                string idestado = dao.DsReturn.Tables["edit_privi"].Rows[0]["ID_ESTADO"].ToString();
                string estado = dao.DsReturn.Tables["edit_privi"].Rows[0]["ESTADO"].ToString();
                item = new ListItem(estado, idestado);
                ddlEstadoEdit.Items.Add(item);

                LlenarDDLsEdit();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_privilegios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            privilegios.ID_privilegio1 = int.Parse(tabla_privilegios.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarPrivilegios(privilegios))
            {
                CargaPrivilegios();
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
            privilegios.ID_modulo1 = int.Parse(ddlModuloRegister.SelectedValue);
            privilegios.ID_rol1 = int.Parse(ddlRolRegister.SelectedValue);
            privilegios.Estado = int.Parse(ddlEstadoRegister.SelectedValue);
            privilegios.Usuario_creacion = Session["logueado"].ToString();

            if (dao.InsertarPrivilegios(privilegios))
            {
                CargaPrivilegios();
                limpiarDDLsRegistro();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Se registro correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                limpiarDDLsRegistro();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se guardo'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            privilegios.ID_modulo1 = int.Parse(ddlModuloRegister.SelectedValue);
            privilegios.ID_rol1 = int.Parse(ddlRolRegister.SelectedValue);
            privilegios.Estado = int.Parse(ddlEstadoRegister.SelectedValue);
            privilegios.Usuario_modificacion = Session["logueado"].ToString();
            privilegios.ID_rol1 = int.Parse(codPrivilegio.Value);

            if (dao.ModificarPrivilegios(privilegios))
            {
                CargaPrivilegios();
                LimpiarDDLsEdit();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Se registro correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                LimpiarDDLsEdit();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se guardo'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }
    }
}