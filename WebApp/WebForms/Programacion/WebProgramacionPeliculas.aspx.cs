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
    public partial class WebProgramacionPeliculas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaBackupPelicula();
                    LlenarListados();
                }
                   
                else
                    SetBackupPelicula();
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        Dao_Backup_Pelicula dao = new Dao_Backup_Pelicula();
        Backup_peliculas peliculas = new Backup_peliculas();
        Dao_Nombre_Pelicula dao_Nombre = new Dao_Nombre_Pelicula();
        Dao_Casa_Productora dao_Casa = new Dao_Casa_Productora();
        Dao_Tipo_Pelicula dao_Tipo_Pelicula = new Dao_Tipo_Pelicula();
        Dao_Ubicacion dao_Ubicacion = new Dao_Ubicacion();
        ManejoError error = new ManejoError();

        void CargaBackupPelicula()
        {
            if (dao.GetBackup_Pelicula())
            {
                tabla_programacion_pelicula.DataSource = dao.DsReturn.Tables["backup_pelicula"];
                tabla_programacion_pelicula.DataBind();
                Session["backup_p"] = dao.DsReturn;
            }
        }

        void SetBackupPelicula()
        {
            tabla_programacion_pelicula.DataSource = ((DataSet)Session["backup_p"]);
            tabla_programacion_pelicula.DataBind();
        }

        void LlenarListados()
        {
            if (dao_Nombre.GetNombre_Pelicula())
            {
                ddlNombreMaterial.DataSource = dao_Nombre.DsReturn.Tables["nombres_peliculas"];
                ddlNombreMaterial.DataTextField = dao_Nombre.DsReturn.Tables["nombres_peliculas"].Columns["NOMBRE"].ToString();
                ddlNombreMaterial.DataValueField = dao_Nombre.DsReturn.Tables["nombres_peliculas"].Columns["ID_NOMBRE_PELICULA"].ToString();
                ddlNombreMaterial.DataBind();
            }
            if (dao_Casa.GetCasa_Productora())
            {
                ddlCasaProductora.DataSource = dao_Casa.DsReturn.Tables["casa_productora"];
                ddlCasaProductora.DataTextField = dao_Casa.DsReturn.Tables["casa_productora"].Columns["NOMBRE"].ToString();
                ddlCasaProductora.DataValueField = dao_Casa.DsReturn.Tables["casa_productora"].Columns["ID_CASA_PRODUCTORA"].ToString();
                ddlCasaProductora.DataBind();
            }
            if (dao_Tipo_Pelicula.GetTipo_Pelicula())
            {
                ddlTipoSerie.DataSource = dao_Tipo_Pelicula.DsReturn.Tables["tipos_peliculas"];
                ddlTipoSerie.DataTextField = dao_Tipo_Pelicula.DsReturn.Tables["tipos_peliculas"].Columns["NOMBRE"].ToString();
                ddlTipoSerie.DataValueField = dao_Tipo_Pelicula.DsReturn.Tables["tipos_peliculas"].Columns["ID_TIPO_PELICULA"].ToString();
                ddlTipoSerie.DataBind();
            }
            if (dao_Ubicacion.GetUbicaciones())
            {
                ddlUbicacion.DataSource = dao_Ubicacion.DsReturn.Tables["ubicacion"];
                ddlUbicacion.DataTextField = dao_Ubicacion.DsReturn.Tables["ubicacion"].Columns["NOMBRE"].ToString();
                ddlUbicacion.DataValueField = dao_Ubicacion.DsReturn.Tables["ubicacion"].Columns["ID_UBICACION"].ToString();
                ddlUbicacion.DataBind();
            }
        }
        void limpiarTextos()
        {
            txtObservaciones.Value = "";
            ddlEstado.SelectedIndex = 0;
            ddlCasaProductora.SelectedIndex = 0;
            ddlNombreMaterial.SelectedIndex = 0;
            ddlTipoSerie.SelectedIndex = 0;
            ddlUbicacion.SelectedIndex = 0;
        }
        void limpiarDDLS()
        {
            ddlEstadoEditar.Items.Clear();
            ddlCasaEditar.Items.Clear();
            ddlNombreEditar.Items.Clear();
            ddlTipoEditar.Items.Clear();
            ddlUbicacionEditar.Items.Clear();
        }

        void LlenarDDLSEditar()
        {
            if (dao_Nombre.GetNombre_Pelicula())
            {
                ListItem item;
                foreach (DataRow list in dao_Nombre.DsReturn.Tables["nombres_peliculas"].Rows)
                {
                    string idnom = list["ID_NOMBRE_PELICULA"].ToString();
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
            if (dao_Tipo_Pelicula.GetTipo_Pelicula())
            {
                ListItem item;
                foreach (DataRow list in dao_Tipo_Pelicula.DsReturn.Tables["tipos_peliculas"].Rows)
                {
                    string idtipo = list["ID_TIPO_PELICULA"].ToString();
                    string tipo = list["NOMBRE"].ToString();
                    item = new ListItem(tipo, idtipo);
                    ddlTipoEditar.Items.Add(item);
                }
            }
            if (dao_Ubicacion.GetUbicaciones())
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
            listItem = new ListItem("COMPLETO", "1");
            ddlEstadoEditar.Items.Add(listItem);
            listItem = new ListItem("EN BLOQUES", "0");
            ddlEstadoEditar.Items.Add(listItem);
        }

        protected void tabla_programacion_pelicula_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_programacion_pelicula.PageIndex = e.NewPageIndex;
            tabla_programacion_pelicula.DataBind();
        }

        protected void tabla_programacion_pelicula_RowEditing(object sender, GridViewEditEventArgs e)
        {
            limpiarDDLS();
            GridViewRow row = tabla_programacion_pelicula.Rows[e.NewEditIndex];
            int id = int.Parse(row.Cells[0].Text);
            if (dao.listBackup_Pelicula(id))
            {
                ListItem item;
                codBackupPelicula.Value = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["ID_BACKUP_PELICULA"].ToString();
                string idnom = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["ID_NOMBRE_PELICULA"].ToString();
                string nom = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["NOMBRE_PELICULA"].ToString();
                item = new ListItem(nom, idnom);
                ddlNombreEditar.Items.Add(item);
                txtObserEditar.Value = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["OBSERVACIONES"].ToString();
                string idtipo = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["ID_TIPO_PELICULA"].ToString();
                string tipo = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["TIPO_PELICULA"].ToString();
                item = new ListItem(tipo, idtipo);
                ddlTipoEditar.Items.Add(item);
                string idcasa = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["ID_CASA_PRODUCTORA"].ToString();
                string casa = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["CASA_PRODUCTORA"].ToString();
                item = new ListItem(casa, idcasa);
                ddlCasaEditar.Items.Add(item);
                string idubica = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["ID_UBICACION"].ToString();
                string ubica = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["UBICACION_CINTA"].ToString();
                item = new ListItem(ubica, idubica);
                ddlUbicacionEditar.Items.Add(item);
                string idestado = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["ID_ESTADO"].ToString();
                string estado = dao.DsReturn.Tables["list_backup_pelicula"].Rows[0]["ESTADO"].ToString();
                item = new ListItem(estado, idestado);
                ddlEstadoEditar.Items.Add(item);

                LlenarDDLSEditar();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEditar", "$('#modalEditar').modal('show');", true);
            e.Cancel = true; // Cancelar la edición en el GridView
        }

        protected void tabla_programacion_pelicula_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            peliculas.ID_backup_pelicula1 = int.Parse(tabla_programacion_pelicula.DataKeys[e.RowIndex].Value.ToString());

            if (dao.EliminarBackupPelicula(peliculas))
            {
                CargaBackupPelicula();
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
            peliculas.ID_nombre1 = int.Parse(ddlNombreMaterial.SelectedValue);
            peliculas.Observaciones = txtObservaciones.Value;
            peliculas.ID_tipo_pelicula1 = int.Parse(ddlTipoSerie.SelectedValue);
            peliculas.ID_casa_productora1 = int.Parse(ddlCasaProductora.SelectedValue);
            peliculas.ID_ubicacion1 = int.Parse(ddlUbicacion.SelectedValue);
            peliculas.Estado = int.Parse(ddlEstado.SelectedValue);

            if (dao.InsertarBackupPelicula(peliculas))
            {
                CargaBackupPelicula();
                limpiarTextos();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Se registro correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                limpiarTextos();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se guardo'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            peliculas.ID_nombre1 = int.Parse(ddlNombreMaterial.SelectedValue);
            peliculas.Observaciones = txtObservaciones.Value;
            peliculas.ID_tipo_pelicula1 = int.Parse(ddlTipoSerie.SelectedValue);
            peliculas.ID_casa_productora1 = int.Parse(ddlCasaProductora.SelectedValue);
            peliculas.ID_ubicacion1 = int.Parse(ddlUbicacion.SelectedValue);
            peliculas.Estado = int.Parse(ddlEstado.SelectedValue);
            peliculas.ID_backup_pelicula1 = int.Parse(codBackupPelicula.Value);

            if (dao.ModificarBackupPelicula(peliculas))
            {
                CargaBackupPelicula();
                limpiarDDLS();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro modificado correctamente'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
            else
            {
                limpiarDDLS();
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('Registro no se modifico'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
            }
        }
    }
}