using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Dao.Programacion;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.WebForms.Programacion
{
    public partial class WebProgramacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaBackupSerie();
                    LlenarListados();
                }
                else
                    SetBackupSerie();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }
        Dao_Backup_Serie dao = new Dao_Backup_Serie();
        Backup_series series = new Backup_series();
        Dao_Nombre_Serie dao_Nombre = new Dao_Nombre_Serie();
        Dao_Casa_Productora dao_Casa = new Dao_Casa_Productora();
        Dao_Ubicacion dao_Ubicacion = new Dao_Ubicacion();
        ManejoError error = new ManejoError();

        void CargaBackupSerie()
        {
            if (dao.GetBackup_Serie())
            {
                tabla_programacion_serie.DataSource = dao.DsReturn.Tables["backup_serie"];
                tabla_programacion_serie.DataBind();
                Session["backup_s"] = dao.DsReturn;
            }
        }
        void SetBackupSerie()
        {
            tabla_programacion_serie.DataSource = ((DataSet)Session["backup_s"]);
            tabla_programacion_serie.DataBind();
        }

        void LlenarListados()
        {
            if (dao_Nombre.GetNombre_Serie())
            {
                ddlNombreMaterial.DataSource = dao_Nombre.DsReturn.Tables["nombres_series"];                
                ddlNombreMaterial.DataTextField = dao_Nombre.DsReturn.Tables["nombres_series"].Columns["NOMBRE"].ToString();
                ddlNombreMaterial.DataValueField = dao_Nombre.DsReturn.Tables["nombres_series"].Columns["ID_NOMBRE_SERIE"].ToString();
                ddlNombreMaterial.DataBind();
            }
            if (dao_Casa.GetCasa_Productora())
            {
                ddlCasaProductora.DataSource = dao_Casa.DsReturn.Tables["casa_productora"];
                ddlCasaProductora.DataTextField = dao_Casa.DsReturn.Tables["casa_productora"].Columns["NOMBRE"].ToString();
                ddlCasaProductora.DataValueField = dao_Casa.DsReturn.Tables["casa_productora"].Columns["ID_CASA_PRODUCTORA"].ToString();
                ddlCasaProductora.DataBind();
            }
            
            if (dao_Ubicacion.GetUbicacionesOpcion())
            {
                ddlUbicacion.DataSource = dao_Ubicacion.DsReturn.Tables["ubicacion"];
                ddlUbicacion.DataTextField = dao_Ubicacion.DsReturn.Tables["ubicacion"].Columns["NOMBRE"].ToString();
                ddlUbicacion.DataValueField = dao_Ubicacion.DsReturn.Tables["ubicacion"].Columns["ID_UBICACION"].ToString();
                ddlUbicacion.DataBind();
            }
        }

        void limpiarTextos()
        {
            txtEpisodioMax.Value = "";
            txtEpisodioMin.Value = "";
            txtObservaciones.Value = "";
            ddlEstado.SelectedIndex = 0;
            ddlCasaProductora.SelectedIndex = 0;
            ddlNombreMaterial.SelectedIndex = 0;
            
            ddlUbicacion.SelectedIndex = 0;
        }
        void limpiarDDLS()
        {
            ddlEstadoEditar.Items.Clear();
            ddlCasaEditar.Items.Clear();
            ddlNombreEditar.Items.Clear();
            
            ddlUbicacionEditar.Items.Clear();
        }
        void LlenarDDLSEditar()
        {
            if (dao_Nombre.GetNombre_Serie())
            {
                ListItem item;
                foreach (DataRow list in dao_Nombre.DsReturn.Tables["nombres_series"].Rows)
                {
                    string idnom = list["ID_NOMBRE_SERIE"].ToString();
                    string nom = list["NOMBRE"].ToString();
                    item = new ListItem(nom, idnom);
                    ddlNombreEditar.Items.Add(item);
                }
            }
            if (dao_Casa.GetCasa_Productora())
            {
                ListItem item;
                foreach (DataRow list in dao_Casa.DsReturn.Tables["casa_productora"].Rows)
                {
                    string idcasa = list["ID_CASA_PRODUCTORA"].ToString();
                    string casa = list["NOMBRE"].ToString();
                    item = new ListItem(casa, idcasa);
                    ddlCasaEditar.Items.Add(item);
                }
            }
            
            if (dao_Ubicacion.GetUbicacionesOpcion())
            {
                ListItem item;
                foreach (DataRow list in dao_Ubicacion.DsReturn.Tables["ubicacion"].Rows)
                {
                    string idubic = list["ID_UBICACION"].ToString();
                    string ubic = list["NOMBRE"].ToString();
                    item = new ListItem(ubic, idubic);
                    ddlUbicacionEditar.Items.Add(item);
                }
            }
            ListItem listItem;
            listItem = new ListItem("COMPLETO","1");
            ddlEstadoEditar.Items.Add(listItem);
            listItem = new ListItem("EN BLOQUES", "0");
            ddlEstadoEditar.Items.Add(listItem);
        }

        void LimpiarFormBuscar()
        {
            NomBusqueda.Value = "";
            FechaBusqueda.Value = "";
            CasaBusqueda.Value = "";
            UbicacionBusqueda.Value = "";
        }

        protected void tabla_programacion_serie_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_programacion_serie.PageIndex = e.NewPageIndex;
            tabla_programacion_serie.DataBind();
        }

        protected void tabla_programacion_serie_RowEditing(object sender, GridViewEditEventArgs e)
        {
            limpiarDDLS();
            GridViewRow row = tabla_programacion_serie.Rows[e.NewEditIndex];
            int id = int.Parse(row.Cells[0].Text);
            if (dao.listBackup_Serie(id))
            {
                ListItem item;
                codBackupSerie.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_BACKUP_SERIE"].ToString();
                string idnom = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_NOMBRE_SERIE"].ToString();
                string nom = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["NOMBRE_SERIE"].ToString();
                item = new ListItem(nom, idnom);
                ddlNombreEditar.Items.Add(item);
                txtEpisodioMinEditar.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["CANTIDAD_EPISODIO_MIN"].ToString();
                txtEpisodioMaxEditar.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["CANTIDAD_EPISODIO_MAX"].ToString();
                txtObserEditar.Value = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["OBSERVACIONES"].ToString();                
                string idcasa = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_CASA_PRODUCTORA"].ToString();
                string casa = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["CASA_PRODUCTORA"].ToString();
                item = new ListItem(casa, idcasa);
                ddlCasaEditar.Items.Add(item);
                string idubica = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_UBICACION"].ToString();
                string ubica = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["UBICACION_CINTA"].ToString();
                item = new ListItem(ubica, idubica);
                ddlUbicacionEditar.Items.Add(item);
                string idestado = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ID_ESTADO"].ToString();
                string estado = dao.DsReturn.Tables["list_backup_serie"].Rows[0]["ESTADO"].ToString();
                item = new ListItem(estado, idestado);
                ddlEstadoEditar.Items.Add(item);

                LlenarDDLSEditar();
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_programacion_serie_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            series.ID_backup_serie1 = int.Parse(tabla_programacion_serie.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarBackupSerie(series))
            {
                CargaBackupSerie();
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
            series.ID_nombre1 = int.Parse(ddlNombreMaterial.SelectedValue);
            series.Cantidad_episodio_min = int.Parse(txtEpisodioMin.Value);
            series.Cantidad_episodio_max = int.Parse(txtEpisodioMax.Value);
            series.Observaciones = txtObservaciones.Value;
            
            series.ID_casa_productora1 = int.Parse(ddlCasaProductora.SelectedValue);
            series.ID_ubicacion1 = int.Parse(ddlUbicacion.SelectedValue);
            series.Estado = int.Parse(ddlEstado.SelectedValue);

            if (dao.InsertarBackupSerie(series))
            {
                CargaBackupSerie();
                limpiarTextos();
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
            series.ID_nombre1 = int.Parse(ddlNombreEditar.SelectedValue);
            series.Cantidad_episodio_min = int.Parse(txtEpisodioMinEditar.Value);
            series.Cantidad_episodio_max = int.Parse(txtEpisodioMaxEditar.Value);
            series.Observaciones = txtObserEditar.Value;
           
            series.ID_casa_productora1 = int.Parse(ddlCasaEditar.SelectedValue);
            series.ID_ubicacion1 = int.Parse(ddlUbicacionEditar.SelectedValue);
            series.Estado = int.Parse(ddlEstadoEditar.SelectedValue);
            series.ID_backup_serie1 = int.Parse(codBackupSerie.Value);

            if (dao.ModificarBackupSerie(series))
            {
                CargaBackupSerie();
                limpiarDDLS();
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
                limpiarDDLS();
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
            string nombre = NomBusqueda.Value;
            string fecha = FechaBusqueda.Value;
            string casa = CasaBusqueda.Value;
            string ubicacion = UbicacionBusqueda.Value;

            string parametros = "WHERE 1=1";
            if (!string.IsNullOrEmpty(nombre))
            {
                parametros += " AND NS.NOMBRE LIKE '%"+ nombre+"%'";
            }
            if (!string.IsNullOrEmpty(fecha))
            {
                DateTime fechaConvertida = DateTime.ParseExact(fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string fechaForm = fechaConvertida.ToString("dd/MM/yyyy");
                parametros += " AND CONVERT(varchar,FECHA_BACKUP, 103) = '" + fechaForm + "'";                            
            }
            if (!string.IsNullOrEmpty(casa))
            {
                parametros += " AND CP.NOMBRE LIKE '%" + casa + "%'";
            }
            if (!string.IsNullOrEmpty(ubicacion))
            {
                parametros += " AND U.NOMBRE LIKE '%" + ubicacion + "%'";
            }

            if (dao.GetBusqueda_Backup_Serie(parametros))
            {
                tabla_programacion_serie.DataSource = dao.DsReturn.Tables["backup_serie"];
                tabla_programacion_serie.DataBind();
                Session["backup_s"] = dao.DsReturn;
                LimpiarFormBuscar();
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