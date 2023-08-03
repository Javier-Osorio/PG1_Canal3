using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Ubicacion : DataLayer
    {
        Conexion conexionDB = new Conexion();
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;


        public bool GetUbicaciones()
        {
            try
            {
                strSql = "SELECT ID_UBICACION, NOMBRE, PATH_UBICACION FROM UBICACIONES_CINTAS";
                DsReturn = conexionDB.DataSQL(strSql, "ubicacion");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarUbicaciones(Ubicaciones u)
        {
            try
            {
                strSql = "INSERT INTO UBICACIONES_CINTAS (ID_UBICACION,NOMBRE,PATH_UBICACION) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_UBICACION), 0) + 1 FROM UBICACIONES_CINTAS), @Unombre, @Upath)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@Unombre", u.Nombre);
                comando.Parameters.AddWithValue("@Upath", u.Path_carpeta);
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

        public bool ModificarUbicaciones(Ubicaciones u)
        {
            try
            {
                strSql = "UPDATE UBICACIONES_CINTAS SET NOMBRE = @nombre,PATH_UBICACION = @path WHERE ID_UBICACION = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", u.Nombre);
                comando.Parameters.AddWithValue("@path", u.Path_carpeta);
                comando.Parameters.AddWithValue("@id",u.ID_ubicacion1);
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

        public bool EliminarUbicaciones(Ubicaciones u)
        {
            try
            {
                strSql = "DELETE FROM UBICACIONES_CINTAS WHERE ID_UBICACION = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", u.ID_ubicacion1);
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

        public bool UbicacionBuscar(string parametros)
        {
            try
            {
                strSql = "SELECT ID_UBICACION, NOMBRE, PATH_UBICACION FROM UBICACIONES_CINTAS " + parametros;
                DsReturn = conexionDB.DataSQL(strSql, "ubicacion");
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