using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Login
{
    public class Roles
    {
        private int ID_rol;
        private string nombre;
        private int estado;
        private string fecha_creacion;
        private string usuario_creacion;
        private string fecha_modificacion;
        private string usuario_modificacion;

        public int ID_rol1 { get => ID_rol; set => ID_rol = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Estado { get => estado; set => estado = value; }
        public string Fecha_creacion { get => fecha_creacion; set => fecha_creacion = value; }
        public string Usuario_creacion { get => usuario_creacion; set => usuario_creacion = value; }
        public string Fecha_modificacion { get => fecha_modificacion; set => fecha_modificacion = value; }
        public string Usuario_modificacion { get => usuario_modificacion; set => usuario_modificacion = value; }
    }
}