using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Login
{
    public class Modulos
    {
        private int ID_modulo;
        private string nombre;
        private string url_path;
        private int nivel;
        private int orden;
        private int ID_modulo_padre;
        private int estado;
        private string fecha_creacion;
        private string usuario_creacion;
        private string fecha_modificacion;
        private string usuario_modificacion;

        public int ID_modulo1 { get => ID_modulo; set => ID_modulo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Url_path { get => url_path; set => url_path = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Orden { get => orden; set => orden = value; }
        public int ID_modulo_padre1 { get => ID_modulo_padre; set => ID_modulo_padre = value; }
        public int Estado { get => estado; set => estado = value; }
        public string Fecha_creacion { get => fecha_creacion; set => fecha_creacion = value; }
        public string Usuario_creacion { get => usuario_creacion; set => usuario_creacion = value; }
        public string Fecha_modificacion { get => fecha_modificacion; set => fecha_modificacion = value; }
        public string Usuario_modificacion { get => usuario_modificacion; set => usuario_modificacion = value; }
    }
}