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
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetBackup_Serie()
        {
            try
            {
                strSql = "SELECT ID_BACKUP_SERIE," +
                    "NS.NOMBRE AS NOMBRE_SERIE," +
                    "CASE WHEN FECHA_BACKUP IS NULL THEN '' ELSE CONVERT(varchar, FECHA_BACKUP, 103) END as FECHA_BACKUP," +
                    "CANTIDAD_EPISODIO_MIN," +
                    "CANTIDAD_EPISODIO_MAX," +
                    "CASE WHEN OBSERVACIONES IS NULL THEN '' ELSE OBSERVACIONES END AS OBSERVACIONES," +
                    "TS.NOMBRE AS TIPO_SERIE," +
                    "CP.NOMBRE AS CASA_PRODUCTORA," +
                    "U.NOMBRE AS UBICACION_CINTA,CASE WHEN ESTADO = 1 THEN 'COMPLETO' WHEN ESTADO = 0 THEN 'EN BLOQUES' END ESTADO " +
                    "FROM BACKUPS_SERIES BS " +
                    "INNER JOIN NOMBRES_SERIES NS ON NS.ID_NOMBRE_SERIE = BS.ID_NOMBRE_SERIE " +
                    "INNER JOIN TIPOS_SERIES TS ON TS.ID_TIPO_SERIE = BS.ID_TIPO_SERIE INNER JOIN CASAS_PRODUCTORAS CP ON CP.ID_CASA_PRODUCTORA = BS.ID_CASA_PRODUCTORA " +
                    "INNER JOIN UBICACIONES_CINTAS U ON U.ID_UBICACION = BS.ID_UBICACION";
                DsReturn = conexionDB.DataSQL(strSql, "backup_serie");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool listBackup_Serie(int cod)
        {
            try
            {
                strSql = "SELECT ID_BACKUP_SERIE,NS.ID_NOMBRE_SERIE,NS.NOMBRE AS NOMBRE_SERIE,CANTIDAD_EPISODIO_MIN,CANTIDAD_EPISODIO_MAX," +
                    "CASE WHEN OBSERVACIONES IS NULL THEN '' ELSE OBSERVACIONES END AS OBSERVACIONES,TS.ID_TIPO_SERIE,TS.NOMBRE AS TIPO_SERIE," +
                    "CP.ID_CASA_PRODUCTORA,CP.NOMBRE AS CASA_PRODUCTORA,U.ID_UBICACION,U.NOMBRE AS UBICACION_CINTA,ESTADO AS ID_ESTADO," +
                    "CASE WHEN ESTADO = 1 THEN 'COMPLETO' WHEN ESTADO = 0 THEN 'EN BLOQUES' END ESTADO FROM BACKUPS_SERIES BS " +
                    "INNER JOIN NOMBRES_SERIES NS ON NS.ID_NOMBRE_SERIE = BS.ID_NOMBRE_SERIE " +
                    "INNER JOIN TIPOS_SERIES TS ON TS.ID_TIPO_SERIE = BS.ID_TIPO_SERIE " +
                    "INNER JOIN CASAS_PRODUCTORAS CP ON CP.ID_CASA_PRODUCTORA = BS.ID_CASA_PRODUCTORA " +
                    "INNER JOIN UBICACIONES_CINTAS U ON U.ID_UBICACION = BS.ID_UBICACION WHERE ID_BACKUP_SERIE = " +cod;
                DsReturn = conexionDB.DataSQL(strSql, "list_backup_serie");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarBackupSerie(Backup_series backups)
        {
            try
            {
                strSql = "INSERT INTO BACKUPS_SERIES(ID_BACKUP_SERIE, ID_NOMBRE_SERIE,FECHA_BACKUP,CANTIDAD_EPISODIO_MIN,CANTIDAD_EPISODIO_MAX,OBSERVACIONES,ID_TIPO_SERIE,ID_CASA_PRODUCTORA,ID_UBICACION,ESTADO) " +
                    "VALUES((SELECT ISNULL(MAX(ID_BACKUP_SERIE), 0) + 1 FROM BACKUPS_SERIES), @nom, GETDATE(), @canmin, @canmax, @obse, @tipo, @casa, @ubi, @estado)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nom", backups.ID_nombre1);
                comando.Parameters.AddWithValue("@canmin", backups.Cantidad_episodio_min);
                comando.Parameters.AddWithValue("@canmax", backups.Cantidad_episodio_max);
                comando.Parameters.AddWithValue("@obse", backups.Observaciones);
                comando.Parameters.AddWithValue("@tipo", backups.ID_tipo_serie1);
                comando.Parameters.AddWithValue("@casa", backups.ID_casa_productora1);
                comando.Parameters.AddWithValue("@ubi", backups.ID_ubicacion1);
                comando.Parameters.AddWithValue("@estado", backups.Estado);
                comando.ExecuteNonQuery();
                conectar.Close();
                return true;
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                conectar.Close();
                return false;
            }
        }

        public bool ModificarBackupSerie(Backup_series backup)
        {
            try
            {
                strSql = "UPDATE BACKUPS_SERIES SET ID_NOMBRE_SERIE = @nom, CANTIDAD_EPISODIO_MIN = @canmin, CANTIDAD_EPISODIO_MAX = @canmax, OBSERVACIONES = @obser, " +
                    "ID_TIPO_SERIE = @tipo, ID_CASA_PRODUCTORA = @casa,ID_UBICACION = @ubi, ESTADO = @estado WHERE ID_BACKUP_SERIE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nom", backup.ID_nombre1);
                comando.Parameters.AddWithValue("@canmin", backup.Cantidad_episodio_min);
                comando.Parameters.AddWithValue("@canmax", backup.Cantidad_episodio_max);
                comando.Parameters.AddWithValue("@obser", backup.Observaciones);
                comando.Parameters.AddWithValue("@tipo", backup.ID_tipo_serie1);
                comando.Parameters.AddWithValue("@casa", backup.ID_casa_productora1);
                comando.Parameters.AddWithValue("@ubi", backup.ID_ubicacion1);
                comando.Parameters.AddWithValue("@estado", backup.Estado);
                comando.Parameters.AddWithValue("@id", backup.ID_backup_serie1);
                comando.ExecuteNonQuery();
                conectar.Close();
                return true;
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                conectar.Close();
                return false;
            }
        }

        public bool EliminarBackupSerie(Backup_series backup)
        {
            try
            {
                strSql = "DELETE FROM BACKUPS_SERIES WHERE ID_BACKUP_SERIE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", backup.ID_backup_serie1);
                comando.ExecuteNonQuery();
                conectar.Close();
                return true;
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                conectar.Close();
                return false;
            }
        }
    }
}