using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp.Modelo_Controlador.Dao.LogIn
{
    public class Dao_Usuarios : DataLayer
    {
        Conexion conexionDB = new Conexion();
        SqlConnection conectar;

        string strSql;

        public bool GetUsuarios()
        {
            try
            {
                strSql = "SELECT U.ID_USUARIO, U.NOMBRE, U.APELLIDO, U.USUARIO, U.CORREO, '********' as CONTRA, CASE WHEN U.ESTADO = 1 THEN 'ACTIVO' " +
                    "WHEN U.ESTADO = 0 THEN 'INACTIVO' END ESTADO, R.NOMBRE FROM USUARIOS U INNER JOIN ROLES R ON R.ID_ROL = U.ID_ROL";
                DsReturn = conexionDB.DataSQL(strSql, "usuario");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarUsuarios(Usuarios usuarios)
        {
            try
            {
                strSql = "INSERT INTO USUARIOS (ID_USUARIO,NOMBRE,APELLIDO,USUARIO,CORREO,CONTRA,ESTADO,ID_ROL,FECHA_CREACION,USUARIO_CREACION,FECHA_MODIFICACION,USUARIO_MODIFICACION) " +
                    "VALUES((SELECT ISNULL(MAX(ID_USUARIO), 0) + 1 FROM USUARIOS), @nombre, @apellido, @usuario, @correo, ENCRYPTBYPASSPHRASE('password', @contra), @estado, @rol, GETDATE(), @usuario_admin, NULL, NULL)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", usuarios.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuarios.Apellido);
                comando.Parameters.AddWithValue("@usuario", usuarios.Usuario);
                comando.Parameters.AddWithValue("@correo", usuarios.Correo);
                comando.Parameters.AddWithValue("@contra", usuarios.Contra);
                comando.Parameters.AddWithValue("@estado", usuarios.Estado);
                comando.Parameters.AddWithValue("@rol", usuarios.ID_rol1);
                comando.Parameters.AddWithValue("@usuario_admin", usuarios.Usuario_creacion);
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

        public bool ModificarUsuarios(Usuarios usuarios)
        {
            try
            {
                strSql = "UPDATE USUARIOS SET NOMBRE = @nombre, APELLIDO = @apellido, USUARIO = @usuario, CORREO = @correo, CONTRA = ENCRYPTBYPASSPHRASE('password', @contra), " +
                    "ESTADO = @estado, ID_ROL = @rol, FECHA_MODIFICACION = GETDATE(), USUARIO_MODIFICACION = @usuario_admin WHERE ID_USUARIO = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", usuarios.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuarios.Apellido);
                comando.Parameters.AddWithValue("@usuario", usuarios.Usuario);
                comando.Parameters.AddWithValue("@correo", usuarios.Correo);
                comando.Parameters.AddWithValue("@contra", usuarios.Contra);
                comando.Parameters.AddWithValue("@estado", usuarios.Estado);
                comando.Parameters.AddWithValue("@rol", usuarios.ID_rol1);
                comando.Parameters.AddWithValue("@usuario_admin", usuarios.Usuario_modificacion);
                comando.Parameters.AddWithValue("@id", usuarios.ID_usuario1);
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

        public bool EliminarUsuarios(Usuarios usuarios)
        {
            try
            {
                strSql = "DELETE FROM USUARIOS WHERE ID_USUARIO = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", usuarios.ID_usuario1);
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