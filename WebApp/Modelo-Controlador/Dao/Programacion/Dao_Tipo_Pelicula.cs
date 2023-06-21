using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Tipo_Pelicula : DataLayer
    {
        Conexion conexionDB = new Conexion();
        SqlConnection conectar;

        string strSql;

        public bool GetTipo_Pelicula()
        {
            try
            {
                strSql = "SELECT ID_TIPO_PELICULA, NOMBRE FROM TIPOS_PELICULAS";
                DsReturn = conexionDB.DataSQL(strSql, "tipos_peliculas");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarTipoPelicula(Tipos_peliculas p)
        {
            try
            {
                strSql = "INSERT INTO TIPOS_PELICULAS (ID_TIPO_PELICULA,NOMBRE) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_TIPO_PELICULA), 0) + 1 FROM TIPOS_PELICULAS), @nombre)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", p.Nombre);
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

        public bool ModificarTipoPelicula(Tipos_peliculas p)
        {
            try
            {
                strSql = "UPDATE TIPOS_PELICULAS SET NOMBRE = @nombre WHERE ID_TIPO_PELICULA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", p.Nombre);
                comando.Parameters.AddWithValue("@id", p.ID_tipo_pelicula1);
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

        public bool EliminarTipoPelicula(Tipos_peliculas p)
        {
            try
            {
                strSql = "DELETE FROM TIPOS_PELICULAS WHERE ID_TIPO_PELICULA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", p.ID_tipo_pelicula1);
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