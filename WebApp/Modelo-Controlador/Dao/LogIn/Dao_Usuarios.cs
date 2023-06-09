﻿using System;
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
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetUsuarios()
        {
            try
            {
                strSql = "SELECT U.ID_USUARIO, U.NOMBRE, U.APELLIDO, U.USUARIO, U.CORREO, '********' as CONTRA, CASE WHEN U.ESTADO = 1 THEN 'ACTIVO' " +
                    "WHEN U.ESTADO = 0 THEN 'INACTIVO' END ESTADO, R.NOMBRE AS ROL FROM USUARIOS U INNER JOIN ROLES R ON R.ID_ROL = U.ID_ROL";
                DsReturn = conexionDB.DataSQL(strSql, "usuarios");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetListUsuarios(int cod)
        {
            try
            {
                strSql = "SELECT U.ESTADO AS ID_ESTADO, CASE WHEN U.ESTADO = 1 THEN 'ACTIVO' " +
                    "WHEN U.ESTADO = 0 THEN 'INACTIVO' END ESTADO, R.ID_ROL, R.NOMBRE AS ROL FROM USUARIOS U " +
                    "INNER JOIN ROLES R ON R.ID_ROL = U.ID_ROL WHERE U.ID_USUARIO = " + cod;
                DsReturn = conexionDB.DataSQL(strSql, "list_usuario_edit");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetRoles()
        {
            try
            {
                strSql = "SELECT ID_ROL, NOMBRE FROM ROLES WHERE ESTADO = 1";
                DsReturn = conexionDB.DataSQL(strSql, "usu_rol");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public string ObtenerUsuario(Usuarios usu)
        {
            string resul = "";
            try
            {
                char firstNom = usu.Nombre.FirstOrDefault();
                resul = firstNom + usu.Apellido;
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }
            return resul;
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
                error.LogError(ex.ToString(), ex.StackTrace);
                conectar.Close();
                return false;
            }
        }

        public bool ModificarUsuarios(Usuarios usuarios)
        {
            try
            {
                strSql = "UPDATE USUARIOS SET NOMBRE = @nombre, APELLIDO = @apellido, USUARIO = @usuario, CORREO = @correo, " +
                    "ESTADO = @estado, ID_ROL = @rol, FECHA_MODIFICACION = GETDATE(), USUARIO_MODIFICACION = @usuario_admin WHERE ID_USUARIO = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", usuarios.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuarios.Apellido);
                comando.Parameters.AddWithValue("@usuario", usuarios.Usuario);
                comando.Parameters.AddWithValue("@correo", usuarios.Correo);
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
                error.LogError(ex.ToString(), ex.StackTrace);
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
                error.LogError(ex.ToString(), ex.StackTrace);
                conectar.Close();
                return false;
            }
        }
    }
}