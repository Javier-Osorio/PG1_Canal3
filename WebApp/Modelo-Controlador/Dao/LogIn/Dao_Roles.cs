using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp.Modelo_Controlador.Dao.LogIn
{
    public class Dao_Roles : DataLayer
    {
        Conexion conexionDB = new Conexion();
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetRoles()
        {
            try
            {
                strSql = "SELECT ID_ROL, NOMBRE, CASE WHEN ESTADO = 1 THEN 'ACTIVO' WHEN ESTADO = 0 THEN 'INACTIVO' END ESTADO FROM ROLES";
                DsReturn = conexionDB.DataSQL(strSql, "roles");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarRoles(Roles roles)
        {
            try
            {
                strSql = "INSERT INTO ROLES(ID_ROL, NOMBRE, ESTADO, FECHA_CREACION, USUARIO_CREACION) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_ROL), 0) + 1 FROM ROLES), @nombre, @estado, GETDATE(), @usuario)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", roles.Nombre);
                comando.Parameters.AddWithValue("@estado", roles.Estado);
                comando.Parameters.AddWithValue("@usuario", roles.Usuario_creacion);
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

        public bool ModificarRoles(Roles roles)
        {
            try
            {
                strSql = "UPDATE ROLES SET NOMBRE = @nombre, ESTADO = @estado, FECHA_MODIFICACION = GETDATE() , USUARIO_MODIFICACION = @usuario WHERE ID_ROL = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", roles.Nombre);
                comando.Parameters.AddWithValue("@estado", roles.Estado);
                comando.Parameters.AddWithValue("@usuario", roles.Usuario_modificacion);
                comando.Parameters.AddWithValue("@id", roles.ID_rol1);
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

        public bool EliminarRoles(Roles roles)
        {
            try
            {
                strSql = "DELETE FROM ROLES WHERE ID_ROL = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", roles.ID_rol1);
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