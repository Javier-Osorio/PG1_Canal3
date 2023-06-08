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
        Ubicaciones ubi = new Ubicaciones(); 
        SqlDataReader reader;

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
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }
    }
}