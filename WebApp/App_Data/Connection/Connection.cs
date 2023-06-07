using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.WebForms
{
    public class Connection
    {
        public SqlConnection OpenSQL()
        {
            string servi = "";
            string bd = "";
            string usuario = "";
            string password = "";

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