using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Nombre_Material : DataLayer
    {
        Conexion conexionDB = new Conexion();
        SqlConnection conectar;

        string strSql;


        public bool GetNombre_Material()
        {
            try
            {
                strSql = "SELECT ID_NOMBRE, NOMBRE FROM NOMBRES_MATERIALES";
                DsReturn = conexionDB.DataSQL(strSql, "nombre_material");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarNombreMaterial(Nombre_material m)
        {
            try
            {
                strSql = "INSERT INTO NOMBRES_MATERIALES (ID_NOMBRE,NOMBRE) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_NOMBRE), 0) + 1 FROM NOMBRES_MATERIALES), @nombre)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
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

        public bool ModificarCasaProductora(Nombre_material m)
        {
            try
            {
                strSql = "UPDATE NOMBRES_MATERIALES SET NOMBRE = @nombre WHERE ID_NOMBRE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", m.Nombre);
                comando.Parameters.AddWithValue("@id", m.ID_Nombre1);
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

        public bool EliminarCasaProductora(Nombre_material m)
        {
            try
            {
                strSql = "DELETE FROM NOMBRES_MATERIALES WHERE ID_NOMBRE = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", m.ID_Nombre1);
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