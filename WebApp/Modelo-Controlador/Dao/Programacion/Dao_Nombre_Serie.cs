using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Nombre_Serie : DataLayer
    {
        Conexion conexionDB = new Conexion();
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetNombre_Serie()
        {
            try
            {
                strSql = "SELECT TOP 100 ID_NOMBRE_SERIE, NOMBRE FROM NOMBRES_SERIES ORDER BY ID_NOMBRE_SERIE DESC";
                DsReturn = conexionDB.DataSQL(strSql, "nombres_series");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarNombreSerie(Nombre_serie s)
        {
            try
            {
                strSql = "INSERT INTO NOMBRES_SERIES (ID_NOMBRE_SERIE,NOMBRE) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_NOMBRE_SERIE), 0) + 1 FROM NOMBRES_SERIES), @nombre)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", s.Nombre);
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

        public bool ModificarNombreSerie(Nombre_serie s)
        {
            try
            {
                strSql = "UPDATE NOMBRES_SERIES SET NOMBRE = @nombre WHERE ID_NOMBRE_SERIE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", s.Nombre);
                comando.Parameters.AddWithValue("@id", s.ID_Nombre_Serie1);
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

        public bool EliminarNombreSerie(Nombre_serie s)
        {
            try
            {
                strSql = "DELETE FROM NOMBRES_SERIES WHERE ID_NOMBRE_SERIE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", s.ID_Nombre_Serie1);
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

        public bool GetBuscarNombre_Serie(Nombre_serie serie)
        {
            try
            {
                strSql = "SELECT ID_NOMBRE_SERIE, NOMBRE FROM NOMBRES_SERIES WHERE NOMBRE LIKE '%" +serie.Nombre+ "%' ORDER BY ID_NOMBRE_SERIE DESC";
                DsReturn = conexionDB.DataSQL(strSql, "nombres_series");
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