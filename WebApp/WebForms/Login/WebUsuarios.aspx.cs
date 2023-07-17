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

        void limpiarDDLsRegister()
        {
            ddlEstadoRegister.SelectedIndex = 0;
            ddlRolRegister.SelectedIndex = 0;
        }
        void limpiarDDLsEdit()
        {
            ddlRolEdit.Items.Clear();
            ddlEstadoEdit.Items.Clear();
        }

        void limpiartextos()
        {
            txtNombreRegister.Value = "";
            txtApellidoRegister.Value = "";
            txtCorreoRegister.Value = "";
            txtContraRegister.Value = "";
            txtContraConfirmRegister.Value = "";
            ddlEstadoRegister.SelectedIndex = 0;
            ddlRolRegister.SelectedIndex = 0;
        }

        protected void tabla_usuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_usuarios.PageIndex = e.NewPageIndex;
            tabla_usuarios.DataBind();
        }

        protected void tabla_usuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            limpiarDDLsEdit();
            GridViewRow row = tabla_usuarios.Rows[e.NewEditIndex];
            codUsuario.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;
            txtApellidoEdit.Value = row.Cells[2].Text;
            txtCorreoEdit.Value = row.Cells[4].Text;

            ListItem item;
            if (dao.GetListUsuarios(int.Parse(row.Cells[0].Text)))
            {
                foreach (DataRow list in dao.DsReturn.Tables["list_usuario_edit"].Rows)
                {
                    string idrol = list["ID_ROL"].ToString();
                    string rol = list["ROL"].ToString();
                    string idesta = list["ID_ESTADO"].ToString();
                    string estado = list["ESTADO"].ToString();

                    item = new ListItem(rol, idrol);
                    ddlRolEdit.Items.Add(item);
                    item = new ListItem(estado, idesta);
                    ddlEstadoEdit.Items.Add(item);
                }
            }

            LlenarEdits();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_usuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            usuarios.ID_usuario1 = int.Parse(tabla_usuarios.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarUsuarios(usuarios))
            {
                CargaUsuarios();
                string script = @"Swal.fire({                        
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'Registro eliminado correctamente',
                        icon: 'success'
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
            }
            else
            {
                string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'El registro no se elimino',
                        icon: 'error'                        
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string ver1 = txtContraConfirmRegister.Value;
            string ver2 = txtContraRegister.Value;
            if (ver1.Equals(ver2))
            {
                usuarios.Nombre = txtNombreRegister.Value;
                usuarios.Apellido = txtApellidoRegister.Value;
                usuarios.Usuario = dao.ObtenerUsuario(usuarios);
                usuarios.Contra = txtContraRegister.Value;
                usuarios.Correo = txtCorreoRegister.Value;
                usuarios.Estado = int.Parse(ddlEstadoRegister.SelectedValue);
                usuarios.ID_rol1 = int.Parse(ddlRolRegister.SelectedValue);
                usuarios.Usuario_creacion = Session["logueado"].ToString();

                if (dao.InsertarUsuarios(usuarios))
                {
                    CargaUsuarios();
                    limpiartextos();
                    string script = @"Swal.fire({                        
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'Registro guardado correctamente',
                        icon: 'success'
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
                }
                else
                {
                    limpiartextos();
                    string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'El registro no se guardo',
                        icon: 'error'                        
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
                }
            }
            else
            {
                string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'Las contraseñas deben ser las mismas',
                        icon: 'error'                        
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
            }
            
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            usuarios.Nombre = txtNombreEdit.Value;
            usuarios.Apellido = txtApellidoEdit.Value;
            usuarios.Usuario = dao.ObtenerUsuario(usuarios);
            usuarios.Correo = txtCorreoEdit.Value;
            usuarios.Estado = int.Parse(ddlEstadoEdit.SelectedValue);
            usuarios.ID_rol1 = int.Parse(ddlRolEdit.SelectedValue);
            usuarios.Usuario_modificacion = Session["logueado"].ToString();
            usuarios.ID_usuario1 = int.Parse(codUsuario.Value);

            if (dao.ModificarUsuarios(usuarios))
            {
                CargaUsuarios();

                string script = @"Swal.fire({                        
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'Registro modificado correctamente',
                        icon: 'success'
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
            }
            else
            {
                string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'El registro no se modifico',
                        icon: 'error'                        
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
            }
        }
    }
}