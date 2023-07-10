using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Tipo_Serie : DataLayer
    {
        Conexion conexionDB = new Conexion();
        ManejoError error = new ManejoError();
        SqlConnection conectar;

        string strSql;

        public bool GetTipo_Serie()
        {
            try
            {
                strSql = "SELECT ID_TIPO_SERIE, NOMBRE FROM TIPOS_SERIES";
                DsReturn = conexionDB.DataSQL(strSql, "tipos_series");
            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarTipoSerie(Tipos_series s)
        {
            try
            {
                strSql = "INSERT INTO TIPOS_SERIES (ID_TIPO_SERIE,NOMBRE) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_TIPO_SERIE), 0) + 1 FROM TIPOS_SERIES), @nombre)";
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

        public bool ModificarTipoSerie(Tipos_series s)
        {
            try
            {
                strSql = "UPDATE TIPOS_SERIES SET NOMBRE = @nombre WHERE ID_TIPO_SERIE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", s.Nombre);
                comando.Parameters.AddWithValue("@id", s.ID_tipo_serie1);
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

        public bool EliminarTipoSerie(Tipos_series s)
        {
            try
            {
                strSql = "DELETE FROM TIPOS_SERIES WHERE ID_TIPO_SERIE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", s.ID_tipo_serie1);
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