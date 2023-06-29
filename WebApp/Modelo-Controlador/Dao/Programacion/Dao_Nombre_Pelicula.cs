using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Nombre_Pelicula : DataLayer
    {
        Conexion conexionDB = new Conexion();
        SqlConnection conectar;

        string strSql;

        public bool GetNombre_Pelicula()
        {
            try
            {
                strSql = "SELECT ID_NOMBRE_PELICULA, NOMBRE FROM NOMBRES_PELICULAS";
                DsReturn = conexionDB.DataSQL(strSql, "nombres_peliculas");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarNombrePelicula(Nombre_pelicula p)
        {
            try
            {
                strSql = "INSERT INTO NOMBRES_PELICULAS (ID_NOMBRE_PELICULA,NOMBRE) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_NOMBRE_PELICULA), 0) + 1 FROM NOMBRES_PELICULAS), @nombre)";
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

        public bool ModificarNombrePelicula(Nombre_pelicula p)
        {
            try
            {
                strSql = "UPDATE NOMBRES_PELICULAS SET NOMBRE = @nombre WHERE ID_NOMBRE_PELICULA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", p.Nombre);
                comando.Parameters.AddWithValue("@id", p.ID_Nombre_pelicula1);
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

        public bool EliminarNombrePelicula(Nombre_pelicula p)
        {
            try
            {
                strSql = "DELETE FROM NOMBRES_PELICULAS WHERE ID_NOMBRE_PELICULA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", p.ID_Nombre_pelicula1);
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