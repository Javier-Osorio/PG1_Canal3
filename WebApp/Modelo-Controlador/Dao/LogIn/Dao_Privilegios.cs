using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp.Modelo_Controlador.Dao.LogIn
{
    public class Dao_Privilegios : DataLayer
    {
        Conexion conexionDB = new Conexion();
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetPrivilegios()
        {
            try
            {
                strSql = "SELECT P.ID_PRIVILEGIO, M.NOMBRE, R.NOMBRE, CASE WHEN P.ESTADO = 1 THEN 'ACTIVO' WHEN P.ESTADO = 0 THEN 'INACTIVO' END ESTADO " +
                    "FROM PRIVILEGIOS P INNER JOIN MODULOS M ON P.ID_MODULO = M.ID_MODULO INNER JOIN ROLES R ON P.ID_ROL = R.ID_ROL";
                DsReturn = conexionDB.DataSQL(strSql, "privilegios");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetModuloPrivilegio()
        {
            try
            {
                strSql = "SELECT ID_MODULO, NOMBRE FROM MODULOS WHERE ESTADO = 1";
                DsReturn = conexionDB.DataSQL(strSql, "modul_privi");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetRolPrivilegio()
        {
            try
            {
                strSql = "SELECT ID_ROL, NOMBRE FROM ROLES WHERE ESTADO = 1";
                DsReturn = conexionDB.DataSQL(strSql, "rol_privi");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetListPrivilegio(int cod)
        {
            try
            {
                strSql = "SELECT M.ID_MODULO, M.NOMBRE AS MODULO, R.ID_ROL, R.NOMBRE AS ROL, P.ESTADO AS ID_ESTADO, " +
                    "CASE WHEN P.ESTADO = 1 THEN 'ACTIVO' WHEN P.ESTADO = 0 THEN 'INACTIVO' END ESTADO " +
                    "FROM PRIVILEGIOS P INNER JOIN MODULOS M ON P.ID_MODULO = M.ID_MODULO INNER JOIN ROLES R ON P.ID_ROL = R.ID_ROL WHERE P.ID_PRIVILEGIO = " + cod;
                DsReturn = conexionDB.DataSQL(strSql, "edit_privi");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarPrivilegios(Privilegios privilegios)
        {
            try
            {
                strSql = "INSERT INTO PRIVILEGIOS(ID_PRIVILEGIO, ID_MODULO, ID_ROL, ESTADO, FECHA_CREACION, USUARIO_CREACION) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_PRIVILEGIO), 0) + 1 FROM PRIVILEGIOS), @modulo, @rol, @estado, GETDATE(), @usuario)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@modulo", privilegios.ID_modulo1);
                comando.Parameters.AddWithValue("@rol", privilegios.ID_rol1);
                comando.Parameters.AddWithValue("@estado", privilegios.Estado);
                comando.Parameters.AddWithValue("@usuario", privilegios.Usuario_creacion);
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

        public bool ModificarPrivilegios(Privilegios privilegios)
        {
            try
            {
                strSql = "UPDATE PRIVILEGIOS SET ID_MODULO = @modulo, ID_ROL = @rol, ESTADO = @estado, FECHA_MODIFICACION = GETDATE(), " +
                    "USUARIO_MODIFICACION = @usuario WHERE ID_PRIVILEGIO = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@modulo", privilegios.ID_modulo1);
                comando.Parameters.AddWithValue("@rol", privilegios.ID_rol1);
                comando.Parameters.AddWithValue("@estado", privilegios.Estado);
                comando.Parameters.AddWithValue("@usuario", privilegios.Usuario_modificacion);
                comando.Parameters.AddWithValue("@id", privilegios.ID_privilegio1);
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

        public bool EliminarPrivilegios(Privilegios privilegios)
        {
            try
            {
                strSql = "DELETE FROM PRIVILEGIOS WHERE ID_PRIVILEGIO = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", privilegios.ID_privilegio1);
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