using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Programacion
{
    public class Nombre_pelicula
    {
        private int ID_Nombre_pelicula;
        private string nombre;

        public int ID_Nombre_pelicula1 { get => ID_Nombre_pelicula; set => ID_Nombre_pelicula = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}