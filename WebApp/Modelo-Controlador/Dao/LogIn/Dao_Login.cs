using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Modelo_Controlador.Connection;
using WebApp.Modelo_Controlador.Model.Login;

namespace WebApp.Modelo_Controlador.Dao
{
    public class Dao_Login : DataLayer
    {
        Conexion conexionDB = new Conexion();
        SqlConnection conectar;
        SqlDataReader reader;

        string strSql;

        public bool ValidarUsuario(Sesion login)
        {
            try
            {
                strSql = "SELECT COUNT(1) FROM USUARIOS WHERE CORREO = @correo AND CONVERT(varchar(50), DECRYPTBYPASSPHRASE('password', CONTRA)) = @contra";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@correo", login.Correo);
                comando.Parameters.AddWithValue("@contra", login.Contra);
                int ver = (int)comando.ExecuteScalar();
                conectar.Close();
                return ver == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                conectar.Close();
                return false;
            }
        }

        public string ObtenerUsuario(Sesion login)
        {
            try
            {
                string usu = "";
                strSql = "SELECT USUARIO FROM USUARIOS WHERE CORREO = @correo";
                conectar = conexionDB.OpenSQL();
                SqlCommand comando = new SqlCommand(strSql, conectar);
                comando.Prepare();
                comando.Parameters.AddWithValue("@correo", login.Correo);
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    usu = reader["USUARIO"].ToString();
                }
                conectar.Close();
                return usu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                conectar.Close();
                return "error";
            }
        }
    }
}