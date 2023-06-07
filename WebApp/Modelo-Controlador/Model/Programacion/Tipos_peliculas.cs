using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Programacion
{
    public class Tipos_peliculas
    {
        private int ID_tipo_pelicula;
        private string nombre;

        public int ID_tipo_pelicula1 { get => ID_tipo_pelicula; set => ID_tipo_pelicula = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}