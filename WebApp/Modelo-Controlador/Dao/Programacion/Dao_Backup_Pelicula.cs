using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Backup_Pelicula : DataLayer
    {
        Conexion conexionDB = new Conexion();
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetBackup_Pelicula()
        {
            try
            {
                strSql = "SELECT TOP 100 ID_BACKUP_PELICULA,NP.NOMBRE AS NOMBRE_PELICULA,CASE WHEN FECHA_BACKUP IS NULL THEN '' ELSE CONVERT(varchar, FECHA_BACKUP, 103) END as FECHA_BACKUP," +
                    "CASE WHEN OBSERVACIONES IS NULL THEN '' ELSE OBSERVACIONES END AS OBSERVACIONES,CP.NOMBRE AS CASA_PRODUCTORA,U.NOMBRE AS UBICACION_CINTA," +
                    "CASE WHEN ESTADO = 1 THEN 'COMPLETO' WHEN ESTADO = 0 THEN 'EN BLOQUES' END ESTADO FROM BACKUPS_PELICULAS BP " +
                    "INNER JOIN NOMBRES_PELICULAS NP ON NP.ID_NOMBRE_PELICULA = BP.ID_NOMBRE_PELICULA " +
                    "INNER JOIN CASAS_PRODUCTORAS CP ON CP.ID_CASA_PRODUCTORA = BP.ID_CASA_PRODUCTORA " +
                    "INNER JOIN UBICACIONES_CINTAS U ON U.ID_UBICACION = BP.ID_UBICACION ORDER BY ID_BACKUP_PELICULA DESC";
                DsReturn = conexionDB.DataSQL(strSql, "backup_pelicula");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarBackupPelicula(Backup_peliculas backups)
        {
            try
            {
                strSql = "INSERT INTO BACKUPS_PELICULAS(ID_BACKUP_PELICULA,ID_NOMBRE_PELICULA, FECHA_BACKUP, OBSERVACIONES,ID_CASA_PRODUCTORA,ID_UBICACION,ESTADO) " +
                    "VALUES((SELECT ISNULL(MAX(ID_BACKUP_PELICULA), 0) + 1 FROM BACKUPS_PELICULAS), @nom, GETDATE(), @obse, @casa, @ubi, @estado)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nom", backups.ID_nombre1);
                comando.Parameters.AddWithValue("@obse", backups.Observaciones);
                //comando.Parameters.AddWithValue("@tipo", backups.ID_tipo_pelicula1);
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

        public bool ModificarBackupPelicula(Backup_peliculas backups)
        {
            try
            {
                strSql = "UPDATE BACKUPS_PELICULAS SET ID_NOMBRE_PELICULA = @nom, OBSERVACIONES = @obse, " +
                    "ID_CASA_PRODUCTORA = @casa, ID_UBICACION = @ubi, ESTADO = @estado WHERE ID_BACKUP_PELICULA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nom", backups.ID_nombre1);
                comando.Parameters.AddWithValue("@obse", backups.Observaciones);
                //comando.Parameters.AddWithValue("@tipo", backups.ID_tipo_pelicula1);
                comando.Parameters.AddWithValue("@casa", backups.ID_casa_productora1);
                comando.Parameters.AddWithValue("@ubi", backups.ID_ubicacion1);
                comando.Parameters.AddWithValue("@estado", backups.Estado);
                comando.Parameters.AddWithValue("@id", backups.ID_backup_pelicula1);
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

        public bool EliminarBackupPelicula(Backup_peliculas backup)
        {
            try
            {
                strSql = "DELETE FROM BACKUPS_PELICULAS WHERE ID_BACKUP_PELICULA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", backup.ID_backup_pelicula1);
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
        public bool listBackup_Pelicula(int cod)
        {
            try
            {
                strSql = "SELECT ID_BACKUP_PELICULA,NP.ID_NOMBRE_PELICULA,NP.NOMBRE AS NOMBRE_PELICULA,CASE WHEN FECHA_BACKUP IS NULL THEN '' ELSE CONVERT(varchar, FECHA_BACKUP, 103) END as FECHA_BACKUP," +
                    "CASE WHEN OBSERVACIONES IS NULL THEN '' ELSE OBSERVACIONES END AS OBSERVACIONES," +
                    "CP.ID_CASA_PRODUCTORA,CP.NOMBRE AS CASA_PRODUCTORA,U.ID_UBICACION,CONCAT(U.NOMBRE, ' - ' ,U.DESCRIPCION) AS UBICACION_CINTA,ESTADO AS ID_ESTADO,CASE WHEN ESTADO = 1 THEN 'COMPLETO' WHEN ESTADO = 0 THEN 'EN BLOQUES' END ESTADO " +
                    "FROM BACKUPS_PELICULAS BP INNER JOIN NOMBRES_PELICULAS NP ON NP.ID_NOMBRE_PELICULA = BP.ID_NOMBRE_PELICULA " +
                    "INNER JOIN CASAS_PRODUCTORAS CP ON CP.ID_CASA_PRODUCTORA = BP.ID_CASA_PRODUCTORA " +
                    "INNER JOIN UBICACIONES_CINTAS U ON U.ID_UBICACION = BP.ID_UBICACION " +
                    "WHERE ID_BACKUP_PELICULA = " + cod;
                DsReturn = conexionDB.DataSQL(strSql, "list_backup_pelicula");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetBuscar_Backup_Pelicula(string parametros)
        {
            try
            {
                strSql = "SELECT ID_BACKUP_PELICULA,NP.NOMBRE AS NOMBRE_PELICULA,CASE WHEN FECHA_BACKUP IS NULL THEN '' ELSE CONVERT(varchar, FECHA_BACKUP, 103) END as FECHA_BACKUP," +
                    "CASE WHEN OBSERVACIONES IS NULL THEN '' ELSE OBSERVACIONES END AS OBSERVACIONES,CP.NOMBRE AS CASA_PRODUCTORA,U.NOMBRE AS UBICACION_CINTA," +
                    "CASE WHEN ESTADO = 1 THEN 'COMPLETO' WHEN ESTADO = 0 THEN 'EN BLOQUES' END ESTADO FROM BACKUPS_PELICULAS BP " +
                    "INNER JOIN NOMBRES_PELICULAS NP ON NP.ID_NOMBRE_PELICULA = BP.ID_NOMBRE_PELICULA " +
                    "INNER JOIN CASAS_PRODUCTORAS CP ON CP.ID_CASA_PRODUCTORA = BP.ID_CASA_PRODUCTORA " +
                    "INNER JOIN UBICACIONES_CINTAS U ON U.ID_UBICACION = BP.ID_UBICACION " + parametros + " ORDER BY ID_BACKUP_PELICULA DESC";
                DsReturn = conexionDB.DataSQL(strSql, "backup_pelicula");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }
    }
}