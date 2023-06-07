using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Programacion
{
    public class Ubicaciones
    {
        private int ID_ubicacion;
        private string nombre;
        private string path_carpeta;

        public int ID_ubicacion1 { get => ID_ubicacion; set => ID_ubicacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Path_carpeta { get => path_carpeta; set => path_carpeta = value; }
    }
}