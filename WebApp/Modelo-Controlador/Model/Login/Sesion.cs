using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Login
{
    public class Sesion
    {
        private string usuario;
        private string contra;
        private string correo;
        private int id_rol; 

        public string Usuario { get => usuario; set => usuario = value; }
        public string Contra { get => contra; set => contra = value; }
        public string Correo { get => correo; set => correo = value; }
        public int Id_rol { get => id_rol; set => id_rol = value; }
    }
}