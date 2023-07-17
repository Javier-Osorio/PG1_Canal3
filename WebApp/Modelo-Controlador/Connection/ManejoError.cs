using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Connection
{
    public class ManejoError
    {
        public void LogError(string strMensaje, string stack)
        {
            try
            {

                string Path = string.Format("~/Excepciones/Log/{0}.txt", DateTime.Now.ToString("dd-MM-yyyy"));

                if (!File.Exists(HttpContext.Current.Server.MapPath(Path)))
                {
                    File.Create(HttpContext.Current.Server.MapPath(Path)).Close();
                }

                using (StreamWriter w = File.AppendText(HttpContext.Current.Server.MapPath(Path)))
                {
                    w.WriteLine("\r\nLog del Error: ");
                    w.WriteLine("{0}", DateTime.Now.ToString());
                    string err = string.Format("Error en: {0} | Mensaje de Error: {1} | Stack: {2}", HttpContext.Current.Request.Url.ToString(), strMensaje, stack);
                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}