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
    public partial class WebModulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaModulos();
                    LlenarListados();
                }

                else
                    SetModulos();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        Dao_Modulos dao = new Dao_Modulos();
        Modulos modulo = new Modulos();
        ManejoError error = new ManejoError();

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

        void LlenarListados()
        {
            if (dao.GetModulosDDL())
            {
                ddlModuloPadreRegister.DataSource = dao.DsReturn.Tables["ddlmodulos"];
                ddlModuloPadreRegister.DataValueField = dao.DsReturn.Tables["ddlmodulos"].Columns["ID_MODULO"].ToString();
                ddlModuloPadreRegister.DataTextField = dao.DsReturn.Tables["ddlmodulos"].Columns["NOMBRE"].ToString();
                ddlModuloPadreRegister.DataBind();
            }
        }

        void LlenarListadosEdit()
        {
            if (dao.GetModulosDDL())
            {
                ListItem item;
                foreach (DataRow list in dao.DsReturn.Tables["ddlmodulos"].Rows)
                {
                    string idmod = list["ID_MODULO"].ToString();
                    string nom = list["NOMBRE"].ToString();
                    item = new ListItem(nom, idmod);
                    ddlModuloPadreEdit.Items.Add(item);
                }
            }

            ListItem listItem;
            listItem = new ListItem("ACTIVO", "1");
            ddlEstadoEdit.Items.Add(listItem);
            listItem = new ListItem("INACTIVO", "0");
            ddlEstadoEdit.Items.Add(listItem);
        }

        void LimpiarDDLs()
        {
            ddlEstadoEdit.Items.Clear();
            ddlModuloPadreEdit.Items.Clear();
        }

        void limpiartextos()
        {
            txtNombreRegister.Value = "";
            txtPathRegister.Value = "";
            ddlEstadoRegister.SelectedIndex = 0;
            ddlModuloPadreRegister.SelectedIndex = 0;
            chModuloPropioEdit.Checked = false;
            chbModuloPropioRegister.Checked = false;
        }

        protected void tabla_modulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_modulos.PageIndex = e.NewPageIndex;
            tabla_modulos.DataBind();
        }

        protected void tabla_modulos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiarDDLs();
            GridViewRow row = tabla_modulos.Rows[e.NewEditIndex];
            string cod = row.Cells[0].Text;
            codModulo.Value = cod;
            txtNombreEdit.Value = row.Cells[1].Text;
            string ver = row.Cells[2].Text;

            if (ver != "&nbsp;")
            {
                txtPathEdit.Value = row.Cells[2].Text;
            }
            else
            {
                txtPathEdit.Value = "";
            }

            if (dao.GetModulosList(int.Parse(cod)))
            {
                ListItem item;
                string idmodulopadre = dao.DsReturn.Tables["ddlmodulosedit"].Rows[0]["ID_MODULO_PADRE"].ToString();
                if (idmodulopadre == cod)
                {
                    chModuloPropioEdit.Checked = true;
                }
                if (idmodulopadre != cod)
                {
                    chModuloPropioEdit.Checked = false;
                    string modulopadre = row.Cells[3].Text;
                    item = new ListItem(modulopadre, idmodulopadre);
                    ddlModuloPadreEdit.Items.Add(item);
                }
                string estado = dao.DsReturn.Tables["ddlmodulosedit"].Rows[0]["ESTADO"].ToString();
                string idestado = dao.DsReturn.Tables["ddlmodulosedit"].Rows[0]["ID_ESTADO"].ToString();
                item = new ListItem(estado, idestado);
                ddlEstadoEdit.Items.Add(item);

                LlenarListadosEdit();
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_modulos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            modulo.ID_modulo1 = int.Parse(tabla_modulos.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarModulo(modulo))
            {
                CargaModulos();
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
            if (chbModuloPropioRegister.Checked == true)
            {
                modulo.Nombre = txtNombreRegister.Value;
                modulo.Url_path = txtPathRegister.Value;
                modulo.Usuario_creacion = Session["logueado"].ToString();
                modulo.Estado = int.Parse(ddlEstadoRegister.SelectedValue);

                if (dao.InsertarModuloPropio(modulo))
                {
                    CargaModulos();
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
                modulo.Nombre = txtNombreRegister.Value;
                modulo.Url_path = txtPathRegister.Value;
                modulo.Usuario_creacion =  Session["logueado"].ToString();
                modulo.ID_modulo_padre1 = int.Parse(ddlModuloPadreRegister.SelectedValue);
                modulo.Estado = int.Parse(ddlEstadoRegister.SelectedValue);

                if (dao.InsertarModulo(modulo))
                {
                    CargaModulos();
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
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (chModuloPropioEdit.Checked == true)
            {
                modulo.ID_modulo1 = int.Parse(codModulo.Value);
                modulo.Nombre = txtNombreEdit.Value;
                modulo.Url_path = txtPathEdit.Value;
                modulo.Usuario_modificacion = Session["logueado"].ToString();
                modulo.ID_modulo_padre1 = int.Parse(codModulo.Value);
                modulo.Estado = int.Parse(ddlEstadoEdit.SelectedValue);
            }
            else
            {
                modulo.ID_modulo1 = int.Parse(codModulo.Value);
                modulo.Nombre = txtNombreEdit.Value;
                modulo.Url_path = txtPathEdit.Value;
                modulo.Usuario_modificacion = Session["logueado"].ToString();
                modulo.ID_modulo_padre1 = int.Parse(ddlModuloPadreEdit.SelectedValue);
                modulo.Estado = int.Parse(ddlEstadoEdit.SelectedValue);
            }

            if (dao.ModificarModulo(modulo))
            {
                CargaModulos();
                limpiartextos();
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
                limpiartextos();
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