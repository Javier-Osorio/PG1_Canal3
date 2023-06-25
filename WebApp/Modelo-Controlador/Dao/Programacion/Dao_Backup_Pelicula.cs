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
        SqlConnection conectar;

        string strSql;

        public bool GetBackup_Pelicula()
        {
            try
            {
                strSql = "SELECT dbo.BACKUPS_PELICULAS.ID_BACKUP_PELICULA, \n"
                        + " dbo.NOMBRES_MATERIALES.NOMBRE, \n"
                        + " (CASE \n"
                        + " WHEN dbo.BACKUPS_PELICULAS.FECHA_BACKUP IS NULL THEN '' \n"
                        + " ELSE CONVERT(varchar, dbo.BACKUPS_PELICULAS.FECHA_BACKUP, 103) \n"
                        + " END) as FECHA_BACKUP, \n"
                        + " (CASE \n"
                        + " WHEN dbo.BACKUPS_PELICULAS.OBSERVACIONES IS NULL THEN '' \n"
                        + " ELSE dbo.BACKUPS_PELICULAS.OBSERVACIONES \n"
                        + " END) AS OBSERVACIONES, \n"
                        + " dbo.TIPOS_PELICULAS.NOMBRE AS TIPO_PELICULA, \n"
                        + " dbo.CASAS_PRODUCTORAS.NOMBRE AS CASA_PRODUCTORA, \n"
                        + " dbo.UBICACIONES_CINTAS.NOMBRE AS UBICACION_CINTA, \n"
                        + " CASE \n"
                        + " WHEN dbo.BACKUPS_PELICULAS.ESTADO = 1 THEN 'COMPLETO' \n"
                        + " END ESTADO \n"
                        + " FROM dbo.BACKUPS_PELICULAS INNER JOIN \n"
                         + " dbo.CASAS_PRODUCTORAS ON dbo.BACKUPS_PELICULAS.ID_CASA_PRODUCTORA = dbo.CASAS_PRODUCTORAS.ID_CASA_PRODUCTORA INNER JOIN \n"
                         + " dbo.NOMBRES_MATERIALES ON dbo.BACKUPS_PELICULAS.ID_NOMBRE = dbo.NOMBRES_MATERIALES.ID_NOMBRE INNER JOIN \n"
                         + " dbo.TIPOS_PELICULAS ON dbo.BACKUPS_PELICULAS.ID_TIPO_PELICULA = dbo.TIPOS_PELICULAS.ID_TIPO_PELICULA INNER JOIN \n"
                         + " dbo.UBICACIONES_CINTAS ON dbo.BACKUPS_PELICULAS.ID_UBICACION = dbo.UBICACIONES_CINTAS.ID_UBICACION ";
                DsReturn = conexionDB.DataSQL(strSql, "backup_pelicula");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarBackupPelicula(Backup_peliculas backups)
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

        public bool ModificarBackupPelicula(Backup_peliculas backup)
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

        public bool EliminarBackupPelicula(Backup_peliculas backup)
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