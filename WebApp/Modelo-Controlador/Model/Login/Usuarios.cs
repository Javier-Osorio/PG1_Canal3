using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Login
{
    public class Usuarios 
    {
        private int ID_usuario;
        private string nombre;
        private string apellido;
        private string usuario;
        private string correo;
        private string contra;
        private int estado;
        private int ID_rol;
        private string fecha_creacion;
        private string usuario_creacion;
        private string fecha_modificacion;
        private string usuario_modificacion;

        public int ID_usuario1 { get => ID_usuario; set => ID_usuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Contra { get => contra; set => contra = value; }
        public int Estado { get => estado; set => estado = value; }
        public string Fecha_creacion { get => fecha_creacion; set => fecha_creacion = value; }
        public string Usuario_creacion { get => usuario_creacion; set => usuario_creacion = value; }
        public string Fecha_modificacion { get => fecha_modificacion; set => fecha_modificacion = value; }
        public string Usuario_modificacion { get => usuario_modificacion; set => usuario_modificacion = value; }
        public int ID_rol1 { get => ID_rol; set => ID_rol = value; }
    }
}