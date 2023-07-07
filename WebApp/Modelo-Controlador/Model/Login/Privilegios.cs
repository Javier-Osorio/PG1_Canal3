using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Login
{
    public class Privilegios
    {
        private int ID_privilegio;
        private int ID_modulo;
        private int ID_rol;
        private int estado;
        private string fecha_creacion;
        private string usuario_creacion;
        private string fecha_modificacion;
        private string usuario_modificacion;

        public int ID_privilegio1 { get => ID_privilegio; set => ID_privilegio = value; }
        public int ID_modulo1 { get => ID_modulo; set => ID_modulo = value; }
        public int ID_rol1 { get => ID_rol; set => ID_rol = value; }
        public int Estado { get => estado; set => estado = value; }
        public string Fecha_creacion { get => fecha_creacion; set => fecha_creacion = value; }
        public string Usuario_creacion { get => usuario_creacion; set => usuario_creacion = value; }
        public string Fecha_modificacion { get => fecha_modificacion; set => fecha_modificacion = value; }
        public string Usuario_modificacion { get => usuario_modificacion; set => usuario_modificacion = value; }
    }
}