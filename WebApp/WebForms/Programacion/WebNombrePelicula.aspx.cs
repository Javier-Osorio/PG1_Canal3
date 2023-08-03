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
    public partial class WebNombrePelicula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack) { 
                    CargaNombrePelicula();
                }
                else
                    SetNombrePelicula();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        Dao_Nombre_Pelicula dao = new Dao_Nombre_Pelicula();
        Nombre_pelicula pelicula = new Nombre_pelicula();
        ManejoError error = new ManejoError();

        void CargaNombrePelicula()
        {
            if (dao.GetNombre_Pelicula())
            {
                tabla_nombre_pelicula.DataSource = dao.DsReturn.Tables["nombres_peliculas"];
                tabla_nombre_pelicula.DataBind();
                Session["nombre_peli"] = dao.DsReturn;
            }
        }
        void SetNombrePelicula()
        {
            tabla_nombre_pelicula.DataSource = ((DataSet)Session["nombre_peli"]);
            tabla_nombre_pelicula.DataBind();
        }
        void LimpiarTexto()
        {
            txtNombreEdit.Value = "";
            txtNombreRegister.Value = "";
        }

        protected void tabla_nombre_pelicula_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_nombre_pelicula.PageIndex = e.NewPageIndex;
            tabla_nombre_pelicula.DataBind();
        }

        protected void tabla_nombre_pelicula_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = tabla_nombre_pelicula.Rows[e.NewEditIndex];
            codNombre.Value = row.Cells[0].Text;
            txtNombreEdit.Value = row.Cells[1].Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_nombre_pelicula_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            pelicula.ID_Nombre_pelicula1 = int.Parse(tabla_nombre_pelicula.DataKeys[e.RowIndex].Value.ToString());
            if (dao.EliminarNombrePelicula(pelicula))
            {
                CargaNombrePelicula();
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
            if (dao.InsertarNombrePelicula(pelicula))
            {
                CargaNombrePelicula();
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
            pelicula.ID_Nombre_pelicula1 = int.Parse(codNombre.Value);
            pelicula.Nombre = txtNombreEdit.Value;
            if (dao.ModificarNombrePelicula(pelicula))
            {
                CargaNombrePelicula();
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
            if (dao.GetBuscarNombre_Pelicula(pelicula))
            {
                tabla_nombre_pelicula.DataSource = dao.DsReturn.Tables["nombres_peliculas"];
                tabla_nombre_pelicula.DataBind();
                Session["nombre_peli"] = dao.DsReturn;
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