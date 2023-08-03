using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Dao.Programacion;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.WebForms.Programacion
{
    public partial class WebTipo_serie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaTipoSerie();
                }
                    
                else
                    SetTipoSerie();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }
        Dao_Tipo_Serie dao = new Dao_Tipo_Serie();
        Tipos_series serie = new Tipos_series();
        ManejoError error = new ManejoError();

        void CargaTipoSerie()
        {
            if (dao.GetTipo_Serie())
            {
                tabla_tipo_serie.DataSource = dao.DsReturn.Tables["tipos_series"];
                tabla_tipo_serie.DataBind();
                Session["series"] = dao.DsReturn;
            }
        }
        void SetTipoSerie()
        {
            tabla_tipo_serie.DataSource = ((DataSet)Session["series"]);
            tabla_tipo_serie.DataBind();
        }

        protected void tabla_tipo_serie_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_tipo_serie.PageIndex = e.NewPageIndex;
            tabla_tipo_serie.DataBind();
        }

        void LimpiarTexto()
        {
            txtNombreRegister.Value = "";
            txtNombreEdit.Value = "";
        }

        protected void tabla_tipo_serie_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_tipo_serie.Rows[e.NewEditIndex];
            codSerie.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_tipo_serie_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            serie.ID_tipo_serie1 = int.Parse(tabla_tipo_serie.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarTipoSerie(serie))
            {
                CargaTipoSerie();
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
            serie.Nombre = txtNombreRegister.Value;
            if (dao.InsertarTipoSerie(serie))
            {
                CargaTipoSerie();
                LimpiarTexto();
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
                LimpiarTexto();
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
            serie.ID_tipo_serie1 = int.Parse(codSerie.Value);
            serie.Nombre = txtNombreEdit.Value;
            if (dao.ModificarTipoSerie(serie))
            {
                CargaTipoSerie();
                LimpiarTexto();
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
                LimpiarTexto();
                string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'El registro no se modifico',
                        icon: 'error'                        
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            serie.Nombre = txtBuscar.Value;
            if (dao.GetBuscar_Tipo_Serie(serie))
            {
                tabla_tipo_serie.DataSource = dao.DsReturn.Tables["tipos_series"];
                tabla_tipo_serie.DataBind();
                Session["series"] = dao.DsReturn;
                txtBuscar.Value = "";
            }
            else
            {
                string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 3000,
                        title: 'No se encontro los registros de la busqueda',
                        icon: 'error'                        
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
            }
        }
    }
}