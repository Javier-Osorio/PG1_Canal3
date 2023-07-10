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
    public partial class WebUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaUsuarios();
                    LlenarRol();
                }

                else
                    SetUsuarios();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        Dao_Usuarios dao = new Dao_Usuarios();
        Usuarios usuarios = new Usuarios();
        ManejoError error = new ManejoError();

        void CargaUsuarios()
        {
            if (dao.GetUsuarios())
            {
                tabla_usuarios.DataSource = dao.DsReturn.Tables["usuarios"];
                tabla_usuarios.DataBind();
                Session["tabla_usu"] = dao.DsReturn;
            }
        }

        void SetUsuarios()
        {
            tabla_usuarios.DataSource = ((DataSet)Session["tabla_usu"]);
            tabla_usuarios.DataBind();
        }

        void LlenarRol()
        {
            if (dao.GetRoles())
            {
                ddlRolRegister.DataSource = dao.DsReturn.Tables["usu_rol"];
                ddlRolRegister.DataValueField = dao.DsReturn.Tables["usu_rol"].Columns["ID_ROL"].ToString();
                ddlRolRegister.DataTextField = dao.DsReturn.Tables["usu_rol"].Columns["NOMBRE"].ToString();
                ddlRolRegister.DataBind();
            }
        }

        void LlenarEdits()
        {
            ListItem item;
            if (dao.GetRoles())
            {
                foreach (DataRow list in dao.DsReturn.Tables["usu_rol"].Rows)
                {
                    string idrol = list["ID_ROL"].ToString();
                    string nom = list["NOMBRE"].ToString();
                    item = new ListItem(nom, idrol);
                    ddlRolEdit.Items.Add(item);
                }
            }

            item = new ListItem("ACTIVO", "1");
            ddlEstadoEdit.Items.Add(item);
            item = new ListItem("INACTIVO", "0");
            ddlEstadoEdit.Items.Add(item);
        }
    }
}