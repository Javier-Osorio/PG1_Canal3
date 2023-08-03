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
    public partial class WebTipo_pelicula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaTipoPelicula();
                }
                    
                else
                    SetTipoPelicula();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        Dao_Tipo_Pelicula dao = new Dao_Tipo_Pelicula();
        Tipos_peliculas pelicula = new Tipos_peliculas();
        ManejoError error = new ManejoError();

        void CargaTipoPelicula()
        {
            if (dao.GetTipo_Pelicula())
            {
                tabla_tipo_pelicula.DataSource = dao.DsReturn.Tables["tipos_peliculas"];
                tabla_tipo_pelicula.DataBind();
                Session["peliculas"] = dao.DsReturn;
            }
        }
        void SetTipoPelicula()
        {
            tabla_tipo_pelicula.DataSource = ((DataSet)Session["peliculas"]);
            tabla_tipo_pelicula.DataBind();
        }

        protected void tabla_tipo_pelicula_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_tipo_pelicula.PageIndex = e.NewPageIndex;
            tabla_tipo_pelicula.DataBind();
        }

        void LimpiarTexto()
        {
            txtNombreRegister.Value = "";
            txtNombreEdit.Value = "";
        }       

        protected void tabla_tipo_pelicula_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_tipo_pelicula.Rows[e.NewEditIndex];
            codPelicula.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView        
        }

        protected void tabla_tipo_pelicula_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            pelicula.ID_tipo_pelicula1 = int.Parse(tabla_tipo_pelicula.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarTipoPelicula(pelicula))
            {
                CargaTipoPelicula();
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
            pelicula.Nombre = txtNombreRegister.Value;
            if (dao.InsertarTipoPelicula(pelicula))
            {
                CargaTipoPelicula();
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
            pelicula.ID_tipo_pelicula1 = int.Parse(codPelicula.Value);
            pelicula.Nombre = txtNombreEdit.Value;
            if (dao.ModificarTipoPelicula(pelicula))
            {
                CargaTipoPelicula();
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
            pelicula.Nombre = txtBuscar.Value;
            if (dao.GetBuscar_Tipo_Pelicula(pelicula))
            {
                tabla_tipo_pelicula.DataSource = dao.DsReturn.Tables["tipos_peliculas"];
                tabla_tipo_pelicula.DataBind();
                Session["peliculas"] = dao.DsReturn;
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