using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modelo_Controlador.Model.Programacion
{
    public class Backup_peliculas
    {
        private int ID_backup_pelicula;
        private int ID_nombre;
        private string nombre;
        private string fecha_backup;
        private string observaciones;
        private int ID_tipo_pelicula;
        private string tipo_pelicula;
        private int ID_casa_productora;
        private string casa_productora;
        private int ID_ubicacion;
        private string ubicacion;
        private int estado;

        public int ID_backup_pelicula1 { get => ID_backup_pelicula; set => ID_backup_pelicula = value; }
        public int ID_nombre1 { get => ID_nombre; set => ID_nombre = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Fecha_backup { get => fecha_backup; set => fecha_backup = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public int ID_tipo_pelicula1 { get => ID_tipo_pelicula; set => ID_tipo_pelicula = value; }
        public string Tipo_pelicula { get => tipo_pelicula; set => tipo_pelicula = value; }
        public int ID_casa_productora1 { get => ID_casa_productora; set => ID_casa_productora = value; }
        public string Casa_productora { get => casa_productora; set => casa_productora = value; }
        public int ID_ubicacion1 { get => ID_ubicacion; set => ID_ubicacion = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        public int Estado { get => estado; set => estado = value; }
    }
}