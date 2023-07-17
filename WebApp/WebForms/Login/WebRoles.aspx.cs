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
                Session["tabla_rol"] = dao.DsReturn;
            }
        }
        void SetRoles()
        {
            tabla_roles.DataSource = ((DataSet)Session["tabla_rol"]);
            tabla_roles.DataBind();
        }

        void limpiarTextos()
        {
            txtNombreRegister.Value = "";
            ddlEstadoRegister.SelectedIndex = 0;
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
            ListItem listItem;
            ListItem item;
            if (dao.GetEstadoRol(int.Parse(row.Cells[0].Text)))
            {
                foreach (DataRow list in dao.DsReturn.Tables["estado_rol_edit"].Rows)
                {
                    string idesta = list["ID_ESTADO"].ToString();
                    string estado = list["ESTADO"].ToString();
                    item = new ListItem(estado, idesta);
                    ddlEstadoEdit.Items.Add(item);
                }
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
                rol.Nombre = txtNombreRegister.Value;
                rol.Estado = int.Parse(ddlEstadoRegister.SelectedValue);
                rol.Usuario_creacion = Session["logueado"].ToString();

                if (dao.InsertarRoles(rol))
                {
                    CargaRoles();
                    limpiarTextos();
                    string script = @"Swal.fire({                        
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'Registro guardado correctamente',
                        icon: 'error'
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
                }
                else
                {
                    limpiarTextos();
                    string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'El registro no se guardo',
                        icon: 'error'                        
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
                }            
                       
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            rol.Nombre = txtNombreEdit.Value;
            rol.Estado = int.Parse(ddlEstadoEdit.SelectedValue);
            rol.Usuario_modificacion = Session["logueado"].ToString();
            rol.ID_rol1 = int.Parse(codRol.Value);

            if (dao.ModificarRoles(rol))
            {
                CargaRoles();
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