using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Dao.Programacion;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.WebForms.Reportes
{
    public partial class WebReporteSerie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaBackupSerie();
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
        ManejoError error = new ManejoError();

        void CargaBackupSerie()
        {
            if (dao.GetBackup_Serie())
            {
                tabla_reporte_programacion_serie.DataSource = dao.DsReturn.Tables["backup_serie"];
                tabla_reporte_programacion_serie.DataBind();
                Session["backup_s"] = dao.DsReturn;
            }
        }
        void SetBackupSerie()
        {
            tabla_reporte_programacion_serie.DataSource = ((DataSet)Session["backup_s"]);
            tabla_reporte_programacion_serie.DataBind();
        }       

        void LimpiarFormBuscar()
        {
            NomBusqueda.Value = "";
            FechaBusqueda.Value = "";
            CasaBusqueda.Value = "";
            UbicacionBusqueda.Value = "";
        }

        protected void tabla_reporte_programacion_serie_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tabla_reporte_programacion_serie.PageIndex = e.NewPageIndex;
            tabla_reporte_programacion_serie.DataBind();
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
                parametros += " AND NS.NOMBRE LIKE '%" + nombre + "%'";
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
                tabla_reporte_programacion_serie.DataSource = dao.DsReturn.Tables["backup_serie"];
                tabla_reporte_programacion_serie.DataBind();
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

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            // Obtener el DataSet almacenado en Session
            DataSet dataSet = Session["backup_s"] as DataSet;

            // Obtener el título y nombre del archivo proporcionados por el usuario
            string title = txtTituloReporte.Text.Trim();
            string fileName = txtArchivopdf.Text.Trim();

            if (title != "" && fileName != "")
            {
                if (dataSet != null)
                {
                    // Obtener el usuario actual
                    string currentUser = Session["logueado"].ToString();

                    // Obtener la fecha actual
                    string currentDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

                    // Crear el documento PDF en memoria
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Crear el documento PDF
                        PdfWriter pdfWriter = new PdfWriter(ms);
                        PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                        Document document = new Document(pdfDocument, iText.Kernel.Geom.PageSize.LETTER);

                        try
                        {
                            // Agregar encabezado con información del usuario y fecha
                            Paragraph headerParagraph = new Paragraph("Generado por: " + currentUser + " - Fecha: " + currentDate)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(12)
                                .SetFontColor(DeviceRgb.BLACK);
                            document.Add(headerParagraph);

                            // Agregar título al reporte
                            Paragraph titleParagraph = new Paragraph(title)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(18)
                                .SetBold();
                            document.Add(titleParagraph);

                            // Agregar contenido de la tabla al reporte
                            DataTable dt = dataSet.Tables["backup_serie"];

                            // Crear una tabla con la misma cantidad de columnas que la DataTable
                            iText.Layout.Element.Table table = new iText.Layout.Element.Table(dt.Columns.Count);

                            // Configurar el diseño automático de la tabla según el contenido
                            table.SetAutoLayout();

                            // Agregar encabezados de columnas                                               
                            Cell headerCell = new Cell().Add(new Paragraph("ID"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Nombre"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Fecha Guardada"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Episodio Minimo"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Episodio Maximo"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Observaciones"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Casa Productora"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Ubicacion Cinta"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);
                            headerCell = new Cell().Add(new Paragraph("Estado"));
                            headerCell.SetBackgroundColor(DeviceRgb.BLACK);
                            headerCell.SetFontColor(DeviceRgb.WHITE);
                            table.AddHeaderCell(headerCell);

                            // Agregar filas y celdas de datos
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (var item in row.ItemArray)
                                {
                                    Cell cell = new Cell().Add(new Paragraph(item.ToString()));
                                    table.AddCell(cell);
                                }
                            }

                            // Agregar la tabla al documento
                            document.Add(table);
                        }
                        catch (Exception ex)
                        {
                            error.LogError(ex.ToString(), ex.StackTrace);
                        }
                        finally
                        {
                            // Cerrar el documento
                            document.Close();
                        }

                        // Enviar el archivo PDF al navegador como una descarga
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(ms.ToArray());
                        Response.End();
                    }
                }
            }
            else
            {
                string script = @"Swal.fire({
                        showConfirmButton: false,
                        timer: 4000,
                        title: 'Ingrese un titulo y/o nombre de archivo',
                        icon: 'error'                        
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);                
            }            
        }
    }
}