using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Backup_Serie : DataLayer
    {
        Conexion conexionDB = new Conexion();
        SqlConnection conectar;

        string strSql;

        public bool GetBackup_Serie()
        {
            try
            {
                strSql = "SELECT dbo.BACKUPS_SERIES.ID_BACKUP_SERIE, "
                        + "  dbo.NOMBRES_MATERIALES.NOMBRE,"
                        + " (CASE WHEN dbo.BACKUPS_SERIES.FECHA_BACKUP IS NULL THEN '' "
                        + " ELSE CONVERT(varchar, dbo.BACKUPS_SERIES.FECHA_BACKUP, 103) "
                        + " END) as FECHA_BACKUP,"
                    + " dbo.BACKUPS_SERIES.CANTIDAD_EPISODIO_MIN, "
                    + " dbo.BACKUPS_SERIES.CANTIDAD_EPISODIO_MAX,"
                    + " (CASE"
                     + " WHEN dbo.BACKUPS_SERIES.OBSERVACIONES IS NULL THEN ''"
                      + " ELSE dbo.BACKUPS_SERIES.OBSERVACIONES"
                    + " END) AS OBSERVACIONES,"
                    + " dbo.TIPOS_SERIES.NOMBRE AS TIPO_SERIE, "
                    + " dbo.CASAS_PRODUCTORAS.NOMBRE AS CASA_PRODUCTORA, "
                    + " dbo.UBICACIONES_CINTAS.NOMBRE AS UBICACION_CINTA,"
                    + " CASE"
                     + " WHEN dbo.BACKUPS_SERIES.ESTADO = 1 THEN 'COMPLETO'"
                      + " WHEN dbo.BACKUPS_SERIES.ESTADO = 2 THEN 'EN BLOQUES'"
                       + " END ESTADO"
                    + " FROM dbo.BACKUPS_SERIES INNER JOIN"
                         + " dbo.CASAS_PRODUCTORAS ON dbo.BACKUPS_SERIES.ID_CASA_PRODUCTORA = dbo.CASAS_PRODUCTORAS.ID_CASA_PRODUCTORA INNER JOIN"
                         + " dbo.NOMBRES_MATERIALES ON dbo.BACKUPS_SERIES.ID_NOMBRE = dbo.NOMBRES_MATERIALES.ID_NOMBRE INNER JOIN"
                         + " dbo.TIPOS_SERIES ON dbo.BACKUPS_SERIES.ID_TIPO_SERIE = dbo.TIPOS_SERIES.ID_TIPO_SERIE INNER JOIN"
                        + " dbo.UBICACIONES_CINTAS ON dbo.BACKUPS_SERIES.ID_UBICACION = dbo.UBICACIONES_CINTAS.ID_UBICACION";
                DsReturn = conexionDB.DataSQL(strSql, "backup_serie");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarBackupSerie(Backup_series backups)
        {
            try
            {
                strSql = "";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                //comando.Parameters.AddWithValue("@nombre", p.Nombre);
                comando.ExecuteNonQuery();
                conectar.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                conectar.Close();
                return false;
            }
        }

        public bool ModificarBackupSerie(Backup_series backup)
        {
            try
            {
                strSql = "";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                //comando.Parameters.AddWithValue("@nombre", s.Nombre);
                //comando.Parameters.AddWithValue("@id", s.ID_tipo_serie1);
                comando.ExecuteNonQuery();
                conectar.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                conectar.Close();
                return false;
            }
        }

        public bool EliminarBackupSerie(Backup_series backup)
        {
            try
            {
                strSql = "";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                //comando.Parameters.AddWithValue("@id", p.ID_tipo_pelicula1);
                comando.ExecuteNonQuery();
                conectar.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                conectar.Close();
                return false;
            }
        }
    }
}