using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp.Modelo_Controlador.Dao.LogIn
{
    public class Dao_Modulos : DataLayer
    {
        Conexion conexionDB = new Conexion();
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetModulos()
        {
            try
            {
                strSql = "SELECT MH.ID_MODULO, MH.NOMBRE AS MODULO, MH.PATH_URL, MP.NOMBRE AS MODULO_PADRE,CASE WHEN MP.ESTADO = 1 THEN 'ACTIVO' " +
                    "WHEN MP.ESTADO = 0 THEN 'INACTIVO' END ESTADO FROM MODULOS MP INNER JOIN MODULOS MH ON MP.ID_MODULO = MH.ID_MODULO_PADRE";
                DsReturn = conexionDB.DataSQL(strSql, "modulos");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetModulosDDL()
        {
            try
            {
                strSql = "SELECT ID_MODULO, NOMBRE FROM MODULOS";
                DsReturn = conexionDB.DataSQL(strSql, "ddlmodulos");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetModulosList(int id)
        {
            try
            {
                strSql = "SELECT ID_MODULO_PADRE, ESTADO AS ID_ESTADO, CASE WHEN ESTADO = 1 THEN 'ACTIVO' " +
                    "WHEN ESTADO = 0 THEN 'INACTIVO' END ESTADO  FROM MODULOS WHERE ID_MODULO = " + id;
                DsReturn = conexionDB.DataSQL(strSql, "ddlmodulosedit");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarModulo(Modulos modulo)
        {
            try
            {
                strSql = "INSERT INTO MODULOS(ID_MODULO, NOMBRE, PATH_URL, ID_MODULO_PADRE, ESTADO, FECHA_CREACION, USUARIO_CREACION) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_MODULO), 0) + 1 FROM MODULOS), @nom, @url, @mod_padre, @estado, GETDATE(), @usuario)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nom", modulo.Nombre);
                comando.Parameters.AddWithValue("@url", modulo.Url_path);
                comando.Parameters.AddWithValue("@mod_padre", modulo.ID_modulo_padre1);
                comando.Parameters.AddWithValue("@estado", modulo.Estado);
                comando.Parameters.AddWithValue("@usuario", modulo.Usuario_creacion);
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

        public bool InsertarModuloPropio(Modulos modulo)
        {
            try
            {
                strSql = "INSERT INTO MODULOS(ID_MODULO, NOMBRE, PATH_URL, ID_MODULO_PADRE, ESTADO, FECHA_CREACION, USUARIO_CREACION) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_MODULO), 0) + 1 FROM MODULOS), @nom, @url, (SELECT ISNULL(MAX(ID_MODULO), 0) + 1 FROM MODULOS), @estado, GETDATE(), @usuario)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nom", modulo.Nombre);
                comando.Parameters.AddWithValue("@url", modulo.Url_path);
                comando.Parameters.AddWithValue("@estado", modulo.Estado);
                comando.Parameters.AddWithValue("@usuario", modulo.Usuario_creacion);
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

        public bool ModificarModulo(Modulos modulo)
        {
            try
            {
                strSql = "UPDATE MODULOS SET NOMBRE = @nom, PATH_URL = @url, ID_MODULO_PADRE = @mod_padre, ESTADO = @estado, " +
                    "FECHA_MODIFICACION = GETDATE(), USUARIO_MODIFICACION = @usuario WHERE ID_MODULO = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nom", modulo.Nombre);
                comando.Parameters.AddWithValue("@url", modulo.Url_path);
                comando.Parameters.AddWithValue("@mod_padre", modulo.ID_modulo_padre1);
                comando.Parameters.AddWithValue("@estado", modulo.Estado);
                comando.Parameters.AddWithValue("@usuario", modulo.Usuario_modificacion);
                comando.Parameters.AddWithValue("@id", modulo.ID_modulo1);
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

        public bool EliminarModulo(Modulos modulo)
        {
            try
            {
                strSql = "DELETE FROM MODULOS WHERE ID_MODULO = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", modulo.ID_modulo1);
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