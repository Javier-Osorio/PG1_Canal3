using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Programacion;

namespace WebApp.Modelo_Controlador.Dao.Programacion
{
    public class Dao_Casa_Productora : DataLayer
    {
        Conexion conexionDB = new Conexion();
        Casa_productoras casa = new Casa_productoras();
        SqlConnection conectar;

        string strSql;


        public bool GetCasa_Productora()
        {
            try
            {
                strSql = "SELECT ID_CASAS_PRODUCTORAS, NOMBRE FROM CASAS_PRODUCTORAS";
                DsReturn = conexionDB.DataSQL(strSql, "casa_productora");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool InsertarCasaProductora(Casa_productoras c)
        {
            try
            {
                strSql = "INSERT INTO CASAS_PRODUCTORAS (ID_CASA_PRODUCTORA,NOMBRE) " +
                    "VALUES ((SELECT ISNULL(MAX(ID_CASA_PRODUCTORA), 0) + 1 FROM CASAS_PRODUCTORAS), @nombre)";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", c.Nombre);
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

        public bool ModificarCasaProductora(Casa_productoras c)
        {
            try
            {
                strSql = "UPDATE CASAS_PRODUCTORAS SET NOMBRE = @nombre WHERE ID_CASA_PRODUCTORA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@nombre", c.Nombre);
                comando.Parameters.AddWithValue("@id", c.ID_casa_productora1);
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

        public bool EliminarCasaProductora(Casa_productoras ca)
        {
            try
            {
                strSql = "DELETE FROM CASAS_PRODUCTORAS WHERE ID_CASA_PRODUCTORA = @id";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@id", ca.ID_casa_productora1);
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