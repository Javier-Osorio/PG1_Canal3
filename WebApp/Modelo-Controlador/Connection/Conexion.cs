using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Connection
{
    public class Conexion
    {
        SqlDataAdapter dt = new SqlDataAdapter();
        DataSet ds;

        public SqlConnection OpenSQL()
        {
            string servi = "DESKTOP-G20CI13";
            string bd = "BD_PG1";
            string usuario = "sa";
            string password = "1234";

            string cadenaConexion = "Data Source=" + servi + ";Initial Catalog=" + bd + "; User ID=" + usuario + "; Password=" + password + "";

            SqlConnection con = new SqlConnection(cadenaConexion);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

            }

            return con;
        }
        public DataSet DataSQL(string strSql, string tabla)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = OpenSQL();
                dt = new SqlDataAdapter(strSql, conexion);
                ds = new DataSet(tabla);
                dt.Fill(ds, tabla);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                CloseSQL(conexion);
            }

            return ds;
        }

        public SqlConnection CloseSQL(SqlConnection con)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return con;
        }
    }
}